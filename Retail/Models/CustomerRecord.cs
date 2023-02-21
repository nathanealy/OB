using System.ComponentModel.DataAnnotations;

namespace Retail.Models
{
    public class CustomerRecord
    {
        [Key]
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

    }

}
