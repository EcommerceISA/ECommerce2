using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce2.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage ="You must select a {0}")]
        [Display(Name ="Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(256, ErrorMessage ="The field {0} must be maximum {1} characters length")]
        [Display(Name ="E-Mail")]
        [Index("Customer_UserName_Index", IsUnique =true)]
        public string UserName { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50, ErrorMessage ="The field {0} must be maximum {1} characters")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50, ErrorMessage ="The field {0} must be maximum {1} characters")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(20, ErrorMessage ="The field {0} must be maximum {1} characters length")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(256, ErrorMessage ="the field {0} must be maximum {1} characters")]
        public string Address { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage ="You must select a {0}")]
        [Display(Name ="State")]
        public int StateId { get; set; }

        [Required(ErrorMessage ="the field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage ="The field {0} must be maximum {1} characters")]
        [Display(Name ="City")]
        public int CityId { get; set; }

        [Display(Name ="Customer")]
        public string FullName { get {return string.Format("{0} {1}", FirstName, LastName); } }

        public virtual State State { get; set; }
        public virtual City City { get; set; }
        public virtual Company Company { get; set; }
        public ICollection<Order> Orders { get; set; }


    }
}