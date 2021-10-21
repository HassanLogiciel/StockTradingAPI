﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Services.ViewModels
{
    public class DepositVm
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string WalletId { get; set; }
        [Required]
        public float Amount { get; set; }
    }
}
