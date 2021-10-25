using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services.Services.Dtos
{
    public class WalletEventDto
    {
        public string EventType { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
