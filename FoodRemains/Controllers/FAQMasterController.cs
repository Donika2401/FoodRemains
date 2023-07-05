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
    public class FAQMasterController : Controller
    {
        private FoodRemainsEntities db = new FoodRemainsEntities();

        // GET: FAQMaster
        public ActionResult Index()
        {
            return View(db.FAQMasters.ToList());
        }

        // GET: FAQMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQMaster fAQMaster = db.FAQMasters.Find(id);
            if (fAQMaster == null)
            {
                return HttpNotFound();
            }
            return View(fAQMaster);
        }

        // GET: FAQMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FAQMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FAQID,Question,Answer")] FAQMaster fAQMaster)
        {
            if (ModelState.IsValid)
            {
                db.FAQMasters.Add(fAQMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fAQMaster);
        }

        // GET: FAQMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQMaster fAQMaster = db.FAQMasters.Find(id);
            if (fAQMaster == null)
            {
                return HttpNotFound();
            }
            return View(fAQMaster);
        }

        // POST: FAQMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FAQID,Question,Answer")] FAQMaster fAQMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fAQMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fAQMaster);
        }

        // GET: FAQMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQMaster fAQMaster = db.FAQMasters.Find(id);
            if (fAQMaster == null)
            {
                return HttpNotFound();
            }
            return View(fAQMaster);
        }

        // POST: FAQMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FAQMaster fAQMaster = db.FAQMasters.Find(id);
            db.FAQMasters.Remove(fAQMaster);
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
