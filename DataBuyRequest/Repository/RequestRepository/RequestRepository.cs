using BuyRequestDomain.Entities;
using Infrastructure.ErrorMessage;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataBuyRequest.Repository.RequestRepository
{
    public class RequestRepository : GenericRepository<BuyRequest>, IRequestRepository
    {
        private readonly BuyRequestContext _context;

        public RequestRepository(BuyRequestContext context) : base(context)
        {
            _context = context;
            SetInclude(x => x.Include(i => i.Products));
        }

    }
}

