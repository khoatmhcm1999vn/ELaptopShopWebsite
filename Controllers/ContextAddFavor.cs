using FinalProjectShopLaptop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectShopLaptop.Controllers
{
    public class ContextAddFavor
    {

        protected AbstractStateAddFavor _addFavor = null;

        public ContextAddFavor(AbstractStateAddFavor addFavor)
        {
            this.TransitionTo(addFavor);
        }

        public void TransitionTo(AbstractStateAddFavor favor)
        {
            this._addFavor = favor;
        }

        public void Favor(int productID, string userID)
        {
            this._addFavor.Favor(productID, userID);
        }

    }
}