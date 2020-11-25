using System;
using System.Collections.Generic;
using System.Linq;
using ClientDependency.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Umbraco.Core.Models;
using USP.Business.Constants;
using USP.Business.Models.Dapper;
using USP.Business.Services;

namespace USP.Test
{
    [TestClass]
    public class CourseImportHasDataChangedTest
    {
        [TestMethod]
        public void HasDataChangedFalse_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsFalse(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_DaysTime_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.DaysandTime = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_Description_Test()
        {
            var courseInformation = GetCourseInformation();

            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.Description = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_Duration_Test()
        {
            var courseInformation = GetCourseInformation();

            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.Duration = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_Fee_Test()
        {
            var courseInformation = GetCourseInformation();

            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.Fee = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_StartDate_Test()
        {
            var courseInformation = GetCourseInformation();

            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.StartDate = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_Title_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.Title = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_ImageID_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.ImageID = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_Overlay_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.OverlayColour = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }


        [TestMethod]
        public void HasDataChangedTrue_Qualification_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.OverlayColour = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_Campus_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.Campus = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_Level_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.Level = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_CourseType_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.CourseType = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_CareerSector_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.CareerSector = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        [TestMethod]
        public void HasDataChangedTrue_AwardingBody_Test()
        {
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);

            var courseImportService = new CourseImportService();
            courseInformation.AwardingBody = "FAIL";
            var hasDataChanged = courseImportService.HasDataChanged(courseInformation, content.Object);

            Assert.IsTrue(hasDataChanged);
        }

        private IEnumerable<IContent> SetupTaxonomy(string value)
        {
            var taxonomyItem = new Mock<IContent>();
            taxonomyItem.Setup(t => t.GetValue<string>(SiteConstants.Fields.TaxonomyName)).Returns(value);
            return new List<IContent> { taxonomyItem.Object };
        }

        private CourseInformation GetCourseInformation()
        {
            string img = null;

            var dayTime = "Weekdays 10am - 4pm";
            var summary = "Summary";
            var duration = "3 years";
            var fee = "£9000";
            var startDate = "September";
            var title = "Awesome Course";
            var overlay = "pink";
            var qualification = "A-Level";
            var awardingBody = "BTEC";
            var campusName = "Seevic";
            var careerSector = "Digital";
            var courseType = "A Level";
            var courseLevel = "Level 3";

            return new CourseInformation()
            {
                AwardingBody = awardingBody,
                BannerHeading = title,
                Title = title,
                Campus = campusName,
                CareerSector = careerSector,
                CourseCode = "1234",
                CourseType = courseType,
                DaysandTime = dayTime,
                Description = summary,
                Duration = duration,
                Fee = fee,
                ImageID = img,
                Level = courseLevel,
                OverlayColour = overlay,
                Qualification = qualification,
                StartDate = startDate,
            };
        }

        private Mock<IContent> MockContent(CourseInformation courseInformation)
        {
            var content = new Mock<IContent>();
            content.Setup(c => c.GetValue<string>(SiteConstants.Fields.DaysAndTimes)).Returns(courseInformation.DaysandTime);
            content.Setup(c => c.GetValue<string>(SiteConstants.Fields.BannerSummary)).Returns(courseInformation.Description);
            content.Setup(c => c.GetValue<string>(SiteConstants.Fields.Duration)).Returns(courseInformation.Duration);
            content.Setup(c => c.GetValue<string>(SiteConstants.Fields.Fee)).Returns(courseInformation.Fee);
            content.Setup(c => c.GetValue<string>(SiteConstants.Fields.StartDate)).Returns(courseInformation.StartDate);
            content.Setup(c => c.GetValue<string>(SiteConstants.Fields.BannerHeading)).Returns(courseInformation.BannerHeading);
            content.Setup(c => c.GetValue<string>(SiteConstants.Fields.BannerImage)).Returns(courseInformation.ImageID);
            content.Setup(c => c.GetValue<string>(SiteConstants.Fields.OverlayColour)).Returns(courseInformation.OverlayColour);
            content.Setup(c => c.GetValue<string>(SiteConstants.Fields.Qualification)).Returns(courseInformation.Qualification);

            // Setup Taxonomy
            content.Setup(c => c.GetValue<IEnumerable<IContent>>(SiteConstants.Fields.AwardingBody))
                .Returns(SetupTaxonomy(courseInformation.AwardingBody));
            content.Setup(c => c.GetValue<IEnumerable<IContent>>(SiteConstants.Fields.Campus))
                .Returns(SetupTaxonomy(courseInformation.Campus));
            content.Setup(c => c.GetValue<IEnumerable<IContent>>(SiteConstants.Fields.CareerSector))
                .Returns(SetupTaxonomy(courseInformation.CareerSector));
            content.Setup(c => c.GetValue<IEnumerable<IContent>>(SiteConstants.Fields.CourseType))
                .Returns(SetupTaxonomy(courseInformation.CourseType));
            content.Setup(c => c.GetValue<IEnumerable<IContent>>(SiteConstants.Fields.CourseLevel))
                .Returns(SetupTaxonomy(courseInformation.Level));
            return content;
        }
    }
}
