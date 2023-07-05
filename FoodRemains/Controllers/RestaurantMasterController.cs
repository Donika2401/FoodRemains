using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodRemains.Models;

namespace FoodRemains.Controllers
{
    public class RestaurantMasterController : Controller
    {
        private FoodRemainsEntities db = new FoodRemainsEntities();

        // GET: RestaurantMaster
        public ActionResult Index()
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];
            var restaurantMasters = db.RestaurantMasters.Include(r => r.OwnerMaster).Where(r=>r.OwnerID == ownerID);
            return View(restaurantMasters.ToList());
        }

        // GET: RestaurantMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantMaster restaurantMaster = db.RestaurantMasters.Find(id);
            if (restaurantMaster == null)
            {
                return HttpNotFound();
            }
            return View(restaurantMaster);
        }

        // GET: RestaurantMaster/Create
        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.OwnerMasters, "OwnerID", "FirstName");
            return View();
        }

        // POST: RestaurantMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestaurantID,RestaurantName,Location,OpeningHour,ClosingHour,AvailableService,Cuisine,Photo,OwnerID")] RestaurantMaster restaurantMaster)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/RestaurantImages/"), fileName);
                        file.SaveAs(path);
                        restaurantMaster.Photo = fileName;
                    }
                }
                catch (Exception e) { }
                restaurantMaster.OwnerID = (int)System.Web.HttpContext.Current.Session["ownerid"];
                db.RestaurantMasters.Add(restaurantMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.OwnerMasters, "OwnerID", "FirstName", restaurantMaster.OwnerID);
            return View(restaurantMaster);
        }

        // GET: RestaurantMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantMaster restaurantMaster = db.RestaurantMasters.Find(id);
            if (restaurantMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.OwnerMasters, "OwnerID", "FirstName", restaurantMaster.OwnerID);
            return View(restaurantMaster);
        }

        // POST: RestaurantMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestaurantID,RestaurantName,Location,OpeningHour,ClosingHour,AvailableService,Cuisine,Photo,OwnerID")] RestaurantMaster restaurantMaster)
        {
            if (ModelState.IsValid)
            {
                restaurantMaster.OwnerID = (int)System.Web.HttpContext.Current.Session["ownerid"];
                db.Entry(restaurantMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.OwnerMasters, "OwnerID", "FirstName", restaurantMaster.OwnerID);
            return View(restaurantMaster);
        }

        // GET: RestaurantMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantMaster restaurantMaster = db.RestaurantMasters.Find(id);
            if (restaurantMaster == null)
            {
                return HttpNotFound();
            }
            return View(restaurantMaster);
        }

        // POST: RestaurantMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurantMaster restaurantMaster = db.RestaurantMasters.Find(id);
            db.RestaurantMasters.Remove(restaurantMaster);
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
