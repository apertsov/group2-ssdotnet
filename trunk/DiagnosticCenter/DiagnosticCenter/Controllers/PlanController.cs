using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DiagnosticCenter.Models;


namespace DiagnosticCenter.Controllers
{
    public class PlanController : Controller
    {
        //Дата з DataPicker
        public ActionResult ChangeDate(string altDatePicker)
        {
            return RedirectToAction("Index", new { PageNo = 1, dateValue = altDatePicker });
        }
        
        public ActionResult Index(int? pageNo, string dateValue)
        {
//-------ДОРОБИТИ------------            
            const int cabsOnPage = 4; //TODO: Read form Paramters Table
            const int dayWorkingHours = 10; //TODO: Read form Paramters Table
//---------------------------            
            //Контекст для запитів
            var context = new DiagnosticsDBModelContainer();
            
            //Визначаємо дату
            DateTime date = new DateTime();
            if (dateValue == null)
            {
                date = DateTime.Today.Date;
            }
            else
            {
                date = Convert.ToDateTime(dateValue).Date;
            }

            ViewBag.Date = date.ToShortDateString();
            
            //Визначаємо відділ, для якого буде виведено розклад та право на зміну розкладу (за авторизованим користувачам)
            Guid idUser = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            var currEmployee = context.Employees.Where(e => e.ID_User == idUser);
            if (currEmployee.Count() == 0)
            {
                return RedirectToAction("Index", "ErrorPage", new
                                                                {
                                                                    errTitle = ViewRes.PlanStrings.Error1Text,
                                                                    errDescription = ViewRes.PlanStrings.Error1Recomendation,
                                                                    errGoBackAction = "Index",
                                                                    errGoBackController = "Plan"
                                                                });   
            }
            int depId = currEmployee.First().ID_Dept;

            //Встановлюємо атрибут enable/disable для випадаючих списків 
            if ((User.IsInRole("HeadNurse")) || (User.IsInRole("Administrator")))
                ViewBag.modifyFlag = true;
            else
                ViewBag.modifyFlag = false;

            //Передаємо назву відділення
            ViewBag.DeptName = context.Employees.Where(e => e.ID_User == idUser).First().Department.Description;

            context = new DiagnosticsDBModelContainer();
            var qEmployeesWithCabinets = context.Employees.Include("Cabinet")
                                                          .Where(e => e.ID_Dept == depId)
                                                          .OrderBy(e => e.Cabinet.Number)
                                                          .GroupBy(e => e.ID_Cabinet)
                                                          .ToList();

            
            //Kількість кабінетів
            int cabCount = qEmployeesWithCabinets.Count();
            ViewBag.CabCount = cabCount;

            //Paging Logic
            int addCount = cabsOnPage;
            int skipCount;
            int counter;
            if (pageNo != null)
            {
                skipCount = (int)((pageNo-1) * cabsOnPage);
            }
            else
            {
                pageNo = 1;
                skipCount = (int)((pageNo-1) * cabsOnPage);
            }
            ViewBag.PageNo = pageNo;

            if (cabCount % cabsOnPage == 0)
                ViewBag.PageCount = cabCount / cabsOnPage;
            else
                ViewBag.PageCount = cabCount / cabsOnPage + 1;

            ViewBag.HasNextPage = (pageNo * cabsOnPage) < cabCount;
            ViewBag.HasPreviousPage = (pageNo) > 1;

            //Мапер індексів кабінетів
            int[] cabinetMap = new int[cabCount];
            int mapIndex = 0;

            //Cписок номерів кабінетів (для шапки)
            List<int> lEmployeesWithCabinets = new List<int>();
            
            //Виводимо список лікарів по кабінетах, для реалізації випадаючих списків
            List<List<KeyValuePair<int, string>>> lEmployeesDataByCabinets;
            lEmployeesDataByCabinets = new List<List<KeyValuePair<int, string>>>();

            foreach (var item in qEmployeesWithCabinets)
            {
                //Cписок номерів кабінетів (для шапки)
                lEmployeesWithCabinets.Add(item.First().Cabinet.Number);

                cabinetMap[mapIndex] = item.First().ID_Cabinet;
                mapIndex++;

                //Виводимо список лікарів по кабінетах, для реалізації випадаючих списків
                List<KeyValuePair<int, string>> selList = new List<KeyValuePair<int, string>>();

                foreach (var i in item)
                {
                    KeyValuePair<int, string> listItem;
                    listItem = new KeyValuePair<int, string>(i.ID_Employee, (i.FirstName + " " + i.Surname));

                    selList.Add(listItem);
                }
                lEmployeesDataByCabinets.Add(selList);         
            }
            
            List<int> cabTitles = new List<int>();
            counter = 0;
            foreach (int cabinet in lEmployeesWithCabinets)
            {
                if ((counter >= skipCount) && (counter < (skipCount + addCount)))
                    cabTitles.Add(cabinet);
                counter++;
            }
            ViewBag.EmployeesWithCabinets = cabTitles;

            List<List<KeyValuePair<int, string>>> emplDataList = new List<List<KeyValuePair<int, string>>>();
            string EmployeesOnPage = string.Empty;
            counter = 0;
            foreach (List<KeyValuePair<int, string>> cabinet in lEmployeesDataByCabinets)
            {
                if ((counter >= skipCount) && (counter < (skipCount + addCount)))
                {
                    emplDataList.Add(cabinet);

                    foreach (var keyValuePair in cabinet)
                        EmployeesOnPage = EmployeesOnPage + keyValuePair.Key.ToString() + ";";
                }
                counter++;
            }  
            ViewBag.EmployeesNamesByCabinets = emplDataList;
            ViewBag.EmployeesOnPage = EmployeesOnPage;

            //Читаэмо таблиці Days та Employees для формувння розкладу

            //Ініціалізація planChart
            List<List<List<SelectListItem>>> planChart = new List<List<List<SelectListItem>>>();

            for (int i = 0; i < dayWorkingHours; i++)
            {
                List<List<SelectListItem>> listOfSelLists = new List<List<SelectListItem>>();
                for (int j = 0; j < cabCount; j++)
                {
                    List<SelectListItem> selList = new List<SelectListItem>();

                    foreach (var item in lEmployeesDataByCabinets[j])
                    {
                        selList.Add(new SelectListItem
                        {
                            Value = item.Key.ToString(),
                            Text = item.Value,
                            Selected = false
                        });
                    }
                    listOfSelLists.Add(selList);
                }
                planChart.Add(listOfSelLists);
            }

            //Заповненна даними planChart
            var qEmployeesByDept = context.Days.Include("Employee")
                                               .Where(d => d.Date == date)
                                               .GroupBy(d => d.Date);

            foreach (var group in qEmployeesByDept)
            {
                List<DateTime> emplTimeChart = new List<DateTime>();

                foreach (var day in group)
                {
                    foreach (var empl in day.Employee)
                    {
                        int cabIndex = 0;
                        while (empl.ID_Cabinet != cabinetMap[cabIndex])
                            cabIndex++;

                        int hour = day.StartTime.Hour;
                        while (hour < day.EndTime.Hour)
                        {
                            int i = 0;
                            foreach (var item in lEmployeesDataByCabinets[cabIndex])
                            {


                                if (empl.ID_Employee == item.Key)
                                    planChart[hour - 8][cabIndex][i].Selected = true;
                                i++;
                            }
                            hour++;
                        }
                        emplTimeChart.Add(day.StartTime);
                        emplTimeChart.Add(day.EndTime);
                    }
                }
            }

            List<List<List<SelectListItem>>> planChartList = new List<List<List<SelectListItem>>>();
            
            foreach (List<List<SelectListItem>> list in planChart)
            {
                counter = 0;
                List<List<SelectListItem>> lst = new List<List<SelectListItem>>();
                foreach (var selList in list)
                {
                    if ((counter >= skipCount) && (counter < (skipCount + addCount)))
                        lst.Add(selList);
                    counter++;
                }
                planChartList.Add(lst);
            }
            ViewBag.PlanChart = planChartList;

            //Обчислюємо напрвцювання працівника за місяць

            DateTime mounthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime mounthEnd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            var qEmployees = context.Employees.Include("Day")
                                               .Where(e => e.ID_Dept == depId)
                                               .OrderBy(e => e.ID_Employee);

            List<KeyValuePair<string, List<int>>> emplRate = new List<KeyValuePair<string, List<int>>>();

            foreach (Employee empl in qEmployees)
            {
                var query = empl.Day.Where(d => ((d.Date >= mounthStart) && (d.Date <= mounthEnd)));
                int r = 0;
                foreach (var day in query)
                    r += day.EndTime.Hour - day.StartTime.Hour;
                List<int> rateList = new List<int>();
                
                rateList.Add(r);
                rateList.Add(empl.Rate);
                emplRate.Add(new KeyValuePair<string, List<int>>(string.Format("{1} {0} {2}", empl.FirstName, empl.Surname, empl.Patronymic), rateList));
            }
            ViewBag.EployeesRate = emplRate.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection inputData)
        {
            var context = new DiagnosticsDBModelContainer();

            //Читаємо дані з форми
            DateTime date = new DateTime();
            int emplDropListCount = 0;
            int pageNo = 0;
            string sEmployeesOnPage = "";
            foreach (var item in inputData)
            {
                if (item.ToString().Contains("date"))
                    date = Convert.ToDateTime(Request.Form[item.ToString()]);

                if (item.ToString().Contains("EmployeesOnPage"))
                    sEmployeesOnPage = Request.Form[item.ToString()];

                if (item.ToString().Contains("PageNo"))
                    pageNo = Convert.ToInt32(Request.Form[item.ToString()]);

                if (item.ToString().Contains("ID_Employee"))
                    emplDropListCount++;
            }
            
            List<int> lEmployeesOnPage = new List<int>();

            string id = "";
            foreach (char c in sEmployeesOnPage)
            {
                if (c != ';')
                {
                    id = id + c;
                }
                else
                {
                    lEmployeesOnPage.Add(Convert.ToInt32(id));
                    id = "";
                }
            }

            //Ініціалізуємо таблицю planChart
            int pageCabCount = emplDropListCount / 10;
            List<List<string>> planChart = new List<List<string>>();
            for (int i = 0; i < pageCabCount; i++)
            {
                List<string> tmp = new List<string>();
                for (int l = 0; l < 10; l++)
                {
                    tmp.Add("0");
                }
                planChart.Add(tmp);
            }

            //Формуємо таблицю planChart
            foreach (var item in Request.Form)
            {
                if (!item.ToString().Contains("ID_Employee"))
                    continue;

                int row, col;
                int j, i = 0;
                while (((item.ToString()[i] < '0') || (item.ToString()[i] > '9')) && (i < item.ToString().Length - 1))
                {
                    i++;
                    if (i >= item.ToString().Length)
                        break;
                }

                j = i;
                while (((item.ToString()[j] >= '0') && (item.ToString()[j] <= '9')) && (j < item.ToString().Length - 1))
                {
                    j++;
                    if (j >= item.ToString().Length)
                        break;
                }

                int.TryParse(item.ToString().Substring(i, j - i), out row);

                i = j + 1;
                while (((item.ToString()[i] < '0') || (item.ToString()[i] > '9')) && (i < item.ToString().Length - 1))
                {
                    i++;
                    if (i >= item.ToString().Length)
                        break;
                }

                j = i;
                while (((item.ToString()[j] >= '0') && (item.ToString()[j] <= '9')))
                {
                    j++;
                    if (j >= item.ToString().Length)
                        break;
                }

                int.TryParse(item.ToString().Substring(i, j - i), out col);

                if (Request.Form[item.ToString()] != "")
                    planChart[col][row] = Request.Form[item.ToString()];
            }

            //Очищуємо звязки для працівників, що присутні на даній сторінці
            //Обовязково кнвертуємо в спсок (.ToList()), щоб закрився рідер і зявилась можливість ребагувати базу
            var lDays = context.Days.Where(d => ((d.Date == date))).ToList();

            foreach (Day day in lDays)
            {
                foreach (int emplId in lEmployeesOnPage)
                {
                    int emp = emplId;
                    Employee employee = context.Employees.Where(e => e.ID_Employee == emp).First();
                    if (day.Employee.Contains(employee))
                    {
                        DiagnosticsDBModelContainer container = new DiagnosticsDBModelContainer();
                        
                        Day originalDay = new Day();
                        Day editedDay = new Day();

                        editedDay = day;
                        originalDay = day;

                        editedDay.Employee.Remove(employee);
                        context.ApplyCurrentValues(originalDay.EntityKey.EntitySetName, editedDay);
                        context.SaveChanges();    
                    }
                }
            }
            //Читаємо таблицю planChart
            List<List<KeyValuePair<int, KeyValuePair<int, int>>>> hoursTable = new List<List<KeyValuePair<int, KeyValuePair<int, int>>>>();
            foreach (var col in planChart)
            {
                int currentCellVal, prevCellVal = 0;
                int startHour = -1, endHour = -1;
                int rowIndex = 0;
                List<KeyValuePair<int, KeyValuePair<int, int>>> hoursList = new List<KeyValuePair<int, KeyValuePair<int, int>>>();
                foreach (var cell in col)
                {
                    KeyValuePair<int, int> hours;
                    KeyValuePair<int, KeyValuePair<int, int>> hoursCell;

                    currentCellVal = Convert.ToInt32(cell);

                    if ((currentCellVal != prevCellVal) && !(currentCellVal <= 0) && (prevCellVal <= 0))
                        startHour = rowIndex;
                    else if (((currentCellVal != prevCellVal)) || (rowIndex >= col.Count()))
                    {
                        endHour = rowIndex;
                    }

                    prevCellVal = currentCellVal;
                    rowIndex++;
                    if ((rowIndex >= col.Count()) && (Convert.ToInt32(col[rowIndex - 1]) > 0))
                    {
                        endHour = rowIndex;
                    }

                    if ((startHour >= 0) && (endHour >= 0) && (startHour < endHour))
                    {
                        hours = new KeyValuePair<int, int>(startHour, endHour);
                        hoursCell = new KeyValuePair<int, KeyValuePair<int, int>>(Convert.ToInt32(col[startHour]), hours);

                        DateTime start = date.AddHours(startHour + 8);
                        DateTime end = date.AddHours(endHour + 8);

                        hoursList.Add(hoursCell);

                        context = new DiagnosticsDBModelContainer();
                        
                        int emplId = Convert.ToInt32(col[startHour]);
                        var qEmployee = context.Employees.Where(e => e.ID_Employee == emplId);

                        //Обовязково кнвертуємо в спсок (.ToList()), щоб закрився рідер і зявилась можливість ребагувати базу
                        var lDay = context.Days.Where(d => ((d.Date == date) && (d.StartTime == start) && (d.EndTime == end))).ToList();
                        if (lDay.Count() == 0)
                        {
                            Day newDay = new Day();

                            newDay.Date = date;
                            newDay.StartTime = start;
                            newDay.EndTime = end;

                            newDay.Employee.Add(qEmployee.First());

                            context.AddToDays(newDay);
                            context.SaveChanges();
                        }
                        else if (!lDay.First().Employee.Contains(qEmployee.First()))
                        {
                            Day originalDay = new Day();
                            Day editedDay = new Day();

                            originalDay = lDay.First();
                            editedDay = lDay.First();
                            
                            editedDay.Employee.Add(qEmployee.First());
                            context.ApplyCurrentValues(originalDay.EntityKey.EntitySetName, editedDay);
                            context.SaveChanges();
                        }
                        //else Запис вже існує - нічого робити не треба                    

                        if (currentCellVal >= 0)
                            startHour = rowIndex - 1;
                        else
                            startHour = 0;
                        endHour = 0;
                    }
                }
                hoursTable.Add(hoursList);

            }


            return RedirectToAction("Index", new { PageNo = pageNo});
        }
    }
}