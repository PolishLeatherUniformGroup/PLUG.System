using ONPA.Gatherings.StateEvents;

namespace ONPA.Gatherings.Domain;

public sealed partial class Event
{
    
    public void ApplyChange(EventCreated change)
    {
        this.Name = change.Name;
        this.Description = change.Description;
        this.Regulations = change.Regulations;
        this.Status= EventStatus.Draft;
        this.PricePerPerson = change.PricePerPerson;
        this.PlannedCapacity = change.PlannedCapacity;
        this.PublishDate = change.PublishDate;
        this.ScheduledStart= change.ScheduledStart;
        this.EnrollmentDeadline = change.EnrollmentDeadline;
    }
    
    public void ApplyChange(EventCapacityChanged change)
    {
        this.PlannedCapacity = change.Capacity;
    }
    
    public void ApplyChange(EventScheduleChanged change)
    {
        this.ScheduledStart = change.ScheduledStart;
        this.EnrollmentDeadline = change.EnrollmentDeadline;
        this.PublishDate = change.PublishDate;
    }
    
    public void ApplyChange(EventPriceChanged change)
    {
        this.PricePerPerson = change.Price;
    }
    
    public void ApplyChange(EventDescriptionModified change)
    {
        this.Name = change.Name;
        this.Description = change.Description;
        this.Regulations = change.Regulations;
    }
    
    public void ApplyChange(EnrollmentAddedToEvent change)
    {
        this._registrations.Add(change.Enrollment);
    }
    
    public void ApplyChange(EnrollmentPaymentRegistered change)
    {
        var enrollment = this._registrations.FirstOrDefault(x => x.Id == change.EnrollmentId);
        if (enrollment != null)
        {
            enrollment.RegisterPayment(change.PaidDate, change.PaidAmount);
        }
    }

    public void ApplyChange(EnrollmentCancelled change)
    {
        var enrollment = this._registrations.FirstOrDefault(x => x.Id == change.EnrollmentId);
        if (enrollment != null)
        {
            enrollment.Cancel(change.CancellationDate, change.RefundableAmount);
        }
    }
    
    public void ApplyChange(EnrollmentRefunded change)
    {
        var enrollment = this._registrations.FirstOrDefault(x => x.Id == change.EnrollmentId);
        if (enrollment != null)
        {
            enrollment.Refund(change.RefundDate, change.RefundAmount);
        }
    }
}