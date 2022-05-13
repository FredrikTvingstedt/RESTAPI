using System;
namespace AdvaniaAPI
{
    public class TrackingDataDHL
    {
        public class Rootobject
        {
            public Shipment[] shipments { get; set; }
        }
        public class Shipment
        {
            public string id { get; set; }
            public string service { get; set; }
            public Origin origin { get; set; }
            public Destination destination { get; set; }
            public Status status { get; set; }
            public Details details { get; set; }
            public Event[] events { get; set; }
        }

        public class Origin
        {
            public Address address { get; set; }
        }

        public class Address
        {
            public string countryCode { get; set; }
            public string addressLocality { get; set; }
        }

        public class Destination
        {
            public Address1 address { get; set; }
        }

        public class Address1
        {
            public string countryCode { get; set; }
            public string addressLocality { get; set; }
        }

        public class Status
        {
            public DateTime timestamp { get; set; }
            public Location location { get; set; }
            public string statusCode { get; set; }
            public string status { get; set; }
            public string description { get; set; }
        }

        public class Location
        {
            public Address2 address { get; set; }
        }

        public class Address2
        {
            public string countryCode { get; set; }
            public string addressLocality { get; set; }
        }

        public class Details
        {
            public Product product { get; set; }
            public bool proofOfDeliverySignedAvailable { get; set; }
            public int totalNumberOfPieces { get; set; }
            public string[] pieceIds { get; set; }
            public Weight weight { get; set; }
            public Volume volume { get; set; }
            public int loadingMeters { get; set; }
        }

        public class Product
        {
            public string productName { get; set; }
        }

        public class Weight
        {
            public int value { get; set; }
        }

        public class Volume
        {
            public float value { get; set; }
        }

        public class Event
        {
            public DateTime timestamp { get; set; }
            public Location1 location { get; set; }
            public string statusCode { get; set; }
            public string status { get; set; }
            public string description { get; set; }
        }

        public class Location1
        {
            public Address3 address { get; set; }
        }

        public class Address3
        {
            public string countryCode { get; set; }
            public string addressLocality { get; set; }
        }

    }

}