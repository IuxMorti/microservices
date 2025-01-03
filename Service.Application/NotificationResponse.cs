using System;
using Service.Domain;

namespace Service.Application;

public class NotificationResponse
{
    public DateTime CreationTime { get; init; }
    public DateTime DispatchTime { get; init; }
    public NotificationStatus Status { get; init; }
    
    public MessageDto? MessageDto { get; init; }
    
}