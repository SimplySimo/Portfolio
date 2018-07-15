using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.ParkingSensors.Model;
using SODA;

namespace DataAccess.ParkingSensors
{
    public class ParkingDataAccess
    {
        private static Dictionary<string, ParkingData> _dataSet = new Dictionary<string, ParkingData>();

        public static Dictionary<string, ParkingData> GetDataSet(string referenceName, string privateKey)
        {
            SodaClient client = new SodaClient("https://data.melbourne.vic.gov.au", privateKey);
            var dataset = client.GetResource<Dictionary<string, object>>(referenceName);

            IEnumerable<Dictionary<string, object>> rows = dataset.GetRows(5000);

            Console.WriteLine("Got {0} results. Dumping first results:", rows.Count());

            _dataSet = ExtractData(rows);

            return _dataSet;
        }

        private static Dictionary<string, ParkingData> ExtractData(IEnumerable<Dictionary<string, object>> rows)
        {
            Dictionary<string, ParkingData> localData = new Dictionary<string, ParkingData>();

            foreach (Dictionary<string, object> keyValue in rows)
            {
                localData.Add(keyValue["bay_id"].ToString(), new ParkingData
                {
                    BayId = Convert.ToInt32(keyValue["bay_id"]),
                    Lat = Convert.ToDouble(keyValue["lat"]),
                    Lon = Convert.ToDouble(keyValue["lon"]),
                    Status = keyValue["status"].ToString()
                });
            }
            return localData;
        }
    }
}
