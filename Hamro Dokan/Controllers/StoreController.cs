using Hamro_dokan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hamro_dokan.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        HamroDokanDBEntities _db = new HamroDokanDBEntities();
        public ActionResult Mobile()
        {
            return View(_db.tblProducts.ToList());
        }
        public ActionResult Pant()
        {
            return View(_db.tblProducts.ToList());
        }
        public ActionResult Tshirt()
        {
            return View(_db.tblProducts.ToList());
        }
        public ActionResult Kurtha()
        {
            return View(_db.tblProducts.ToList());
        }
        public ActionResult Detail(int id)
        {
            tblProduct tb = _db.tblProducts.Where(m => m.ProductId == id).FirstOrDefault();
            return View(tb);
        }
    }
}