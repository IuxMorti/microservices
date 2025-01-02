using Service.Application;
using Service.Domain;

namespace Service.Persistence.Repositories;

public interface IBuyerRepository
{
    Task<BuyerModel> CreateBuyerAsync();
    Task<IReadOnlyList<BuyerModel>> GetAllBuyerAsync();
    Task<BuyerModel> GetBuyerById(Guid id);
}

public interface IOrderRepository
{
    Task<OrderModel> CreateOrderToBuyer(Guid buyerId);
}

public class NotificationStatusRepository : INotificationStatusRepository
{
    private readonly ApplicationContext _context;

    public NotificationStatusRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task CreateOrUpdateStatus(NotificationStatusDto statusModel)
    {
        await _context.Statuses.AddAsync(new NotificationStatusModel()
        {
            Id = Guid.NewGuid(),
            Status = statusModel.Status,
            CountRetries = statusModel.CountRetries,
            CreationTime = statusModel.CreationTime,
            DispatchTime = statusModel.DispatchTime,
        });
    }
}