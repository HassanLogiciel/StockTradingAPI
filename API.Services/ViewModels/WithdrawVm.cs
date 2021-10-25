using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Services.ViewModels
{
    public class WithdrawVm
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string WalletId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*(\.[0-9]{1,2})?$", ErrorMessage = "Please Enter the valid amount.")]
        public float Amount { get; set; }
    }
}
