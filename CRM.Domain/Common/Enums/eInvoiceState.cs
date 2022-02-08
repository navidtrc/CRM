namespace CRM.Domain.Common.Enums
{
    public enum eInvoiceState
    {
        NotSentYet,
        SentToRepair,
        BackFromRepair,
        NeedInquiry,
        Repairing,
        Ready,
        Done
    }
}
