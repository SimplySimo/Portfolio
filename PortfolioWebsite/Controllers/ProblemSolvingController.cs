using System.Web.Mvc;

namespace MelbourneData.Controllers
{
    public class ProblemSolvingController : Controller
    {
        [HttpGet]
        public ActionResult ProblemSolving()
        {
            return View();
        }
    }
}