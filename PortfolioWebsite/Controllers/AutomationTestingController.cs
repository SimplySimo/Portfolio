using System.Web.Mvc;

namespace MelbourneData.Controllers
{
    public class AutomationTestingController : Controller
    {
        [HttpGet]
        public ActionResult AutomationTesting()
        {
            return View();
        }
    }
}