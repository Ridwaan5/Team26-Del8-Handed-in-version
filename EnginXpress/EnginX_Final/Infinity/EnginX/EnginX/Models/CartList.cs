using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnginX.Models
{
    public class CartList
    {
        public int ID{ get; set; }

        public int PtID { get; set; }

        public decimal vatAmount { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        
        public string Description { get; set; }
        public int Quntity { get; set; }
        public decimal Price { get; set; }
    }
}