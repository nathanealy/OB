using System.ComponentModel.DataAnnotations;

namespace Retail.Models
{
    public class CustomerInformation
    {
        [Key]
        public string? id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
    }
}
