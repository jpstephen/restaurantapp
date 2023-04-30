using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Data;
using RestaurantApp.Models;
using System.Linq;

namespace RestaurantApp.Controllers
{
    public class BillsController : Controller
    {
        

        private readonly ApplicationDbContext _context;
        public BillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Bills> allItems = _context.allBills.Select(p => new Bills()
            {
                BillId = p.BillId,
                addDeliveryInBill = p.addDeliveryInBill,
                addMobileNo = p.addMobileNo,
                vatAmount = p.vatAmount,
                billAmountWithoutVat = p.billAmountWithoutVat,
                billAmountWithVat = p.billAmountWithVat,
                createdTime = p.createdTime
            }).OrderByDescending(o => o.createdTime);
            return View(allItems);
        }
    }
}
