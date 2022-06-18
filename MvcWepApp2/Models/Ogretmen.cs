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
    public class Ogretmen : ModelBase
    {
        SqlDataProcess data;
        public Ogretmen()
        {
            data=new SqlDataProcess();
        }
        
        public string AdSoyad { get; set; }
        public string Kodu { get; set; }
        public string Sifre { get; set; }
        public Sinif _Sinif { get; set; }

        public List<Ogretmen> ListeGetir()
        {
            List<Ogretmen> ogretmenler = new List<Ogretmen>();


            DataTable dtOgretmenler = data.GetExecuteDataTable("Egitmen_ListeGetir", CommandType.StoredProcedure);


            foreach (DataRow dataRow in dtOgretmenler.Rows)
            {
                ogretmenler.Add(new Ogretmen
                {
                    Id = Convert.ToInt32(dataRow["Id"]),
                    AdSoyad = dataRow["AdSoyad"].ToString(),
                    Kodu = dataRow["Kodu"].ToString(),
                    _Sinif = new Sinif
                    {
                        SinifAdi = dataRow["SinifAdi"].ToString(),
                        SinifKodu = dataRow["SinifKodu"].ToString()
                    }

                });
            }
            return ogretmenler;
        }
        public bool Ekle()
        {
            try
            {
                data.SetExecuteNonQuery("Insert_Teacher",
                    CommandType.StoredProcedure,
                    new SqlParameter("@AdSoyad",AdSoyad),
                    new SqlParameter("@Kodu",Kodu),
                    new SqlParameter("@SinifId",_Sinif.Id)
                    );

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Sil()
        {
            try
            {
                return data.SetExecuteNonQuery("Delete_Teacher", CommandType.StoredProcedure,
                    new SqlParameter("@Id", Id));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Ogretmen OgretmenGetirId()
        {
            DataTable dt = data.GetExecuteDataTable("Get_Teacher", CommandType.StoredProcedure, new SqlParameter("@Id", Id));
            Ogretmen _ogretmen = new Ogretmen();
            if (dt.Rows.Count > 0)
            {
                _ogretmen.Id = dt.Rows[0].Field<int>("Id");
                _ogretmen.AdSoyad = dt.Rows[0].Field<string>("AdSoyad");
                _ogretmen.Kodu = dt.Rows[0].Field<string>("Kodu");
                _ogretmen._Sinif = new Sinif { SinifAdi = dt.Rows[0].Field<string>("SinifAdi") };
            }

            return _ogretmen;

        }
        public bool Duzenle()
        {
            return data.SetExecuteNonQuery("Update_Teacher",
                CommandType.Text,
                new SqlParameter("@SinifId", _Sinif.Id),
                new SqlParameter("@Id", Id)
                );

        }


        public bool GirisYap()
        {

            DataTable dt = data.GetExecuteDataTable("Select * from Egitmen where Kodu=@Kodu and Sifre=@Sifre",
                CommandType.Text,
                new SqlParameter("@Kodu", Kodu),
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
    }
}