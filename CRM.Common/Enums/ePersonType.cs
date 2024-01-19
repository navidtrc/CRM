using CRM.Common.Resources.StringResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Common.Enums
{
    public enum ePersonType
    {
        [Display(Name = "Staff", ResourceType = typeof(Resource))]
        Staff,

        [Display(Name = "Customer", ResourceType = typeof(Resource))]
        Customer
    }
}
