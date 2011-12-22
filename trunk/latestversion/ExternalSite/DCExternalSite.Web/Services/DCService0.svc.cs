using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using DCExternalSite.Web.Models;

namespace DCExternalSite.Web.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DCService0
    {
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }
        private DiagnosticsDBEntities db = new DiagnosticsDBEntities();
        [OperationContract]
        public List<Patient> GetPatients()
        {
            List<Patient> patients = (from patient in db.Patients select patient).ToList();
            return patients;
        }
        [OperationContract]
        public List<Examination> GetExaminations(int ID_Patient)
        {
            List<Examination> examinations = (from examination in db.Examinations
                                              where examination.Patient.ID_Patient == ID_Patient
                                              select examination).ToList();
            return examinations;
        }
    }
}
