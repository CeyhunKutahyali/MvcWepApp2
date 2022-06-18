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
    public class DersProgrami : ModelBase
    {

        SqlDataProcess data;
        public DersProgrami()
        {
            data = new SqlDataProcess();
        }

        
        public string DersAdi { get; set; }
        public string SinifAdi { get; set; }
        public string EgitmenKodu { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public int SinifId { get; set; }
        

        public List<DersProgrami> ListeGetir()
        {
            DataTable dt = data.GetExecuteDataTable("DersProgrami_Getir", CommandType.StoredProcedure, new SqlParameter("@Kodu",EgitmenKodu));

            List<DersProgrami> dersProgrami = new List<DersProgrami>();
            foreach(DataRow dr in dt.Rows)
            {
                dersProgrami.Add(new DersProgrami
                {
                    Id= dr.Field<int>("Id"),
                    SinifId= dr.Field<int>("SinifId"),
                    DersAdi = dr.Field<string>("DersAdi"),
                    SinifAdi = dr.Field<string>("SinifAdi"),
                    Baslangic = dr.Field<DateTime>("Baslangic"),
                    Bitis = dr.Field<DateTime>("Bitis")
                }) ;
            }
            return dersProgrami;
        }

        
    }
}