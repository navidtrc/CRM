using CRM.Application.SecurityApplication.Commands.Update;
using CRM.Application.WebFramework.Api;
using CRM.Domain.Common.Enums;
using System;

namespace CRM.Application.SecurityApplication.Models
{
    public class StaffUpdateViewModel : BaseViewModel<StaffUpdateViewModel, StaffUpdateCommand>
    {
        public string EncryptedId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public eGender Gender { get; set; }
        public string NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid? UserId { get; set; }
    }
}
