using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Models
{
    public class VendorBills
    {
        [Key]
        public int VendorBillId { get; set; }

        public int VendorId { get; set; }

        [Required]
        public float AmountPaid { get; set; }

        public DateTime VendorBillDate { get; set; } = DateTime.Now;
    }
}
