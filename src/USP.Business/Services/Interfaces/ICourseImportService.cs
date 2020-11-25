using System;
using System.Collections.Generic;
using Umbraco.Core.Models;
using USP.Business.Models.Api;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Dapper;

namespace USP.Business.Services.Interfaces
{
    public interface ICourseImportService
    {
        List<IContent> GetCoursePages();
        void CreateCourse(CourseInformation courseInformation, Guid parentFolder);
        bool HasDataChanged(CourseInformation courseInformation, IContent umbracoPage);
        void UpdateCourse(CourseInformation courseInformation, IContent umbracoPage);
        ApiVoidResult ValidateTaxonomy(IEnumerable<string> taxonomies);
    }
}
