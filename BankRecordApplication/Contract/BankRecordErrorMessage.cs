using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;

namespace ApplicationBankRecord.Contract
{
    public class BankRecordErrorMessage : ErrorMessage<BankRecordDTO>
    {
        public BankRecordErrorMessage(string code, string error, BankRecordDTO entity) : base(code, error, entity)
        {

        }
    }
}
