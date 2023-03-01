using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Retail.Models;

public partial class AccountActivity
{
    [Key]
    public long Id { get; set; }

    [Required]
    [DisplayName("Transaction Description")]
    public string Description { get; set; } = null!;

    [Required]
    [DisplayName("Transaction Date")]
    public DateTime TransactionDate { get; set; }

    [Required]
    [DisplayName("Post Date")]
    public DateTime PostDate { get; set; }

    [Required]
    [Precision(13, 2)]
    [DisplayName("Transaction Amount")]
    public decimal TransactionAmount { get; set; }

    [Required]
    [DisplayName("Account Number")]
    [MaxLength(20, ErrorMessage = "Maximum characters 20")]
    public string Account { get; set; } = null!;

    [Required]
    [Precision(13, 2)]
    [DisplayName("Transaction Balance")]
    public decimal TransactionBalance { get; set; }

    [Required]
    [DisplayName("Transaction Type")]
    [MaxLength(1, ErrorMessage = "Maximum characters 1")]
    public string TransactionType { get; set; } = null!;
}
