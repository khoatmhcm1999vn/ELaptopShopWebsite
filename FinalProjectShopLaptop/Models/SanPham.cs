using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.Models
{
    public class SanPham
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string CPU { get; set; }
        public string Ram { get; set; }
        public string HDD { get; set; }
        public string Card { get; set; }
        public decimal? Price { get; set; }
        public string KichThuocManHinh { get; set; }
        public int SoLuong { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }  
        [Display(Name = "Nhom San Pham")]
        public int NhomSanPhamId { get; set; }
        public virtual NhomSanPham NhomSanPham { get; set; }
        public int HangId { get; set; }
        public virtual Hang Hang { get; set; }
        public SanPham()
        {
            ImagePath = "~/ProductImg/1.jpg";
        }
    }
}