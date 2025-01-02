using Microsoft.AspNetCore.Mvc;
using Service.Application;
using Service.Notification.Gateway.Client;
using Service.Persistence.Repositories;

namespace urfu_microservices_4;

[Route("v1/api/buyers")]
public class BuyersController : Controller
{
    private readonly IBuyerRepository _buyerRepository;

    public BuyersController(IBuyerRepository buyerRepository)
    {
        _buyerRepository = buyerRepository;
    }
    
    [HttpPost]
    public async Task<ActionResult<List<Guid>>> CreateBuyers([FromBody] CreateBuyersDto createBuyersDto)
    {
        var result = new List<Guid>(createBuyersDto.Count);
        
        for (var i = 0; i < createBuyersDto.Count; i++)
        {
            var buyer = await _buyerRepository.CreateBuyerAsync();
            
            result.Add(buyer.Id);
        }

        return result;
    }

}

public class CreateBuyersDto
{
    public int Count { get; init; }
}



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
                    Faker.Lorem.Paragraph(),
                    buyer.Id,
                    orderId
                )
            );
        }

        return orderId;
    }
}