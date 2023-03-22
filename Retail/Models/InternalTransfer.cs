using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Retail.Models;

public partial class InternalTransfer
{
    [Key]
    public long Id { get; set; }

    [StringLength(20, ErrorMessage = "Maximum account length is 20 characters")]
    [Required(ErrorMessage = "Please select From Account")]
    [DisplayName("From Account")]
    public string FromAccount { get; set; } = null!;

    [StringLength(20, ErrorMessage = "Maximum account length is 20 characters")]
    [Required(ErrorMessage = "Please select To Account")]
    [DisplayName("To Account")]
    public string ToAccount { get; set; } = null!;

    [Column(TypeName = "money")]
    [DisplayName("Amount")]
    public decimal Amount { get; set; }

    [StringLength(1, ErrorMessage = "Maximum scheduling option length is 1 character")]
    [Required(ErrorMessage = "Please select To Scheduling Option")]
    [DisplayName("Scheduling Option")]
    public string SchedulingOption { get; set; } = null!;

    [DisplayName("Transfer Date")]
    [Column(TypeName = "date")]
    public DateTime? TransferDate { get; set; }

    [StringLength(1, ErrorMessage = "Maximum frequency length is 1 character")]
    [DisplayName("Freqency")]
    public string? Frequency { get; set; }

    [StringLength(1, ErrorMessage = "Maximum delivery length is 1 character")]
    [DisplayName("Delivery")]
    public string? Delivery { get; set; }

    [Column(TypeName = "date")]
    [DisplayName("End By")]
    public DateTime? EndBy { get; set; }

    [DisplayName("Number of Transfers")]
    public long? NumberOfTransfers { get; set; }

    [StringLength(30, ErrorMessage = "Maximum memo length is 20 character")]
    public string? Memo { get; set; }
}
