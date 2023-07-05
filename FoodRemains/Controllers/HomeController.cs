using FoodRemains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodRemains.Controllers
{
    public class HomeController : Controller
    {
        private FoodRemainsEntities db = new FoodRemainsEntities();
        public ActionResult Index()
        {
            var postedFoodMasters = (from p in db.PostFoodMasters
                                     join r in db.RestaurantMasters on p.RestaurantID equals r.RestaurantID
                                     select new
                                     {
                                         PostedFoodID = p.PostedFoodID,
                                         FoodItem = p.FoodItem,
                                         Quantity = p.Quantity,
                                         ServesPerson = p.ServesPerson,
                                         Description = p.Description,
                                         RestaurantID = p.RestaurantID,
                                         RestaurantName = r.RestaurantName,
                                         Photo = r.Photo,
                                         OpeningHour = r.OpeningHour,
                                         ClosingHour = r.ClosingHour
                                     }).ToList().Select(x => new PostFoodMaster()
                                     {
                                         PostedFoodID = x.PostedFoodID,
                                         FoodItem = x.FoodItem,
                                         Quantity = x.Quantity,
                                         ServesPerson = x.ServesPerson,
                                         Description = x.Description,
                                         RestaurantID = x.RestaurantID,
                                         RestaurantName = x.RestaurantName,
                                         Photo = x.Photo,
                                         OpeningHour = x.OpeningHour,
                                         ClosingHour = x.ClosingHour
                                     });
           
            return View(postedFoodMasters.Where(x => x.Quantity > 0).OrderByDescending(p => p.PostedFoodID).ToList());
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}