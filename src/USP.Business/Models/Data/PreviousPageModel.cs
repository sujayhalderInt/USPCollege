using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP.Business.Models.Data
{
    public class PreviousPage
    {
        public int PageNo { get; set; }
        public string SortBy { get; set; }
        public string Keyword { get; set; }
        public string[] SelectedTaxonomy { get; set; }
        public string[] DocTypeAliases { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
