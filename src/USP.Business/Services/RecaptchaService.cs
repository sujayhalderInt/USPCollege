using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class RecaptchaService : IRecaptchaService
    {
        public bool VerifyRecaptcha(string captchaResponse, string secretKey)
        {
            var client = new WebClient();
            var result = client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}");
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            return status;
        }
    }
}
