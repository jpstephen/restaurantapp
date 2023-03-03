using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Models
{
    public class Items
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        public float Price { get; set; }
    }
}
