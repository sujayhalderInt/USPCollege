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
    public class CourseImport_GetCoursesTest
    {
        [TestMethod]
        public void GetCoursesTest()
        {
            // Setup Content Service
            var rootContent = new Mock<IContent>(MockBehavior.Strict);
            rootContent.Setup(c => c.ContentType.Alias).Returns(PageHome.ModelTypeAlias);
            rootContent.Setup(c => c.Id).Returns(0);

            var pageList = GetPageList();

            var contentService = new Mock<IContentService>(MockBehavior.Strict);
            contentService.Setup(c => c.GetRootContent()).Returns(new List<IContent> {rootContent.Object});
            contentService.Setup(c => c.GetDescendants(0)).Returns(pageList);

            var courseImportService = new CourseImportService();
            courseImportService.ContentService = contentService.Object;
            var result = courseImportService.GetCoursePages();

            contentService.Verify();
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Select(r => r.Id).UnsortedSequenceEqual(new[] {1, 2, 6}));
        }

        private List<IContent> GetPageList()
        {
            var pageList = new List<IContent>
            {
                CreatePage(PageCourseDetail.ModelTypeAlias, 1).Object,
                CreatePage(PageCourseDetail.ModelTypeAlias, 2).Object,
                CreatePage(PageBlogDetail.ModelTypeAlias, 3).Object,
                CreatePage(PageBlogListing.ModelTypeAlias, 4).Object,
                CreatePage(PageEventDetail.ModelTypeAlias, 5).Object,
                CreatePage(PageCourseDetail.ModelTypeAlias, 6).Object,
                CreatePage(PageSearch.ModelTypeAlias, 7).Object,
            };
            return pageList;
        }

        private Mock<IContent> CreatePage(string contentTypeAlias, int id)
        {
            var content = new Mock<IContent>(MockBehavior.Strict);
            content.Setup(c => c.ContentType.Alias).Returns(contentTypeAlias);
            content.Setup(c => c.Id).Returns(id);
            return content;
        }
    }
}
