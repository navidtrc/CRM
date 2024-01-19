using Azure.Core;
using CRM.Common.Api;
using CRM.Common.Resources.StringResources;
using CRM.Common.Utilities;
using CRM.Entities.DataModels.Security;
using CRM.Repository.Core;
using CRM.Service.Authentication;
using CRM.Service.Email;
using CRM.Service.Sms;
using CRM.ViewModels.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtService jwtService;
        private readonly ISmsService smsService;
        private readonly IEmailService emailService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IUnitOfWork uow, IJwtService jwtService, ISmsService smsService, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            this._uow = uow;
            this.jwtService = jwtService;
            this.smsService = smsService;
            this.emailService = emailService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public UserInfoViewModel GetCurrentUser()
        {
            var userId = Guid.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var query = from user in _uow.Users.TableNoTracking
                        join person in _uow.People.TableNoTracking
                        on user.PersonId equals person.Id
                        where user.Id == userId
                        select new UserInfoViewModel
                        {
                            FullName = person != null ? person.FullName : string.Empty,
                            Id = user.Id,
                            Username = user.UserName
                        };
            return query.FirstOrDefault();
        }

        public DataSourceResult GetAllUsers_DataSourceResult(DataSourceRequest request)
        {
            var query = from users in _uow.Users.TableNoTracking
                        join person in _uow.People.TableNoTracking
                        on users.PersonId equals person.Id into userProfiles
                        from allUserProfiles in userProfiles.DefaultIfEmpty()
                        select new
                        {
                            Id = users.Id,
                            FirstName = allUserProfiles.FirstName,
                            LastName = allUserProfiles.LastName,
                            Avatar = allUserProfiles.Avatar,
                            BirthDate = allUserProfiles.BirthDate,
                            CreatedDate = allUserProfiles.CreatedDate,
                            Gender = allUserProfiles.Gender,
                            Mobile = users.PhoneNumber,
                            Lockout = users.LockoutEnabled,
                        };
            var result = query.ToDataSourceResult(request);
            return result;
        }
        public async Task<ResultContent<string>> Login(LoginViewModel loginViewModel, CancellationToken cancellationToken)
        {
            var result = await _uow.Users.GetByPhoneAndPass(loginViewModel.PhoneNumber, loginViewModel.Password, cancellationToken);
            if (result.IsSuccess)
                return new ResultContent<string>(true, await jwtService.Generate(result.Data));
            return new ResultContent<string>(result.IsSuccess, null, result.Message);
        }

        //public async Task<ResultContent> Register(RegisterViewModel registerViewModel, CancellationToken cancellationToken)
        //{
        //    if (registerViewModel.User != null)
        //    {
        //        if (registerViewModel.User.Password != registerViewModel.User.ConfirmPassword)
        //            return new ResultContent(false, Resource.ResourceManager.GetString("PasswordNotMatch"));

        //        if (await _uow.Users.TableNoTracking.FirstOrDefaultAsync(w => w.UserName == registerViewModel.User.UserName, cancellationToken) != null)
        //            return new ResultContent(false, Resource.ResourceManager.GetString("TryAnotherUserName"));
        //    }

        //    Entities.DataModels.Security.User user = new Entities.DataModels.Security.User();
        //    if (registerViewModel.User != null)
        //    {
        //        user = new Entities.DataModels.Security.User()
        //        {
        //            UserName = registerViewModel.User.UserName,
        //            Email = registerViewModel.Person.Email,
        //            PhoneNumber = registerViewModel.User.PhoneNumber,
        //        };
        //        await _uow.Users.AddAsync(user, registerViewModel.User.Password, cancellationToken);
        //    }

        //    var person = new Person
        //    {
        //        BirthDate = registerViewModel.Person.BirthDate,
        //        ePersonType = registerViewModel.Person.ePersonType,
        //        FirstName = registerViewModel.Person.FirstName,
        //        LastName = registerViewModel.Person.LastName,
        //        Gender = registerViewModel.Person.Gender,
        //        NationalCode = registerViewModel.Person.NationalCode,
        //        User = registerViewModel.User != null ? user : null
        //    };
        //    if (person.ePersonType == Common.Enums.ePersonType.Staff)
        //    {
        //        var lastStaff = await _uow.Staffs.TableNoTracking.OrderByDescending(d => d.StaffCode).FirstOrDefaultAsync();
        //        var lastCode = lastStaff == null ? 0 : lastStaff.StaffCode++;
        //        person.Staff = new Entities.DataModels.General.Staff
        //        {
        //            StaffCode = lastCode
        //        };
        //    }
        //    else if (person.ePersonType == Common.Enums.ePersonType.Customer)
        //    {
        //        var lastCustomer = await _uow.Customers.TableNoTracking.OrderByDescending(d => d.CustomerCode).FirstOrDefaultAsync();
        //        var lastCode = lastCustomer == null ? 0 : lastCustomer.CustomerCode++;
        //        person.Customer = new Entities.DataModels.General.Customer
        //        {
        //            CustomerCode = lastCode
        //        };
        //    }
        //    await _uow.People.AddAsync(person, cancellationToken);
        //    await _uow.CompleteAsync(cancellationToken);
        //    return new ResultContent(true, Resource.ResourceManager.GetString("UserCreated"));
        //}

        public async Task<ResultContent> Logout(Guid id, CancellationToken cancellationToken)
        {
            await _uow.Users.UpdateSecuirtyStampAsync(await _uow.Users.GetByIdAsync(cancellationToken, id), cancellationToken);
            return new ResultContent(true, Resource.ResourceManager.GetString("LogOutSuccessfully"));
        }

        public async Task<ResultContent> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel, HttpRequest request, CancellationToken cancellationToken)
        {
            var user = await _uow.Users.TableNoTracking.FirstOrDefaultAsync(f => f.PhoneNumber == forgetPasswordViewModel.Phone);
            if (user == null)
                return new ResultContent(false, Resource.ResourceManager.GetString("UserNotFound"));
            string link = $"{request.HttpContext.Request.Scheme}://{request.HttpContext.Request.Host.Value}/forgetpass/{Guid.NewGuid()}";

            switch (forgetPasswordViewModel.ForgetType)
            {
                case Common.eForgetPasswordVia.EMAIL:
                    await emailService.SendAsync(user.Email, "Reset Password", link, cancellationToken);
                    break;
                case Common.eForgetPasswordVia.SMS:
                    await smsService.SendSmsAsync(user.PhoneNumber, link, cancellationToken);
                    break;
                default:
                    break;
            }
            return new ResultContent(true, Resource.ResourceManager.GetString("ForgetLinkSent"));
        }

        public async Task<ResultContent> ForgetPasswordConfirm(ForgetPasswordConfirmViewModel forgetPasswordConfirmViewModel, CancellationToken cancellationToken)
        {
            if (forgetPasswordConfirmViewModel.Password != forgetPasswordConfirmViewModel.ConfirmPassword)
                return new ResultContent(false, Resource.ResourceManager.GetString("PasswordNotMatch"));

            var user = await _uow.Users.GetByIdAsync(cancellationToken, Guid.Parse(forgetPasswordConfirmViewModel.Id));
            user.PasswordHash = SecurityHelper.GetSha256Hash(forgetPasswordConfirmViewModel.Password);
            await _uow.Users.UpdateAsync(user, cancellationToken, true);
            return new ResultContent(true, Resource.ResourceManager.GetString("PasswordChanged"));
        }

        public async Task<ResultContent> Lockout(UserLockoutViewModel userLockoutViewModel, CancellationToken cancellationToken)
        {
            var user = await _uow.Users.GetByIdAsync(cancellationToken, Guid.Parse(userLockoutViewModel.UserId));
            user.LockoutEnabled = userLockoutViewModel.LockoutEnabled;
            user.LockoutEnd = user.LockoutEnabled == false ? null : DateTimeOffset.MaxValue;
            await _uow.Users.UpdateAsync(user, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            string message = userLockoutViewModel.LockoutEnabled ? "User Locked" : "User Unlocked";
            return new ResultContent(true, Resource.ResourceManager.GetString(message));
        }

        public async Task<ResultContent> SendCode(SendCodeViewModel sendCodeViewModel, CancellationToken cancellationToken)
        {
            var user = await _uow.Users.GetByIdAsync(cancellationToken, Guid.Parse(sendCodeViewModel.Id));

            Random random = new Random();
            user.TempCode = random.Next(1000, 10000).ToString();
            user.ExpireTempCodeTime = DateTime.Now.AddMinutes(5);
            await _uow.Users.UpdateAsync(user, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            //switch (sendCodeViewModel.ContactType)
            //{
            //    case Common.eForgetPasswordVia.EMAIL:
            //        await emailService.SendAsync(user.Email, $"کد تایید - {user.TempCode}", $"کد تایید - {user.TempCode}", cancellationToken);
            //        break;
            //    case Common.eForgetPasswordVia.SMS:
            //        await smsService.SendSmsAsync(user.PhoneNumber, $"کد تایید - {user.TempCode}", cancellationToken);
            //        break;
            //    default:
            //        break;
            //}
            return new ResultContent(true);
        }
        
        public async Task<ResultContent> Confirmation(ConfirmCodeViewModel confirmCodeViewModel, CancellationToken cancellationToken)
        {
            var user = await _uow.Users.GetByIdAsync(cancellationToken, Guid.Parse(confirmCodeViewModel.Id));
            var isCorrect = confirmCodeViewModel.Code.ToString() == user.TempCode
                && user.ExpireTempCodeTime > DateTime.Now;

            if (!isCorrect)
                return new ResultContent(false, "دوباره تلاش کنید.");

            switch (confirmCodeViewModel.ContactType)
            {
                case Common.eForgetPasswordVia.EMAIL:
                    user.EmailConfirmed = true;
                    break;
                case Common.eForgetPasswordVia.SMS:
                    user.PhoneNumberConfirmed = true;
                    break;
            }

            user.TempCode = null;
            user.ExpireTempCodeTime = DateTime.MinValue;
            await _uow.Users.UpdateAsync(user, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return new ResultContent(true);
        }

        public async Task<ResultContent> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var user = await _uow.Users.GetByIdAsync(cancellationToken, id);
            await _uow.Users.DeleteAsync(user, cancellationToken);
            return new ResultContent(true, Resource.ResourceManager.GetString("UserDeleted"));
        }
        public async Task<ResultContent> DeletePerson(long id, CancellationToken cancellationToken)
        {
            var person = await _uow.People.GetByIdAsync(cancellationToken, id);
            await _uow.People.DeleteAsync(person, cancellationToken);
            if (person.ePersonType == Common.Enums.ePersonType.Staff)
            {
                var staff = await _uow.Staffs.GetByIdAsync(cancellationToken, person.Id);
                await _uow.Staffs.DeleteAsync(staff, cancellationToken);
            }
            else
            {
                var customer = await _uow.Customers.GetByIdAsync(cancellationToken, person.Id);
                await _uow.Customers.DeleteAsync(customer, cancellationToken);
            }
            await _uow.CompleteAsync(cancellationToken);
            return new ResultContent(true, Resource.ResourceManager.GetString("UserDeleted"));
        }

    }
}
