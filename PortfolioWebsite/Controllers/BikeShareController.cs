using System.Web.Mvc;

namespace MelbourneData.Controllers
{
    public class BikeShareController : Controller
    {
        // GET: BikeShare
        public ActionResult BikeShare()
        {
            return View();
        }

        public ActionResult GetParkingInfo(int BayId)
        {
            return null;
        }
    }
}