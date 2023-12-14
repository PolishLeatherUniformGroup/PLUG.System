namespace ONPA.WebApp.Data;

public static class ApplicationStatusExtensions
{
    public static string ToDisplayString(this ApplicationStatus status)
    {
        return status switch
        {
            ApplicationStatus.Received => "Wpłynął",
            ApplicationStatus.Validated => "Zweryfikowany",
            ApplicationStatus.Invalid => "Nieprawidłowy",
            ApplicationStatus.NotRecomended => "Nie rekomendowany",
            ApplicationStatus.AwaitDecision => "Oczekuje decyzji",
            ApplicationStatus.Accepted => "Zaakceptowany",
            ApplicationStatus.Rejected => "Odrzucony ",
            ApplicationStatus.RejectionAppealed => "W odwołaniu",
            ApplicationStatus.AppealSuccessful => "Odwołanie przyjęte",
            ApplicationStatus.AppealRejected => "Odwołanie odrzucone",
            _ => ""
        };
    }

    public static string ToBackgroundColor(this ApplicationStatus status)
    {
        return status switch
        {
            ApplicationStatus.Received => "#605f5e;",
            ApplicationStatus.Validated => "#605f5e;",
            ApplicationStatus.Invalid => "#fb3640;",
            ApplicationStatus.NotRecomended => "#fb3640;",
            ApplicationStatus.AwaitDecision => "#605f5e;",
            ApplicationStatus.Accepted => "#244f26;",
            ApplicationStatus.Rejected => "#fb3640;",
            ApplicationStatus.RejectionAppealed => "#605f5e;",
            ApplicationStatus.AppealSuccessful => "#244f26;",
            ApplicationStatus.AppealRejected => "#fb3640;",
            _ => "#605f5e;"
        };
    }
}