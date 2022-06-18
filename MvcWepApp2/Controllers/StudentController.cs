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
    public class StudentController : Controller
    {
        //public StudentController()
        //{
        //    if (string.IsNullOrEmpty(Session["Kullanici"].ToString()))
        //    {
        //        Response.Redirect("Home/Index");
        //    }
        //}
        // GET: Student
        public ActionResult Index()
        {
            //TODO : Session kontrolü eklenecek...
            Ogrenci ogrenci = new Ogrenci();
            Sinif sinif = new Sinif();
            OgrenciDTO ogrenciDTO = new OgrenciDTO();
            ogrenciDTO.Ogrenciler = ogrenci.OgrencileriGetir();
            ogrenciDTO.Siniflar = sinif.ListeGetir();
            return View(ogrenciDTO);
        }

        [HttpPost]
        public ActionResult Ekle(string AdSoyad, string Numara, int SinifId)
        {
            Ogrenci ogrenci = new Ogrenci();
            ogrenci.AdSoyad = AdSoyad;
            ogrenci.Numara = Numara;
            ogrenci._Sinif = new Sinif { Id = SinifId };
            


            if (ogrenci.Ekle())
            {
                return RedirectToAction("Index", "Student");
            }
            else
            {
                return RedirectToAction("Index", "Student");
            }
        }
        [HttpPost]
        public bool Sil(int Id)
        {
            Ogrenci ogrenci = new Ogrenci();
            ogrenci.Id = Id;
            return ogrenci.Sil();
        }
    }
}