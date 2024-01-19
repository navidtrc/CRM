using CRM.Common.Api;
using CRM.Common.Resources.StringResources;
using CRM.Entities.DataModels.Security;
using CRM.Repository.Core;
using CRM.ViewModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CRM.Common.Enums;
using CRM.Entities.DataModels.General;
using System;

namespace CRM.Service.People
{
    public class PeopleService : IPeopleService
    {
        private readonly IUnitOfWork _uow;
        public PeopleService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public async Task<ResultContent<DataSourceResult>> GetAsync(DataSourceRequest request, ePersonType personType, CancellationToken cancellationToken)
        {
            if (personType == ePersonType.Staff)
            {
                var result = await _uow.Staffs.TableNoTracking
                        .Where(w => w.IsDeleted == false)
                        .Include(i => i.Person.User)
                        .ToDataSourceResultAsync(request, cancellationToken);
                return new ResultContent<DataSourceResult>(true, result);
            }
            else
            {
                var result = await _uow.Customers.TableNoTracking
                        .Where(w => w.IsDeleted == false)
                        .Include(i => i.Person.User)
                        .ToDataSourceResultAsync(request, cancellationToken);
                return new ResultContent<DataSourceResult>(true, result);
            }
        }
        public async Task<ResultContent> DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var person = await _uow.People.GetByIdAsync(cancellationToken, id);
            if (person != null)
            {
                await _uow.People.DeleteAsync(person, cancellationToken);
                await _uow.CompleteAsync(cancellationToken);
            }
            return new ResultContent(true);
        }
        public async Task<ResultContent> Create(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken)
        {
            Entities.DataModels.Security.User user = null;
            if (registerViewModel.User != null)
            {
                user = new Entities.DataModels.Security.User();
                if (registerViewModel.User.Password != registerViewModel.User.ConfirmPassword)
                    return new ResultContent(false, Resource.ResourceManager.GetString("PasswordNotMatch"));

                if (await _uow.Users.TableNoTracking.FirstOrDefaultAsync(w => w.UserName == registerViewModel.User.UserName, cancellationToken) != null)
                    return new ResultContent(false, Resource.ResourceManager.GetString("TryAnotherUserName"));

                user = new Entities.DataModels.Security.User()
                {
                    UserName = registerViewModel.User.UserName,
                    Email = registerViewModel.User.Email,
                    PhoneNumber = registerViewModel.User.PhoneNumber,
                };
                await _uow.Users.AddAsync(user, registerViewModel.User.Password, cancellationToken);
            }

            var person = new Person
            {
                BirthDate = registerViewModel.Person.BirthDate,
                ePersonType = registerViewModel.Person.ePersonType,
                FirstName = registerViewModel.Person.FirstName,
                LastName = registerViewModel.Person.LastName,
                Gender = registerViewModel.Person.Gender,
                NationalCode = registerViewModel.Person.NationalCode,
                User = user
            };
            if (person.ePersonType == ePersonType.Staff)
            {
                var lastStaff = await _uow.Staffs.TableNoTracking.OrderByDescending(d => d.StaffCode).FirstOrDefaultAsync();
                var lastCode = lastStaff == null ? 1 : lastStaff.StaffCode++;
                person.Staff = new Entities.DataModels.General.Staff
                {
                    StaffCode = lastCode
                };
            }
            else if (person.ePersonType == ePersonType.Customer)
            {
                var lastCustomer = await _uow.Customers.TableNoTracking.OrderByDescending(d => d.CustomerCode).FirstOrDefaultAsync();
                var lastCode = lastCustomer == null ? 1 : lastCustomer.CustomerCode++;
                person.Customer = new Entities.DataModels.General.Customer
                {
                    CustomerCode = lastCode
                };
            }
            await _uow.People.AddAsync(person, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return new ResultContent(true, Resource.ResourceManager.GetString("UserCreated"));
        }

        public async Task<ResultContent> Put(PersonUser_AddEdit_ViewModel registerViewModel, CancellationToken cancellationToken)
        {
            var person = await _uow.People.Table
                .Include(i => i.User)
                .Include(i => i.Staff)
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(i => i.Id == registerViewModel.Person.Id, cancellationToken);

            var (FirstName, LastName, BirthDate, Gender) = registerViewModel.Person;
            
            person.FirstName = FirstName;
            person.LastName = LastName;
            person.BirthDate = BirthDate;
            person.Gender = Gender;

            if (person.User != null)
            {
                // FOR CUSTOMER: If user is not null, it means user for customer is modified or its created
                // Implimentation must change in future as soon as posible ...
                if (person.User.Email != registerViewModel.User.Email)
                    person.User.EmailConfirmed= false;
                person.User.Email = registerViewModel.User.Email;

                if (person.User.PhoneNumber != registerViewModel.User.PhoneNumber)
                    person.User.PhoneNumberConfirmed = false;
                person.User.PhoneNumber = registerViewModel.User.PhoneNumber;
            }

            await _uow.People.UpdateAsync(person, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return new ResultContent(true);
        }
    }
}