using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP.Business.Extensions
{
    public static class GuidExtensions
    {
        public static string ToIndexString(this Guid guid)
        {
            return guid.ToString().Replace("-", "");
        }
    }
}
