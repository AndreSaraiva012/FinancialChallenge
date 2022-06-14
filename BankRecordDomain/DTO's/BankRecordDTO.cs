using BankRecordDomain.Entities;
using System;

namespace BankRecordDomain.DTO_s
{
    public class BankRecordDTO
    {
        public Origin? Origin { get; set; }
        public Guid? OriginId { get; set; }
        public string Description { get; set; }
        public Entities.Type Type { get; set; }
        public decimal Amount { get; set; }
    }
}
