using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;
using Bogus;
using System;
using System.Collections.Generic;

namespace BankRecord.UnitTest.AutoFaker
{
    public class BankRecordAutoFaker
    {
        public static BankRecordDTO GenerateBankRecordDTO()
        {
            BankRecordDTO bankRecordDTO = new Faker<BankRecordDTO>()
           .RuleFor(x => x.Origin, x => x.PickRandom<Origin>())
           .RuleFor(x => x.OriginId, Guid.NewGuid())
           .RuleFor(x => x.Description, x => x.Random.String(1, 50))
           .RuleFor(x => x.Type, BankRecordDomain.Entities.Type.Receive)
           .RuleFor(x => x.Amount, x => x.Random.Decimal(1, 100));

            return bankRecordDTO;
        }

    }
}