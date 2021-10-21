using API.Data.Data;
using API.Data.Interfaces;
using API.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityContext _identityContext;

        public UserRepository(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _identityContext.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _identityContext.Users.Where(c=>c.Id == id).FirstOrDefaultAsync();
        }
    }
}
