using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    // This Controller stores all the Action Methods of GSI
    public class CompanyManagmentController : Controller
    {
        //Private Database variable
        private ApplicationDbContext _db;

        public CompanyManagmentController(ApplicationDbContext db)
        {
            //Assign global db to private variable
            _db = db;
        }
        public IActionResult Index(int x)
        {
            // Global variables used to pass values to Javascript2.js Message Box
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
            // load crops table data to 'data' variable
            var data =_db.crops.ToList();
            return View(data);
        }


        public IActionResult Actuator(int x)
        {
            // Global variables used to pass values to Javascript2.js Message Box

            if (x == 1)
            {
                ViewBag.Message = "Added successfully!";
                ViewBag.function = "ShowErrorPopup()";
            }
            else if(x==2)
            {
                ViewBag.Message = "Allready exist!";
                ViewBag.function = "ShowErrorPopup()";
            }
            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();
            // load types table data to 'type' ViewBag
            @ViewBag.type = _db.types.ToList();
            return View();
        }

        public IActionResult sensor(int x)
        {
            // Global variables used to pass values to Javascript2.js Message Box

            if (x == 1)
            {
                ViewBag.Message = "Added successfully!";
                ViewBag.function = "ShowErrorPopup()";
            }
            else if (x == 2)
            {
                ViewBag.Message = "Allready exsist!";
                ViewBag.function = "ShowErrorPopup()";
            }

            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();

            // load types table data to 'type' ViewBag
            @ViewBag.type = _db.types.ToList();

            return View();
        }

        public IActionResult historicalW()
        {
            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();
          
            return View();
        }
        public IActionResult addsensor(Sensor model)
        {
            //This action methord used to add Sensor to the table.
            var data = _db.sensors.Where(x => x.CropID == model.CropID && x.type == model.type).ToList();
            int x = 0;

            // Check it's duplicate (Sensor).
            if (data.Count == 0)
            {
                // If it's not duplicated, add the Sensor.
                _db.Add(model);
                _db.SaveChanges();
                x = 1;
            }
            else
            {
                //If it's duplicated , Show and error message
                x = 2;
            }

            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();

            // load types table data to 'type' ViewBag
            @ViewBag.type = _db.types.ToList();

            return RedirectToAction("sensor", new {x=x});
        }

        [HttpPost]
        public IActionResult saveType(Typem model)
        {
            //This action methord used to add Types to the table.

            var data = _db.types.Where(x => x.Name == model.Name).ToList();
            
            // Check it's duplicate (types).
            if (data.Count == 0)
            {
                // If it's not duplicated, add the Type.

                _db.Add(model);
                _db.SaveChanges();
                ViewBag.Message = "Added successfully!";
                ViewBag.function = "ShowErrorPopup()";

            }
            else
            {
                //If it's duplicated , Show and error message

                ViewBag.Message = "Type allready exsist!";
                ViewBag.function = "ShowErrorPopup()";
            }

            return View("addType");
        }
        public IActionResult addType()
        {
            //Show Type Viewer
            return View();
        }

        public IActionResult water()
        {
            // This action methord is used to view the 'view' 

            water wt = new water();

            //Initiate variables
            wt.WaterUsed = 0;
            wt.WaterAssigned=0;

            //Store 'waterrequests' table's data to the ViewBag
            @ViewBag.history = _db.waterrequests.OrderByDescending(x => x.Date).ToList();

            //Store 'companies' table's data to the 'data' variable

            var data = _db.companies.Where(x => x.id == StaticClass.Companyid).FirstOrDefault();

            //Store data variable   to the ViewBag
            @ViewBag.currentwater = data.water;

            //Store crop table's data  to the ViewBag
            @ViewBag.crop = _db.crops.ToList();

            //Store type table's data  to the ViewBag
            @ViewBag.type = _db.types.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Addwaterrequest(waterrequest model)
        {
            //This Action methord is used to Add data to the waterrequest table
            
            model.companyid = StaticClass.Companyid;
        
            model.Status=0;
          
            model.Date = DateTime.Now;

            _db.Add(model);
            _db.SaveChanges();

            //Store crop table's data  to the ViewBag
            @ViewBag.crop = _db.crops.ToList();

            //Store type table's data  to the ViewBag
            @ViewBag.type = _db.types.ToList();
            
            // Pass Message to pop-up to display.
            ViewBag.Message = "Requested successfully!";
            ViewBag.function = "ShowErrorPopup()";

            return RedirectToAction("water");
        }
        public IActionResult Crop(int x)
        {
            // Global variables used to pass values to Javascript2.js Message Box

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

            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();

            // load type table data to 'type' ViewBag
            @ViewBag.type = _db.types.ToList();

            return View();
        }

        public IActionResult historicM()
        {
            // This action methord is used to View 'Actuator/Sensor for crop'
            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();
     
            return View();
        }

        [HttpPost]
        public IActionResult GethistoricW(CropDTO model)
        {
            // This Action methord is used to feed data to 'History of water use per field' Viewer

            // load waterrequests table data to 'history' ViewBag
            @ViewBag.history = _db.waterrequests.OrderByDescending(x => x.Date).ToList();

            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();

            // load water table data to 'water' ViewBag
            @ViewBag.water = _db.Water.Where(x=>x.cropid==model.id).ToList();
          
            return View("historicalW");
        }

        [HttpPost]
        public IActionResult GethistoricM(CropDTO model)
        {
            // This Action methord is used to feed data to 'Actuator/Sensor for crop' Viewer

            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();

            // load sensor table data to 'sensor' ViewBag
            @ViewBag.sensor = _db.sensors.Where(x => x.CropID == model.id).ToList();

            // load actuators table data to 'actuators' ViewBag
            @ViewBag.actuators = _db.actuators.Where(x => x.Cropid == model.id).ToList();

            // load waterrequests table data to 'history' ViewBag
            @ViewBag.history = _db.waterrequests.OrderByDescending(x => x.Date).ToList();

            return View("historicM");
        }

        public IActionResult Delcrop(int id)
        {
            // This Action methord is used to delete crop

            //Call crop by id
            var data = _db.crops.Where(x => x.id == id).FirstOrDefault();

            //Delete crop
            _db.crops.Remove(data);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddCrop(crop model)
        {
            // This action method is used to add crop
            model.Companyid = StaticClass.Companyid;
            int x = 0;

            // Check duplicates.
            var data = _db.crops.Where(x => x.Name == model.Name && x.Companyid == model.Companyid).ToList();
            if (data.Count == 0)
            {
                // No duplicates , add crop
                x = 1;
                _db.Add(model);
                _db.SaveChanges();
            }
            else
            {
                // If it's duplicated , show message.
                x = 2;
            }

            // load crops table data to 'crop' ViewBag
            @ViewBag.crop = _db.crops.ToList();

            // load types table data to 'type' ViewBag
            @ViewBag.type = _db.types.ToList();

            return RedirectToAction("Index",new {x=x});
        }



        [HttpPost]
        public IActionResult AddActuator(actuator model)
        {
            //This action methord is used to  Add Actuator to the table
            var data = _db.actuators.Where(x => x.Companyid == StaticClass.Companyid && x.Type == model.Type && x.Cropid==model.Cropid).ToList();
            int x = 0;

            // check duplicates
            if (data.Count == 0)
            {
                //if it's not duplicated , add the Actuator
                model.Companyid = StaticClass.Companyid;
                _db.Add(model);
                _db.SaveChanges();
                x = 1;
            }
            else
            {
                //if it's duplicated, show the Message
                x = 2;
            }
       
            return RedirectToAction("Actuator", new { x=x});
        }


    }
}
