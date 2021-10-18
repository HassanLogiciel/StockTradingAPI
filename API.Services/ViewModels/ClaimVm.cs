using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ViewModels
{
    public class ClaimVm
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Type { get; set; }
       
        [Required]
        public string Value { get; set; }
    }
}
