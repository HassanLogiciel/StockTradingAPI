using API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Interfaces
{
    public interface IAppSettingsRepo  
    {
        public Task<AppSetting> GetSettings();
    }
}
