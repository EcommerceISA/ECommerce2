using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ECommerce2.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }

        [Required(ErrorMessage ="The field {0} is required ")]
        [Range(1, double.MaxValue, ErrorMessage ="You must select a {0}")]
        [Index("Warehouse_CompanyId_Name_Index", 1, IsUnique =true)]
        [Display(Name="Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50, ErrorMessage ="The field {0} must be maximun {1} characters length")]
        [Display(Name= "Warehouse")]
        [Index("Warehouse_CompanyId_Name_Index", 2, IsUnique =true)]
        public string Name { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50, ErrorMessage ="The field {0} must select a {1}")]
        [Display(Name="Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        
        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(100, ErrorMessage ="The field {0} must be maximum {1} characters length")]
        [Display(Name="Address")]
        public string Address { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage ="You must select a {0} ")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage ="You must select a {0}")]
        [Display(Name="City")]
        public int CityId { get; set; }

        public virtual Company Company { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}