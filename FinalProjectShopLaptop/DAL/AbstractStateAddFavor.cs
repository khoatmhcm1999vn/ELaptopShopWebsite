using FinalProjectShopLaptop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.DAL
{
    public abstract class AbstractStateAddFavor
    {

        protected ContextAddFavor _state;

        public void SetState(ContextAddFavor state)
        {
            this._state = state;
        }

        public abstract void Favor(int productID, string userID);

    }

}