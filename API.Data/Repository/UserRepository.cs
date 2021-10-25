using API.Data.Data;
using API.Data.Interfaces;
using API.Data.Model;
using API.Data.Specification;
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
        public async Task<ApplicationUser> GetUserAsync(ApplicationUserSpecification specification)
        {
            return await _identityContext.Users.Where(specification.Criteria).FirstOrDefaultAsync();
        }

        public async Task<List<ApplicationUser>> ListUsersAsync(ApplicationUserSpecification specification)
        {
            return await _identityContext.Users.Where(specification.Criteria).ToListAsync();
        }
    }
}
