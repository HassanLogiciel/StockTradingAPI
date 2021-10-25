using API.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Services.ViewModels
{
    public class TransactionStatusVm
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string TransactionId { get; set; }
        [Required]
        public int StatusId { get; set; }
        public Status Status
        {
            get
            {
                if (StatusId == 0)
                {
                    return Status.Approved;
                }
                else if(StatusId == 1)
                {
                    return Status.Pending;
                }
                else if (StatusId == 2)
                {
                    return Status.Rejected;
                }
                else
                {
                    return Status.Invalid;
                }
            }
        }
    }
}
