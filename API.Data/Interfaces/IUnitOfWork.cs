using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Model
{
    public interface IUnitOfWork : IDisposable
    {
        public Task SaveChanges();
    }
}
