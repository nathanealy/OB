// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Retail.Areas.Identity.Data;
using Retail.Data;
using Retail.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Retail.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<RetailUser> _signInManager;
        private readonly UserManager<RetailUser> _userManager;
        private readonly IUserStore<RetailUser> _userStore;
        private readonly IUserEmailStore<RetailUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<RetailUser> userManager,
            IUserStore<RetailUser> userStore,
            SignInManager<RetailUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<RetailUser>)GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Birth Date")]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The Social Security Number must be at least 6 and at max 9 characters long.", MinimumLength = 6)]
            [Display(Name = "Social Security Number")]
            [DataType(DataType.Text)]
            public string SSN { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.Name = Input.Name;
                user.DOB = Input.DOB;
                user.SocialSecurityNumber = Input.SSN;

                var userQuery = _context.RetailUser.Where(r => r.SocialSecurityNumber == Input.SSN);

                if (!userQuery.Any())
                {
                    await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                    var result = await _userManager.CreateAsync(user, Input.Password);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Create account data.");

                        var status = CreateBankAccounts(user.SocialSecurityNumber);

                        _logger.LogInformation("User created a new account with password.");

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }

                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Social Security Number already exists");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private string CreateBankAccounts(string ssn)
        {
            var numberOfAccountsGenerator = new Random();
            var numberOfAccounts = numberOfAccountsGenerator.Next(1, 10);

            string[] accountNames = { "Free Checking", "Everything Checking", "No Minimum Checking", "Fashion Checking", "All-Star Checking", "Pioneer Checking", "Steelers Checking", "Pirates Checking", "Penguins Checking", "Maulers Checking" };

            for (var i = 1; i <= numberOfAccounts; i++)
            {
                var accountNumberGenerator = new Random();
                var accountNumber = accountNumberGenerator.Next(1, 999999999);

                AccountInformation accountInformation = new AccountInformation();
                accountInformation.AccountNumber = accountNumber.ToString().PadRight(9, '0');

                var descriptionGenerator = new Random();
                var c = descriptionGenerator.Next(0,9);

                accountInformation.Description = accountNames[c];
                accountInformation.Nickname = accountNames[c];

                var balanceGenerator = new Random();
                var balance = balanceGenerator.Next(100, 1000000);

                accountInformation.AccountBalance = balance;
                accountInformation.AvailableBalance = balance;
                accountInformation.HoldBalance = 0;
                accountInformation.HoldsTotal = 0;

                _context.AccountInformation.Add(accountInformation);

                AssociatedAccount associatedAccount= new AssociatedAccount();

                associatedAccount.SocialSecurityNumber = ssn;
                associatedAccount.AccountNumber = accountNumber.ToString().PadRight(9, '0');

                _context.AssociatedAccount.Add(associatedAccount);

                var transactionNumberGenerator = new Random();
                var transactionNumber = transactionNumberGenerator.Next(2, 20);

                var transactionTypeGenerator = new Random();
                var transactionGenerator = new Random();
                
                for (var x = 1; x <= transactionNumber; x++)
                {
                    AccountActivity accountActivity= new AccountActivity();
                    
                    accountActivity.Account = accountNumber.ToString().PadRight(9, '0');
                    accountActivity.PostDate = DateTime.Now;
                    accountActivity.TransactionDate = DateTime.Now;
                    accountActivity.Description = "Transaction";

                    var transactionType = transactionTypeGenerator.Next(2);

                    string[] types = { "D", "C" };

                    accountActivity.TransactionType = types[transactionType];
                    accountActivity.TransactionBalance = balance;
                    accountActivity.TransactionAmount = transactionGenerator.Next(10, 4000);

                    _context.AccountActivity.Add(accountActivity);  
                }

            }

            try
            {
                _context.SaveChanges();
            } catch
            {
                return "error";
            }

            return "OK";
        }

        private RetailUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<RetailUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(RetailUser)}'. " +
                    $"Ensure that '{nameof(RetailUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<RetailUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<RetailUser>)_userStore;
        }
    }
}
