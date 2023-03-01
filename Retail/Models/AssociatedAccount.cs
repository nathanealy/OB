using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Retail.Models;

public partial class AssociatedAccount
{
    [Key]
    public long Id { get; set; }

    [Required]
    [DisplayName("Social Security Number")]
    [MaxLength(9, ErrorMessage = "Maximum characters 9")]
    public string SocialSecurityNumber { get; set; } = null!;

    [Required]
    [DisplayName("Account Number")]
    [MaxLength(20, ErrorMessage = "Maximum characters 20")]
    public string AccountNumber { get; set; } = null!;
}
