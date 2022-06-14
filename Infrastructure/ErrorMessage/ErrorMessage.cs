using System.Collections.Generic;

namespace Infrastructure.ErrorMessage
{
    public class ErrorMessage<T> where T : class
    {
        public string Code { get; set; }
        public List<string> Message { get; set; }
        public T Contract { get; set; }

        public ErrorMessage(string code, List<string> message, T contract)
        {
            Code = code;
            Message = message;
            Contract = contract;
        }
    }
}
