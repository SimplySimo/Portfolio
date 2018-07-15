namespace DataAccess.ParkingSensors.Model
{
    public class ParkingData
    {
        public int BayId { set; get; }
        public string Status { set; get; }
        public double Lat { set; get; }
        public double Lon { set; get; }
        public string Address { set; get; }
    }
}
