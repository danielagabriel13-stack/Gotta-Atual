using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace GOTTA.Controllers
{
    [AllowAnonymous]
    public class NavBarController : Controller
    {

        public IActionResult Navbar()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
               
                return PartialView("_NavAutenticado");
            }
            else
            {
             
                return PartialView("_NavAnonimo");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
              
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Home/Login.cshtml");
        }

       
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }
    }
}
