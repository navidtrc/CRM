using CRM.Common.Enums;
using CRM.Common.Resources.StringResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.ViewModels.ViewModels
{
    public class PersonUser_AddEdit_ViewModel
    {
        public UserViewModel User { get; set; }
        public PersonViewModel Person { get; set; }
    }
    public class UserViewModel
    {
        [Display(Name = "Id", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Id { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }
    }
    public class PersonViewModel
    {
        [Display(Name = "Id", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public long Id { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Resource)), Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(Resource))]
        public eGender Gender { get; set; }

        [Display(Name = "NationalCode", ResourceType = typeof(Resource))]
        public string NationalCode { get; set; }

        [Display(Name = "BirthDate", ResourceType = typeof(Resource))]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "PersonType", ResourceType = typeof(Resource))]
        public ePersonType ePersonType { get; set; }

        public void Deconstruct(
            out string FirstName,
            out string LastName,
            out DateTime? BirthDate,
            out eGender Gender
        )
        {
            FirstName = this.FirstName;
            LastName = this.LastName;
            BirthDate = this.BirthDate;
            Gender = this.Gender;
        }
    }
}
