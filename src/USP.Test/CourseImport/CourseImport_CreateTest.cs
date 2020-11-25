using System;
using System.Collections.Generic;
using System.Linq;
using ClientDependency.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Dapper;
using USP.Business.Services;

namespace USP.Test
{
    [TestClass]
    public class CourseImport_CreateTest
    {
        [TestMethod]
        public void CreateCourseTest()
        {
            var parentFolder = new Guid();
            var courseInformation = GetCourseInformation();

            // Setup Content Service
            var content = MockContent(courseInformation);
            content.Setup(c => c.Published).Returns(false);

            var rootContent = new Mock<IContent>(MockBehavior.Strict);
            rootContent.Setup(c => c.ContentType.Alias).Returns(FolderTaxonomyFolder.ModelTypeAlias);

            var taxonomyList = GetTaxonomyList(courseInformation);

            var contentService = new Mock<IContentService>(MockBehavior.Strict);
            contentService.Setup(c => c.CreateContent(courseInformation.Title, parentFolder, SiteConstants.AliasCourseDetail, 0)).Returns(content.Object);
            contentService.Setup(c => c.GetRootContent()).Returns(new List<IContent> { rootContent.Object });
            contentService.Setup(c => c.GetDescendants(rootContent.Object)).Returns(taxonomyList);

            contentService.Setup(c => c.Save(content.Object, 0, true));

            var courseImportService = new CourseImportService();
            courseImportService.ContentService = contentService.Object;
            courseImportService.CreateCourse(courseInformation, parentFolder);

            contentService.Verify();
            content.Verify();
            contentService.Verify(c => c.Save(content.Object, 0, true));
        }

        private static List<IContent> GetTaxonomyList(CourseInformation courseInformation)
        {
            var awardTaxonomy = new Mock<IContent>(MockBehavior.Strict);
            awardTaxonomy.Setup(c => c.ContentType.Alias).Returns(DataTaxonomy.ModelTypeAlias);
            awardTaxonomy.Setup(c => c.GetValue<string>(SiteConstants.Fields.TaxonomyName))
                .Returns(courseInformation.AwardingBody);
            awardTaxonomy.Setup(c => c.Key).Returns(new Guid("b53beb2e-bbc6-4dcb-a8f1-ffc955e39004"));

            var courseLevelTaxonomy = new Mock<IContent>(MockBehavior.Strict);
            courseLevelTaxonomy.Setup(c => c.ContentType.Alias).Returns(DataTaxonomy.ModelTypeAlias);
            courseLevelTaxonomy.Setup(c => c.GetValue<string>(SiteConstants.Fields.TaxonomyName))
                .Returns(courseInformation.Level);
            courseLevelTaxonomy.Setup(c => c.Key).Returns(new Guid("67757af8-fe26-4456-badf-b7b1c9fb2aad"));

            var careerTaxonomy = new Mock<IContent>(MockBehavior.Strict);
            careerTaxonomy.Setup(c => c.ContentType.Alias).Returns(DataTaxonomy.ModelTypeAlias);
            careerTaxonomy.Setup(c => c.GetValue<string>(SiteConstants.Fields.TaxonomyName))
                .Returns(courseInformation.CareerSector);
            careerTaxonomy.Setup(c => c.Key).Returns(new Guid("45a90532-ec3d-4628-b71d-a459b7e45039"));

            var campusTaxonomy = new Mock<IContent>(MockBehavior.Strict);
            campusTaxonomy.Setup(c => c.ContentType.Alias).Returns(DataTaxonomy.ModelTypeAlias);
            campusTaxonomy.Setup(c => c.GetValue<string>(SiteConstants.Fields.TaxonomyName)).Returns(courseInformation.Campus);
            campusTaxonomy.Setup(c => c.Key).Returns(new Guid("585f7634-ed0d-4c13-9300-32fa86b59ae4"));

            var courseTypeTaxonomy = new Mock<IContent>(MockBehavior.Strict);
            courseTypeTaxonomy.Setup(c => c.ContentType.Alias).Returns(DataTaxonomy.ModelTypeAlias);
            courseTypeTaxonomy.Setup(c => c.GetValue<string>(SiteConstants.Fields.TaxonomyName)).Returns(courseInformation.CourseType);
            courseTypeTaxonomy.Setup(c => c.Key).Returns(new Guid("585f7634-ed0d-4c13-9300-32fa86b59dbc"));

            var taxonomyList = new List<IContent>
            {
                awardTaxonomy.Object,
                courseLevelTaxonomy.Object,
                careerTaxonomy.Object,
                campusTaxonomy.Object,
                courseTypeTaxonomy.Object
            };
            return taxonomyList;
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
            var bannerHeading = "My Awesome Course";
            var overlay = "pink";
            var qualification = "A-Level";
            var awardingBody = "BTEC";
            var campusName = "Seevic";
            var careerSector = "Digital";
            var courseType = "A Level";
            var courseLevel = "Level 3";
            var content = "STUFF GOES HERE";

            return new CourseInformation()
            {
                AwardingBody = awardingBody,
                BannerHeading = bannerHeading,
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
                Content = content
            };
        }

        private Mock<IContent> MockContent(CourseInformation courseInformation)
        {
            var content = new Mock<IContent>(MockBehavior.Strict);

            content.Setup(c => c.SetValue(SiteConstants.Fields.RemsCourseCode, courseInformation.CourseCode));

            content.Setup(c => c.SetValue(SiteConstants.Fields.DaysAndTimes, courseInformation.DaysandTime));
            content.Setup(c => c.SetValue(SiteConstants.Fields.BannerSummary, courseInformation.Description));
            content.Setup(c => c.SetValue(SiteConstants.Fields.Duration, courseInformation.Duration));
            content.Setup(c => c.SetValue(SiteConstants.Fields.Fee, courseInformation.Fee));
            content.Setup(c => c.SetValue(SiteConstants.Fields.StartDate, courseInformation.StartDate));
            content.Setup(c => c.SetValue(SiteConstants.Fields.BannerHeading, courseInformation.BannerHeading));
            content.Setup(c => c.SetValue(SiteConstants.Fields.BannerImage, courseInformation.ImageID));
            content.Setup(c => c.SetValue(SiteConstants.Fields.OverlayColour, courseInformation.OverlayColour));
            content.Setup(c => c.SetValue(SiteConstants.Fields.Qualification, courseInformation.Qualification));
            content.Setup(c => c.SetValue(SiteConstants.Fields.MetaTitle, courseInformation.Title));

            content.Setup(c => c.SetValue(SiteConstants.Fields.MainContent, courseInformation.Content));

            content.Setup(c => c.SetValue(SiteConstants.Fields.AwardingBody, "umb://document/b53beb2ebbc64dcba8f1ffc955e39004"));
            content.Setup(c => c.SetValue(SiteConstants.Fields.CourseLevel, "umb://document/67757af8fe264456badfb7b1c9fb2aad"));
            content.Setup(c => c.SetValue(SiteConstants.Fields.CareerSector, "umb://document/45a90532ec3d4628b71da459b7e45039"));
            content.Setup(c => c.SetValue(SiteConstants.Fields.Campus, "umb://document/585f7634ed0d4c13930032fa86b59ae4"));
            content.Setup(c => c.SetValue(SiteConstants.Fields.CourseType, "umb://document/585f7634ed0d4c13930032fa86b59dbc"));

            return content;
        }
    }
}
