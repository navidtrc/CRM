using CRM.Service.Staff;
using CRM.ViewModels.ViewModels;
using CRM.WebFramework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace CRM.UI.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class StaffController : ControllerBase
    {
        
    }
}
