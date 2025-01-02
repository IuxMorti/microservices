using System;
using Service.Domain;

namespace Service.Application;

public class NotificationStatusDto
{
    public int CountRetries { get; init; }
    public DateTime CreationTime { get; init; }
    public DateTime DispatchTime { get; init; }
    public DateTime ResponseTime { get; init; }

    public NotificationStatus Status { get; init; }
}