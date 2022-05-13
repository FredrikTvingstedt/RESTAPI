namespace AdvaniaAPI
{

    public class TrackingDataUPS
    {

        public class Rootobject
        {
            public Trackresponse trackResponse { get; set; }
        }

        public class Trackresponse
        {
            public Shipment[] shipment { get; set; }
        }

        public class Shipment
        {
            public Package[] package { get; set; }
        }

        public class Package
        {
            public string trackingNumber { get; set; }
            public Activity[] activity { get; set; }
        }

        public class Activity
        {
            public Location location { get; set; }
            public Status status { get; set; }
            public string date { get; set; }
            public string time { get; set; }
        }

        public class Location
        {
            public Address address { get; set; }
        }

        public class Address
        {
            public string city { get; set; }
            public string stateProvince { get; set; }
            public string postalCode { get; set; }
            public string country { get; set; }
        }

        public class Status
        {
            public string type { get; set; }
            public string description { get; set; }
            public string code { get; set; }
        }

    }
}