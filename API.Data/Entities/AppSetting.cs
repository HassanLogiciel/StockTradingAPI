using API.Services.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data.Entities
{
   public class AppSetting : EntityBase
    {
        public virtual Currency Currency { get; set; }
    }
}
