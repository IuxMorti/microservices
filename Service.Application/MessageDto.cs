using System;

namespace Service.Application;

public record MessageDto(string ChannelType,
    string Recipient,
    string ContentType,
    string ContentData,
    Guid BuyerId,
    Guid OrderId);