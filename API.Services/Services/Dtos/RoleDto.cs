using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Services.Services.Dtos
{
    public class RoleDto
    {
        [Required]
        public string RoleName { get; set; }
    }
}
