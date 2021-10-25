using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services.Services.Dtos
{
    public class LoginDto
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public int Expiration { get; set; }
    }
}
