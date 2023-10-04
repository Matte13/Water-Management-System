using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
     
        }

        public IActionResult Index()
        {

            MyModel model = new MyModel();
        

            if (StaticClass.type == "C")
            {
                // if it's 'C',Redirect the page to GA
                ViewBag.Message = "";
                ViewBag.function = "";
                return RedirectToAction("Index","CompanyManagment");
            }else if(StaticClass.type == "W")
            {
                //  if it's 'W',Redirect the page to GSI
                ViewBag.Message = "";
                ViewBag.function = "";
                return RedirectToAction("Index", "WaterManagment");
            }
            else if (StaticClass.type == "0")
            {
                //Shows error message.
                ViewBag.Message = "User not found!";
                ViewBag.function= "ShowErrorPopup()";
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}