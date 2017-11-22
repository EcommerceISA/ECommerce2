using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce2.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50, ErrorMessage ="The field {0} must be maximum {1} characters length")]
        [Index("Status_description_Index", IsUnique =true)]
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}