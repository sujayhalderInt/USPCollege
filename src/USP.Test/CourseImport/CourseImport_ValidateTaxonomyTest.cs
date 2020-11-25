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
    public class CourseImport_ValidateTaxonomyTest
    {
        [TestMethod]
        public void ValidateTaxonomy_ValidTest()
        {
            // Setup Content Service
            var rootContent = new Mock<IContent>(MockBehavior.Strict);
            rootContent.Setup(c => c.ContentType.Alias).Returns(FolderTaxonomyFolder.ModelTypeAlias);

            var taxonomyList = GetTaxonomyList();

            var contentService = new Mock<IContentService>(MockBehavior.Strict);
            contentService.Setup(c => c.GetRootContent()).Returns(new List<IContent>{ rootContent.Object });
            contentService.Setup(c => c.GetDescendants(rootContent.Object)).Returns(taxonomyList);

            var courseImportService = new CourseImportService();
            courseImportService.ContentService = contentService.Object;
            var result = courseImportService.ValidateTaxonomy(new []{"Seevic", "Digital", "Level 1", "BTEC"});

            contentService.Verify();
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ValidateTaxonomy_InvalidTest()
        {
            // Setup Content Service
            var rootContent = new Mock<IContent>(MockBehavior.Strict);
            rootContent.Setup(c => c.ContentType.Alias).Returns(FolderTaxonomyFolder.ModelTypeAlias);

            var taxonomyList = GetTaxonomyList();

            var contentService = new Mock<IContentService>(MockBehavior.Strict);
            contentService.Setup(c => c.GetRootContent()).Returns(new List<IContent> { rootContent.Object });
            contentService.Setup(c => c.GetDescendants(rootContent.Object)).Returns(taxonomyList);

            var courseImportService = new CourseImportService();
            courseImportService.ContentService = contentService.Object;
            var result = courseImportService.ValidateTaxonomy(new[] { "Seevic", "Digital", "Level 1", "BTEC", "Not There" });

            contentService.Verify();
            Assert.IsFalse(result.Success);
        }

        private static List<IContent> GetTaxonomyList()
        {
            var awardTaxonomy = new Mock<IContent>(MockBehavior.Strict);
            awardTaxonomy.Setup(c => c.ContentType.Alias).Returns(DataTaxonomy.ModelTypeAlias);
            awardTaxonomy.Setup(c => c.GetValue<string>(SiteConstants.Fields.TaxonomyName))
                .Returns("BTEC");

            var courseLevelTaxonomy = new Mock<IContent>(MockBehavior.Strict);
            courseLevelTaxonomy.Setup(c => c.ContentType.Alias).Returns(DataTaxonomy.ModelTypeAlias);
            courseLevelTaxonomy.Setup(c => c.GetValue<string>(SiteConstants.Fields.TaxonomyName))
                .Returns("Level 1");

            var careerTaxonomy = new Mock<IContent>(MockBehavior.Strict);
            careerTaxonomy.Setup(c => c.ContentType.Alias).Returns(DataTaxonomy.ModelTypeAlias);
            careerTaxonomy.Setup(c => c.GetValue<string>(SiteConstants.Fields.TaxonomyName))
                .Returns("Digital");

            var campusTaxonomy = new Mock<IContent>(MockBehavior.Strict);
            campusTaxonomy.Setup(c => c.ContentType.Alias).Returns(DataTaxonomy.ModelTypeAlias);
            campusTaxonomy.Setup(c => c.GetValue<string>(SiteConstants.Fields.TaxonomyName)).Returns("Seevic");

            var taxonomyList = new List<IContent>
            {
                awardTaxonomy.Object,
                courseLevelTaxonomy.Object,
                careerTaxonomy.Object,
                campusTaxonomy.Object
            };
            return taxonomyList;
        }
    }
}
