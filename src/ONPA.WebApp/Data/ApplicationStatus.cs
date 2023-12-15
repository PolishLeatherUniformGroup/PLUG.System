namespace ONPA.WebApp.Data;

public enum ApplicationStatus
{
    Received = 0,
    Validated = 1,
    Invalid = 2,
    NotRecomended = 3,
    AwaitDecision = 4,
    Accepted = 5,
    Rejected = 6,
    RejectionAppealed = 7,
    AppealSuccessful = 8,
    AppealRejected = 9
}