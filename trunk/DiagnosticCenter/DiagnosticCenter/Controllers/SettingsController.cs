using System.Linq;
using System.Web.Mvc;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Controllers
{ 
    public class SettingsController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit()
        {
            DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();
            
            Settings parameter = db.Settings.FirstOrDefault();

            return View(parameter);
        }
        
        [HttpPost]
        public ActionResult Edit(Settings newParameter)
        {
            DiagnosticsDBModelContainer db = new DiagnosticsDBModelContainer();
            
            if (ModelState.IsValid)
            {
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