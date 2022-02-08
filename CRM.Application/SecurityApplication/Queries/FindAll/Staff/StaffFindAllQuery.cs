using Kendo.Mvc.UI;
using MediatR;

namespace CRM.Application.SecurityApplication.Queries.FindAll.Staff
{
    public class StaffFindAllQuery : IRequest<DataSourceResult>
    {
        public DataSourceRequest Request { get; set; }
    }
}
