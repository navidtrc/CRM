using CRM.Common.Resources.StringResources;
using System.ComponentModel.DataAnnotations;

namespace CRM.Common.Enums
{
    public enum eInvoiceStatus
    {
        [Display(Name = "NotSentYet", ResourceType = typeof(Resource))]
        NotSentYet,
        
        [Display(Name = "SentToRepair", ResourceType = typeof(Resource))]
        SentToRepair,
        
        [Display(Name = "Repairing", ResourceType = typeof(Resource))]
        Repairing,
        
        [Display(Name = "Ready", ResourceType = typeof(Resource))]
        Ready,
        
        [Display(Name = "NeedInquiry", ResourceType = typeof(Resource))]
        NeedInquiry,
        
        [Display(Name = "SentToRepairAfterInquiry", ResourceType = typeof(Resource))]
        SentToRepairAfterInquiry,
        
        [Display(Name = "Done", ResourceType = typeof(Resource))]
        Done

    }
}
