using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task Create(T model);
        public void Update(T model);
        public void Delete(T model);
    }
}
