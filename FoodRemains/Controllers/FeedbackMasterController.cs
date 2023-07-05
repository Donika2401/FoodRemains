using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodRemains.Models;

namespace FoodRemains.Controllers
{
    public class FeedbackMasterController : Controller
    {
        private FoodRemainsEntities db = new FoodRemainsEntities();

        // GET: FeedbackMaster
        public ActionResult Index()
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];

            var feedbackMasters = (from f in db.FeedbackMasters
                                   join r in db.RestaurantMasters on f.RestaurantID equals r.RestaurantID
                                   where r.OwnerID == ownerID
                                   select new
                                   {
                                       FeedbackID = f.FeedbackID,
                                       FeedbackType = f.FeedbackType,
                                       Description = f.Description,
                                       FirstName = f.FirstName,
                                       LastName = f.LastName,
                                       EmailID = f.EmailID,
                                       RestaurantID = f.RestaurantID,
                                       RestaurantName = r.RestaurantName
                                   }).ToList().Select(x => new FeedbackMaster()
                                   {
                                       FeedbackID = x.FeedbackID,
                                       FeedbackType = x.FeedbackType,
                                       Description = x.Description,
                                       FirstName = x.FirstName,
                                       LastName = x.LastName,
                                       EmailID = x.EmailID,
                                       RestaurantID = x.RestaurantID,
                                       RestaurantName = x.RestaurantName
                                   }); 
            //var feedbackMasters = db.FeedbackMasters.Include(o => o.RestaurantMaster);
            return View(feedbackMasters.ToList());
        }

        // GET: FeedbackMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackMaster feedbackMaster = db.FeedbackMasters.Find(id);
            if (feedbackMaster == null)
            {
                return HttpNotFound();
            }
            return View(feedbackMaster);
        }

        // GET: FeedbackMaster/Create
        public ActionResult Create()
        {
            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters, "RestaurantID", "RestaurantName");
            return View();
        }

        // POST: FeedbackMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeedbackID,FeedbackType,Description,FirstName,LastName,EmailID,RestaurantID")] FeedbackMaster feedbackMaster)
        {
            if (ModelState.IsValid)
            {
                db.FeedbackMasters.Add(feedbackMaster);
                db.SaveChanges();
                TempData["message"] = "Thank you for your valuable feedback.";
                return RedirectToAction("Create");
            }

            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters, "RestaurantID", "RestaurantName", feedbackMaster.RestaurantID);
            return View(feedbackMaster);
        }

        // GET: FeedbackMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackMaster feedbackMaster = db.FeedbackMasters.Find(id);
            if (feedbackMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters, "RestaurantID", "RestaurantName", feedbackMaster.RestaurantID);
            return View(feedbackMaster);
        }

        // POST: FeedbackMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeedbackID,FeedbackType,Description,FirstName,LastName,EmailID,RestaurantID")] FeedbackMaster feedbackMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedbackMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters, "RestaurantID", "RestaurantName", feedbackMaster.RestaurantID);
            return View(feedbackMaster);
        }

        // GET: FeedbackMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackMaster feedbackMaster = db.FeedbackMasters.Find(id);
            if (feedbackMaster == null)
            {
                return HttpNotFound();
            }
            return View(feedbackMaster);
        }

        // POST: FeedbackMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeedbackMaster feedbackMaster = db.FeedbackMasters.Find(id);
            db.FeedbackMasters.Remove(feedbackMaster);
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
