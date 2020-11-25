using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP.Business.Models.Data
{
    public class Sitemap
    {
        public virtual DateTime LastModified { get; set; }
        public virtual string Url { get; set; }
        public virtual bool HideFromSitemap { get; set; }
    }
}
