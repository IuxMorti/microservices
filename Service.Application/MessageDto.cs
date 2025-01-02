using System;

namespace Service.Application;

public record MessageDto(string ChannelType,
    string ContactInformation,
    string Message,
    Guid BuyerId,
    Guid OrderId);