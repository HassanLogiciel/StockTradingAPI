using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T> GetById(string id);
    }
}
