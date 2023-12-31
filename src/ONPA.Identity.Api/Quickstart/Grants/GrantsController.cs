// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace ONPA.Identity.Api.Quickstart.Grants;

/// <summary>
/// This sample controller allows a user to revoke grants given to clients
/// </summary>
[SecurityHeaders]
[Authorize]
public class GrantsController : Controller
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clients;
    private readonly IResourceStore _resources;
    private readonly IEventService _events;

    public GrantsController(IIdentityServerInteractionService interaction,
        IClientStore clients,
        IResourceStore resources,
        IEventService events)
    {
        this._interaction = interaction;
        this._clients = clients;
        this._resources = resources;
        this._events = events;
    }

    /// <summary>
    /// Show list of grants
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return this.View("Index", await this.BuildViewModelAsync());
    }

    /// <summary>
    /// Handle postback to revoke a client
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Revoke(string clientId)
    {
        await this._interaction.RevokeUserConsentAsync(clientId);
        await this._events.RaiseAsync(new GrantsRevokedEvent(this.User.GetSubjectId(), clientId));

        return this.RedirectToAction("Index");
    }

    private async Task<GrantsViewModel> BuildViewModelAsync()
    {
        var grants = await this._interaction.GetAllUserGrantsAsync();

        var list = new List<GrantViewModel>();
        foreach (var grant in grants)
        {
            var client = await this._clients.FindClientByIdAsync(grant.ClientId);
            if (client != null)
            {
                var resources = await this._resources.FindResourcesByScopeAsync(grant.Scopes);

                var item = new GrantViewModel()
                {
                    ClientId = client.ClientId,
                    ClientName = client.ClientName ?? client.ClientId,
                    ClientLogoUrl = client.LogoUri,
                    ClientUrl = client.ClientUri,
                    Description = grant.Description,
                    Created = grant.CreationTime,
                    Expires = grant.Expiration,
                    IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                    ApiGrantNames = resources.ApiScopes.Select(x => x.DisplayName ?? x.Name).ToArray()
                };

                list.Add(item);
            }
        }

        return new GrantsViewModel
        {
            Grants = list
        };
    }
}
