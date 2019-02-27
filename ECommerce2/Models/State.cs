using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce2.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50, ErrorMessage ="The field {0} be must maximum the {1} characters")]
        [Display(Name ="State")]
        [Index("State_Name_Index", IsUnique =true)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

    }
}