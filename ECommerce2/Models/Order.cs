using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce2.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage ="You must select a {0}")]
        [Display(Name ="Customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage ="The field {0} is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Status Status { get; set; }
        public virtual Company Company { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}