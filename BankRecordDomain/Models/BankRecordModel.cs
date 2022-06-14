using BankRecordDomain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BankRecordDomain.Models
{
    public class BankRecordModel
    {
        public List<BankRecord> BankRecords { get; set; }
        public decimal Total => BankRecords.Sum(x => x.Amount);
    }
}
