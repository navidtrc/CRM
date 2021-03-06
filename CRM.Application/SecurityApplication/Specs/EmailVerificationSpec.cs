using Common.Utilities;
using CRM.Domain.Models.Security;
using EmailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CRM.Application.SecurityApplication.Specs
{
    public class EmailVerificationSpec : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            Func<User, bool> permitted = user =>
            {
                if (user == null)
                    return false;
                if (!user.EmailConfirmed)
                    return false;
                return true;
            };
            return x => permitted(x);
        }
    }
}
