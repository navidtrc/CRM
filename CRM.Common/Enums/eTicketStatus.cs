using System.ComponentModel.DataAnnotations;

namespace CRM.Common.Enums
{
    public enum eTicketStatus
    {
        [Display(Name = "جدید")]
        Waiting = 0,

        [Display(Name = "بررسی تعمیرکار")]
        Checking = 1,

        [Display(Name = "استعلام")]
        Inquiry = 2,

        [Display(Name = "آماده تعمیر")]
        ReadyToRepair = 3,

        [Display(Name = "در حال تعمیر")]
        Repairing = 4,

        [Display(Name = "آماده+")]
        Ready_Repaired = 5,

        [Display(Name = "آماده-")]
        Ready_NotRepaired = 6,

        [Display(Name = "بسته شده")]
        Done = 7
    }
}
