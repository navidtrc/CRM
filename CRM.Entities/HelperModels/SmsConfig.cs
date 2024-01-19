using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.HelperModels
{
    public class SmsConfig
    {
        private static string _key = nameof(SmsConfig);
        public static string Key
        {
            get { return _key; }
        }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SendFrom { get; set; }
        public bool IsFlash { get; set; }
        public int TimeOut { get; set; }
        public Method Method { get; set; }
    }
}
