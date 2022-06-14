using Infrastructure.BaseClass;
using System;

namespace DocumentDomain.Entities
{
    public class Document : EntityBase
    {
        private DateTimeOffset? _date;
        public string Number { get; set; }
        public DateTimeOffset Date { get; set; }
        public TypeDoc TypeDoc { get; set; }
        public Operations Operations { get; set; }
        public bool Paid { get; set; }
        public DateTimeOffset? PaymentDate
        {
            get
            {
                if (Paid == true)
                    return _date = DateTimeOffset.Now;
                else
                    return null;
            }
            set
            {
                _date = value;
            }
        }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public string? Observation { get; set; }
    }
}
