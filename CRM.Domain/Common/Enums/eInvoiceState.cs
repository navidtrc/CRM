namespace CRM.Domain.Common.Enums
{
    public enum eInvoiceState
    {
        NotSentYet = 1,
        SentToRepair,
        BackFromRepair,
        NeedInquiry,
        Repairing,
        Ready,
        Done
    }
}
