namespace Service.Domain;

public class BuyerModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public IReadOnlyList<string> MailingChannels { get; init; }
    
    public List<OrderModel> Orders { get; init; }
}