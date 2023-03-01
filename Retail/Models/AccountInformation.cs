using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Retail.Models;

public partial class AccountInformation
{
    [Required]
    [Key]
    [MaxLength(20, ErrorMessage = "Maximum characters 20")]
    [DisplayName("Account Number")]
    public string AccountNumber { get; set; } = null!;

    [Required]
    [Precision(13, 2)]
    [DisplayName("Account Balance")]
    public decimal AccountBalance { get; set; }

    [Required]
    [Precision(13, 2)]
    [DisplayName("Available Balance")]
    public decimal AvailableBalance { get; set; }

    [Required]
    [Precision(13, 2)]
    [DisplayName("Hold Balance")]
    public decimal HoldBalance { get; set; }

    [Required]
    [Precision(13, 2)]
    [DisplayName("Hosts Total")]
    public decimal HoldsTotal { get; set; }

    [Required]
    [MaxLength(20, ErrorMessage = "Maximum characters 20")]
    [DisplayName("Account Description")]
    public string Description { get; set; } = null!;

    [Required]
    [MaxLength(20, ErrorMessage = "Maximum characters 20")]
    [DisplayName("Account Nickname")]
    public string Nickname { get; set; } = null!;
}
