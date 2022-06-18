using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWepApp2.Models
{
    public class ProfileDTO
    {
        public Ogrenci _Ogrenci { get; set; }
        public List<Sinif> Siniflar { get; set; }
        
    }
}