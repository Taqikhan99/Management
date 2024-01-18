using Management.Web.Models.ViewModels;
using Management.Web.Repositories.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace Management.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepo accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            this.accountRepo = accountRepo;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginRequest)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var user =await accountRepo.VerifyLogin(loginRequest.Username, loginRequest.Password);
                    if(user !=null)
                    {   
                        //add claimns
                        var claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                        new Claim(ClaimTypes.Name, user.Username) 
                        };

                        //initialize an instance of claims identity
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                        var principal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties());

                        //after setting up the signin redirect to the homepage
                        return RedirectToAction("Index", "Home");   

                    }
                    else
                    {
                        ViewBag.Response = "Invalid Username or Password";
                    }

                    return View(loginRequest);
                }

                return View();
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
           
        }

        //For Logut
        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to LoginPage   
            return LocalRedirect("/");
        }
    }
}
