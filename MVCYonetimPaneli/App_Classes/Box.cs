using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCYonetimPaneli.App_Classes
{
    using Models;
    public class Box
    {
        private List<Urunler> product = new List<Urunler>();

        public List<Urunler> Product
        {
            get { return product; }
            set { product = value; }
        }

    }
}