using CRM.Repository.Core;
namespace CRM.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;
        public CustomerService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        
    }
}