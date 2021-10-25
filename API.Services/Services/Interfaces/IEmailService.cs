using API.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<Response> SendEmail(string sendTo, string sendFrom, string body, string subject);
    }
}
