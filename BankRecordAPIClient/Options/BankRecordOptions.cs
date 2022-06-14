using System;

namespace BankRecordAPIClient
{
    public class BankRecordOptions
    {
        private string _baseAddress;
        public string BaseAddress
        {
            get
            {
                return _baseAddress ?? throw new InvalidOperationException("Base Address API Financeiro must be setted.");
            }
            set { _baseAddress = value; }
        }
        private string _endPoint;
        public string EndPoint
        {
            get
            {
                return _endPoint ?? throw new InvalidOperationException("BankRecord EndPoint must be setted.");
            }
            set { _endPoint = value; }
        }
        public string GetCashBankEndPoint() => $"{BaseAddress}/{EndPoint}";
    }
}
