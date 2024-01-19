using CRM.Common.Resources.StringResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Common.Enums
{
    public enum ePeopleType
    {
        [Display(Name = "Natural", ResourceType = typeof(Resource))]
        Natural,

        [Display(Name = "Legal", ResourceType = typeof(Resource))]
        Legal
    }
}
