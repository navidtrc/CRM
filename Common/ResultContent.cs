using Newtonsoft.Json;
using System.Reflection;

namespace Common
{
    public class OperationResult
    {
        public OperationResult(bool isSuccess, string message = null) : base()
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

    public class OperationResult<TData> : OperationResult
    {
        public OperationResult(bool isSuccess, TData data, string message = null) : base(isSuccess, message)
        {
            Data = data;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }
    }
}
