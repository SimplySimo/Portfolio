using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DataAccess.GeoCoding;
using DataAccess.MelbourneDataPortal.ParkingSensors;
using DataAccess.MelbourneDataPortal.ParkingSensors.Model;

namespace MelbourneData.Controllers
{
    public class ParkingSensorsController : Controller
    {
        private static List<ParkingData> _data = new List<ParkingData>();
        private List<ParkingData> _model = new List<ParkingData>();
        private static ParkingData _currentData = new ParkingData();

        public ActionResult ParkingSensors()
        {
            return View(_model);
        }

        public ActionResult Refresh()
        {
            _data = ParkingDataAccess.GetDataSet(ConfigurationManager.AppSettings["ParkingSensorsDataset"],
                ConfigurationManager.AppSettings["CityOfMelbourneKey"]).Values.ToList();
            _model = _data;

            var occupiedBays = _model.Count(x => x.Status.Equals("Present"));
            var unOccupiedBays = _model.Count(x => x.Status.Equals("Unoccupied"));

            ViewData.Add("occupiedBays", occupiedBays);
            ViewData.Add("UnOccupiedBays", unOccupiedBays);

            return View("ParkingListPartial", _model);
        }

        public ActionResult ParkingSpotDetail(int bayid)
        {
            _currentData = _data.FirstOrDefault(x => x.BayId == bayid);
            ViewData.Add("mapimage", Mapurlgenerator(_currentData.Lat, _currentData.Lon, ConfigurationManager.AppSettings["GoogleGeoCodingKey"]));

            var geocodingresult = Geocoding.GETAsync(_currentData.Lat, _currentData.Lon).Result.results.First();

            _currentData.Address = geocodingresult.formatted_address;

            return View(_currentData);
        }

        private string Mapurlgenerator(double lat, double lon, string key)
        {
            return "https://maps.googleapis.com/maps/api/staticmap?center=melbourne,victoria&zoom=14&size=600x600&maptype=roadmap&markers=color:blue%7clabel:s%7c" + lat + "," + lon + "&key=" + key;
        }
    }
}
