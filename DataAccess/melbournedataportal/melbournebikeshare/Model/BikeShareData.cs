using System;

namespace DataAccess.MelbourneDataPortal.MelbourneBikeShare.Model
{
    public class BikeShareData
    {
        public int BayId { set; get; }
        public string Featurename { set; get; }
        public string Coordinates { set; get; }
        public int TerminalName { set; get; }
        public int NBBikes { set; get; }
        public int NBEmptydoc { set; get; }
        public DateTime UploadDate { set; get; }
    }
}
