using Common;
using CRM.Application.SecurityApplication.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.SecurityApplication.Queries.FindById
{
    class FindPersonByIdQuery : IRequest<OperationResult<StaffSelectViewModel>>
    {
    }
}
