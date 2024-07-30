using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eCommerce.Models
{
    public class CategoryDetail
    {
        public int CATEGORY_ID { get; set; }
        [Required(ErrorMessage = "Category Name Required")]
        [StringLength(100, ErrorMessage = "Minimun 3 and minimun 5 and maximum 100 characters are allwed", MinimumLength = 3)]
        public string CATEGORY_NAME { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
    }

    public class ProductDetail
    {
        public int PRODUCT_ID { get; set; }
        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(100, ErrorMessage = "Monimum 3 and minimum 5 and maximum 100 characters are allwed", MinimumLength = 3)]
        public string PRODUCT_NAME { get; set; }
        [Required]
        [Range(1, 50)]
        public Nullable<int> CATEGORY_ID { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public Nullable<System.DateTime> DESCRIPTION { get; set; }
        public string PRODUCT_IMAGE { get; set; }
        public Nullable<bool> IS_FEATURED { get; set; }
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public Nullable<int> QUANTITY { get; set; }
        [Required]
        [Range(typeof(decimal), "1", "200000", ErrorMessage = "Invalid Price")]
        public Nullable<decimal> PRICE { get; set; }
        public SelectList Categories { get; set; }
    }
}