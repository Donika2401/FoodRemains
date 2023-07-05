using FoodRemains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodRemains.Controllers
{
    public class TopDonorsController : Controller
    {
        private FoodRemainsEntities db = new FoodRemainsEntities();

        // GET: TopDonors
        public ActionResult Index()
        {
            var topDonors = (from p in db.PostFoodMasters
                             join r in db.RestaurantMasters on p.RestaurantID equals r.RestaurantID
                             join o in db.OwnerMasters on r.OwnerID equals o.OwnerID
                             //group p by new { p.RestaurantID, p.Quantity } into g
                             select new
                             {
                                 RestaurantID = r.RestaurantID,
                                 RestaurantName = r.RestaurantName,
                                 Location = r.Location,
                                 OpeningHour = r.OpeningHour,
                                 ClosingHour = r.ClosingHour,
                                 AvailableService = r.AvailableService,
                                 Cuisine = r.Cuisine,
                                 Photo = r.Photo,
                                 OwnerID = r.OwnerID,
                                 Quantity = p.Quantity,
                                 Website = o.Website
                             }).ToList().Select(x => new RestaurantMaster()
                             {
                                 RestaurantID = x.RestaurantID,
                                 RestaurantName = x.RestaurantName,
                                 Location = x.Location,
                                 OpeningHour = x.OpeningHour,
                                 ClosingHour = x.ClosingHour,
                                 AvailableService = x.AvailableService,
                                 Cuisine = x.Cuisine,
                                 Photo = x.Photo,
                                 OwnerID = x.OwnerID,
                                 Quantity = x.Quantity,
                                 Website = x.Website
                             });

            return View(topDonors.ToList().OrderBy(x=>x.RestaurantID));
            //return View(db.RestaurantMasters.ToList());
        }
    }
}