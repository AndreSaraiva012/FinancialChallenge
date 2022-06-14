using BankRecordDomain.Entities;
using Infrastructure.Pagination;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBankRecord.Repository
{
    public class BankRecordRepository : GenericRepository<BankRecord>, IBankRecordRepository
    {
        private readonly BankRecordContext _context;

        public BankRecordRepository(BankRecordContext context) : base(context)
        {
            _context = context;
        }

        public List<BankRecord> GetAll(PageParamenters pageParamenters)
        {
            return _context.Set<BankRecord>().OrderBy(x => x.Id)
                                             .Skip((pageParamenters.PageNumber - 1) * pageParamenters.PageSize)
                                             .Take(pageParamenters.PageSize)
                                             .AsNoTracking().ToList();
        }

        public async Task<BankRecord> GetByIdAsync(Guid id)
        {
            return await _context.Set<BankRecord>()
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<BankRecord> GetByOriginIdAsync(Guid id)
        {
            return await _context.Set<BankRecord>()
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(e => e.OriginId == id);
        }
    }
}
