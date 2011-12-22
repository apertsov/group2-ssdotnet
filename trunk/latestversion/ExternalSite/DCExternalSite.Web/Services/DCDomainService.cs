
namespace DCExternalSite.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using DCExternalSite.Web.Models;


    // Implements application logic using the DiagnosticsDBEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class DCDomainService : LinqToEntitiesDomainService<DiagnosticsDBEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Cabinets' query.
        public IQueryable<Cabinet> GetCabinets()
        {
            return this.ObjectContext.Cabinets;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Days' query.
        public IQueryable<Day> GetDays()
        {
            return this.ObjectContext.Days;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Departments' query.
        public IQueryable<Department> GetDepartments()
        {
            return this.ObjectContext.Departments;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Employees' query.
        public IQueryable<Employee> GetEmployees()
        {
            return this.ObjectContext.Employees;
        }



       
        // public IQueryable<Employee> GetEmployee(int employeeId)
        //  {
        //      return this.ObjectContext.Employees.Where(e => e.ID_Employee == employeeId);
        //  }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Examinations' query.
        public IQueryable<Examination> GetExaminations()
        {
            return this.ObjectContext.Examinations.Include("Employee").Include("ExaminationType"); //("Employee Include Department") //.OrderBy(e => e.ID_Examination); 
        }

        public IQueryable<Examination> GetExaminationsByID_Patient(int ID_Patient)
        {
            return this.ObjectContext.Examinations.Include("Employee").Include("ExaminationType").Where(e => e.ID_Patient.Equals(ID_Patient)); //("Employee Include Department") //.OrderBy(e => e.ID_Examination); 
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ExaminationTemplates' query.
        public IQueryable<ExaminationTemplate> GetExaminationTemplates()
        {
            return this.ObjectContext.ExaminationTemplates;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ExaminationTypes' query.
        public IQueryable<ExaminationType> GetExaminationTypes()
        {
            return this.ObjectContext.ExaminationTypes;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'News' query.
        public IQueryable<News> GetNews()
        {
            return this.ObjectContext.News.Where(e => e.Type.Equals(2));
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Patients' query.
        public IQueryable<Patient> GetPatients()
        {
            return this.ObjectContext.Patients;
        }


        public Patient GetPatientById(int id)
        {
            return this.ObjectContext.Patients.FirstOrDefault(p => p.ID_Patient == id);
        }


        //temp

        public Patient GetUserByEmail(string email)
        {

            return this.ObjectContext.Patients.FirstOrDefault(p => p.Email == email);

        }
        //

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Referrals' query.
        public IQueryable<Referral> GetReferrals()
        {
            return this.ObjectContext.Referrals;
        }
    }
}


