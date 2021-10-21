using API.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        public Task<DbResult> SaveChanges();
    }
}
