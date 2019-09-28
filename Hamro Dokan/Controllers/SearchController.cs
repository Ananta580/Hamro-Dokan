using Hamro_dokan.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hamro_dokan.Controllers
{
    public class FormData
    {
        public string ad1file { get; set; }
    }
    
    public class SearchController : Controller
    {
        // GET: Search
        HamroDokanDBEntities _db = new HamroDokanDBEntities();
        //static string p;
        //public ActionResult Search()
        //{
        //    //List<tblProduct> td = _db.tblProducts.Where(m => m.ProductName == p || m.tblCategory.CategoryName == p || m.Brand == p).ToList();
        //    return View(_db.tblProducts.Where(m => m.ProductName.Contains(p) || m.tblCategory.CategoryName.Contains(p) || m.Brand.Contains(p)).ToList());
        //}
        public ActionResult Index()
        {
            return View(_db.tblProducts.ToList());
        }
        public ActionResult _Text()
        {
            return View(_db.tblProducts.ToList());
        }
        public ActionResult _Image()
        {
            return View(_db.tblProducts.ToList());
        }
        [HttpPost]
        public ActionResult Text(string searching)
        {

            ViewBag.MyString = searching;
            return View(_db.tblProducts.Where(x => x.tblCategory.CategoryName.Contains(searching) ||x.Brand==searching||x.ProductName==searching||searching == null).ToList());
        }
        [HttpPost]
        public ActionResult Image(HttpPostedFileBase file, string result = "")
        {
            
            FormData fd = null;
            string nodata = "";
            if (Request.Files["ad1file"].ContentLength > 0)
            {
                string relativePath = "~/Upload/" + Path.GetFileName(Request.Files["ad1file"].FileName);
                string physicalPath = Server.MapPath(relativePath);

                Request.Files["ad1file"].SaveAs(physicalPath);

                fd = new FormData
                {
                    ad1file = relativePath
                    
                };
                nodata = physicalPath;
            }
            if (fd != null)
            {
                        var psi = new ProcessStartInfo();
                        psi.FileName = @"C:\Users\anant\AppData\Local\Programs\Python\Python37-32\python.exe";
                        
                        var script = @"C:\Users\anant\OneDrive\Desktop\Hamro dokan\Hamro dokan\ImageClassificationWithRandomForest1\main.py";
                        psi.Arguments = $"\"{script}\" \"{nodata}\"";
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;
                        psi.RedirectStandardOutput = true;
                        psi.RedirectStandardError = true;
                        var errors = "";

                        using (var process = Process.Start(psi))
                        {
                            errors = process.StandardError.ReadToEnd();
                            result = process.StandardOutput.ReadToEnd();
                            
                        }
                ViewBag.ss= errors;
            }
            string item = "";
            string color = "";
            var variable =result.Split();
            color = variable[0];
            item = variable[1];
            return View(_db.tblProducts.Where(x =>( x.tblCategory.CategoryName.Contains(item) /*&& x.Color==color*/)|| item == null).ToList());

        }
    }
}