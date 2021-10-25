using API.Data.Entities;
using API.Data.Interfaces;
using API.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace API.Data.Specification
{
    public class ApplicationUserSpecification : Specification<ApplicationUser>
    {
        public new Expression<Func<ApplicationUser, bool>> Criteria { get; } = c => true;
        public ApplicationUserSpecification(Expression<Func<ApplicationUser, bool>> expression) => Criteria = expression;
        public static ApplicationUserSpecification ById(string id)
        {
            return new ApplicationUserSpecification(c => c.Id == id);
        }

        public static ApplicationUserSpecification All()
        {
            return new ApplicationUserSpecification(c => true);
        }
    }
}
