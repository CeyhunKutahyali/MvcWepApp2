using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MvcWepApp2
{
    public static class Sqlbaglanti
    {

        
        public static SqlConnection Baglantial() //Sql Bağlantı Parm.
        {
            SqlConnection baglanti = new SqlConnection("Data Source=BLGA02; Initial Catalog = YonetimPaneli; Integrated Security=True");
            return baglanti; //Bağlantı döndür.
        }
       

    }
}