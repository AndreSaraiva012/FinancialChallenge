using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;
using BankRecordDomain.Models;
using Infrastructure.Pagination;
using System;
using System.Threading.Tasks;

namespace ApplicationBR.Interfaces
{
    public interface IBankRecordApplication
    {
        Task AddBankRecord(BankRecordDTO bankRecord);
        Task<BankRecordModel> GetAllBankRecord(PageParamenters pageParamenters);
        Task<BankRecord> GetBankRecordById(Guid Id);
        Task<BankRecord> GetBankRecordByOriginId(Guid Id);
        Task<BankRecord> PutBankRecord(Guid id, BankRecordDTO bankRecord);
    }
}
