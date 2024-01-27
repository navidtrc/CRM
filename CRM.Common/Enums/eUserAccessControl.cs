using System.ComponentModel.DataAnnotations;

namespace CRM.Common.Enums
{
    public enum eAccessType
    {
        View = 0,
        Api = 1,
        None = 2
    }
    public enum eAccessControl
    {
        [Display(Name = "صفحه ی ورود کاربر")]
        AccountLogin,
        [Display(Name = "Api ورود کاربر")]
        AccountLoginApi,
        [Display(Name = "Api ثبت نام کاربر")]
        AccountRegisterApi,
        [Display(Name = "Api خروج کاربر")]
        AccountLogoutApi,
        [Display(Name = "Api درخواست فراموشی کلمه عبور کاربر")]
        AccountForgetPasswordApi,
        [Display(Name = "صفحه ی تغییر کلمه عبور")]
        AccountChangePassword,
        [Display(Name = "Api تغییر کلمه ی عبور")]
        AccountForgetPasswordConfirmApi,
        PermissionGetPermissions,
        PermissionSaveAccessPermissionGroup,
        PermissionGetAccessesForPermissionGroup,
        PermissionGetGroupAccessList,
        PermissionSaveDefaultRoleAccesses,
        PermissionGetDefaultRoleAccesses,
        PermissionGetRoleList,
        PermissionSavePermissionReverse,
        PermissionSavePermission,
        PermissionGetUserList,
        PermissionGetUsersBy,
        PermissionGetAllPermissions,
        PermissionUserAccessAndRole,
        PermissionDefaultRoleAccess,
        PermissionAccessPermissionGroup,
        PanelDashboard,
        PanelInvoice,
        UserGetUserListApi,
        AccountDeleteApi,
        AccountLockoutApi,
        StaffGetList,
        PeopleDelete,
        PeopleGetList,
        PeopleRegisterApi,
        TicketGetList
    }
}
