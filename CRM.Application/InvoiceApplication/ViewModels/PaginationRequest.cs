using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CRM.Application.InvoiceApplication.ViewModels
{
    public class PaginationRequest 
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filters { get; set; }
        public Sort Sort { get; set; }
    }   
    public class Filter
    {
        public string Column { get; set; }
        public eFilterCondition Condition { get; set; }
        public string Value { get; set; }
        public eFilterValueType Type { get; set; }
    }
    public class Sort
    {
        public string Column { get; set; }
        public eSortType SortType { get; set; }
    }
}
