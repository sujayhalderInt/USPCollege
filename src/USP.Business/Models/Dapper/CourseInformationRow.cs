using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP.Business.Models.Dapper
{
    public class CourseInformationRow
    {
        public long ProspectusID { get; set; }
        public string CourseCode { get; set; }
        public int ProspectusCategoryID { get; set; }
        public string ProspectusCategoryDesc { get; set; }
        public string ProspectusCategoryText { get; set; }
        public int ProspectusDisplayOrder { get; set; }
    }

    public class CourseInformation
    {
        public string CourseCode { get; set; }

        public string Title { get; set; }
        public string Qualification { get; set; }
        public string CourseType { get; set; }
        public string Level { get; set; }
        public string AwardingBody { get; set; }
        public string Duration { get; set; }
        public string DaysandTime { get; set; }
        public string StartDate { get; set; }
        public string Fee { get; set; }
        public string CareerSector { get; set; }
        public string Campus { get; set; }
        public string Description { get; set; }
        public string ImageID { get; set; }
        public string OverlayColour { get; set; }
        public string BannerHeading { get; set; }
        public string Content { get; set; }
    }
}
