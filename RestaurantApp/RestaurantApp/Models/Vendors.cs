using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Models
{
    public class Vendors
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Vendor name")]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
