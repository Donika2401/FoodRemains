using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodRemains.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace FoodRemains.Controllers
{
    public class OrderDetailController : Controller
    {
        private FoodRemainsEntities db = new FoodRemainsEntities();

        // GET: OrderDetail
        public ActionResult Index()
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];

            var orderDetails = (from o in db.OrderDetails
                                join r in db.RestaurantMasters on o.RestaurantID equals r.RestaurantID
                                join p in db.PostFoodMasters on o.PostedFoodID equals p.PostedFoodID
                                where r.OwnerID == ownerID
                                select new
                                {
                                    OrderID = o.OrderID,
                                    RestaurantID = o.RestaurantID,
                                    PostedFoodID = o.PostedFoodID,
                                    Quantity = o.Quantity,
                                    CustomerFirstName = o.CustomerFirstName,
                                    CustomerLastName = o.CustomerLastName,
                                    ContactNo = o.ContactNo,
                                    EmailID = o.EmailID,
                                    OrderReferenceNo = o.OrderReferenceNo,
                                    RestaurantName = r.RestaurantName,
                                    FoodItem = p.FoodItem
                                }).ToList().Select(x => new OrderDetail()
                                {
                                    OrderID = x.OrderID,
                                    RestaurantID = x.RestaurantID,
                                    PostedFoodID = x.PostedFoodID,
                                    Quantity = x.Quantity,
                                    CustomerFirstName = x.CustomerFirstName,
                                    CustomerLastName = x.CustomerLastName,
                                    ContactNo = x.ContactNo,
                                    EmailID = x.EmailID,
                                    OrderReferenceNo = x.OrderReferenceNo,
                                    RestaurantName = x.RestaurantName,
                                    FoodItem = x.FoodItem
                                });

            //var orderDetails = db.OrderDetails.Include(o => o.PostFoodMaster).Include(o => o.RestaurantMaster);
            return View(orderDetails.ToList());
        }

        // GET: OrderDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetail/Create
        public ActionResult Create(int? restaurantID, int? postedFoodID, int? quantity)
        {
            Session["restaurantID"] = restaurantID;
            Session["postedFoodID"] = postedFoodID;
            Session["quantity"] = quantity;

            ViewBag.PostedFoodID = new SelectList(db.PostFoodMasters, "PostedFoodID", "FoodItem");
            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters, "RestaurantID", "RestaurantName");
            return View();
        }

        // POST: OrderDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,RestaurantID,PostedFoodID,Quantity,CustomerFirstName,CustomerLastName,EmailID,ContactNo,OrderReferenceNo")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                int postedFoodID = (int)System.Web.HttpContext.Current.Session["postedFoodID"];
                orderDetail.RestaurantID = (int)System.Web.HttpContext.Current.Session["restaurantID"];
                orderDetail.PostedFoodID = postedFoodID;
                int quantity = (int)System.Web.HttpContext.Current.Session["quantity"];

                if (quantity > 0 && orderDetail.Quantity > quantity)
                {
                    TempData["error"] = "Enter valid Quantity.";
                }
                else
                {
                    orderDetail.OrderReferenceNo = (orderDetail.CustomerFirstName.Substring(0, 1).ToUpper() + "" + orderDetail.CustomerLastName.Substring(0, 1).ToUpper() + "" + System.Guid.NewGuid().ToString().Substring(0, 6)).ToUpper();
                    db.OrderDetails.Add(orderDetail);
                    //db.SaveChanges();

                    PostFoodMaster postFoodMaster = db.PostFoodMasters.Find(orderDetail.PostedFoodID);
                    postFoodMaster.Quantity = quantity - orderDetail.Quantity;
                    db.Entry(postFoodMaster).State = EntityState.Modified;
                    db.SaveChanges();

                    SendSMS(orderDetail.ContactNo,"FreeFeeds - "+ DateTime.Now.ToString("f") + ", Order Ref No: " + orderDetail.OrderReferenceNo + ". Thank You! "+ orderDetail.CustomerFirstName);
                    TempData["message"] = "Order placed successfully. Your Order Reference No is: " + orderDetail.OrderReferenceNo;
                }
                return RedirectToAction("Create");
            }

            ViewBag.PostedFoodID = new SelectList(db.PostFoodMasters, "PostedFoodID", "FoodItem", orderDetail.PostedFoodID);
            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters, "RestaurantID", "RestaurantName", orderDetail.RestaurantID);
            return View(orderDetail);
        }

        public void SendSMS(string phoneNo,string orderMessage)
        {
            string accountSid = "ACa270f27186d164290fb659d2c6ebc069";
            string authToken = "67aaf69aa8f5467d6ff3fff6f8b9a380";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: orderMessage,
                from: new Twilio.Types.PhoneNumber("+19793169581"),
                to: new Twilio.Types.PhoneNumber("+91" +phoneNo)
            );
        }

        // GET: OrderDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostedFoodID = new SelectList(db.PostFoodMasters, "PostedFoodID", "FoodItem", orderDetail.PostedFoodID);
            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters.Where(p => p.OwnerID == ownerID), "RestaurantID", "RestaurantName", orderDetail.RestaurantID);
            return View(orderDetail);
        }

        // POST: OrderDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,RestaurantID,PostedFoodID,Quantity,CustomerFirstName,CustomerLastName,EmailID,ContactNo,OrderReferenceNo")] OrderDetail orderDetail)
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostedFoodID = new SelectList(db.PostFoodMasters, "PostedFoodID", "FoodItem", orderDetail.PostedFoodID);
            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters.Where(r => r.OwnerID == ownerID), "RestaurantID", "RestaurantName", orderDetail.RestaurantID);
            return View(orderDetail);
        }

        // GET: OrderDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
