using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Controllers
{ 
    public class SettingsController : Controller
    {
        public ActionResult Edit()
        {
            DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();
            
            Settings parameter = db.Settings.FirstOrDefault();

            BindingList<SelectListItem> startHoursList = new BindingList<SelectListItem>();
            for (int i = 7; i < 14; i++)
            {
                SelectListItem selItem = new SelectListItem{Value = i.ToString(),
                                                            Text = i.ToString() + "-00"
                                                            };
                if (parameter.WorkDayStartHour == i)
                    selItem.Selected = true;
                else
                    selItem.Selected = false;
                startHoursList.Add(selItem);
            }
            ViewBag.startHoursList = startHoursList;

            BindingList<SelectListItem> endHoursList = new BindingList<SelectListItem>();
            for (int i = 14; i < 20; i++)
            {
                SelectListItem selItem = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString() + "-00"
                };
                if (parameter.WorkDayEndHour == i)
                    selItem.Selected = true;
                else
                    selItem.Selected = false;
                endHoursList.Add(selItem);
            }
            ViewBag.endtHoursList = endHoursList;
            return View(parameter);
        }
        
        [HttpPost]
        public ActionResult Edit(Settings newParameter)
        {
            DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();
            
            if (ModelState.IsValid)
            {
                //db.Parameters.Attach(parameter);
                //db.ObjectStateManager.ChangeObjectState(parameter, EntityState.Modified);
                var oldParameterList = db.Settings.Where(p => p.ID_Settings == newParameter.ID_Settings);
                if (oldParameterList.Count() == 0)
                    db.AddToSettings(newParameter);
                else
                {
                    Settings oldParameter = oldParameterList.First();
                    db.ApplyCurrentValues(oldParameter.EntityKey.EntitySetName, newParameter);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Edit");
        }
    }
}