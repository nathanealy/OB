﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Retail.Areas.Identity.Data;

// Add profile data for application users by adding properties to the RetailUser class
public class RetailUser : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }
    [PersonalData]
    public DateTime DOB { get; set; }
    [PersonalData]
    public string? SocialSecurityNumber { get; set; }
}

