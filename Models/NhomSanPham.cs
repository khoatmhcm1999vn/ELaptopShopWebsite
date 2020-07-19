using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.Models
{
    public class NhomSanPham
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public ICollection<SanPham> SanPhams { get; set; }
    }
}