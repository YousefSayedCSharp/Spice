using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Coupon Type")]
        public string CouponType { get; set; }

        public enum ECouponType {Percent=0,Doller=1}

        public double Discount { get; set; }

        [Display(Name = "Minimum Ammount")]
        public double MinimumAmmount { get; set; }

        public byte[] Picture { get; set; }

        [Required]
        [Display(Name="Is Active")]
        public bool IsActive { get; set; }
    }
}
