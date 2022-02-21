using AutoMapper;
using CRM.Application.WebFramework.Api;
using CRM.Domain.Common.Enums;
using CRM.Domain.Models.Security;
using Newtonsoft.Json;
using System;

namespace CRM.Application.InvoiceApplication.ViewModels
{
    public class CustomerViewModel : BaseViewModel<CustomerViewModel, Customer>
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedDatePersian { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastModifiedDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LastModifiedDatePersian { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public eGender Gender { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NationalCode { get; set; }

        public string PhoneNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? BirthDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BirthDatePersian { get; set; }

        public string CustomerCode { get; set; }

        public override void CustomMappings(IMappingExpression<Customer, CustomerViewModel> mapping)
        {
            mapping.ForMember(
                    dest => dest.CreatedDatePersian,
                    config => config.MapFrom(src => $"{src.CreatedDate.ToPersianDateTime()}"));
            mapping.ForMember(
                    dest => dest.LastModifiedDatePersian,
                    config => config.MapFrom(src => src.LastModifiedDate != null ? $"{((DateTime)src.LastModifiedDate).ToPersianDateTime()}" : null));
            mapping.ForMember(
                    dest => dest.BirthDatePersian,
                    config => config.MapFrom(src => src.BirthDate != null ? $"{((DateTime)src.BirthDate).ToPersianDateTime()}" : null));
            mapping.ForMember(
                    dest => dest.FullName,
                    config => config.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
