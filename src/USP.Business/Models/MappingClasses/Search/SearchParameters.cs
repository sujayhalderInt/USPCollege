using System;
using System.Collections.Generic;
using USP.Business.Enums;

namespace USP.Business.Models.MappingClasses.Search
{
    public class SearchParameters
    {
        public string Term { get; set; }

        public string[] DocTypeAliases { get; set; }

        public Dictionary<string, List<string>> Taxonomy { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string SearchType { get; set; }
        public List<string> BasicTaxonomy { get; set; }
        public int[] ExcludeNodeId { get; set; }
        public string ParentId { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string ApplyNowCourse  { get; set; }
    }
}