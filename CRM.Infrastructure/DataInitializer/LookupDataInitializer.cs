using CRM.Infrastructure.Persistance.Core;
using Newtonsoft.Json;

namespace CRM.Infrastructure.DataInitializer
{
    public class LookupDataInitializer : IDataInitializer
    {
        private readonly IUnitOfWork _uow;
        public LookupDataInitializer(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public void InitializeData()
        {
            if (!_uow.Lookup.ExistType("InvoiceState").Data)
            {
                _uow.Lookup.AddTypeValue("InvoiceState", "NotSent", "ارسال نشده");
                _uow.Lookup.AddTypeValue("InvoiceState", "SentToRepair", "ارسال شده به تعمیرگاه");
                _uow.Lookup.AddTypeValue("InvoiceState", "BackFromRepair", "برگشت از تعمیرگاه");
                _uow.Lookup.AddTypeValue("InvoiceState", "NeedInquiry", "نیاز به استعلام");
                _uow.Lookup.AddTypeValue("InvoiceState", "Repairing", "در حال تعمیر");
                _uow.Lookup.AddTypeValue("InvoiceState", "Ready", "آماده");
                _uow.Lookup.AddTypeValue("InvoiceState", "Done", "تحویل داده شده");
            }
            if (!_uow.Lookup.ExistType("DeviceType").Data)
            {
                _uow.Lookup.AddTypeValue("DeviceType", "Shaver", "شیور");
                _uow.Lookup.AddTypeValue("DeviceType", "Trimmer", "تریمر");
                _uow.Lookup.AddTypeValue("DeviceType", "Epilady", "اپیلیدی");
                _uow.Lookup.AddTypeValue("DeviceType", "Hairdryer", "سشوار");
                _uow.Lookup.AddTypeValue("DeviceType", "Straighteners", "حالت دهنده");
                _uow.Lookup.AddTypeValue("DeviceType", "Toothbrush", "مسواک");
                _uow.Lookup.AddTypeValue("DeviceType", "Cleanerstand", "پایه شستشو");
                _uow.Lookup.AddTypeValue("DeviceType", "Charger", "شارژر");
                _uow.Lookup.AddTypeValue("DeviceType", "Other", "غیره");

            }
            if (!_uow.Lookup.ExistType("DeviceBrand").Data)
            {
                _uow.Lookup.AddTypeValue("DeviceBrand", "Braun", "براون");
                _uow.Lookup.AddTypeValue("DeviceBrand", "Philips", "فیلیپس");
                _uow.Lookup.AddTypeValue("DeviceBrand", "Remington", "رمینگتون");
                _uow.Lookup.AddTypeValue("DeviceBrand", "Panasonic", "پاناسونیک");
                _uow.Lookup.AddTypeValue("DeviceBrand", "Moser", "موزر");
                _uow.Lookup.AddTypeValue("DeviceBrand", "Wahl", "وال");
                _uow.Lookup.AddTypeValue("DeviceBrand", "Promax", "پرو مکس");
                _uow.Lookup.AddTypeValue("DeviceBrand", "Other", "متفرقه");
            }
            if (!_uow.Lookup.ExistType("SmsSetting").Data)
            {
                var smsSetting = new
                {
                    server = "http://188.0.240.110/api/select",
                    op = "send",
                    uname = "09120710235",
                    pass = "faraz0044283121",
                    from = "3000505"
                };
                var jsonString = JsonConvert.SerializeObject(smsSetting);
                _uow.Lookup.AddTypeValue("SmsSetting", jsonString);
            }
            if (!_uow.Lookup.ExistType("InvoiceStateChangeSmsContent").Data)
            {
                var message = @"{0} عزیز، فاکتور تعمیری شما به شماره {1} به وضعیت {2} در آماده است.
تغییرات وضعیت فاکتور شما به مراتب به اطلاعتان خواهد رسید.
فروشگاه براون موسوی";
                _uow.Lookup.AddTypeValue("InvoiceStateChangeSmsContent", message);
            }
            if (!_uow.Lookup.ExistType("InvoiceCreateSmsContent").Data)
            {
                var message = @"{0} عزیز، فاکتور تعمیری شما به شماره {1} ثبت شد.
تغییرات وضعیت فاکتور شما به مراتب به اطلاعتان خواهد رسید.
فروشگاه براون موسوی";
                _uow.Lookup.AddTypeValue("InvoiceCreateSmsContent", message);
            }
        }
    }
}
