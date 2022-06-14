using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationBankRecord.Interface
{
    public interface IBankRecordApplication
    {
        Task AddBankRecord(BankRecordDTO bankRecord);
        Task<List<BankRecord>> GetAllBankRecord(PageParamenters pageParamenters);
        Task<BankRecord> GetBankRecordById(Guid Id);
        Task<BankRecord> GetBankRecordByOriginId(Guid Id);
        Task PutBankRecord(Guid id, BankRecordDTO cashbook);
    }
}
