using System.Web.Mvc;
using DataAccess.MelbourneDataPortal.MelbourneBikeShare.Model;
using DataAccess.MelbourneDataPortal.MelbourneBikeShare;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace MelbourneData.Controllers
{
    public class BikeShareController : Controller
    {
        private static List<BikeShareData> _model = new List<BikeShareData>();

        public ActionResult BikeShare()
        {
            _model = BikeShareDataAccess.GetDataSet(ConfigurationManager.AppSettings["BikeShareDataset"],
                ConfigurationManager.AppSettings["CityOfMelbourneKey"]).Values.ToList();

            return View(_model);
        }

        public ActionResult GetParkingInfo()
        {
            int BayId = 10;
            BikeShareData _data = _model.Find(item => item.BayId == BayId);
            return PartialView("BikeShareDetailsPartial", _data);
        }
    }
}