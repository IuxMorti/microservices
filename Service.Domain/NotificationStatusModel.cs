namespace Service.Domain;

public class NotificationStatusModel
{
    public Guid Id { get; init; }
    
    public int CountRetries { get; init; }
    public DateTime CreationTime { get; init; }
    public DateTime DispatchTime { get; init; }
    public DateTime ResponseTime { get; init; }

    public NotificationStatus Status { get; init; }
    
}



public enum NotificationStatus
{
    NoSend,
    OK,
}