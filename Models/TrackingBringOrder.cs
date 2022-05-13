using System;

namespace AdvaniaAPI
{
    public class TrackingDataBring
    {

        public class Rootobject
        {
            public string apiVersion { get; set; }
            public Consignmentset[] consignmentSet { get; set; }
        }

        public class Consignmentset
        {
            public string consignmentId { get; set; }
            public string previousConsignmentId { get; set; }
            public Packageset[] packageSet { get; set; }
            public object recipientName { get; set; }
            public Recipientaddress recipientAddress { get; set; }
            public Recipienthandlingaddress recipientHandlingAddress { get; set; }
            public string senderReference { get; set; }
            public string senderCustomerNumber { get; set; }
            public string senderCustomerMasterNumber { get; set; }
            public string senderName { get; set; }
            public Senderaddress senderAddress { get; set; }
            public object senderHandlingAddress { get; set; }
            public string senderCustomerType { get; set; }
            public string recipientCustomerNumber { get; set; }
            public string recipientCustomerMasterNumber { get; set; }
            public string recipientCustomerType { get; set; }
            public object totalListPrice { get; set; }
            public object totalContractPrice { get; set; }
            public object listPricePackageCount { get; set; }
            public object contractPricePackageCount { get; set; }
            public object currencyCode { get; set; }
            public object[] consignmentActionSet { get; set; }
            public string senderLogo { get; set; }
            public float totalWeightInKgs { get; set; }
            public float totalVolumeInDm3 { get; set; }
            public bool isPickupNoticeAvailable { get; set; }
        }

        public class Recipientaddress
        {
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string postalCode { get; set; }
            public string city { get; set; }
            public string countryCode { get; set; }
            public string country { get; set; }
        }

        public class Recipienthandlingaddress
        {
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string postalCode { get; set; }
            public string city { get; set; }
            public string countryCode { get; set; }
            public string country { get; set; }
        }

        public class Senderaddress
        {
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string postalCode { get; set; }
            public string city { get; set; }
            public string countryCode { get; set; }
            public string country { get; set; }
        }

        public class Packageset
        {
            public string statusDescription { get; set; }
            public object[] descriptions { get; set; }
            public string packageNumber { get; set; }
            public string previousPackageNumber { get; set; }
            public string productName { get; set; }
            public string productCode { get; set; }
            public string productLink { get; set; }
            public string brand { get; set; }
            public int lengthInCm { get; set; }
            public int widthInCm { get; set; }
            public int heightInCm { get; set; }
            public float volumeInDm3 { get; set; }
            public float weightInKgs { get; set; }
            public object listPrice { get; set; }
            public object contractPrice { get; set; }
            public object currencyCode { get; set; }
            public string pickupCode { get; set; }
            public object shelfNumber { get; set; }
            public object dateOfReturn { get; set; }
            public object dateOfEstimatedDelivery { get; set; }
            public object estimatedTimeSpanOfDelivery { get; set; }
            public string senderName { get; set; }
            public Senderaddress1 senderAddress { get; set; }
            public object senderHandlingAddress { get; set; }
            public object recipientName { get; set; }
            public Recipientaddress1 recipientAddress { get; set; }
            public Recipienthandlingaddress1 recipientHandlingAddress { get; set; }
            public object recipientMobileNumber { get; set; }
            public object recipientEmailAddress { get; set; }
            public Eventset[] eventSet { get; set; }
            public object[] additionalServiceSet { get; set; }
            public object requestedPackage { get; set; }
            public string thirdPartyMobileNumber { get; set; }
            public object expectedPickupUnitId { get; set; }
            public string expectedPickupUnitName { get; set; }
            public string expectedPickupUnitURL { get; set; }
        }

        public class Senderaddress1
        {
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string postalCode { get; set; }
            public string city { get; set; }
            public string countryCode { get; set; }
            public string country { get; set; }
        }

        public class Recipientaddress1
        {
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string postalCode { get; set; }
            public string city { get; set; }
            public string countryCode { get; set; }
            public string country { get; set; }
        }

        public class Recipienthandlingaddress1
        {
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string postalCode { get; set; }
            public string city { get; set; }
            public string countryCode { get; set; }
            public string country { get; set; }
        }

        public class Eventset
        {
            public string description { get; set; }
            public string status { get; set; }
            public object lmEventCode { get; set; }
            public object lmCauseCode { get; set; }
            public object lmMeasureCode { get; set; }
            public Recipientsignature recipientSignature { get; set; }
            public string unitId { get; set; }
            public object unitInformationUrl { get; set; }
            public string unitType { get; set; }
            public string postalCode { get; set; }
            public string city { get; set; }
            public string countryCode { get; set; }
            public string country { get; set; }
            public DateTime dateIso { get; set; }
            public string displayDate { get; set; }
            public string displayTime { get; set; }
            public bool consignmentEvent { get; set; }
            public bool insignificant { get; set; }
            public string gpsXCoordinate { get; set; }
            public string gpsYCoordinate { get; set; }
            public string gpsMapUrl { get; set; }
        }

        public class Recipientsignature
        {
            public string name { get; set; }
            public object linkToImage { get; set; }
        }

    }

}