using Hamro_dokan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hamro_dokan.Controllers
{
    public class ProductController : Controller
    {
        HamroDokanDBEntities _db = new HamroDokanDBEntities();
        // GET: Banner
        [Authorize(Users = "admin@hamrodokan.com")]
        public ActionResult Index()
        {
            return View(_db.tblProducts.ToList());
        }
        [Authorize(Users = "admin@hamrodokan.com")]
        public ActionResult Create()
        {
            List<tblCategory> td = _db.tblCategories.ToList();
            ViewBag.data = td;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(vw_Everything tb)
        {
            tblProduct tp = new tblProduct();
            tp.Brand = tb.Brand;
            tp.Color = tb.Color;
            tp.CategoryId = Convert.ToInt32( tb.CategoryName);
            tp.Description = tb.Description;
            tp.Price = tb.Price;
            tp.ProductName = tb.ProductName;
            
            
            HttpPostedFileBase fup = Request.Files["Img"];
            if(tp.CategoryId==1)
            {
                if (fup != null)
            {
                tp.Img = fup.FileName;
                fup.SaveAs(Server.MapPath("~/Images/Products/Mobile/" + fup.FileName));
            }
            }
            if (tp.CategoryId == 2)
            {
                if (fup != null)
                {
                    tp.Img = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/Images/Products/Pant/" + fup.FileName));
                }
            }
            if (tp.CategoryId == 3)
            {
                if (fup != null)
                {
                    tp.Img = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/Images/Products/Tshirt/" + fup.FileName));
                }
            }
            if (tp.CategoryId == 4)
            {
                if (fup != null)
                {
                    tp.Img = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/Images/Products/Kurtha/" + fup.FileName));
                }
            }

            _db.tblProducts.Add(tp);
            if (_db.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }


            return View();
        }
        [Authorize(Users = "admin@hamrodokan.com")]
        public ActionResult Edit(int id)
        {
            List<tblCategory> td = _db.tblCategories.ToList();
            ViewBag.data2 = td;
            tblProduct tb = _db.tblProducts.Where(u => u.ProductId == id).FirstOrDefault();
            return View(tb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(vw_Everything tb)
        {
            tblProduct tp = _db.tblProducts.Where(u => u.ProductId == tb.ProductId).FirstOrDefault();

            tp.Brand = tb.Brand;
            tp.CategoryId = Convert.ToInt32(tb.CategoryName);
            tp.Description = tb.Description;
            tp.Price = tb.Price;
            tp.ProductName = tb.ProductName;
            tp.Color = tb.Color;
            HttpPostedFileBase fup = Request.Files["Img"];
            if (fup != null)
            {
                if (fup.FileName != "")
                {
                    if (tp.CategoryId == 3)
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Products/Tshirt/" + tb.Img));

                        tp.Img = fup.FileName;
                        fup.SaveAs(Server.MapPath("~/Images/Products/Tshirt/" + fup.FileName));
                    }
                    if (tp.CategoryId == 1)
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Products/Mobile/" + tb.Img));

                        tp.Img = fup.FileName;
                        fup.SaveAs(Server.MapPath("~/Images/Products/Mobile/" + fup.FileName));
                    }
                    if (tp.CategoryId == 2)
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Products/Pant/" + tb.Img));

                        tp.Img = fup.FileName;
                        fup.SaveAs(Server.MapPath("~/Images/Products/Pant/" + fup.FileName));
                    }
                    if (tp.CategoryId == 4)
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Products/Kurtha/" + tb.Img));

                        tp.Img = fup.FileName;
                        fup.SaveAs(Server.MapPath("~/Images/Products/Kurtha/" + fup.FileName));
                    }
                }
                else
                {
                    tp.Img = tb.Img;
                }
            }
            if (_db.SaveChanges() > 0)
            {
                return RedirectToAction("Index","Product");
            }

            return View();
        }
        static int uid;
        [Authorize(Users = "admin@hamrodokan.com")]
        public ActionResult Delete(int id)
        {
            uid = id;
            tblProduct tb = _db.tblProducts.Where(u => u.ProductId == id).FirstOrDefault();
            return View(tb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            List<tblCart> tc = _db.tblCarts.Where(m => m.ProductId == uid).ToList();
            _db.tblCarts.RemoveRange(tc);
            _db.SaveChanges();
            tblProduct tb = _db.tblProducts.Where(u => u.ProductId == uid).FirstOrDefault();
            if (tb.CategoryId== 1)
            {
                System.IO.File.Delete(Server.MapPath("~/Images/Products/Mobile/" + tb.Img));
            }
            if (tb.CategoryId == 2)
            {
                System.IO.File.Delete(Server.MapPath("~/Images/Products/Pant/" + tb.Img));
            }
            if (tb.CategoryId == 3)
            {
                System.IO.File.Delete(Server.MapPath("~/Images/Products/Tshirt/" + tb.Img));
            }
            if (tb.CategoryId == 4)
            {
                System.IO.File.Delete(Server.MapPath("~/Images/Products/Kurtha/" + tb.Img));
            }
            _db.tblProducts.Remove(tb);
            _db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
    }
}
