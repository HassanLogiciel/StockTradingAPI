using API.Services.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data.Entities
{
    public class Stock : EntityBase
    {
        public int Company { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
