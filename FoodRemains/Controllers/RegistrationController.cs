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
    public class RegistrationController : Controller
    {
        private FoodRemainsEntities db = new FoodRemainsEntities();

        // GET: Registration
        public ActionResult Index()
        {
            return View(db.OwnerMasters.ToList());
        }

        // GET: Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerMaster ownerMaster = db.OwnerMasters.Find(id);
            if (ownerMaster == null)
            {
                return HttpNotFound();
            }
            return View(ownerMaster);
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OwnerID,FirstName,LastName,EmailID,ContactNo,Password,BusinessName,Website,IsAdmin")] OwnerMaster ownerMaster)
        {
            if (ModelState.IsValid)
            {
                ownerMaster.IsAdmin = true;
                db.OwnerMasters.Add(ownerMaster);
                db.SaveChanges();
                TempData["message"] = "Registered Successfully.";
                return RedirectToAction("Create");
            }

            return View(ownerMaster);
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerMaster ownerMaster = db.OwnerMasters.Find(id);
            if (ownerMaster == null)
            {
                return HttpNotFound();
            }
            return View(ownerMaster);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerID,FirstName,LastName,EmailID,ContactNo,Password,BusinessName,Website,IsAdmin")] OwnerMaster ownerMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ownerMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ownerMaster);
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerMaster ownerMaster = db.OwnerMasters.Find(id);
            if (ownerMaster == null)
            {
                return HttpNotFound();
            }
            return View(ownerMaster);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnerMaster ownerMaster = db.OwnerMasters.Find(id);
            db.OwnerMasters.Remove(ownerMaster);
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
