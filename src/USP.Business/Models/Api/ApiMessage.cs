using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP.Business.Models.Api
{
    public class ApiVoidResult
    {
        public ApiVoidResult()
        {
            
        }

        public ApiVoidResult(bool success)
        {
            Success = success;
        }

        public ApiVoidResult(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public ApiVoidResult(bool success, string errorMessage, Exception exception)
        {
            Success = success;
            Exception = exception;
            ErrorMessage = errorMessage;
        }

        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
    }
}
