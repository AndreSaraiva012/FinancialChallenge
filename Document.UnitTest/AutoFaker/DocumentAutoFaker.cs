using Bogus;
using DocumentDomain.DTO_s;
using DocumentDomain.Entities;
using System;

namespace Document.UnitTest.AutoFaker
{
    public class DocumentAutoFaker
    {
        public static DocumentDTO GenerateDocumentDTO()
        {
            DocumentDTO document = new Faker<DocumentDTO>()
                .RuleFor(x => x.Number, x => x.Random.String(1, 256))
                .RuleFor(x => x.Date, DateTime.UtcNow)
                .RuleFor(x => x.TypeDoc, x => x.PickRandom<TypeDoc>())
                .RuleFor(x => x.Operations, Operations.Entry)
                .RuleFor(x => x.Paid, x => x.Random.Bool())
                .RuleFor(x => x.PaymentDate, DateTime.Now)
                .RuleFor(x => x.Description, x => x.Random.String(1, 50))
                .RuleFor(x => x.Total, x => x.Random.Decimal(1, 100))
                .RuleFor(x => x.Observation, x => x.Random.String(1, 50));

            return document;
        }

    }
}
