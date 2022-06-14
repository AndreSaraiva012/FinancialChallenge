namespace BuyRequestDomain.Entities
{
    public enum Status
    {
        Received = 1,
        AwaitingDelivery = 2,
        AwaitingDownload = 3,
        Concluded = 4
    }
}
