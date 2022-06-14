using BuyRequestDomain.DTO_s;
using BuyRequestDomain.Entities;
using BuyRequestDomain.ViewModels;
using Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationBuyRequest.Interfaces
{
    public interface IBuyRequestApplication
    {
        Task<BuyRequest> AddBuyRequest(BuyRequestDTO buyRequest);
        Task<List<BuyRequest>> GetAllBuyRequest(PageParamenters pageParameters);
        Task<BuyRequest> GetBuyRequestsById(Guid id);
        Task<BuyRequest> GetBuyRequestsByClient(Guid ClientId);
        Task<BuyRequest> UpdateBuyRequest(BuyRequestDTO buyRequest);
        Task<BuyRequest> UpdateBuyRequestStatus(Guid id, Status state);
        Task<BuyRequest> DeleteBuyRequest(Guid id);
    }
}
