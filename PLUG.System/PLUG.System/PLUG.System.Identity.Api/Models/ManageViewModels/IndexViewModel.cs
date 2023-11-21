﻿using Microsoft.AspNetCore.Identity;

namespace PLUG.System.Identity.Api.Models.ManageViewModels
{
    public record IndexViewModel
    {
        public bool HasPassword { get; init; }

        public IList<UserLoginInfo> Logins { get; init; }

        public string PhoneNumber { get; init; }

        public bool TwoFactor { get; init; }

        public bool BrowserRemembered { get; init; }
    }
}
