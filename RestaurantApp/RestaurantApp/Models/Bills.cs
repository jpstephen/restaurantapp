using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Models
{
    public class Bills
    {
            [Key]
            public int BillId { get; set; }
            public string? addSignatureInBill { get; set; }

            public string? addDeliveryInBill { get; set; }

            public string? addMobileNo { get; set; }

            public string? itemsadded { get; set; }

            public float? vatAmount { get; set; }

            public float? billAmountWithoutVat { get; set; }

            public float? billAmountWithVat { get; set; }

            public string? billDeleted { get; set; } = "N";

            public DateTime deletedTime { get; set; }

            public DateTime createdTime { get; set; } = DateTime.Now;
      
    }
}
