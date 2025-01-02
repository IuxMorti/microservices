using System;
using Service.Domain;

namespace Service.Application;

public record MessageDto(string ChannelType, string Message, Guid BuyerId, Guid OrderId);