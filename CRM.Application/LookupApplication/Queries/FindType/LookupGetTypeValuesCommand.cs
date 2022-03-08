using Common;
using CRM.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.LookupApplication.Queries.FindType
{
    public class LookupGetTypeValuesCommand : IRequest<OperationResult<List<Lookup>>>
    {
        public List<string> Types { get; set; }
    }
}
