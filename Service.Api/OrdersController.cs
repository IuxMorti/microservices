using Microsoft.AspNetCore.Mvc;
using Service.Application;
using Service.Notification.Gateway.Client;
using Service.Persistence.Repositories;

namespace Service.Api;

[Route("v1/api/orders")]
public class OrdersController : Controller
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly INotificationClient _notificationClient;

    public OrdersController(IBuyerRepository buyerRepository, INotificationClient notificationClient)
    {
        _buyerRepository = buyerRepository;
        _notificationClient = notificationClient;
    }
    
    [HttpPost("{userId}")]
    public async Task<ActionResult<Guid>> CreateOrder([FromRoute]Guid userId)
    {
        var buyer =  await _buyerRepository.GetBuyerById(userId);
        var orderId = Guid.NewGuid();
        
        foreach (var type in buyer.MailingChannels)
        {
            var contactInformation = type switch
            {
                "sms" => Faker.Phone.Number(),
                "email" => Faker.Internet.Email(),
                "push" => Guid.NewGuid().ToString()[..23],
                _ => throw new ArgumentOutOfRangeException()
            };

            await _notificationClient.SengMessage(
                new MessageDto(
                    type,
                    contactInformation,
                    "text",
                    Faker.Lorem.Paragraph(),
                    buyer.Id,
                    orderId
                )
            );
        }

        return orderId;
    }
}