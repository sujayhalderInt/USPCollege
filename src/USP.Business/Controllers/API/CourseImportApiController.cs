using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using USP.Business.Constants;
using USP.Business.Models.Api;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Dapper;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.API
{
    public class CourseImportApiController : UmbracoApiController
    {
        private readonly IRemsDatabaseService _remsDatabaseService;
        private readonly ICourseImportService _courseImportService;

        public CourseImportApiController(IRemsDatabaseService remsDatabaseService, ICourseImportService courseImportService)
        {
            _remsDatabaseService = remsDatabaseService;
            _courseImportService = courseImportService;
        }

        public IHttpActionResult GetCourses()
        {
            var data = _remsDatabaseService.GetCourses();
            var locationSettings = GetSettings();

            var errors = new List<string>();

            if (locationSettings == null)
            {
                throw new Exception("Import settings not found");
            }

            var umbracoCourses = _courseImportService.GetCoursePages();

            int remsCourses = data.Count;
            int invalid = 0;
            int created = 0;
            int updated = 0;

            foreach (var courseInformation in data)
            {
                var isValid = ValidateCourseInfo(courseInformation);
                if (!isValid.Success)
                {
                    errors.Add(isValid.ErrorMessage);
                    invalid++;
                    continue;
                }


                var umbracoPage = umbracoCourses
                    .FirstOrDefault(c => c.GetValue<string>("rEMSCourseCode") == courseInformation.CourseCode);

                if (umbracoPage == null)
                {
                    // REMS data without matching Umbraco Page
                    var folder = FindCoursePage(courseInformation.CourseType, locationSettings);
                    _courseImportService.CreateCourse(courseInformation, folder);
                    created++;
                }
                else
                {
                    if (_courseImportService.HasDataChanged(courseInformation, umbracoPage))
                    {
                        // DATA has changed, update Page
                        _courseImportService.UpdateCourse(courseInformation, umbracoPage);
                        updated++;
                    }
                }

                Logger.Info<CourseImportApiController>($"REMS Records {remsCourses}. {invalid} invalid. {created} created. {updated} updated.");
            }


            return Json(
            new {
                Errors = errors,
                REMS_Records = data.Count,
                Invalid = invalid,
                Created = created,
                Updated = updated
            });
        }

        private ApiVoidResult ValidateCourseInfo(CourseInformation courseInformation)
        {
            var dataOK = ValidateCourseFields(courseInformation);
            if (!dataOK.Success)
            {
                var msg = $"Course {courseInformation.CourseCode} rejected: {dataOK.ErrorMessage}";
                Logger.Info<CourseImportApiController>(msg);
                return new ApiVoidResult(false, msg);
            }


            var taxonmyOk = _courseImportService.ValidateTaxonomy(GetTaxonomies(courseInformation));

            if (!taxonmyOk.Success)
            {
                var msg = $"Course {courseInformation.CourseCode} rejected: {taxonmyOk.ErrorMessage}";
                Logger.Info<CourseImportApiController>(msg);
                return new ApiVoidResult(false, msg);
            }

            return new ApiVoidResult(true);
        }

        private ApiVoidResult ValidateCourseFields(CourseInformation courseInformation)
        {
            var invalidFields = new List<string>();

            if (string.IsNullOrWhiteSpace(courseInformation.Title))
            {
                invalidFields.Add("Title");
            }

            if (string.IsNullOrWhiteSpace(courseInformation.Campus))
            {
                invalidFields.Add("Campus");
            }

            if (string.IsNullOrWhiteSpace(courseInformation.CareerSector))
            {
                invalidFields.Add("Career Sector");
            }

            if (string.IsNullOrWhiteSpace(courseInformation.CourseType))
            {
                invalidFields.Add("Course Type");
            }

            if (string.IsNullOrWhiteSpace(courseInformation.AwardingBody))
            {
                invalidFields.Add("Awarding Body");
            }

            if (string.IsNullOrWhiteSpace(courseInformation.Level))
            {
                invalidFields.Add("Level");
            }


            return invalidFields.Count > 0 
                ? new ApiVoidResult(false, $"Invalid fields - {string.Join(", ", invalidFields)}") 
                : new ApiVoidResult(true);
        }

        private static List<string> GetTaxonomies(CourseInformation courseInformation)
        {
            var taxonomies = new List<string>();
            if (!string.IsNullOrWhiteSpace(courseInformation.AwardingBody))
            {
                taxonomies.AddRange(courseInformation.AwardingBody.Split(','));
            }
            if (!string.IsNullOrWhiteSpace(courseInformation.Campus))
            {
                taxonomies.AddRange(courseInformation.Campus.Split(','));
            }
            if (!string.IsNullOrWhiteSpace(courseInformation.CareerSector))
            {
                taxonomies.AddRange(courseInformation.CareerSector.Split(','));
            }
            if (!string.IsNullOrWhiteSpace(courseInformation.CourseType))
            {
                taxonomies.AddRange(courseInformation.CourseType.Split(','));
            }
            if (!string.IsNullOrWhiteSpace(courseInformation.Level))
            {
                taxonomies.AddRange(courseInformation.Level.Split(','));
            }
            return taxonomies;
        }

        private Guid FindCoursePage(string courseType, SettingsCourseImport locationSettings)
        {
            var locs = locationSettings.CourseTypeParents
                .OfType<DataCourseLocation>();

            var loc = locs.FirstOrDefault(l => l.CourseType
                                         .FirstOrDefault()
                                         ?.GetPropertyValue<string>(SiteConstants.Fields.TaxonomyName).ToLower().Trim() == courseType.ToLower().Trim());

            var page = loc?.ParentCoursePage.FirstOrDefault();
            if (page == null)
            {
                return Guid.Empty;
            }

            return page.GetKey();
        }

        private SettingsCourseImport GetSettings()
        {
            var node = Umbraco.TypedContentAtXPath("//" + SettingsCourseImport.ModelTypeAlias)?.FirstOrDefault();

            return node == null ? null : new SettingsCourseImport(node);
        }
    }
}
