using DocumentDomain.Entities;
using System;

namespace DocumentDomain.DTO_s
{
    public class DocumentDTO
    {
        public string Number { get; set; }
        public DateTimeOffset Date { get; set; }
        public TypeDoc TypeDoc { get; set; }
        public Operations Operations { get; set; }
        public bool Paid { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public string? Observation { get; set; }
    }
}