using AutoMapper;
using CRM.Application.WebFramework.Api;
using CRM.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.LookupApplication.ViewModels
{
    public class LookupViewModel : BaseViewModel<LookupViewModel, Lookup>
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

        public string TypeTitle { get; set; }

        public int TypeId { get; set; }

        public string Aux1 { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Aux2 { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Aux3 { get; set; }

        public override void CustomMappings(IMappingExpression<Lookup, LookupViewModel> mapping)
        {
            mapping.ForMember(
                    dest => dest.CreatedDatePersian,
                    config => config.MapFrom(src => $"{src.CreatedDate.ToPersianDateTime()}"));
            mapping.ForMember(
                    dest => dest.LastModifiedDatePersian,
                    config => config.MapFrom(src => src.LastModifiedDate != null ? $"{((DateTime)src.LastModifiedDate).ToPersianDateTime()}" : null));
        }
    }

}
