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
    public class PostFoodMasterController : Controller
    {
        private FoodRemainsEntities db = new FoodRemainsEntities();

        // GET: PostFoodMaster
        public ActionResult Index()
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];

            var postFoodMasters = (from p in db.PostFoodMasters
                                   join r in db.RestaurantMasters on p.RestaurantID equals r.RestaurantID
                                   where r.OwnerID == ownerID
                                   select new
                                   {
                                       PostedFoodID = p.PostedFoodID,
                                       FoodItem = p.FoodItem,
                                       Quantity = p.Quantity,
                                       ServesPerson = p.ServesPerson,
                                       Description = p.Description,
                                       RestaurantID = p.RestaurantID,
                                       RestaurantName = r.RestaurantName
                                   }).ToList().Select(x => new PostFoodMaster()
                                   {
                                       PostedFoodID = x.PostedFoodID,
                                       FoodItem = x.FoodItem,
                                       Quantity = x.Quantity,
                                       ServesPerson = x.ServesPerson,
                                       Description = x.Description,
                                       RestaurantID = x.RestaurantID,
                                       RestaurantName = x.RestaurantName
                                   }); ;

            //var postFoodMasters = db.PostFoodMasters.Include(p => p.RestaurantMaster);
            return View(postFoodMasters.ToList());
        }

        // GET: PostFoodMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostFoodMaster postFoodMaster = db.PostFoodMasters.Find(id);
            if (postFoodMaster == null)
            {
                return HttpNotFound();
            }
            return View(postFoodMaster);
        }

        // GET: PostFoodMaster/Create
        public ActionResult Create()
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];

            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters.Where(r => r.OwnerID == ownerID), "RestaurantID", "RestaurantName");
            return View();
        }

        // POST: PostFoodMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostedFoodID,FoodItem,Quantity,ServesPerson,Description,RestaurantID")] PostFoodMaster postFoodMaster)
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];
            if (ModelState.IsValid)
            {
                db.PostFoodMasters.Add(postFoodMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters.Where(r => r.OwnerID == ownerID), "RestaurantID", "RestaurantName", postFoodMaster.RestaurantID);
            return View(postFoodMaster);
        }

        // GET: PostFoodMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostFoodMaster postFoodMaster = db.PostFoodMasters.Find(id);
            if (postFoodMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters.Where(r => r.OwnerID == ownerID), "RestaurantID", "RestaurantName", postFoodMaster.RestaurantID);
            return View(postFoodMaster);
        }

        // POST: PostFoodMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostedFoodID,FoodItem,Quantity,ServesPerson,Description,RestaurantID")] PostFoodMaster postFoodMaster)
        {
            int ownerID = (int)System.Web.HttpContext.Current.Session["ownerid"];
            if (ModelState.IsValid)
            {
                db.Entry(postFoodMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantID = new SelectList(db.RestaurantMasters.Where(r => r.OwnerID == ownerID), "RestaurantID", "RestaurantName", postFoodMaster.RestaurantID);
            return View(postFoodMaster);
        }

        // GET: PostFoodMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostFoodMaster postFoodMaster = db.PostFoodMasters.Find(id);
            if (postFoodMaster == null)
            {
                return HttpNotFound();
            }
            return View(postFoodMaster);
        }

        // POST: PostFoodMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostFoodMaster postFoodMaster = db.PostFoodMasters.Find(id);
            db.PostFoodMasters.Remove(postFoodMaster);
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
