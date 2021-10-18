using API.Common;
using API.Services.Services.Dtos;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services.Interfaces
{
    public interface IClaimService
    {
        public Task<Response> AddClaim(ClaimVm model);
    }
}
