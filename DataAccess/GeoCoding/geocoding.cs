using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DataAccess.GeoCoding.Model;
using Newtonsoft.Json;

namespace DataAccess.GeoCoding
{
    public static class Geocoding
    {
        private const string key = "AIzaSyDklNWhszRKRIj_KOFO_5-xGQs-CeoRdoI";

        public static async Task<GeoCodingModel.RootObject> GETAsync(double lat, double lng)
        {
            GeoCodingModel.RootObject root;
            string requrstUrl = "https://maps.googleapis.com/maps/api/geocode/json?";
            string latlng = "latlng=" + lat + "," + lng;
            string keyString = "&key=" + key;

            HttpClient request = new HttpClient();

            try
            {
                HttpResponseMessage response = request.GetAsync(requrstUrl + latlng + keyString).Result;
                string result = await response.Content.ReadAsStringAsync();
                root = JsonConvert.DeserializeObject<GeoCodingModel.RootObject>(result);
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    string errorText = reader.ReadToEnd();
                    Console.WriteLine(errorText);
                }
                throw;
            }
            return root;
        }
    }
}
