using System.ComponentModel.DataAnnotations;

namespace NUProjectDemo.Models
{
    public class Products
    {
        [Key]
        public int idproducts { get; set; }
        public string productname { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public DateTime datetimeadded { get; set; }
        public string description { get; set; }

    }
}
