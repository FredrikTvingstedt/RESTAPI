using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TrackingDataAccess;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace AdvaniaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvaniaAPIController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdvaniaAPIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<bts_TrackingOrders> Get()
        {
            using (THoloconEntities entities = new THoloconEntities())
            {
                return entities.bts_TrackingOrders.ToList();
            }
        }

        [HttpGet("{TrackingNumber}")]
        public bts_TrackingOrders Get(string TrackingNumber)
        {
            using (THoloconEntities entities = new THoloconEntities())
            {
                return entities.bts_TrackingOrders.FirstOrDefault(e => e.ShipmentId == TrackingNumber);
            }
        }

        [HttpPost("{TrackingNumber}")]
        public async void Post([FromBody] bts_TrackingOrders bts_trackingOrders, string TrackingNumber, string Carrier ,string PurchaseOrderNumber, string ExternalOrderNumber)
        {
            //POSTNORD
            if (System.Text.RegularExpressions.Regex.Match(Carrier, "(postnord|Postnord|PostNord|POSTNORD)").Success)
            {
                {
                    TrackingDataPostNord.Rootobject Data = new TrackingDataPostNord.Rootobject();

                    var authKey = _configuration.GetValue<string>("AuthKeyPostNord");
                    var authUrl = _configuration.GetValue<string>("AuthUrlPostNord");
                    var publicUrl = _configuration.GetValue<string>("PublicUrlPostNord");
                    
                    string Track = "";
                    var URL = string.Format("{0}{1}&id={2}",authUrl, authKey, TrackingNumber);
                    using (var httpClient = new HttpClient())
                    {
                        var respons = await httpClient.GetAsync(URL);
                        Track = await respons.Content.ReadAsStringAsync();
                        Data = JsonConvert.DeserializeObject<TrackingDataPostNord.Rootobject>(Track);
                    }

                    using (THoloconEntities entities = new THoloconEntities())
                    {
                        {
                            bts_trackingOrders.ShipmentId = Data.TrackingInformationResponse.shipments[0].shipmentId;
                            bts_trackingOrders.ShipmentCarrier = "POSTNORD";
                            bts_trackingOrders.ShipmentStatus = Data.TrackingInformationResponse.shipments[0].status;
                            if (Data.TrackingInformationResponse.shipments[0].status == "DELIVERED")
                            {
                                bts_trackingOrders.ShipmentDeliveryDate = Data.TrackingInformationResponse.shipments[0].deliveryDate;
                            }
                            else if (Data.TrackingInformationResponse.shipments[0].status == "RETURNED")
                            {
                                bts_trackingOrders.ShipmentDeliveryDate = Data.TrackingInformationResponse.shipments[0].deliveryDate;
                            }
                            else
                            {
                                bts_trackingOrders.ShipmentDeliveryDate = null;
                            }
                            bts_trackingOrders.ShipmentTrackingURL = publicUrl + Data.TrackingInformationResponse.shipments[0].shipmentId;
                            bts_trackingOrders.PurchaseOrderNumber = PurchaseOrderNumber;
                            bts_trackingOrders.ExternalOrderNumber = ExternalOrderNumber;
                            if (Data.TrackingInformationResponse.shipments[0].status == "DELIVERED")
                            {
                                bts_trackingOrders.KeepTrack = false;
                            }
                            else if (Data.TrackingInformationResponse.shipments[0].status == "RETURNED")
                            {
                                bts_trackingOrders.KeepTrack = false;
                            }
                            else
                            {
                                bts_trackingOrders.KeepTrack = true;
                            }
                        }

                        entities.bts_TrackingOrders.Add(bts_trackingOrders);
                        entities.SaveChanges();
                    }
                }
            }

            //UPS
            else if (System.Text.RegularExpressions.Regex.Match(Carrier, "(ups|UPS|Ups)").Success)
            {
                {
                    TrackingDataUPS.Rootobject Data = new TrackingDataUPS.Rootobject();

                    var authUrl = _configuration.GetValue<string>("AuthUrlUPS");
                    var publicUrl = _configuration.GetValue<string>("PublicUrlUPS");
                    string Track = "";
                    var URL = string.Format("{0}{1}", authUrl, TrackingNumber);
                    using (var httpClient = new HttpClient())
                    {
                        var authUsername = _configuration.GetValue<string>("AuthUsernameUPS");
                        var authPassword = _configuration.GetValue<string>("AuthPasswordUPS");
                        var authAccessLicenseNumber = _configuration.GetValue<string>("AuthAccessLicenseNumberUPS");
                        

                        httpClient.DefaultRequestHeaders.Clear();

                        //Header authorization Username,Password,AccessKey

                        httpClient.DefaultRequestHeaders.Add("Username", authUsername);
                        httpClient.DefaultRequestHeaders.Add("Password", authPassword);
                        httpClient.DefaultRequestHeaders.Add("AccessLicenseNumber", authAccessLicenseNumber);

                        //Define request data format  
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var respons = await httpClient.GetAsync(URL);
                        Track = await respons.Content.ReadAsStringAsync();
                        Data = JsonConvert.DeserializeObject<TrackingDataUPS.Rootobject>(Track);
                    }

                    string dateInput = Data.trackResponse.shipment[0].package[0].activity[0].date + Data.trackResponse.shipment[0].package[0].activity[0].time;
                    var parsedDate = DateTime.ParseExact(dateInput,"yyyyMMddHHmmss", null);

                    using (THoloconEntities entities = new THoloconEntities())
                    {
                        {
                            bts_trackingOrders.ShipmentId = Data.trackResponse.shipment[0].package[0].trackingNumber;
                            bts_trackingOrders.ShipmentCarrier = "UPS";
                            bts_trackingOrders.ShipmentStatus = Data.trackResponse.shipment[0].package[0].activity[0].status.description;
                            if (Data.trackResponse.shipment[0].package[0].activity[0].status.description == "Delivered")
                            {
                                bts_trackingOrders.ShipmentDeliveryDate = parsedDate; 
                            }
                            else if (Data.trackResponse.shipment[0].package[0].activity[0].status.description == "DeliveryAttempted")
                            {
                                bts_trackingOrders.ShipmentDeliveryDate = parsedDate;
                            }
                            else
                            {
                                bts_trackingOrders.ShipmentDeliveryDate = null;
                            }
                            bts_trackingOrders.ShipmentTrackingURL = publicUrl + Data.trackResponse.shipment[0].package[0].trackingNumber;
                            bts_trackingOrders.PurchaseOrderNumber = PurchaseOrderNumber;
                            bts_trackingOrders.ExternalOrderNumber = ExternalOrderNumber;
                            if (Data.trackResponse.shipment[0].package[0].activity[0].status.description == "Delivered")
                            {
                                bts_trackingOrders.KeepTrack = false;
                            }
                            else
                            {
                                bts_trackingOrders.KeepTrack = true;
                            }
                        }
                        entities.bts_TrackingOrders.Add(bts_trackingOrders);
                        entities.SaveChanges();
                    }
                }
            }

            //DHL
            else if (System.Text.RegularExpressions.Regex.Match(Carrier, "(dhl|DHL|Dhl)").Success)
            {
                {
                    TrackingDataDHL.Rootobject Data = new TrackingDataDHL.Rootobject();

                    var authUrl = _configuration.GetValue<string>("AuthUrlDHL");
                    var publicUrl = _configuration.GetValue<string>("PublicUrlDHL");
                    string Track = "";
                    var URL = string.Format("{0}{1}", authUrl, TrackingNumber);
                    using (var httpClient = new HttpClient())
                    {
                        var authKey = _configuration.GetValue<string>("AuthKeyDHL");
                        var authUser = _configuration.GetValue<string>("AuthUserDHL");
                        httpClient.DefaultRequestHeaders.Clear();

                        //Header authorization Username,Password,AccessKey

                        httpClient.DefaultRequestHeaders.Add(authUser, authKey);
                       
                        //Define request data format  
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var respons = await httpClient.GetAsync(URL);
                        Track = await respons.Content.ReadAsStringAsync();
                        Data = JsonConvert.DeserializeObject<TrackingDataDHL.Rootobject>(Track);
                    }

                    using (THoloconEntities entities = new THoloconEntities())
                    {
                        {
                        bts_trackingOrders.ShipmentId =  TrackingNumber;
                        bts_trackingOrders.ShipmentCarrier = "DHL";
                        bts_trackingOrders.ShipmentStatus = Data.shipments[0].events[0].status;
                        if (Data.shipments[0].events[0].status == "Delivered")
                        {
                        bts_trackingOrders.ShipmentDeliveryDate = Data.shipments[0].events[0].timestamp;
                        }
                        else
                        {
                        bts_trackingOrders.ShipmentDeliveryDate = null;
                        }
                        bts_trackingOrders.ShipmentDeliveryDate = Data.shipments[0].events[0].timestamp;
                        bts_trackingOrders.ShipmentTrackingURL = publicUrl + TrackingNumber;
                        bts_trackingOrders.PurchaseOrderNumber = PurchaseOrderNumber;
                        bts_trackingOrders.ExternalOrderNumber = ExternalOrderNumber;
                        if (Data.shipments[0].events[0].status == "Delivered")
                        {
                        bts_trackingOrders.KeepTrack = false;
                        }
                        else
                        {
                        bts_trackingOrders.KeepTrack = true;
                        }
                        }
                        entities.bts_TrackingOrders.Add(bts_trackingOrders);
                        entities.SaveChanges();
                    }
                }
            }
            //BRING
            else if (System.Text.RegularExpressions.Regex.Match(Carrier, "(bring|Bring|BRING)").Success)
            {
                TrackingDataBring.Rootobject Data = new TrackingDataBring.Rootobject();

                var authUrl = _configuration.GetValue<string>("AuthUrlBring");
                var publicUrl = _configuration.GetValue<string>("PublicUrlBring");
                string Track = "";
                var URL = string.Format("{0}{1}", authUrl, TrackingNumber);
                using (var httpClient = new HttpClient())
                {
                    var respons = await httpClient.GetAsync(URL);
                    Track = await respons.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<TrackingDataBring.Rootobject>(Track);
                }

                using (THoloconEntities entities = new THoloconEntities())
                {
                    {
                        bts_trackingOrders.ShipmentId = TrackingNumber;
                        bts_trackingOrders.ShipmentCarrier = "BRING";
                        bts_trackingOrders.ShipmentStatus = Data.consignmentSet[0].packageSet[0].eventSet[0].status;
                        if (Data.consignmentSet[0].packageSet[0].eventSet[0].status == "DELIVERED")
                        {
                            bts_trackingOrders.ShipmentDeliveryDate = Data.consignmentSet[0].packageSet[0].eventSet[0].dateIso;
                        }
                        else
                        {
                            bts_trackingOrders.ShipmentDeliveryDate = null;
                        }
                        bts_trackingOrders.ShipmentTrackingURL = publicUrl + Data.consignmentSet[0].packageSet[0].packageNumber;
                        bts_trackingOrders.PurchaseOrderNumber = PurchaseOrderNumber;
                        bts_trackingOrders.ExternalOrderNumber = ExternalOrderNumber;
                        if ( Data.consignmentSet[0].packageSet[0].eventSet[0].status == "DELIVERED")
                        { 
                            bts_trackingOrders.KeepTrack = false; 
                        }
                        else
                        {
                            bts_trackingOrders.KeepTrack = true;
                        }
                    }
                    entities.bts_TrackingOrders.Add(bts_trackingOrders);
                    entities.SaveChanges();
                }
            }

            //TNT
            else if (System.Text.RegularExpressions.Regex.Match(Carrier, "(tnt|TNT|Tnt)").Success)
            {
                var publicUrl = _configuration.GetValue<string>("PublicUrlTNT");
                using (THoloconEntities entities = new THoloconEntities())
                {
                    {
                        bts_trackingOrders.ShipmentId = TrackingNumber;
                        bts_trackingOrders.ShipmentCarrier = "TNT";
                        bts_trackingOrders.ShipmentStatus = null;
                        bts_trackingOrders.ShipmentDeliveryDate = null;
                        bts_trackingOrders.ShipmentTrackingURL = publicUrl + TrackingNumber;
                        bts_trackingOrders.PurchaseOrderNumber = PurchaseOrderNumber;
                        bts_trackingOrders.ExternalOrderNumber = ExternalOrderNumber;
                        bts_trackingOrders.KeepTrack = true;
                    }
                    entities.bts_TrackingOrders.Add(bts_trackingOrders);
                    entities.SaveChanges();
                }
            }

            //ÖVRIGA TRANSPORTÖRER
            else if (System.Text.RegularExpressions.Regex.Match(Carrier, "(best|schenker|övrig|dsv|dpd|fedex|gls|posten|egen|eget|transportör|logistik||)").Success)
            {
                using (THoloconEntities entities = new THoloconEntities())
                {
                    {
                        bts_trackingOrders.ShipmentId = TrackingNumber;
                        bts_trackingOrders.ShipmentCarrier = "ÖVRIG";
                        bts_trackingOrders.ShipmentStatus = null;
                        bts_trackingOrders.ShipmentDeliveryDate = null;
                        bts_trackingOrders.ShipmentTrackingURL = null;
                        bts_trackingOrders.PurchaseOrderNumber = PurchaseOrderNumber;
                        bts_trackingOrders.ExternalOrderNumber = ExternalOrderNumber;
                        bts_trackingOrders.KeepTrack = true;
                    }
                    entities.bts_TrackingOrders.Add(bts_trackingOrders);
                    entities.SaveChanges();
                }
            }
        }
    }
}


