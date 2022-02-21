using AutoMapper;
using Common.Enums;
using CRM.Application.LookupApplication.ViewModels;
using CRM.Application.WebFramework.Api;
using CRM.Domain.Models.Ticket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CRM.Application.InvoiceApplication.ViewModels
{
    public class InvoiceViewModel : BaseViewModel<InvoiceViewModel, Invoice>
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

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public string DatePersian { get; set; }

        public int StateId { get; set; }
        public LookupViewModel State { get; set; }

        public int CustomerId { get; set; }

        public CustomerViewModel Customer { get; set; }

        public ICollection<DeviceViewModel> Devices { get; set; }
        public object this[string propertyName]
        {
            get
            {
                PropertyInfo property = GetType().GetProperty(propertyName);
                return property.GetValue(this, null);
            }
            set
            {
                PropertyInfo property = GetType().GetProperty(propertyName);
                property.SetValue(this, value, null);
            }
        }
        public override void CustomMappings(IMappingExpression<Invoice, InvoiceViewModel> mapping)
        {
            mapping.ForMember(
                   dest => dest.CreatedDatePersian,
                   config => config.MapFrom(src => $"{src.CreatedDate.ToPersianDateTime()}"));
            mapping.ForMember(
                    dest => dest.LastModifiedDatePersian,
                    config => config.MapFrom(src => src.LastModifiedDate != null ? $"{((DateTime)src.LastModifiedDate).ToPersianDateTime()}" : null));
            mapping.ForMember(
                    dest => dest.DatePersian,
                    config => config.MapFrom(src => $"{src.CreatedDate.ToPersianDateTime()}"));
        }
    }
    public class DeviceViewModel : BaseViewModel<DeviceViewModel, Device>
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

        public int DeviceTypeId { get; set; }

        public LookupViewModel DeviceType { get; set; }

        public int DeviceBrandId { get; set; }

        public LookupViewModel DeviceBrand { get; set; }

        public string Model { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Accessories { get; set; }

        public bool ShopWarranty { get; set; }

        public bool RepairWarranty { get; set; }

        public long CustomerPrice { get; set; }

        public long ShopPrice { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceViewModel Invoice { get; set; }
        public int InquiryId { get; set; }
        public InquiryViewModel Inquiry { get; set; }
        public override void CustomMappings(IMappingExpression<Device, DeviceViewModel> mapping)
        {
            mapping.ForMember(
                   dest => dest.CreatedDatePersian,
                   config => config.MapFrom(src => $"{src.CreatedDate.ToPersianDateTime()}"));
            mapping.ForMember(
                    dest => dest.LastModifiedDatePersian,
                    config => config.MapFrom(src => src.LastModifiedDate != null ? $"{((DateTime)src.LastModifiedDate).ToPersianDateTime()}" : null));
        }
    }
    public class InquiryViewModel : BaseViewModel<InquiryViewModel,Inquiry>
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

        public string Reason { get; set; }

        public long Price { get; set; }

        public int DeviceId { get; set; }

        public DeviceViewModel Device { get; set; }

        public ICollection<InquiryCallViewModel> InquiryDates { get; set; }
        public override void CustomMappings(IMappingExpression<Inquiry, InquiryViewModel> mapping)
        {
            mapping.ForMember(
                   dest => dest.CreatedDatePersian,
                   config => config.MapFrom(src => $"{src.CreatedDate.ToPersianDateTime()}"));
            mapping.ForMember(
                    dest => dest.LastModifiedDatePersian,
                    config => config.MapFrom(src => src.LastModifiedDate != null ? $"{((DateTime)src.LastModifiedDate).ToPersianDateTime()}" : null));
        }
    }
    public class InquiryCallViewModel : BaseViewModel<InquiryCallViewModel,InquiryCall>
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

        public DateTime CallDateTime { get; set; }

        public string CallDateTimePersian { get; set; }

        public bool? IsAnswered { get; set; }

        public bool? IsConfirmed { get; set; }

        public int InquiryId { get; set; }

        public InquiryViewModel Inquiry { get; set; }
        public override void CustomMappings(IMappingExpression<InquiryCall, InquiryCallViewModel> mapping)
        {
            mapping.ForMember(
                   dest => dest.CreatedDatePersian,
                   config => config.MapFrom(src => $"{src.CreatedDate.ToPersianDateTime()}"));
            mapping.ForMember(
                    dest => dest.LastModifiedDatePersian,
                    config => config.MapFrom(src => src.LastModifiedDate != null ? $"{((DateTime)src.LastModifiedDate).ToPersianDateTime()}" : null));
            mapping.ForMember(
                   dest => dest.CallDateTimePersian,
                   config => config.MapFrom(src => $"{src.CallDateTime.ToPersianDateTime()}"));
        }

    }
    //public static class InvoiceMapper
    //{
    //    public static InvoiceViewModel ToViewModel(this Invoice invoice)
    //    {
    //        return new InvoiceViewModel
    //        {
    //            Id = invoice.Id,
    //            Guid = invoice.Guid,
    //            CreatedDate = invoice.CreatedDate,
    //            CreatedDatePersian = invoice.CreatedDate.ToPersianDateTime(),
    //            LastModifiedDate = invoice.LastModifiedDate,
    //            LastModifiedDatePersian = invoice.LastModifiedDate != null ? $"{((DateTime)invoice.LastModifiedDate).ToPersianDateTime()}" : null,
    //            Description = invoice.Description,
    //            Number = invoice.Number,
    //            Date = invoice.Date,
    //            DatePersian = invoice.Date.ToPersianDateTime(),
    //            State = new LookupViewModel
    //            {
    //                Id = invoice.State.Id,
    //                Guid = invoice.State.Guid,
    //                CreatedDate = invoice.State.CreatedDate,
    //                CreatedDatePersian = invoice.State.CreatedDate.ToPersianDateTime(),
    //                LastModifiedDate = invoice.State.LastModifiedDate,
    //                LastModifiedDatePersian = invoice.State.LastModifiedDate != null ? $"{((DateTime)invoice.State.LastModifiedDate).ToPersianDateTime()}" : null,
    //                Description = invoice.State.Description,
    //                TypeTitle = invoice.State.TypeTitle,
    //                TypeId = invoice.State.TypeId,
    //                Aux1 = invoice.State.Aux1,
    //                Aux2 = invoice.State.Aux2,
    //                Aux3 = invoice.State.Aux3
    //            },
    //            CustomerId = invoice.CustomerId,
    //            Customer = new CustomerViewModel
    //            {
    //                Id = invoice.Customer.Id,
    //                Guid = invoice.Customer.Guid,
    //                CreatedDate = invoice.Customer.CreatedDate,
    //                CreatedDatePersian = invoice.Customer.CreatedDate.ToPersianDateTime(),
    //                LastModifiedDate = invoice.Customer.LastModifiedDate,
    //                LastModifiedDatePersian = invoice.Customer.LastModifiedDate != null ? $"{((DateTime)invoice.Customer.LastModifiedDate).ToPersianDateTime()}" : null,
    //                Description = invoice.Customer.Description,
    //                BirthDate = invoice.Customer.BirthDate,
    //                BirthDatePersian = invoice.Customer.BirthDate != null ? $"{((DateTime)invoice.Customer.BirthDate).ToPersianDateTime()}" : null,
    //                CustomerCode = invoice.Customer.CustomerCode,
    //                FirstName = invoice.Customer.FirstName,
    //                LastName = invoice.Customer.LastName,
    //                FullName = $"{invoice.Customer.FirstName} {invoice.Customer.LastName}",
    //                Gender = invoice.Customer.Gender,
    //                NationalCode = invoice.Customer.NationalCode,
    //                PhoneNumber = invoice.Customer.PhoneNumber
    //            },
    //            Devices = invoice.Devices.Select(s => new DeviceViewModel
    //            {
    //                Id = s.Id,
    //                Guid = s.Guid,
    //                CreatedDate = s.CreatedDate,
    //                CreatedDatePersian = s.CreatedDate.ToPersianDateTime(),
    //                LastModifiedDate = s.LastModifiedDate,
    //                LastModifiedDatePersian = s.LastModifiedDate != null ? $"{((DateTime)s.LastModifiedDate).ToPersianDateTime()}" : null,
    //                Description = s.Description,
    //                Accessories = s.Accessories,
    //                CustomerPrice = s.CustomerPrice,
    //                DeviceBrand = new LookupViewModel
    //                {
    //                    Id = s.DeviceBrand.Id,
    //                    Guid = s.DeviceBrand.Guid,
    //                    CreatedDate = s.DeviceBrand.CreatedDate,
    //                    CreatedDatePersian = s.DeviceBrand.CreatedDate.ToPersianDateTime(),
    //                    LastModifiedDate = s.DeviceBrand.LastModifiedDate,
    //                    LastModifiedDatePersian = s.DeviceBrand.LastModifiedDate != null ? $"{((DateTime)s.DeviceBrand.LastModifiedDate).ToPersianDateTime()}" : null,
    //                    Description = s.DeviceBrand.Description,
    //                    TypeTitle = s.DeviceBrand.TypeTitle,
    //                    TypeId = s.DeviceBrand.TypeId,
    //                    Aux1 = s.DeviceBrand.Aux1,
    //                    Aux2 = s.DeviceBrand.Aux2,
    //                    Aux3 = s.DeviceBrand.Aux3
    //                },
    //                DeviceBrandId = s.DeviceBrandId,
    //                DeviceType = new LookupViewModel
    //                {
    //                    Id = s.DeviceType.Id,
    //                    Guid = s.DeviceType.Guid,
    //                    CreatedDate = s.DeviceType.CreatedDate,
    //                    CreatedDatePersian = s.DeviceType.CreatedDate.ToPersianDateTime(),
    //                    LastModifiedDate = s.DeviceType.LastModifiedDate,
    //                    LastModifiedDatePersian = s.DeviceType.LastModifiedDate != null ? $"{((DateTime)s.DeviceType.LastModifiedDate).ToPersianDateTime()}" : null,
    //                    Description = s.DeviceType.Description,
    //                    TypeTitle = s.DeviceType.TypeTitle,
    //                    TypeId = s.DeviceType.TypeId,
    //                    Aux1 = s.DeviceType.Aux1,
    //                    Aux2 = s.DeviceType.Aux2,
    //                    Aux3 = s.DeviceType.Aux3
    //                },
    //                DeviceTypeId = s.DeviceType.Id,
    //                Inquiry = s.Inquiry == null ? null : new InquiryViewModel
    //                {
    //                    Id = s.Inquiry.Id,
    //                    Guid = s.Inquiry.Guid,
    //                    CreatedDate = s.Inquiry.CreatedDate,
    //                    CreatedDatePersian = s.Inquiry.CreatedDate.ToPersianDateTime(),
    //                    LastModifiedDate = s.Inquiry.LastModifiedDate,
    //                    LastModifiedDatePersian = s.Inquiry.LastModifiedDate != null ? $"{((DateTime)s.Inquiry.LastModifiedDate).ToPersianDateTime()}" : null,
    //                    Description = s.Inquiry.Description,
    //                    Price = s.Inquiry.Price,
    //                    Reason = s.Inquiry.Reason,
    //                    InquiryDates = s.Inquiry.InquiryDates == null ? null
    //                    : s.Inquiry.InquiryDates.Select(d => new InquiryCallViewModel
    //                    {
    //                        Id = d.Id,
    //                        Guid = d.Guid,
    //                        CreatedDate = d.CreatedDate,
    //                        CreatedDatePersian = d.CreatedDate.ToPersianDateTime(),
    //                        LastModifiedDate = d.LastModifiedDate,
    //                        LastModifiedDatePersian = d.LastModifiedDate != null ? $"{((DateTime)d.LastModifiedDate).ToPersianDateTime()}" : null,
    //                        Description = d.Description,
    //                        IsAnswered = d.IsAnswered,
    //                        CallDateTime = d.CallDateTime,
    //                        CallDateTimePersian = d.CallDateTime.ToPersianDateTime(),
    //                        InquiryId = d.InquiryId,
    //                        IsConfirmed = d.IsConfirmed
    //                    }).ToList(),
    //                    DeviceId = s.Inquiry.DeviceId
    //                },
    //                InquiryId = s.InquiryId,
    //                Model = s.Model,
    //                InvoiceId = s.InvoiceId,
    //                RepairWarranty = s.RepairWarranty,
    //                ShopPrice = s.ShopPrice,
    //                ShopWarranty = s.ShopWarranty,
    //            }).ToList(),
    //        };
    //    }
    //}
}
