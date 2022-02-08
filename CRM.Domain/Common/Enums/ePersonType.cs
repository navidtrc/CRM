using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Common.Enums
{
    public enum ePersonType
    {
        [Display(Name = "Staff")]
        Staff,

        [Display(Name = "Customer")]
        Customer
    }
}
