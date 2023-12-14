using Microsoft.AspNetCore.Mvc;

namespace ONPA.Organizations.Contract.Requests;

public sealed record CreateOrganizationRequest(string Name, string CardPrefix, string TaxId, string AccountNumber, string Address, string ContactEmail, string? Regon)
{
}

public sealed record UpdateOrganizationDataRequest([FromRoute]Guid OrganizationId,[FromBody]OrganizationData Data)
{
}


public sealed record OrganizationData(string Name, string CardPrefix, string TaxId, string AccountNumber, string Address, string ContactEmail, string? Regon);

public sealed record UpdateOrganizationSettingsRequest([FromRoute]Guid OrganizationId,[FromBody]OrganizationSettings Settings)
{
}

public sealed record OrganizationSettings(int RequiredRecommendations,int DaysForAppeal, int FeePaymentMonth);

public sealed record AddMembershipFeeRequest([FromRoute]Guid OrganizationId,[FromBody]MembershipFee Fee)
{
}

public sealed record MembershipFee(int Year, int Amount, DateTime DueDate);