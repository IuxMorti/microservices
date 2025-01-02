using Service.Domain;

namespace Service.Persistence.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly ApplicationContext _context;

    public OrderRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<OrderModel> CreateOrderToBuyer(Guid buyerId)
    {
        var order = new OrderModel()
        {
            BuyerModelId = buyerId,
            Id = Guid.NewGuid()
        };
        
        await _context.Order.AddAsync(order);

        return order;
    }
}