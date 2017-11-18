using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce2.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50, ErrorMessage ="The field {0} must be maximum {1} characters")]
        [Display(Name ="City")]
        [Index("City_Name_Index", 2, IsUnique =true)]
        public string Name { get; set; }

        [Required(ErrorMessage ="the field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage ="You must select a {0}")]
        [Display(Name ="State")]
        [Index("City_Name_Index", 1, IsUnique = true)]
        public int StateId { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }

    }
}