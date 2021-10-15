using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        List<T> GetAll();
    }
}
