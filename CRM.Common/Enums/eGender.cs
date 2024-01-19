using CRM.Common.Resources.StringResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Common.Enums
{
    public enum eGender
    {
        [Display(Name = "Female", ResourceType = typeof(Resource))]
        Female,

        [Display(Name = "Male", ResourceType = typeof(Resource))]
        Male,
        
        [Display(Name = "Other", ResourceType = typeof(Resource))]
        Other

    }
}
