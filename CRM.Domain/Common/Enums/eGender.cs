using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Common.Enums
{
    public enum eGender
    {
        [Display(Name = "Female")]
        Female,

        [Display(Name = "Male")]
        Male,
        
        [Display(Name = "Other")]
        Other
    }
}
