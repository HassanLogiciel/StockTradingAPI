using API.Data.Data;
using API.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _applicationContext;

        public Repository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task Create(T model)
        {
            await _applicationContext.AddAsync<T>(model);
        }

        public void Delete(T model)
        {
            _applicationContext.Remove<T>(model);
        }
        public void Update(T model)
        {
            _applicationContext.Update<T>(model);
        }
    }
}
