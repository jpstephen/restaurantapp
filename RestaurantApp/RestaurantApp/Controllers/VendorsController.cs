using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Data;
using RestaurantApp.Models;
using System.Diagnostics;
using System.Dynamic;

namespace RestaurantApp.Controllers
{
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VendorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            IEnumerable<Vendors> allVendors = _context.MVendors;
            IEnumerable<VendorBills> allVendorBills = _context.MVendorBills;
            mymodel.VendorBills = allVendorBills;
            mymodel.Vendors = allVendors;
            return View(mymodel);
        }

        public IActionResult Create()
        {

            //return PartialView("_createItem");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendors vendorobj)
        {
            if (ModelState.IsValid)
            {
                _context.MVendors.Add(vendorobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction("Index");
            }

            return View(vendorobj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var vendorfromdb = _context.MVendors.Find(id);

            if (vendorfromdb == null)
            {
                return NotFound();
            }
            return View(vendorfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vendors vendorobj)
        {
            if (ModelState.IsValid)
            {
                _context.MVendors.Update(vendorobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(vendorobj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var vendorfromdb = _context.MVendors.Find(id);

            if (vendorfromdb == null)
            {
                return NotFound();
            }
            return View(vendorfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteVendor(int? id)
        {
            var deleterecord = _context.MVendors.Find(id);
            if (deleterecord == null)
            {
                return NotFound();
            }
            _context.MVendors.Remove(deleterecord);
            _context.SaveChanges();
            TempData["ResultOk"] = "Data Deleted Successfully !";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddVendorRecord(VendorBills vendorbillobj)
        {
            //if (!ModelState.IsValid)
            //{
            //    //return BadRequest("Failed to add data");
            //    return BadRequest("Failed to add data"); ;
            //}

            _context.MVendorBills.Add(vendorbillobj);
            _context.SaveChanges();
            TempData["ResultOk"] = "Record Added Successfully !";
            return this.Ok("Form Data received!");
        }

        public JsonResult GetVendorBills(int vendorid)
        {
            Debug.WriteLine(vendorid);

            var result = _context.MVendorBills.ToList();

            return Json(result);
        }
    }
}
