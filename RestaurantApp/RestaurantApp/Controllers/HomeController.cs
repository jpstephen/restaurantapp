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

        public async Task<IActionResult> Index()
        {

            IEnumerable<Items> allItems = _context.MItems;
            return View(allItems);
        }

        [HttpGet]
        public ActionResult GeneratePDF(string allbills)
        {
            float totalamount = 0;
            float totalvat = 0;
            string htmlcontent = "";
            float finalamount = 0; 

            List<Bill>? allItemsadded = new List<Bill>();

            var thebills = JsonConvert.DeserializeObject<Bills>(allbills);

            if (thebills != null)
            {
                
                if (thebills.itemsadded != null)
                {
                    allItemsadded = JsonConvert.DeserializeObject<List<Bill>>(thebills.itemsadded);
                }
            }

            var document = new PdfDocument();

            

            
            htmlcontent += "<table class='table-condensed'>";
            htmlcontent += "<tr><td colspan=5 style='padding-right:25px;' align='right'>" + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt") + "</td></tr>";
            htmlcontent += "<tr><td colspan=5 align='center'><p style='font-size:20px;'><strong>LE LAZARO CAFE</strong></p></td></tr>";
            htmlcontent += "<tr><td colspan=5 align='center'>Hamdan Street, behind Dama's Gold" +
                            "<br/>Abu Dhabi, UAE.<br/>"+
                            "02 633 7336 / 050 780 1413</td></tr>";
            htmlcontent += "<tr><td colspan=5 align='center'>TRN : 100383785100003</td></tr>";
            htmlcontent += "<tr><td colspan=5 align='center'><h3>TAX INVOICE</h3></td></tr>";
            htmlcontent += "<tr><td colspan=5 align='left' style='border-bottom:1px solid black'>Bill No. : {0}</td></tr>";
            htmlcontent += "<tr><th width='35%'>Item</th><th width='15%'>Price</th><th width='15%'>Quantity</th><th width='15%'>VAT</th><th width='10%'>Amount</th></tr>";
            htmlcontent += "<tr><td style='height:20px;border-top:1px solid black' colspan=5></td></tr>";

            if(allItemsadded != null)
            {
                foreach (var item in allItemsadded)
                {
                    string vat = ((5f / 100f) * item.total).ToString("0.00");
                    float amountminusvat = item.total - float.Parse(vat);

                    htmlcontent += "<tr><td align='center'>" + item.name + "</td>";
                    htmlcontent += "<td align='center'>" + item.price + "</td>";
                    htmlcontent += "<td align='center'>" + item.quantity + "</td>";
                    htmlcontent += "<td align='center'>" + vat + "</td>";
                    htmlcontent += "<td align='center'>" + amountminusvat + "</td></tr>";

                    totalamount += amountminusvat;
                    totalvat += float.Parse(vat);
                }
            }

            

            finalamount = totalamount + totalvat;

            htmlcontent += "<tr><td style='height:20px;'></td><td></td><td></td><td></td><td></td></tr>";
            htmlcontent += "<tr><td style='height:20px;border-top:1px solid black;' colspan=5></td></tr>";

            htmlcontent += "<tr><td></td><td></td><td align='center'></td><td align='center'>" + totalvat.ToString("0.00") + "</td><td>"+totalamount.ToString("0.00")+"</td></tr>";

            htmlcontent += "<tr><td align='center' colspan=5 style='font-size:20px;'>Total : " + finalamount.ToString("0.00") +"</td></tr>";
            htmlcontent += "<tr><td style='height:5px;' colspan=5></td></tr>";


            if (thebills != null)
            {
                thebills.vatAmount = totalvat;
                thebills.billAmountWithoutVat = totalamount;
                thebills.billAmountWithVat = finalamount;
                if (thebills.addDeliveryInBill != "" && thebills.addDeliveryInBill != null)
                {
                    htmlcontent += "<tr><td align='center'>Delivery to : </td><td colspan=4 align='left'>" + thebills.addDeliveryInBill + "</td></tr>";
                }
                if (thebills.addMobileNo != "" && thebills.addMobileNo != null)
                {
                    htmlcontent += "<tr><td align='center'>Mobile no. : </td><td colspan=4 align='left'>" + thebills.addMobileNo + "</td></tr>";
                }
                if (thebills.addSignatureInBill != "" && thebills.addSignatureInBill != null)
                {
                    htmlcontent += "<tr><td style='height:40px;' colspan=5></td></tr>";

                    htmlcontent += "<tr><td align='center'>SIGNATURE</td></tr>";
                }

                _context.allBills.Add(thebills);
                _context.SaveChanges();

                htmlcontent = string.Format(htmlcontent, thebills.BillId.ToString().PadLeft(4, '0'));
            }

            htmlcontent += "<tr><td style='height:30px;' colspan=5></td></tr>";
            htmlcontent += "<tr><td colspan=5 align='center'><b>THANK YOU!! &nbsp;&nbsp;&nbsp; VISIT AGAIN!!!</b></td></tr>";

            htmlcontent += "</table>";

            

            totalamount = 0;



            var config = new PdfGenerateConfig();
            config.PageOrientation = PageOrientation.Landscape;
            config.ManualPageSize = new PdfSharpCore.Drawing.XSize(590, 297);


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
        public string? name { get; set; }
        public float price { get; set; }
        public float quantity { get; set; }
        public float total { get; set; }
    }
}