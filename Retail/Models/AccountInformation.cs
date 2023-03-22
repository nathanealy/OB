using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Retail.Models;

public partial class AccountInformation
{
    private string _AccountNumber;
    private string _MaskedAccountNumber;
    private decimal _AccountBalance;
    private decimal _AvailableBalance;
    private decimal _HoldBalance;
    private decimal _HoldsTotal;
    private string _Description;
    private string _Nickname;

    [Required]
    [Key]
    [MaxLength(20, ErrorMessage = "Maximum characters 20")]
    [DisplayName("Account Number")]
    public string AccountNumber { 
        get {
            return _AccountNumber.Trim();
        } 
        set { 
            _AccountNumber = value; 
        } 
    }

    [Required]

    [MaxLength(20, ErrorMessage = "Maximum characters 20")]
    [DisplayName("Masked Account Number")]
    public string MaskedAccountNumber
    {
        get
        {
            return _MaskedAccountNumber.Trim();
        }
        set
        {
            _MaskedAccountNumber = value;
        }
    }

    [Required]
    [Precision(13, 2)]
    [DisplayName("Account Balance")]
    public decimal AccountBalance { get { return _AccountBalance; } set { _AccountBalance = value; } }

    [Required]
    [Precision(13, 2)]
    [DisplayName("Available Balance")]
    public decimal AvailableBalance { get { return _AvailableBalance; } set { _AccountBalance = value; } }

    [Required]
    [Precision(13, 2)]
    [DisplayName("Holds Balance")]
    public decimal HoldBalance { get { return _HoldBalance; } set { _HoldBalance = value; } }

    [Required]
    [Precision(13, 2)]
    [DisplayName("Holds Total")]
    public decimal HoldsTotal { get { return _HoldsTotal; } set { _HoldsTotal = value; } }

    [Required]
    [MaxLength(20, ErrorMessage = "Maximum characters 20")]
    [DisplayName("Account Description")]
    public string Description { get { return _Description.Trim(); } set { _Description = value; } }

    [Required]
    [MaxLength(20, ErrorMessage = "Maximum characters 20")]
    [DisplayName("Account Nickname")]
    public string Nickname { get { return _Nickname.Trim(); } set { _Nickname = value; } } 
}
