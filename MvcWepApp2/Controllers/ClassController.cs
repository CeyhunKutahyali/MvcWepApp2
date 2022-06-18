using MvcWepApp2.Core;
using MvcWepApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWepApp2.Controllers
{
    [SessionControl]
    public class ClassController : Controller
    {

        //public ClassController()
        //{
        //    if (string.IsNullOrEmpty(Session["Kullanici"].ToString()))
        //    {
        //        Response.Redirect("Home/Index");
        //    }
        //}
        // GET: Class
        public ActionResult Index()
        {
           
            Sinif sinif = new Sinif();
            return View(sinif.ListeGetir());
        }

        [HttpPost]
        public ActionResult Ekle(string sinifAdi, string sinifKodu)
        {
            Sinif sinif = new Sinif
            {
                SinifAdi = sinifAdi,
                SinifKodu = sinifKodu
            };

            

            if (sinif.Ekle())
            {
                return RedirectToAction("Index", "Class");
            }
            else
            {
                return RedirectToAction("Index", "Class");
            }
        }

        [HttpPost]
        public void Sil(int Id) {
            Sinif sinif = new Sinif();
            sinif.Id = Id;
            sinif.Sil();

        }

              
        public ActionResult DersProgrami()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(string dersAdi,DateTime dersBaslangic,DateTime dersBitis, int Id)
        {
            return View();
        }
    }
}