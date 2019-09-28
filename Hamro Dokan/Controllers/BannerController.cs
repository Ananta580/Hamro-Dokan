using Hamro_dokan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hamro_dokan.Controllers
{
    public class BannerController : Controller
    {
        HamroDokanDBEntities _db = new HamroDokanDBEntities();
        // GET: Banner
        [Authorize(Users = "admin@hamrodokan.com")]
        public ActionResult Index()
        {
            return View(_db.tblBanners.ToList());
        }
        [Authorize(Users = "admin@hamrodokan.com")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(tblBanner tb)
        {

            HttpPostedFileBase fup = Request.Files["Path"];
            if (fup != null)
            {
                tb.Path = fup.FileName;
                fup.SaveAs(Server.MapPath("~/Images/Banners/" + fup.FileName));
            }
            _db.tblBanners.Add(tb);
            if (_db.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }


            return View();
        }
        [Authorize(Users = "admin@hamrodokan.com")]
        public ActionResult Edit(int id)
        {
            tblBanner tb = _db.tblBanners.Where(u => u.BannerId == id).FirstOrDefault();
            return View(tb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblBanner bd)
        {
            tblBanner tb = _db.tblBanners.Where(u => u.BannerId == bd.BannerId).FirstOrDefault();

            tb.Header = bd.Header;
            tb.Caption = bd.Caption;
            HttpPostedFileBase fup = Request.Files["Path"];
            if (fup != null)
            {
                if (fup.FileName != "")
                {
                    System.IO.File.Delete(Server.MapPath("~/Images/Banners/" + bd.Path));

                    tb.Path = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/Images/Banners/" + fup.FileName));


                }
                else
                {
                    tb.Path = bd.Path;
                }
            }
            if (_db.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        static int uid;
        [Authorize(Users = "admin@hamrodokan.com")]
        public ActionResult Delete(int id)
        {
            uid = id;
            tblBanner tb = _db.tblBanners.Where(u => u.BannerId == id).FirstOrDefault();
            return View(tb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            tblBanner tb = _db.tblBanners.Where(u => u.BannerId == uid).FirstOrDefault();
            System.IO.File.Delete(Server.MapPath("~/Images/Banners/" + tb.Path));
            _db.tblBanners.Remove(tb);
            _db.SaveChanges();
            return RedirectToAction("Index", "Banner");
        }
    }
}