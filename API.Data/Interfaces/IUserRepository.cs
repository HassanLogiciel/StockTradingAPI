using API.Data.Model;
using API.Data.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Interfaces
{
    public interface IUserRepository 
    {
        Task<List<ApplicationUser>> ListUsersAsync(ApplicationUserSpecification specification);
        Task<ApplicationUser> GetUserAsync(ApplicationUserSpecification specification);
    }
}
