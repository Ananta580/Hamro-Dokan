using Hamro_dokan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hamro_dokan.Controllers
{
    public class HomeController : Controller
    {
        HamroDokanDBEntities _db = new HamroDokanDBEntities();
        public ActionResult Index()
        {
            return View(_db.tblProducts.ToList());
        }
        public ActionResult _Slider()
        {
            return PartialView(_db.tblBanners.ToList());
        }
        public ActionResult Manager()
        {
            return View();
        }
        public ActionResult AddToCart(int id)
        {       
            tblCart tc = new tblCart();
            tc.ProductId = id;
            tblUser ur = _db.tblUsers.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            if(ur==null)
            {
                return RedirectToAction("Login", "Account");
            }
            tc.UserId = ur.UserId;
            _db.tblCarts.Add(tc);
            _db.SaveChanges();
            tc =_db.tblCarts.Where(m => m.UserId == ur.UserId).FirstOrDefault();
            return RedirectToAction("Cart", "Home");
        }
        public ActionResult Cart()
        {
            
            tblUser ur = _db.tblUsers.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            List<tblCart> tc= _db.tblCarts.Where(m => m.UserId == ur.UserId).ToList();
            return View(tc);
        }
        static int uid;
        public ActionResult DeleteCart(int id)
        {
            uid = id;
            tblCart tb = _db.tblCarts.Where(u => u.CartId == id).FirstOrDefault();
            return View(tb);
        }
        [HttpPost]
        public ActionResult DeleteCart()
        {
            tblCart tb = _db.tblCarts.Where(u => u.CartId == uid).FirstOrDefault();
            _db.tblCarts.Remove(tb);
            _db.SaveChanges();
            return RedirectToAction("Cart", "Home");
        }
        public ActionResult Checkout()
        {
            return View();
        }
    }
}