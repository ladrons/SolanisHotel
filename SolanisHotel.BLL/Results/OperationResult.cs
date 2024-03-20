using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.BLL.Results
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
        public Exception? Exception { get; set; }

        public OperationResult()
        {

        }

        public OperationResult(bool success)
        {
            Success = success;
        }

        public OperationResult(bool succes, string message)
        {
            Success = succes;
            Message = message;
        }

        public OperationResult(bool succes, string message, Exception ex)
        {
            Success = succes;
            Message = message;
            Exception = ex;
        }

        public OperationResult(bool succes, string message, object data)
        {
            Success = succes;
            Message = message;
            Data = data;
        }
    }
}