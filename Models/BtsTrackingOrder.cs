using System;

namespace AdvaniaAPI.Models
{
    public partial class BtsTrackingOrder
    {
        public string ShipmentId { get; set; }
        public string ShipmentCarrier { get; set; }
        public string ShipmentStatus { get; set; }
        public DateTime? ShipmentDeliveryDate { get; set; }
        public string ShipmentTrackingUrl { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ExternalOrderNumber { get; set; }
        public bool KeepTrack { get; set; }
    }
}
