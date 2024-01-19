using System;

namespace CRM.Entities.HelperModels
{
    public class EmailConfig
    {
        private static string _key = nameof(EmailConfig);
        public static string Key
        {
            get { return _key; }
        }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string Email { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
    }
    
}
