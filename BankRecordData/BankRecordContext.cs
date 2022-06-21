using BankRecordDomain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DataBankRecord
{
    public class BankRecordContext : DataContext
    {
        public BankRecordContext(DbContextOptions<BankRecordContext> options) : base(options) { }
        public DbSet<BankRecord> BankRecords { get; set; }

    }
}
