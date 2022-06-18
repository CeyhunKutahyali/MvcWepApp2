using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWepApp2.Models
{
    public class OgrenciDTO
    {
        public List<Ogrenci> Ogrenciler { get; set; }
        public List<Sinif> Siniflar { get; set; }
    }
}