namespace ONPA.Gatherings.Contract.Requests.Dtos;

public record CreateEnrollment(string FirstName, string LastName, string Email, List<Participant> Participants, DateTime EnrollDate);