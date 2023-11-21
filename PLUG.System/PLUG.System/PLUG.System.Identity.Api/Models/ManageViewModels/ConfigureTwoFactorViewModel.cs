﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace PLUG.System.Identity.Api.Models.ManageViewModels
{
    public record ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; init; }

        public ICollection<SelectListItem> Providers { get; init; }
    }
}
