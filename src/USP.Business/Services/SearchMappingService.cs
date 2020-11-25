using System;
using System.Collections.Generic;
using System.Linq;
using Examine;
using umbraco;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class SearchMappingService : ISearchMappingService
    {
        public IEnumerable<SearchResultItem> MapSearchResults(IEnumerable<SearchResult> searchResults)
        {
            IList<SearchResultItem> finalResults = new List<SearchResultItem>();
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            foreach (var resultItem in searchResults)
            {
                var content = umbracoHelper.TypedContent(resultItem.Id);
                if (content != null)
                {
                    SearchResultItem mappedItem;

                    if (resultItem.Fields[SiteConstants.Fields.NodeTypeAlias] == SiteConstants.AliasCourseDetail)
                    {
                        var courseItem = new CourseDetailResultItem(resultItem);
                        var node = new PageCourseDetail(content);
                        courseItem.Campuses = node.Campus?.OfType<DataTaxonomy>().Select(c => c.TaxonomyName).ToArray();
                        courseItem.CareerSector = node.CareerSector?.OfType<DataTaxonomy>().Select(s => s.Name)
                            .FirstOrDefault();
                        courseItem.Image = node.BannerImage;
                        courseItem.SubHeading = node.Qualification;
                        mappedItem = courseItem;
                    }
                    else if (resultItem.Fields[SiteConstants.Fields.NodeTypeAlias] == PageCareerDetail.ModelTypeAlias)
                    {
                        var careerItem = new CareerDetailResultItem(resultItem);
                        var node = new PageCareerDetail(content);
                        careerItem.Image = node.BannerImage;
                        mappedItem = careerItem;
                    }
                    else if (resultItem.Fields[SiteConstants.Fields.NodeTypeAlias] == PageEventDetail.ModelTypeAlias)
                    {
                        var eventItem = new EventDetailResultItem(resultItem);
                        var node = new PageEventDetail(content);
                        eventItem.StartDate = node.StartDate;
                        eventItem.EndDate = node.EndDate;
                        eventItem.StartTime = node.StartTime;
                        eventItem.EndTime = node.EndTime;
                        eventItem.Campus = node.Campus?.OfType<DataTaxonomy>()
                            .Select(t => t.TaxonomyName)
                            .FirstOrDefault();

                        var eventType = node.EventType?.OfType<DataTaxonomy>()
                            .Select(t => t.TaxonomyName)
                            .FirstOrDefault();

                        if (string.IsNullOrWhiteSpace(eventType))
                        {
                            eventType = library.GetDictionaryItem("Global Search.Result Types." + resultItem.Fields[SiteConstants.Fields.NodeTypeAlias]);
                        }

                        eventItem.SubHeading = GetEventDate(node);
                        eventItem.SearchResultType = $"{eventType} - {eventItem.Campus} Campus";

                        mappedItem = eventItem;
                    }
                    else if (resultItem.Fields[SiteConstants.Fields.NodeTypeAlias] == PageBlogDetail.ModelTypeAlias)
                    {
                        var blogItem = new BlogDetailResultItem(resultItem);
                        var node = new PageBlogDetail(content);
                        var author = node.BlogAuthor.FirstOrDefault().OfType<DataAuthor>();
                        var authorName = author.FirstName + " " + author.LastName;
                        blogItem.Image = node.BannerImage;
                        blogItem.PublishedDate = node.PublishedDate;
                        blogItem.AuthorName = authorName;
                        blogItem.AuthorPortraitImage = author.PortraitImage;

                        blogItem.SubHeading = blogItem.PublishedDate.ToString("dd MMMM yyyy") + " - " + blogItem.AuthorName;
                        mappedItem = blogItem;
                    }
                    else if (resultItem.Fields[SiteConstants.Fields.NodeTypeAlias] == PageNewsDetail.ModelTypeAlias)
                    {
                        var newsItem = new NewsDetailResultItem(resultItem);
                        var node = new PageNewsDetail(content);
                        newsItem.Image = node.BannerImage;
                        newsItem.PublishedDate = node.PublishedDate;
                        newsItem.SubHeading = newsItem.PublishedDate.ToString("dd MMMM yyyy");
                        mappedItem = newsItem;
                    }
                    else
                    {
                        mappedItem = new SearchResultItem(resultItem);
                    }

                    if (string.IsNullOrWhiteSpace(mappedItem.SearchResultType))
                    {
                        var resultType = library.GetDictionaryItem("Global Search.Result Types." + resultItem.Fields[SiteConstants.Fields.NodeTypeAlias]);
                        if (string.IsNullOrWhiteSpace(resultType))
                        {
                            resultType = library.GetDictionaryItem("Global Search.Result Types.Default");
                        }

                        mappedItem.SearchResultType = resultType;
                    }

                    mappedItem.Key = content.GetKey();
                    mappedItem.Url = umbracoHelper.NiceUrl(resultItem.Id);
                    finalResults.Add(mappedItem);
                }
            }

            return finalResults;
        }

        private string GetEventDate(PageEventDetail eventItem)
        {
            var result = eventItem.StartDate.ToString("dd MMMM yyyy");
            if(eventItem.EndDate > DateTime.MinValue)
            {
                result += $" - {eventItem.EndDate:dd MMMM yyyy}, ";
            }
            result += $" {eventItem.StartTime} - {eventItem.EndTime}";

            return result;
        }
    }
}