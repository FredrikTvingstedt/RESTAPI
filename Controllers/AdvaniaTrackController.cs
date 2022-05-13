using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TrackingDataAccess;

namespace AdvaniaAPI.Controllers
{
    public class AdvaniaTrackController : Controller
    {
        public ActionResult Index()
        {
            THoloconEntities db = new THoloconEntities();
            return View(db.bts_TrackingOrders.ToList());
        }

        public ActionResult Details(string id)
        {
            THoloconEntities db = new THoloconEntities();
            return View(db.bts_TrackingOrders.FirstOrDefault(e => e.ShipmentId == id));
        }

        public ActionResult Order(string id)
        {
            THoloconEntities db = new THoloconEntities();
            return View(db.bts_TrackingOrders.FirstOrDefault(e => e.PurchaseOrderNumber == id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] bts_TrackingOrders bts_trackingOrders)
        {
            THoloconEntities db = new THoloconEntities();
            if (ModelState.IsValid)
            {
                db.bts_TrackingOrders.Add(bts_trackingOrders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bts_trackingOrders);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, bts_TrackingOrders bts_trackingOrders)
        {
            using (THoloconEntities entities = new THoloconEntities())
            {
                var entity = entities.bts_TrackingOrders.FirstOrDefault(e => e.ShipmentId == id);
                entity.ShipmentStatus = bts_trackingOrders.ShipmentStatus;
                entity.ShipmentCarrier = bts_trackingOrders.ShipmentCarrier;
                entity.ShipmentDeliveryDate = bts_trackingOrders.ShipmentDeliveryDate;
                entity.ShipmentTrackingURL = bts_trackingOrders.ShipmentTrackingURL;
                entity.PurchaseOrderNumber = bts_trackingOrders.PurchaseOrderNumber;
                entity.ExternalOrderNumber = bts_trackingOrders.ExternalOrderNumber;
                entity.KeepTrack = true;
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPut]
        public void Put(string id, [FromBody] bts_TrackingOrders bts_trackingOrders)
        {
            using (THoloconEntities entities = new THoloconEntities())
            {
                var entity = entities.bts_TrackingOrders.FirstOrDefault(e => e.ShipmentId == id);
                entity.ShipmentStatus = bts_trackingOrders.ShipmentStatus;
                entity.ShipmentCarrier = bts_trackingOrders.ShipmentCarrier;
                entity.ShipmentDeliveryDate = bts_trackingOrders.ShipmentDeliveryDate;
                entity.ShipmentTrackingURL = bts_trackingOrders.ShipmentTrackingURL;
                entity.PurchaseOrderNumber = bts_trackingOrders.PurchaseOrderNumber;
                entity.ExternalOrderNumber = bts_trackingOrders.ExternalOrderNumber;
                entity.KeepTrack = true;
                entities.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (THoloconEntities entities = new THoloconEntities())
            {
                entities.bts_TrackingOrders.Remove(entities.bts_TrackingOrders.FirstOrDefault(e => e.ShipmentId == id));
                entities.SaveChanges();
                
            }
        }
    }
}


