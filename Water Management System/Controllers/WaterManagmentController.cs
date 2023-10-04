using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class WaterManagmentController : Controller
    {
        //Private Database variable
        private ApplicationDbContext _db;
        public WaterManagmentController(ApplicationDbContext db)
        {
            //Assign global db to private variable
            _db = db;
        }

        public IActionResult UserManager()
        {
            //Get user data to the 'data' variable
            var data = (from wr in _db.Users
                        join comp in _db.companies on wr.companyid equals comp.id
                        where wr.isApproved == 0
                        select new UserDTO { CompanyName=comp.name,username=wr.username,Userid=wr.id }).ToList();
            
            ViewBag.data2 = (from wr in _db.Users
                        join comp in _db.companies on wr.companyid equals comp.id
                        where wr.isApproved == 1 && comp.water>0
                        select new UserDTO { CompanyName = comp.name, username = wr.username, Userid = wr.id,water=comp.water }).ToList();

            return View(data);
        }

        // This Action methord is used to Approve user  requests
        public IActionResult ApproveUser(int id)
        {
            var data = _db.Users.Where(x => x.id == id).FirstOrDefault();

            //set apporved flag to 1 for approve
            data.isApproved = 1;
            _db.Update(data);
            _db.SaveChanges();
            return RedirectToAction("UserManager");
        }

        // This Action methord is used to reject user  requests
        public IActionResult RejectUser(int id)
        {
            var data = _db.Users.Where(x => x.id == id).FirstOrDefault();

            //set apporved flag to 2 for reject
            data.isApproved = 2;
            _db.Update(data);
            _db.SaveChanges();
            var data2 = _db.companies.Where(x => x.id == data.companyid).FirstOrDefault();
            _db.Remove(data2);
            _db.SaveChanges();

            return RedirectToAction("UserManager");
        }

        // This Action methord is used to approve water  requests
        public IActionResult Approvewater(int id)
        {
            var data = _db.waterrequests.Where(x => x.id == id).FirstOrDefault();

            //set Staus flag to 1 for Approve
            data.Status = 1;
            int value = data.waterreqtesed;
            _db.Update(data);
            _db.SaveChanges();
            var data2 = _db.companies.Where(x => x.id == data.companyid).FirstOrDefault();
            data2.water =  value;
            _db.Update(data2);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // This action methord is used to show Statictics
        public IActionResult Statictics()
        {
            @ViewBag.data = null;

            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();

            // load company table data to 'comp' ViewBag
            @ViewBag.comp = _db.companies.Where(x=>x.water>0).ToList();

            return View();
        }

        // This methord is used to pass crop data to the JQuery
        public JsonResult GetCrop(int id)
        {
            List<crop> data = _db.crops.Where(x=>x.Companyid==id).ToList();

            return Json(data);
        }


        // This action methord is used to get Statictics Search data
        [HttpPost]
        public IActionResult StaticticsPOST()
        {

            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();

            // load companies table data to 'comp' ViewBag
            @ViewBag.comp = _db.companies.Where(x => x.water > 0).ToList();

            // User input parameters to the variables
            var compid = Request.Form["compid"];
            var cropid = Request.Form["CropDropdown"];

            // Store company data as per the user inputs
            ViewBag.data = _db.Water.Where(x => x.Companyid == int.Parse(compid) && x.cropid == int.Parse(cropid)).ToList();

            return View("Statictics");
        }

        // This action methord is used to Reject water requests from users.
        public IActionResult Rejectwater(int id)
        {
            var data = _db.waterrequests.Where(x => x.id == id).FirstOrDefault();

            // Set Status to 2 for Reject
            data.Status = 2;
             _db.Update(data);
            _db.SaveChanges();

                 

            return RedirectToAction("Index");
        }

        // this Action Methord is used to View GSI main page
        public IActionResult Index(int x)
        {
			if (x == 1)
			{

				ViewBag.Message = "Added successfully!";
				ViewBag.function = "ShowErrorPopup()";
			}
			else if (x == 2)
			{
				ViewBag.Message = "Allready exist!";
				ViewBag.function = "ShowErrorPopup()";
			}

            // Store company data in the Viewbag
            ViewBag.comany=_db.companies.Where(x=>x.water>0).ToList();
            var data = (from wr in _db.waterrequests
                        join comp in _db.companies on wr.companyid equals comp.id where wr.Status==0 
                        select new waterrequest { Compname = comp.name, waterreqtesed = wr.waterreqtesed,Date=wr.Date,companyid=wr.companyid,id=wr.id }).ToList();

            return View(data);
        }
    }
}
