using CRM.Common;
using CRM.Common.Resources.StringResources;
using System.ComponentModel.DataAnnotations;

namespace CRM.ViewModels.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Display(Name = "UserName", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Display(Name = "ForgetPasswordType", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public eForgetPasswordVia ForgetType { get; set; }


    }
}
