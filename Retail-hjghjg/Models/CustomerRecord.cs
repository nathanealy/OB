using System.ComponentModel.DataAnnotations;

namespace Retail.Models
{
    public class CustomerRecord
    {
        [Key]
        public int ContactId { get; set; }
        // user ID from AspNetUser table.
        public string? OwnerID { get; set; }
        public string? fullName { get; set; }
        public DateTime birthDate { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? zipcode { get; set; }
        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }

}
