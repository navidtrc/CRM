using Common.Exceptions;
using CRM.Domain.Common.Enums;
using CRM.Domain.Models.Core;
using System;

namespace CRM.Domain.Models.Security
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public eGender Gender { get; set; }
        public string NationalCode { get; private set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] Avatar { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        public void UpdateNationalCode(string nationalCode)
        {
            int code;
            bool isValid = int.TryParse(nationalCode, out code);
            if (isValid && nationalCode.Length == 10)
                NationalCode = nationalCode;
            else
                throw new LogicException($"{nationalCode} is not a valid national code");
        }
    }
}
