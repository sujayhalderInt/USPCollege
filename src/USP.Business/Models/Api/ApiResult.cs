using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP.Business.Models.Api
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }

        public T Result { get; set; }
    }
}
