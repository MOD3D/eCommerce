using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class ShippingDetail
    {
        public int SHIPPING_DETAIL_ID { get; set; }
        [Required]
        public Nullable<int> MEMBER_ID { get; set; }
        [Required]
        public string ADRESS { get; set; }
        [Required]
        public string CITY { get; set; }
        [Required]
        public string STATE { get; set; }
        [Required]
        public string COUNTRY { get; set; }
        [Required]
        public string ZIP_CODE { get; set; }
        public Nullable<int> ORDER_ID { get; set; }
        public Nullable<decimal> AMOUNT_PAID { get; set; }
        [Required]
        public string PAYMENT_TYPE { get; set; }
    }
}
