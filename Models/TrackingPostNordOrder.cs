using System;

namespace AdvaniaAPI
{
    public class TrackingDataPostNord
    {
        public class Rootobject
        {
            public Trackinginformationresponse TrackingInformationResponse { get; set; }
        }

        public class Trackinginformationresponse
        {
            public Shipment[] shipments { get; set; }
        }

        public class Shipment
        {
            public string shipmentId { get; set; }
            public int assessedNumberOfItems { get; set; }
            public DateTime deliveryDate { get; set; }
            public bool flexChangePossible { get; set; }
            public Service service { get; set; }
            public Consignor consignor { get; set; }
            public Consignee consignee { get; set; }
            public Statustext statusText { get; set; }
            public string status { get; set; }
            public Totalweight totalWeight { get; set; }
            public Totalvolume totalVolume { get; set; }
            public Item[] items { get; set; }
            public Additionalservice[] additionalServices { get; set; }
            public object[] splitStatuses { get; set; }
            public Shipmentreference[] shipmentReferences { get; set; }
        }

        public class Service
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        public class Consignor
        {
            public string name { get; set; }
            public Address address { get; set; }
        }

        public class Address
        {
            public string countryCode { get; set; }
            public string country { get; set; }
            public string postCode { get; set; }
        }

        public class Consignee
        {
            public Address1 address { get; set; }
        }

        public class Address1
        {
            public string city { get; set; }
            public string countryCode { get; set; }
            public string country { get; set; }
            public string postCode { get; set; }
        }

        public class Statustext
        {
            public string header { get; set; }
            public string body { get; set; }
        }

        public class Totalweight
        {
            public string value { get; set; }
            public string unit { get; set; }
        }

        public class Totalvolume
        {
            public string value { get; set; }
            public string unit { get; set; }
        }

        public class Item
        {
            public string itemId { get; set; }
            public DateTime dropOffDate { get; set; }
            public DateTime deliveryDate { get; set; }
            public string typeOfItemActual { get; set; }
            public string typeOfItemActualName { get; set; }
            public string status { get; set; }
            public Statustext1 statusText { get; set; }
            public Acceptor acceptor { get; set; }
            public Statedmeasurement statedMeasurement { get; set; }
            public Event[] events { get; set; }
            public Reference[] references { get; set; }
        }

        public class Statustext1
        {
            public string header { get; set; }
            public string body { get; set; }
        }

        public class Acceptor
        {
            public string signatureReference { get; set; }
            public string name { get; set; }
        }

        public class Statedmeasurement
        {
            public Weight weight { get; set; }
            public Length length { get; set; }
            public Height height { get; set; }
            public Width width { get; set; }
            public Volume volume { get; set; }
        }

        public class Weight
        {
            public string value { get; set; }
            public string unit { get; set; }
        }

        public class Length
        {
            public string value { get; set; }
            public string unit { get; set; }
        }

        public class Height
        {
            public string value { get; set; }
            public string unit { get; set; }
        }

        public class Width
        {
            public string value { get; set; }
            public string unit { get; set; }
        }

        public class Volume
        {
            public string value { get; set; }
            public string unit { get; set; }
        }

        public class Event
        {
            public DateTime eventTime { get; set; }
            public string eventCode { get; set; }
            public string status { get; set; }
            public string eventDescription { get; set; }
            public Location location { get; set; }
        }

        public class Location
        {
            public string locationId { get; set; }
            public string displayName { get; set; }
            public string name { get; set; }
            public string countryCode { get; set; }
            public string country { get; set; }
            public string postcode { get; set; }
            public string city { get; set; }
            public string locationType { get; set; }
        }

        public class Reference
        {
            public string value { get; set; }
            public string type { get; set; }
            public string name { get; set; }
        }

        public class Additionalservice
        {
            public string code { get; set; }
            public string groupCode { get; set; }
            public string name { get; set; }
        }

        public class Shipmentreference
        {
            public string value { get; set; }
            public string type { get; set; }
            public string name { get; set; }
        }

    }

    public class TrackingURL
    {
        public string url { get; set; }
    }

}