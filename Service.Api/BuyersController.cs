using Microsoft.AspNetCore.Mvc;
using Service.Application;
using Service.Persistence.Repositories;

namespace Service.Api;

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