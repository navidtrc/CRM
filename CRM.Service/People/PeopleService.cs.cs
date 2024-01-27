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
using CRM.Entities.DataModels.Security;
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
            // چک کردن یکی بودن پسورد و تکرار آن
            if (!string.IsNullOrEmpty(registerViewModel.User.Password) && registerViewModel.User.Password != registerViewModel.User.ConfirmPassword)
                return new ResultContent(false, Resource.ResourceManager.GetString("PasswordNotMatch"));

            // چک کردن تکراری نبودن شماره تماس
            if (await _uow.Users.TableNoTracking.FirstOrDefaultAsync(w => w.PhoneNumber == registerViewModel.User.PhoneNumber, cancellationToken) != null)
                return new ResultContent(false, Resource.ResourceManager.GetString("DuplicatePhone"));

            // چک کردن تکراری نبودن ایمیل
            if (await _uow.Users.TableNoTracking.FirstOrDefaultAsync(w => w.Email == registerViewModel.User.Email, cancellationToken) != null)
                return new ResultContent(false, Resource.ResourceManager.GetString("DuplicateEmail"));

            Entities.DataModels.Security.User user = new Entities.DataModels.Security.User
            {
                UserName = registerViewModel.User.PhoneNumber,
                Email = registerViewModel.User?.Email,
                PhoneNumber = registerViewModel.User.PhoneNumber,
            };

            // وقتی کاربر مشتری میباشد و ما برای مشتری پسورد در نظر نگرفتیم. چون مشتری اطلاعات لاگین به سیستم لازم نداره
            // معمولا هنگام ایجاد تیکت جدید وقتی میخواییم مشخصات مشتری رو وارد کنیم
            if (registerViewModel.Person.ePersonType == ePersonType.Customer && string.IsNullOrEmpty(registerViewModel.User.Password))
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTime.MaxValue;
                user.PasswordHash = $"!@#{user.PhoneNumber}$%^";
            }
            await _uow.Users.AddAsync(user, registerViewModel.User.Password, cancellationToken);

            var person = new Person
            {
                BirthDate = registerViewModel.Person.BirthDate,
                PersonType = registerViewModel.Person.ePersonType,
                FirstName = registerViewModel.Person.FirstName,
                LastName = registerViewModel.Person.LastName,
                Gender = registerViewModel.Person.Gender,
                User = user
            };
            if (person.PersonType == ePersonType.Staff)
            {
                var lastStaff = await _uow.Staffs.TableNoTracking.OrderByDescending(d => d.StaffCode).FirstOrDefaultAsync();
                var lastCode = lastStaff == null ? 1 : lastStaff.StaffCode++;
                person.Staff = new Entities.DataModels.Security.Staff
                {
                    StaffCode = lastCode
                };
            }
            else if (person.PersonType == ePersonType.Customer)
            {
                var lastCustomer = await _uow.Customers.TableNoTracking.OrderByDescending(d => d.CustomerCode).FirstOrDefaultAsync();
                var lastCode = lastCustomer == null ? 1 : lastCustomer.CustomerCode++;
                person.Customer = new Entities.DataModels.Security.Customer
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

            if (person.User.Email != registerViewModel.User.Email)
                person.User.EmailConfirmed = false;
            person.User.Email = registerViewModel.User.Email;

            if (person.User.PhoneNumber != registerViewModel.User.PhoneNumber)
                person.User.PhoneNumberConfirmed = false;
            person.User.PhoneNumber = registerViewModel.User.PhoneNumber;
            person.User.UserName = registerViewModel.User.PhoneNumber;

            await _uow.People.UpdateAsync(person, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return new ResultContent(true);
        }
    }
}