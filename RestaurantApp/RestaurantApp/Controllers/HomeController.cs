using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Data;
using RestaurantApp.Models;
using System.Diagnostics;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.Globalization;

namespace RestaurantApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Items> allItems = _context.MItems;
            return View(allItems);
        }

        [HttpGet]
        public ActionResult GeneratePDF(string allbills)
        {

            var thebills = JsonConvert.DeserializeObject<List<Bill>>(allbills);
            var document = new PdfDocument();


            
            string htmlcontent = "<table class='table-condensed' width='100%'>";
            htmlcontent += "<tr><td colspan=5 align='right'>" + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt") + "</td></tr>";
            htmlcontent += "<tr><td colspan=5 align='center'><h2>LE LAZARO CAFE</h2>" +
                "<br/>Hamdan Street, behind Dama's Gold" +
                "<br/>Abu Dhabi, UAE.</td></tr>";
            
            htmlcontent += "<tr><td style='height:40px;border-bottom:1px solid black' colspan=5></td></tr>";
            htmlcontent += "<tr><th>Item</th><th>Price</th><th>Quantity</th><th>Amount</th><th>VAT</th></tr>";
            htmlcontent += "<tr><td style='height:20px;border-top:1px solid black' colspan=5></td></tr>";

            float totalamount = 0;
            float totalvat = 0;

            foreach(var item in thebills)
            {
                string vat = ((5f / 100f) * item.total).ToString("0.00");
                float amountminusvat = item.total - float.Parse(vat);

                htmlcontent += "<tr><td align='center'>" + item.name + "</td>";
                htmlcontent += "<td align='center'>" + item.price + "</td>";
                htmlcontent += "<td align='center'>" + item.quantity + "</td>";
                htmlcontent += "<td align='center'>" + amountminusvat + "</td>";
                htmlcontent += "<td align='center'>" + vat + "</td></tr>";

                totalamount += amountminusvat;
                totalvat += float.Parse(vat);
            }

            float finalamount = totalamount + totalvat;

            htmlcontent += "<tr><td style='height:20px;'></td><td></td><td></td><td></td><td></td></tr>";
            htmlcontent += "<tr><td style='height:20px;border-top:1px solid black;' colspan=5></td></tr>";

            htmlcontent += "<tr><td></td><td></td><td align='center'></td><td align='center'>" + totalamount.ToString("0.00") + "</td><td>"+totalvat.ToString("0.00")+"</td></tr>";
            htmlcontent += "<tr><td style='height:20px;' colspan=5></td></tr>";
            htmlcontent += "<tr><td align='center' colspan=5>Total : "+ finalamount.ToString("0.00") +"</td></tr>";
            htmlcontent += "<tr><td style='height:30px;' colspan=5></td></tr>";
            htmlcontent += "<tr><td colspan=5 align='center'>THANK YOU!!    VISIT AGAIN!!!</td></tr>";

            htmlcontent += "</table>";

            totalamount = 0;

            var config = new PdfGenerateConfig();
            config.PageOrientation = PageOrientation.Landscape;
            config.ManualPageSize = new PdfSharpCore.Drawing.XSize(500, 297);

            PdfGenerator.AddPdfPages(document, htmlcontent, config);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            return File(response, "application/pdf");
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



    public class Bill
    {

        public string name { get; set; }
        public float price { get; set; }
        public float quantity { get; set; }
        public float total { get; set; }
    }
}