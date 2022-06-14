using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;
using System;
using System.Threading.Tasks;

namespace BankRecordAPIClient
{
    public interface IBankRecordClient
    {
        Task<bool> PostCashBank(BankRecordDTO bankRecord);
        BankRecordDTO MessageBody(Origin origin, Guid originId, string description, BankRecordDomain.Entities.Type type, decimal amount);
    }
}
