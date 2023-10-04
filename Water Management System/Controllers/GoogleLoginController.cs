using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Server.HttpSys;

namespace WebApplication1.Controllers
{
    public class GoogleLoginController : Controller
    {
        //Private Database variable
        private ApplicationDbContext _db;
     
        public GoogleLoginController(ApplicationDbContext db)
        {
            //Assign global db to private variable
            _db = db;
        }

        public IActionResult Signup(int x,string y)
        {
            // Global variables used to pass values to Javascript2.js Message Box

            if (x == 1)
			{
				ViewBag.Message = "Successfully sent for Approvel.";
				ViewBag.function = "ShowErrorPopup()";
			}
			else if (x == 2)
			{
				ViewBag.Message = y;
				ViewBag.function = "ShowErrorPopup()";
			}

            // load company table data to 'company' variable
            @ViewBag.company = _db.companies.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Signupnow(signup model)
        {
            // This action methord is used for Signup process

            string msg = "";
            int x = 0;

            // load company table data to 'data' variable
            var data = _db.companies.Where(x => x.name == model.Company).FirstOrDefault();
           
            // Check comapny allreadt exist.
            if (data == null )
            {
                //Company not in the database, add the Company & User.
                x = 1;
                company cmp = new company();
                cmp.name = model.Company;
                cmp.water = 0;
                _db.Add(cmp);
                _db.SaveChanges();
                 data = _db.companies.Where(x => x.name == model.Company).FirstOrDefault();

                // Add user to the database
                User usr = new User();
                usr.username = model.Username;
                usr.isApproved = 0;
                usr.companyid = data.id;
                usr.type = "C";
                _db.Add(usr);
                _db.SaveChanges();
                x = 1;
            }
            else
            {
                // Company found, Shows the message
                x = 2;

                var data2 = _db.Users.Where(x => x.companyid == data.id && x.username == model.Username).ToList();

                if (data2.Count > 0)
                {
                    msg = "Company name and User name Allready exist";
                }
                else 
                {
                    msg = "Company name Allready exist";

                }

                return RedirectToAction("Signup", new { x = x, y = msg });

            }

            return RedirectToAction("Signup", new {x=x,y=msg}) ;
        }
        public IActionResult Index()
        {
            // Redirect to the Google site to authenticate
            return new ChallengeResult(
                GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse", "GoogleLogin") // Where google responds back
                   // RedirectUri = "https://localhost:7244/"
                }); 

         
        }
        public async Task<IActionResult> SignOutFromGoogleLogin()
        {
            //Check if any cookie value is present
            if (HttpContext.Request.Cookies.Count > 0)
            {
                //Check for the cookie value with the name mentioned for authentication and delete each cookie
                var siteCookies = HttpContext.Request.Cookies.Where(c => c.Key.Contains(".AspNetCore.") || c.Key.Contains("Microsoft.Authentication"));
                foreach (var cookie in siteCookies)
                {
                    Response.Cookies.Delete(cookie.Key);
                }
            }

            //signout with any cookie present 
            await HttpContext.SignOutAsync("External");
            StaticClass.CurrentUser = "";
            StaticClass.CompanyName = "";
            StaticClass.Companyid = 0;
            StaticClass.type = "";
            return  RedirectToAction("Index", "Home");
        }

        /// Google Login Response After Login Operation From Google Page
        public async Task<IActionResult> GoogleResponse()
        {
            ViewBag.Message = "";
            ViewBag.function = "";

            //Check authentication response as mentioned on startup file as o.DefaultSignInScheme = "External"
            var authenticateResult = await HttpContext.AuthenticateAsync("External");
            if (!authenticateResult.Succeeded)
                return BadRequest(); // TODO: Handle this better.

            //Check if the redirection has been done via google or any other links
            if (authenticateResult.Principal.Identities.ToList()[0].AuthenticationType.ToLower() == "google")
            {
                //check if principal value exists or not 
                if (authenticateResult.Principal != null)
                {
                    //get google account id for any operation to be carried out on the basis of the id
                    var googleAccountId = authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    //claim value initialization as mentioned on the startup file with o.DefaultScheme = "Application"
                    var claimsIdentity = new ClaimsIdentity("Application");
                    if (authenticateResult.Principal != null)
                    {
                        //Now add the values on claim and redirect to the page to be accessed after successful login
                        var details = authenticateResult.Principal.Claims.ToList();
                        claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier));// Full Name Of The User
                        claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.Email)); // Email Address of The User
                       
                        await HttpContext.SignInAsync("Application", new ClaimsPrincipal(claimsIdentity));


                       StaticClass.image = authenticateResult.Principal.FindFirstValue("picture");

                        //******************
                        string[] userInfo = { authenticateResult.Principal.FindFirst(ClaimTypes.Name).Value, authenticateResult.Principal.FindFirst(ClaimTypes.Email).Value };
                        
                        
                        
                        //*******************
                        StaticClass.CurrentUser = userInfo[1];
                        var data = _db.Users.Where(x => x.username == StaticClass.CurrentUser && x.isApproved==1).FirstOrDefault();
                        if (data == null)
                        {
                            SignOutFromGoogleLogin();
                            StaticClass.type = "0";
                            return RedirectToAction("Index", "Home");
                        }

                        StaticClass.Companyid = data.companyid;
                        var data2 = _db.companies.Where(x => x.id == data.companyid).FirstOrDefault();
                        StaticClass.CompanyName = data2.name;
                        StaticClass.type = data.type;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
