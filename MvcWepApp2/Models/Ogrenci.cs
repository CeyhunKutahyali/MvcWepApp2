using MvcWepApp2.DAL;
using MvcWepApp2.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace MvcWepApp2.Models
{
    public class Ogrenci : ModelBase
    {
        SqlDataProcess data;
        public Ogrenci()
        {
            data = new SqlDataProcess();
        }

        
        public string AdSoyad { get; set; }
        public string Numara { get; set; }
        public Sinif _Sinif { get; set; }
        public int Deleted { get; set; }
        public int SinifId { get; set; }


        public List<Ogrenci> OgrencileriGetir()
        {
            List<Ogrenci> ogrenciler = new List<Ogrenci>();


            DataTable dtogrenciler = data.GetExecuteDataTable("Ogrenci_ListeGetir", CommandType.StoredProcedure);


            foreach (DataRow dataRow in dtogrenciler.Rows)
            {
                ogrenciler.Add(new Ogrenci
                {
                    Id = Convert.ToInt32(dataRow["Id"]),
                    AdSoyad = dataRow["AdSoyad"].ToString(),
                    Numara = dataRow["Numara"].ToString(),
                    _Sinif = new Sinif
                    {
                        SinifAdi = dataRow["SinifAdi"].ToString(),
                        SinifKodu = dataRow["SinifKodu"].ToString()
                    }

                });
            }
            return ogrenciler;
        }

        public List<Ogrenci> OgrenciListesiGetirSinifId()
        {
            List<Ogrenci> ogrenciler = new List<Ogrenci>();


            DataTable dtogrenciler = data.GetExecuteDataTable("Ogrenci_ListeGetir_SinifId", CommandType.StoredProcedure, new SqlParameter("@SinifId",SinifId));


            foreach (DataRow dataRow in dtogrenciler.Rows)
            {
                ogrenciler.Add(new Ogrenci
                {
                    Id = Convert.ToInt32(dataRow["Id"]),
                    AdSoyad = dataRow["AdSoyad"].ToString(),
                    Numara = dataRow["Numara"].ToString()
                });
            }
            return ogrenciler;
        }
        public Ogrenci OgrenciGetirId()
        {
            DataTable dt = data.GetExecuteDataTable("Get_Student", CommandType.StoredProcedure, new SqlParameter("@Id", Id));
            Ogrenci _ogrenci = new Ogrenci();
            if (dt.Rows.Count > 0)
            {
                _ogrenci.Id = dt.Rows[0].Field<int>("Id");
                _ogrenci.AdSoyad = dt.Rows[0].Field<string>("AdSoyad");
                _ogrenci.Numara = dt.Rows[0].Field<string>("Numara");
                _ogrenci._Sinif = new Sinif { SinifAdi = dt.Rows[0].Field<string>("SinifAdi") };
            }
            
            return _ogrenci;

        }
        public bool Ekle()
        {
            try
            {
                return data.SetExecuteNonQuery("Insert_Student",
                    CommandType.StoredProcedure,
                    new SqlParameter("@AdSoyad", AdSoyad),
                    new SqlParameter("@Numara", Numara),
                    new SqlParameter("@SinifId", _Sinif.Id)

                    );
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
                return data.SetExecuteNonQuery("Delete_Student", CommandType.StoredProcedure,
                    new SqlParameter("@Id", Id));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Duzenle()
        {
            return data.SetExecuteNonQuery("Update Ogrenci set SinifId=@sinifid where Id=@id",
                CommandType.Text,
                new SqlParameter("@sinifid",_Sinif.Id),
                new SqlParameter("@id",Id)
                );

        }
    }
}
