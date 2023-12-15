using ONPA.Common.Application;

namespace ONPA.Organizations.Api.Application.Commands;

public sealed record CreateOrganizationCommand(Guid TenantId, string Name, string CardPrefix, string TaxId, string AccountNumber, string Address, string ContactEmail, string? Regon) : ApplicationCommandBase(TenantId);