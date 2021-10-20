using API.Services.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data.Entities
{
    public class Currency : EntityBase
    {
        public string Country { get; set; }
        public string CurrencyCode { get; set; }
    }
}
