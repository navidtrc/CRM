using CRM.Common.Resources.StringResources;
using System.ComponentModel.DataAnnotations;

namespace CRM.ViewModels.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "UserName", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        [Display(Name = "Cookie", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public bool AppendCookie { get; set; }
    }
}
