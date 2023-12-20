using ONPA.Common.Application;
using ONPA.Organizations.Contract.Responses;

namespace ONPA.Organizations.Api.Application.Queries;

public sealed record GetOrganizationBySlugQuery(string Slug) : ApplicationQueryBase<TenantInfo?>(Guid.Empty);