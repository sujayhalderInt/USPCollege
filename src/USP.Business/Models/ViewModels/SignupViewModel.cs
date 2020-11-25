using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Business.Models.ContentModels;

namespace USP.Business.Models.ViewModels
{
    public class SignupViewModel
    {
        public SettingsSite Settings { get; set; }
        public string ApplicationUrl { get; set; }
    }
}
