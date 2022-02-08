using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Common.Enums
{
    public enum eContactType
    {
        [Display(Name = "PhoneNumber")]
        PhoneNumber,

        [Display(Name = "Address")]
        Address,
        
        [Display(Name = "Email")]
        Email,

        [Display(Name = "SocialMedia")]
        SocialMedia
    }
}
