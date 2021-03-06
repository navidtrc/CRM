using Common;
using CRM.Domain.Common.Enums;
using MediatR;
using System;

namespace CRM.Application.SecurityApplication.Commands.Create
{
    public class StaffCreateCommand : IRequest<OperationResult>
    {
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public eGender Gender { get; set; }
        public string NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public ePersonType ePersonType { get; set; }
        public Guid? UserId { get; set; }
    }
}
