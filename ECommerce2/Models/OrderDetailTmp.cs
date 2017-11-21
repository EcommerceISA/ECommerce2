﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce2.Models
{
    public class OrderDetailTmp
    {
        [Key]
        public int OrderDetailTmpId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(256, ErrorMessage ="The field {0} must be maximum {1} chacarters length")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(100, ErrorMessage ="The field {0} must be maximum {1} characters length")]
        [Display(Name ="Product")]
        public string Description { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [Range(0, double.MaxValue, ErrorMessage ="You must enter calues in {0} between {1} and {2}")]
        [Display(Name ="Tax Rate")]
        public double TaxRate { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]
        [Range(0, double.MaxValue, ErrorMessage ="You must enter values in {0} between {1} and {2}")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]
        [Range(0, double.MaxValue, ErrorMessage ="You must enter values in {0} between {1} and {2}")]
        public double Quantity { get; set; }

        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]
        public decimal Value { get; set; }

        public virtual Product Product { get; set; }

    }
}