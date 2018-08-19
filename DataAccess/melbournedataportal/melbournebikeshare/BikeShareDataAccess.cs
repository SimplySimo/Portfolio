using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.MelbourneDataPortal.MelbourneBikeShare.Model;
using SODA;

namespace DataAccess.MelbourneDataPortal.MelbourneBikeShare
{
    public class BikeShareDataAccess
    {
        private static Dictionary<string, BikeShareData> _dataSet = new Dictionary<string, BikeShareData>();

        public static Dictionary<string, BikeShareData> GetDataSet(string referenceName, string privateKey)
        {
            SodaClient client = new SodaClient("https://data.melbourne.vic.gov.au", privateKey);
            var dataset = client.GetResource<Dictionary<string, object>>(referenceName);

            IEnumerable<Dictionary<string, object>> rows = dataset.GetRows(5000);

            Console.WriteLine("Got {0} results. Dumping first results:", rows.Count());

            _dataSet = ExtractData(rows);

            return _dataSet;
        }

        private static Dictionary<string, BikeShareData> ExtractData(IEnumerable<Dictionary<string, object>> rows)
        {
            Dictionary<string, BikeShareData> localData = new Dictionary<string, BikeShareData>();

            foreach (Dictionary<string, object> keyValue in rows)
            {
                localData.Add(keyValue["id"].ToString(), new BikeShareData
                {
                    BayId = Convert.ToInt32(keyValue["id"]),
                    Featurename = keyValue["featurename"].ToString(),
                    NBBikes = Convert.ToInt32(keyValue["nbbikes"]),
                    NBEmptydoc = Convert.ToInt32(keyValue["nbemptydoc"]),
                    UploadDate = DateTime.Parse(keyValue["uploaddate"].ToString()),
                    Coordinates = keyValue["coordinates"].ToString(),
                    TerminalName = Convert.ToInt32(keyValue["terminalname"])
                });
            }
            return localData;
        }
    }
}
