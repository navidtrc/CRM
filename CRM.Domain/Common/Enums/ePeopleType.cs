using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Common.Enums
{
    public enum ePeopleType
    {
        [Display(Name = "Natural")]
        Natural,

        [Display(Name = "Legal")]
        Legal
    }
}
