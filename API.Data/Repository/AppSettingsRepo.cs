using API.Data.Data;
using API.Data.Entities;
using API.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class AppSettingsRepo : IAppSettingsRepo
    {
        private readonly ApplicationContext _applicationContext;

        public AppSettingsRepo(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<AppSetting> GetSettings()
        {
            return await _applicationContext.AppSettings.FirstOrDefaultAsync();
        }
    }
}
