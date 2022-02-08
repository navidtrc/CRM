using AutoMapper;
using CRM.Application.WebFramework.Api;
using CRM.Application.WebFramework.Filters;
using CRM.Domain.Common.Enums;
using CRM.Domain.Models.Security;
using System;

namespace CRM.Application.SecurityApplication.Models
{
    public class StaffSelectViewModel : BaseViewModel<StaffSelectViewModel, Staff>
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public eGender Gender { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] Avatar { get; set; }
        public ePersonType ePersonType { get; set; }
        public string StaffCode { get; set; }

        //public User User { get; set; }
        //public Guid? UserId { get; set; }
        public override void CustomMappings(IMappingExpression<Staff, StaffSelectViewModel> mapping)
        {
            mapping.ForMember(
                    dest => dest.FullName,
                    config => config.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
