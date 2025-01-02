using Microsoft.EntityFrameworkCore;
using RandomStringCreator;
using Service.Domain;

namespace Service.Persistence.Repositories;

public class BuyerRepository : IBuyerRepository
{
    private readonly ApplicationContext _context;

    public BuyerRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<BuyerModel> CreateBuyerAsync()
    {
        var creator = new StringCreator();

        var buyer = new BuyerModel()
        {
            Id = Guid.NewGuid(),
            MailingChannels = new[] { "push", "sms", "email" }.Take(Random.Shared.Next(1, 3)).ToList(),
            Name = new StringCreator().Get(15)
        };

        await _context.Buyers.AddAsync(buyer);

        return buyer;
    }

    public async Task<IReadOnlyList<BuyerModel>> GetAllBuyerAsync()
    {
        return _context.Buyers.ToList();
    }

    public async Task<BuyerModel> GetBuyerById(Guid id)
    {
        return await _context.Buyers.Where(b => b.Id == id).SingleAsync();
    }
}