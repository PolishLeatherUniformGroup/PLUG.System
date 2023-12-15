namespace ONPA.Gatherings.Contract.Requests;

public record CreateEnrollment(string FirstName, string LastName, string Email, List<Participant> Participants, DateTime EnrollDate);