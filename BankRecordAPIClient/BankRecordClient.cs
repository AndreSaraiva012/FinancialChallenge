using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BankRecordAPIClient
{

    public class BankRecordClient : IBankRecordClient
    {
        private readonly HttpClient _client;
        private readonly BankRecordOptions _options;
        public BankRecordClient(HttpClient client, IOptions<BankRecordOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        public BankRecordDTO MessageBody(Origin origin, Guid originId, string description, BankRecordDomain.Entities.Type type, decimal amount)
        {
            BankRecordDTO bankRecords = new BankRecordDTO()
            {
                Origin = origin,
                OriginId = originId,
                Description = description,
                Type = type,
                Amount = amount
            };

            return bankRecords;
        }


        public async Task<bool> PostCashBank(BankRecordDTO bankRecord)
        {
            var aux = _options.GetCashBankEndPoint();
            var response = await _client.PostAsJsonAsync(aux, bankRecord);

            if (!response.IsSuccessStatusCode)
            {
                var returnList = response.Content.ToString();
                throw new Exception(returnList);
            }

            return response != null && response.IsSuccessStatusCode;
        }
    }
}
