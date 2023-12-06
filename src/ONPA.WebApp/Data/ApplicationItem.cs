using Microsoft.FluentUI.AspNetCore.Components;

namespace ONPA.WebApp.Data
{
    public class ApplicationItem
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime ApplicationDate { get; set; }
    }

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
}
