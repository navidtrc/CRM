using System.ComponentModel.DataAnnotations;

namespace CRM.Common.Enums
{
    public enum eTicketStatus
    {
        [Display(Name = "جدید")]
        Waiting = 0,

        [Display(Name = "بررسی تعمیرکار")]
        Repairer_Check = 1,

        [Display(Name = "بررسی مرکز")]
        Store_Check = 2,

        [Display(Name = "استعلام")]
        Inquiry = 3,

        [Display(Name = "آماده تعمیر")]
        ReadyToRepair = 4,

        [Display(Name = "در حال تعمیر")]
        Repairing = 5,

        [Display(Name = "آماده+")]
        Ready_Repaired = 6,

        [Display(Name = "آماده-")]
        Ready_NotRepaired = 7,

        [Display(Name = "بسته شده")]
        Done = 8
    }
}
