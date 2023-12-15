namespace ONPA.Membership.Contract;

public static class Routes
{
    public const string SingleMember = "{memberId}";
    public const string SingleMemberFees = "{memberId}/fees";
    public const string SingleMemberFee = "{memberId}/fees/{feeId}";
    public const string SingleMemberSuspensions = "{memberId}/suspensions";
    public const string SingleMemberExpels = "{memberId}/expels";
    public const string SingleMemberSuspensionsAppeal = "{memberId}/suspensions/appeal";
    public const string SingleMemberExpelsAppeal = "{memberId}/expels/appeal";
    public const string SingleMemberType = "{memberId}/type";
    public const string SingleMemberExpiration = "{memberId}/expiration";
}