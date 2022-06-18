using MvcWepApp2.DAL;
using MvcWepApp2.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcWepApp2.Models
{
    public class Kullanici : ModelBase
    {
        SqlDataProcess data;
        public Kullanici()
        {
            data = new SqlDataProcess();
        }

        
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public int Deleted { get; set; }
        public bool GirisYap()
        {

            DataTable dt = data.GetExecuteDataTable("Get_List_User",
                CommandType.StoredProcedure,
                new SqlParameter("@KullaniciAdi", KullaniciAdi),
                new SqlParameter("@Sifre", Sifre));
                

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            else
            {
                return false;

            }


        }
        public List<Kullanici> TumKullanicilariGetir()
        {
            List<Kullanici> kullanicilar = new List<Kullanici>();
            DataTable dtkullanicilistesi = data.GetExecuteDataTable("Get_All_User", CommandType.StoredProcedure);

            foreach (DataRow dataRow in dtkullanicilistesi.Rows)
            {
                kullanicilar.Add(new Kullanici
                {
                    Id = Convert.ToInt32(dataRow["Id"]),
                    KullaniciAdi = dataRow["KullaniciAdi"].ToString(),
                    Sifre = dataRow["Sifre"].ToString(),
                    AdSoyad = dataRow["AdSoyad"].ToString()
                });
            }
            return kullanicilar;
        }
        public bool KullaniciEkle()
        {
            try
            {
                return data.SetExecuteNonQuery("Insert_User",
                    CommandType.Text,
                    new SqlParameter("@KullaniciAdi", KullaniciAdi),
                    new SqlParameter("@sifre", Sifre),
                    new SqlParameter("@AdSoyad", AdSoyad)
                    );
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool KullaniciSil()
        {
            try
            {
                return data.SetExecuteNonQuery("Delete_User",
                    CommandType.StoredProcedure,
                    new SqlParameter("@Id", Id)
                    );
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Kullanici KullaniciDetayGetir()
        {
            Kullanici kullanici = new Kullanici();

            DataTable dtkullanici = data.GetExecuteDataTable("Get_User_Detail",
                CommandType.StoredProcedure,
                new SqlParameter("@Id", Id));

            if (dtkullanici.Rows.Count > 0)
            {
                kullanici.AdSoyad = dtkullanici.Rows[0]["AdSoyad"].ToString();
                kullanici.KullaniciAdi = dtkullanici.Rows[0]["KullaniciAdi"].ToString();
            }
            return kullanici;
        }
        public bool KullaniciGuncelle()
        {
            try
            {
                return data.SetExecuteNonQuery("Update_User",
                    CommandType.StoredProcedure,
                    new SqlParameter("@KullaniciAdi", KullaniciAdi),
                    new SqlParameter("@AdSoyad", AdSoyad),
                    new SqlParameter("@Id", Id)
                    );
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}