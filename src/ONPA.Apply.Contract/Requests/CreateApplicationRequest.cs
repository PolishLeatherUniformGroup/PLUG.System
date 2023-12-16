using ONPA.Common.Infrastructure;

namespace ONPA.Apply.Contract.Requests;

public sealed record CreateApplicationRequest(string FirstName, string LastName, string Email, string Phone, string Address, string[] Recommenders):MultiTenantRequest
{
}