using BuyRequestDomain.Entities;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBuyRequest.Repository.ProductRequestRepository
{
    public class ProductRequestRepository : GenericRepository<ProductRequest>, IProductRequestRepository
    {
        private readonly BuyRequestContext _context;

        public ProductRequestRepository(BuyRequestContext context) : base(context)
        {
            _context = context;
        }

    }
}
