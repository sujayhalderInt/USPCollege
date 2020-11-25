using System;
using System.Collections.Generic;
using Examine;
using Examine.Providers;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Extensions;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Data;
using USP.Business.Services;

namespace USP.Business.EventHandler
{
    public class ExamineIndexer : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            BaseIndexProvider indexer = ExamineManager.Instance.IndexProviderCollection[SiteConstants.SearchIndex.GlobalSearchIndexer];
            indexer.GatheringNodeData += SetSiteSearchFields;
        }

        private void SetSiteSearchFields(object sender, IndexingNodeDataEventArgs e)
        {
            if (UmbracoContext.Current == null)
            {
                UmbracoContextFake.FakeContext();
            }

            var helper = new UmbracoHelper(UmbracoContext.Current);
            var factory = new WidgetIndexFactory();
            var indexer = factory.GetGridIndexer();

            var nodeTypeAlias = e.Fields[SiteConstants.Fields.NodeTypeAlias];
            if (e.Fields.ContainsKey(SiteConstants.Fields.MainContentPlaceHolder))
            {
                // MAIN CONTENT
                var data = e.Fields[SiteConstants.Fields.MainContentPlaceHolder].FromJson<GridLayout>();
                if (data != null)
                {
                    var mainContent = indexer.IndexGrid(data);
                    e.Fields[SiteConstants.Fields.MainContentPlaceHolder] = mainContent;
                }
            }

            if (e.Fields.ContainsKey(SiteConstants.Fields.Campus))
            {
                List<string> tagIdList = new List<string>();
                List<string> tagNameList = new List<string>();
                IndexTaxonomy(e.Fields, tagIdList, tagNameList, SiteConstants.Fields.Campus, helper);
                e.Fields.Add(SiteConstants.Fields.SortCampus, string.Join(" ", tagNameList));
            }

            if (e.Fields.ContainsKey(SiteConstants.Fields.EventType))
            {
                List<string> tagIdList = new List<string>();
                List<string> tagNameList = new List<string>();
                IndexTaxonomy(e.Fields, tagIdList, tagNameList, SiteConstants.Fields.EventType, helper);
                e.Fields.Add(SiteConstants.Fields.SortEventType, string.Join(" ", tagNameList));
            }

            if (e.Fields.ContainsKey(SiteConstants.Fields.BannerHeading))
            {
                e.Fields.Add(SiteConstants.Fields.SortBannerHeadingDesc, (1000 - e.Fields[SiteConstants.Fields.BannerHeading].Trim()[0]).ToString());
            }

            if (e.Fields.ContainsKey("path"))
            {
                e.Fields.Add(SiteConstants.Fields.ExpandedPath, string.Join(" ", e.Fields["path"].Split(',')));
            }

            if (e.Fields.ContainsKey(SiteConstants.Fields.BlogAuthor))
            {
                var authorGuid = e.Fields[SiteConstants.Fields.BlogAuthor];
                var node = helper.TypedContent(authorGuid);
                if (node != null)
                {
                    var author = node.OfType<DataAuthor>();
                    e.Fields[SiteConstants.Fields.SortBlogAuthorLastName] = author.LastName.Trim() + " " + author.FirstName.Trim();
                }
            }

            // Taxonomy
            IndexTaxonomies(e.Fields, helper);

            SetSortDate(e.Fields, helper);
            SetFilterDates(e.Fields, helper);
        }

        private void SetSortDate(Dictionary<string, string> eFields, UmbracoHelper helper)
        {
            var date = DateTime.MinValue;
            var nodeTypeAlias = eFields[SiteConstants.Fields.NodeTypeAlias];

            if (eFields.ContainsKey(SiteConstants.Fields.StartDate))
            {
                DateTime.TryParse(eFields[SiteConstants.Fields.StartDate], out date);
            }

            if (eFields.ContainsKey(SiteConstants.Fields.PublishedDate))
            {
                DateTime.TryParse(eFields[SiteConstants.Fields.PublishedDate], out date);
            }

            if (date == DateTime.MinValue)
            {
                DateTime.TryParse(eFields["updateDate"], out date);
            }

            eFields.Add(SiteConstants.Fields.SortDate, date.ToString("yyyyMMddhhmmss"));
        }

        private void SetFilterDates(Dictionary<string, string> eFields, UmbracoHelper helper)
        {
            var startDate = DateTime.MinValue;
            var endDate = DateTime.MinValue;
            var nodeTypeAlias = eFields[SiteConstants.Fields.NodeTypeAlias];

            if (eFields.ContainsKey(SiteConstants.Fields.StartDate))
            {
                DateTime.TryParse(eFields[SiteConstants.Fields.StartDate], out startDate);
            }

            if (eFields.ContainsKey(SiteConstants.Fields.EndDate))
            {
                DateTime.TryParse(eFields[SiteConstants.Fields.EndDate], out endDate);
            }

            if (eFields.ContainsKey(SiteConstants.Fields.PublishedDate))
            {
                DateTime.TryParse(eFields[SiteConstants.Fields.PublishedDate], out startDate);
                DateTime.TryParse(eFields[SiteConstants.Fields.PublishedDate], out endDate);
            }

            if (startDate == DateTime.MinValue)
            {
                DateTime.TryParse(eFields["updateDate"], out startDate);
            }

            if (endDate == DateTime.MinValue)
            {
                endDate = startDate;
            }

            eFields.Add(SiteConstants.Fields.FilterStartDate, startDate.ToString("yyyyMMddhhmmss"));
            eFields.Add(SiteConstants.Fields.FilterEndDate, endDate.ToString("yyyyMMddhhmmss"));
        }

        private void IndexTaxonomies(Dictionary<string, string> eFields, UmbracoHelper helper)
        {
            List<string> tagIdList = new List<string>();
            List<string> tagNameList = new List<string>();

            IndexTaxonomy(eFields, tagIdList, tagNameList, SiteConstants.Fields.CourseType, helper);
            IndexTaxonomy(eFields, tagIdList, tagNameList, SiteConstants.Fields.CourseLevel, helper);
            IndexTaxonomy(eFields, tagIdList, tagNameList, SiteConstants.Fields.AwardingBody, helper);
            IndexTaxonomy(eFields, tagIdList, tagNameList, SiteConstants.Fields.CareerSector, helper);
            IndexTaxonomy(eFields, tagIdList, tagNameList, SiteConstants.Fields.Campus, helper);
            IndexTaxonomy(eFields, tagIdList, tagNameList, SiteConstants.Fields.EventType, helper);
            IndexTaxonomy(eFields, tagIdList, tagNameList, SiteConstants.Fields.NewsTopics, helper);
            IndexTaxonomy(eFields, tagIdList, tagNameList, SiteConstants.Fields.BlogTopics, helper);
            IndexTaxonomy(eFields, tagIdList, tagNameList, SiteConstants.Fields.BlogAuthor, helper);

            eFields.Add(SiteConstants.Fields.TaxonomyId, string.Join(" ", tagIdList));
            eFields.Add(SiteConstants.Fields.TaxonomyName, string.Join(" ", tagNameList));

            var level = new List<string>();
            IndexTaxonomy(eFields, new List<string>(), level, SiteConstants.Fields.CourseLevel, helper);
            eFields.Add(SiteConstants.Fields.SortCourseLevel, string.Join(" ", level));

            var subject = new List<string>();
            IndexTaxonomy(eFields, new List<string>(), subject, SiteConstants.Fields.CareerSector, helper);
            eFields.Add(SiteConstants.Fields.SortSubjectArea, string.Join(" ", subject));
        }

        private void IndexTaxonomy(Dictionary<string, string> eFields, List<string> tagIdList, List<string> tagNameList, string tagName, UmbracoHelper helper)
        {
            if (!eFields.ContainsKey(tagName) || string.IsNullOrWhiteSpace(eFields[tagName]))
            {
                return;
            }

            try
            {
                var tags = eFields[tagName].Split(',');
                foreach (var tag in tags)
                {
                    var node = helper.TypedContent(tag);
                    if (node == null) continue;

                    if (!tagIdList.Contains(node.GetKey().ToIndexString()))
                    {
                        tagIdList.Add(node.GetKey().ToIndexString());
                    }

                    if (tagName == SiteConstants.Fields.BlogAuthor)
                    {
                        var author = new DataAuthor(node);
                        var authorName = author.FirstName + " " + author.LastName;
                        if (!tagNameList.Contains(authorName))
                        {
                            tagNameList.Add(authorName);
                        }
                    }
                    else
                    {
                        var taxonomy = new DataTaxonomy(node);
                        if (!tagNameList.Contains(taxonomy.TaxonomyName))
                        {
                            tagNameList.Add(taxonomy.TaxonomyName);
                        }
                    }
                }
            }
            catch (Exception e) { }
        }
    }
}