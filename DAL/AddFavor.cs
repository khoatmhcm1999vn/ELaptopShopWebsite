using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.DAL
{
    public class AddFavor : AbstractStateAddFavor
    {

        public override void Favor(int productID, string userID)
        {
            SanPhamDAL.sanPhamDAL.addFavouriteSanPham(productID, userID);
        }

    }

}