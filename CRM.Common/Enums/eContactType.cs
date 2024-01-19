using CRM.Common.Resources.StringResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Common.Enums
{
    public enum eContactType
    {
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resource))]
        PhoneNumber,

        [Display(Name = "Address", ResourceType = typeof(Resource))]
        Address,
        
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        Email,

        [Display(Name = "SocialMedia", ResourceType = typeof(Resource))]
        SocialMedia
    }
}
