using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Umbraco.Core;
using Umbraco.Core.Services;
using USP.Business.Models.Dapper;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class FakeRemsDatabaseService : IRemsDatabaseService
    {
        private IDataTypeService _service = null;

        public IDataTypeService DataTypeService
        {
            get => _service ?? (ApplicationContext.Current.Services.DataTypeService);
            set => _service = value;
        }

        public List<CourseInformation> GetCourses()
        {
            var results = new List<CourseInformation>();
            var dummyContent = @"
                <h2>Some content</h2>
                <p>Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Even the all-powerful Pointing has no control about the blind texts it is an almost unorthographic life One day however a small line of blind text by the name of Lorem Ipsum decided to leave for the far World of Grammar.</p>
                <p>Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Even the all-powerful Pointing has no control about the blind texts it is an almost unorthographic life One day however a small line of blind text by the name of Lorem Ipsum decided to leave for the far World of Grammar.</p>
                <h2>Some more content</h2>
                <p>Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Even the all-powerful Pointing has no control about the blind texts it is an almost unorthographic life One day however a small line of blind text by the name of Lorem Ipsum decided to leave for the far World of Grammar.</p>
                <p>Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Even the all-powerful Pointing has no control about the blind texts it is an almost unorthographic life One day however a small line of blind text by the name of Lorem Ipsum decided to leave for the far World of Grammar.</p>
                <p>Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Even the all-powerful Pointing has no control about the blind texts it is an almost unorthographic life One day however a small line of blind text by the name of Lorem Ipsum decided to leave for the far World of Grammar.</p>
            ";

            results.Add(FakeCourse("FK-381-A1", "btec", "Unity Programming", "Seevic, Palmer's", "Creative & Digital", dummyContent, "Professional qualification", "Mon-Wed 9am-5pm", "Get a BTEC in Unity Programming, innit", "3 Years", "£18,000", "1465", "Level 1", "Green", "Level 1 BTEC", "November", "BTEC Unity Programming"));
            results.Add(FakeCourse("FK-381-B2", "aat", "Unity Programming", "Seevic", "Creative & Digital", dummyContent, "A-Level", "Mon-Fri 9am-5pm", null, "4 Years", "£25,000", "1487", "Level 4", "Teal", "A Level", "August", "A Level Unity Programming"));


            return results;
        }

        private CourseInformation FakeCourse(string courseCode, string awardingBody, string bannerHeading, string campus, string careerSector, 
            string content, string courseType, string daysTime, string description, string duration, string fee, string imageId, string level, 
            string overlayColour, string qualification, string startDate, string title)
        {
            var course = new CourseInformation();
            course.AwardingBody = awardingBody;
            course.BannerHeading = bannerHeading;
            course.Campus = campus;
            course.CareerSector = careerSector;
            course.Content = content;
            course.CourseCode = courseCode;
            course.CourseType = courseType;
            course.DaysandTime = daysTime;
            course.Description = description;
            course.Duration = duration;
            course.Fee = fee;
            course.ImageID = imageId;
            course.Level = level;
            course.OverlayColour = MapColor(overlayColour);
            course.Qualification = qualification;
            course.StartDate = startDate;
            course.Title = title;

            return course;
        }

        private string MapColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                return null;
            }

            var statusEditor = DataTypeService.GetAllDataTypeDefinitions().First(x => x.Name == "Overlay Colour Dropdown");
            int preValueId = DataTypeService.GetPreValuesCollectionByDataTypeId(statusEditor.Id)
                .PreValuesAsDictionary.Where(d => d.Value.Value.ToLower() == color.Trim().ToLower())
                .Select(f => f.Value.Id).FirstOrDefault();

            return preValueId.ToString();
        }
    }
}
