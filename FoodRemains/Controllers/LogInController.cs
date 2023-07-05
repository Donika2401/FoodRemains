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
    public class LogInController : Controller
    {
        private FoodRemainsEntities db = new FoodRemainsEntities();

        // GET: LogIn
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Index(LogInMaster owner)
        {
            if (ModelState.IsValid)
            {
                var r = db.OwnerMasters.FirstOrDefault(x => x.EmailID == owner.EmailID && x.Password == owner.Password);
                if (r == null)
                {
                    TempData["Message"] = "Please enter correct Email ID and Password";
                    return View();
                }
                else
                {
                    var user = r.EmailID;
                    Session["email"] = user;
                    Session["userrole"] = r.IsAdmin;
                    Session["ownerid"] = r.OwnerID;
                    return RedirectToAction("Index", "PostFoodMaster");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["email"] != null) 
            {
                Session["email"] = null;
                Session["userrole"] = null;
                Session["ownerid"] = null;
                Session.RemoveAll();        
            }
            return RedirectToAction("Index", "Home");
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
