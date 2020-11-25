using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Extensions;
using USP.Business.Models.Api;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Dapper;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class CourseImportService : ICourseImportService
    {
        private IContentService _contentService = null;

        public IContentService ContentService
        {
            get => _contentService ?? (ApplicationContext.Current.Services.ContentService);
            set => _contentService = value;
        }

        private IMediaService _mediaService = null;

        public IMediaService MediaService
        {
            get => _mediaService ?? (ApplicationContext.Current.Services.MediaService);
            set => _mediaService = value;
        }



        public List<IContent> GetCoursePages()
        {
            var hp =ContentService.GetRootContent()
                .FirstOrDefault(c => c.ContentType.Alias == PageHome.ModelTypeAlias);
            if (hp == null) return new List<IContent>();

            var results = ContentService.GetDescendants(hp.Id)
                .Where(p => p.ContentType.Alias == PageCourseDetail.ModelTypeAlias)
                .ToList();

            return results;
        }

        public void CreateCourse(CourseInformation courseInformation, Guid parentFolder)
        {
            var page = ContentService.CreateContent(courseInformation.Title, parentFolder, SiteConstants.AliasCourseDetail);
            page.SetValue(SiteConstants.Fields.RemsCourseCode, courseInformation.CourseCode);
            SetTaxonomy(page, SiteConstants.Fields.CourseType, courseInformation.CourseType);

            UpdateCourse(courseInformation, page);
        }

        public bool HasDataChanged(CourseInformation courseInformation, IContent umbracoPage)
        {
            if (courseInformation.DaysandTime != umbracoPage.GetValue<string>(SiteConstants.Fields.DaysAndTimes))
            {
                return true;
            }

            if (courseInformation.Description != umbracoPage.GetValue<string>(SiteConstants.Fields.BannerSummary))
            {
                return true;
            }

            if (courseInformation.Duration != umbracoPage.GetValue<string>(SiteConstants.Fields.Duration))
            {
                return true;
            }

            if (courseInformation.Fee != umbracoPage.GetValue<string>(SiteConstants.Fields.Fee))
            {
                return true;
            }

            if (courseInformation.StartDate != umbracoPage.GetValue<string>(SiteConstants.Fields.StartDate))
            {
                return true;
            }

            if (courseInformation.Title != umbracoPage.GetValue<string>(SiteConstants.Fields.BannerHeading))
            {
                return true;
            }

            if (courseInformation.ImageID != umbracoPage.GetValue<string>(SiteConstants.Fields.BannerImage))
            {
                return true;
            }

            if (courseInformation.OverlayColour != umbracoPage.GetValue<string>(SiteConstants.Fields.OverlayColour))
            {
                return true;
            }

            if (courseInformation.Qualification != umbracoPage.GetValue<string>(SiteConstants.Fields.Qualification))
            {
                return true;
            }

            if (!TaxonomyMatches(courseInformation.AwardingBody, umbracoPage, SiteConstants.Fields.AwardingBody))
            {
                return true;
            }
            if (!TaxonomyMatches(courseInformation.Campus, umbracoPage, SiteConstants.Fields.Campus))
            {
                return true;
            }
            if (!TaxonomyMatches(courseInformation.CareerSector, umbracoPage, SiteConstants.Fields.CareerSector))
            {
                return true;
            }
            if (!TaxonomyMatches(courseInformation.CourseType, umbracoPage, SiteConstants.Fields.CourseType))
            {
                return true;
            }
            if (!TaxonomyMatches(courseInformation.Level, umbracoPage, SiteConstants.Fields.CourseLevel))
            {
                return true;
            }
            return false;
        }

        public ApiVoidResult ValidateTaxonomy(IEnumerable<string> taxonomies)
        {
            var inputTaxonomy = taxonomies.Select(t => t.ToLower().Trim()).ToList();

            var folder = ContentService.GetRootContent()
                .Where(c => c.ContentType.Alias == FolderTaxonomyFolder.ModelTypeAlias)
                ?.FirstOrDefault();

            if (folder == null)
                return new ApiVoidResult(false, "Root taxonomy folder not found");

            var taxonomy = ContentService.GetDescendants(folder)
                .Where(t => t.ContentType.Alias == DataTaxonomy.ModelTypeAlias)
                .Select(t => t.GetValue<string>(SiteConstants.Fields.TaxonomyName)?.ToLower().Trim())
                .ToList();

            if (taxonomy.ContainsAll(inputTaxonomy))
            {
                return new ApiVoidResult(true);
            }

            var missingTaxonomy = inputTaxonomy.Where(t => !taxonomy.Contains(t));

            return new ApiVoidResult(false, "Missing Taxonomy: " + string.Join(", ", missingTaxonomy));
        }

        private bool TaxonomyMatches(string newValue, IContent umbracoPage, string fieldName)
        {
            var taxId = umbracoPage.GetValue<IEnumerable<IContent>>(fieldName).ToList();

            if (taxId.IsNullOrEmpty() && string.IsNullOrWhiteSpace(newValue))
            {
                return true;
            }

            var pageTaxonomies = taxId.Select(x => x.GetValue<string>(SiteConstants.Fields.TaxonomyName)).ToArray();
            var remTaxonomies = newValue.Split(',');

            return pageTaxonomies.UnsortedSequenceEqual(remTaxonomies);
        }

        public void UpdateCourse(CourseInformation courseInformation, IContent umbracoPage)
        {
            umbracoPage.SetValue(SiteConstants.Fields.BannerHeading, courseInformation.BannerHeading);
            umbracoPage.SetValue(SiteConstants.Fields.BannerSummary, courseInformation.Description);
            umbracoPage.SetValue(SiteConstants.Fields.DaysAndTimes, courseInformation.DaysandTime);
            umbracoPage.SetValue(SiteConstants.Fields.Duration, courseInformation.Duration);
            umbracoPage.SetValue(SiteConstants.Fields.Fee, courseInformation.Fee);
            umbracoPage.SetValue(SiteConstants.Fields.Qualification, courseInformation.Qualification);
            umbracoPage.SetValue(SiteConstants.Fields.StartDate, courseInformation.StartDate);
            umbracoPage.SetValue(SiteConstants.Fields.MetaTitle, courseInformation.Title);
            umbracoPage.SetValue(SiteConstants.Fields.MainContent, courseInformation.Content);

            SetTaxonomy(umbracoPage, SiteConstants.Fields.AwardingBody, courseInformation.AwardingBody);
            SetTaxonomy(umbracoPage, SiteConstants.Fields.Campus, courseInformation.Campus);
            SetTaxonomy(umbracoPage, SiteConstants.Fields.CareerSector, courseInformation.CareerSector);
            SetTaxonomy(umbracoPage, SiteConstants.Fields.CourseLevel, courseInformation.Level);

            if (!string.IsNullOrWhiteSpace(courseInformation.OverlayColour))
            {
                int col;
                if (int.TryParse(courseInformation.OverlayColour, out col))
                {
                    umbracoPage.SetValue(SiteConstants.Fields.OverlayColour, col);
                }
            }


            // Set Image
            if (!string.IsNullOrWhiteSpace(courseInformation.ImageID))
            {
                Guid? imageGuid = GetImageGuid(courseInformation);

                if (imageGuid.HasValue)
                {
                    var image = Udi.Create("media", imageGuid.Value);
                    umbracoPage.SetValue(SiteConstants.Fields.BannerImage, image.ToString());
                }
            }

            if (umbracoPage.Published)
            {
                ContentService.SaveAndPublishWithStatus(umbracoPage);
            }
            else
            {
                ContentService.Save(umbracoPage);
            }
        }

        private Guid? GetImageGuid(CourseInformation courseInformation)
        {
            Guid? imageGuid = null;
            int imageId;

            if (int.TryParse(courseInformation.ImageID, out imageId))
            {
                imageGuid = MediaService.GetById(imageId)?.Key;
            }
            else
            {
                Guid outGuid;
                if (Guid.TryParse(courseInformation.ImageID, out outGuid))
                {
                    imageGuid = outGuid;
                }
            }
            return imageGuid;
        }

        private void SetTaxonomy(IContent umbracoPage, string fieldName, string fieldValue)
        {
            var values = fieldValue.Split(',');
            List<string> taxonomyIds = new List<string>();
            

            foreach (var taxonomyName in values)
            {
                var locaUdi = Udi.Create(Umbraco.Core.Constants.UdiEntityType.Document, GetTaxonomyKey(taxonomyName));
                taxonomyIds.Add(locaUdi.ToString());
            }

            umbracoPage.SetValue(fieldName, string.Join(",", taxonomyIds));
        }

        private Guid GetTaxonomyKey(string taxonomyName)
        {
            var folder = ContentService.GetRootContent()
                .Where(c => c.ContentType.Alias == FolderTaxonomyFolder.ModelTypeAlias)
                ?.FirstOrDefault();

            if (folder == null)
                return Guid.Empty;

            var taxonomy = ContentService
                .GetDescendants(folder)
                .FirstOrDefault(t => t.ContentType.Alias == DataTaxonomy.ModelTypeAlias &&
                            t.GetValue<string>(SiteConstants.Fields.TaxonomyName)?.ToLower().Trim() == taxonomyName.ToLower().Trim());

            if (taxonomy == null)
                return Guid.Empty;

            return taxonomy.Key;
        }
    }
}