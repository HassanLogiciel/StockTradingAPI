using API.Services.Services.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace API.Data.Entities
{
    public class StatusDb : EntityBase
    {
        public Status Status { get; set; }
        public string Name { get; set; }
    }
}
