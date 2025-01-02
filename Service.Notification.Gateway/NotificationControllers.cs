using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Service.Application.Gateway;

[Route("api/v1/notifications")]
public class NotificationControllers : Controller
{
    private readonly INotificationProducer _producer;

    public NotificationControllers(INotificationProducer producer)
    {
        _producer = producer;
    }


    [HttpPost]
    public async Task<ActionResult> SendMessage([FromBody] MessageDto messageDto)
    {
        await _producer.ProduceAsync(messageDto);

        return NoContent();
    }
}