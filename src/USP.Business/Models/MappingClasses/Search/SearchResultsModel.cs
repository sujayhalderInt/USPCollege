using System;
using System.Collections.Generic;
using Examine;
using Umbraco.Core.Models;
using USP.Business.Constants;

namespace USP.Business.Models.MappingClasses.Search
{
    public class SearchResultItem
    {
        public SearchResultItem(SearchResult result)
        {
            Result = result;
            Id = result.Id;
            Heading = result.Fields[SiteConstants.Fields.BannerHeading];
            Summary = result.Fields[SiteConstants.Fields.BannerSummary];
            Content = result.Fields[SiteConstants.Fields.MainContent];
            DocTypeAlias = result.Fields[SiteConstants.Fields.NodeTypeAlias];
        }

        public int Id { get; set; }

        public string DocTypeAlias { get; set; }
        
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }

        public string Url { get; set; }
        public string Sizes { get; set; }
        public SearchResult Result { get; set; }
        public Guid Key { get; set; }
        public string SearchResultType { get; set; }
    }

    public class CourseDetailResultItem : SearchResultItem
    {
        public string Qualification { get; set; }
        public string[] Campuses { get; set; }
        public IPublishedContent Image { get; set; }
        public string CareerSector { get; set; }

        public CourseDetailResultItem(SearchResult result) : base(result)
        {
            Qualification = result.Fields[SiteConstants.Fields.Qualification];
        }
    }

    public class CareerDetailResultItem : SearchResultItem
    {
        public IPublishedContent Image { get; set; }

        public CareerDetailResultItem(SearchResult result) : base(result)
        {
        }
    }

    public class EventDetailResultItem : SearchResultItem
    {
        public EventDetailResultItem(SearchResult result) : base(result)
        {
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Campus { get; set; }
    }

    public class BlogDetailResultItem : SearchResultItem
    {
        public BlogDetailResultItem(SearchResult result) : base(result)
        {
        }

        public IPublishedContent Image { get; set; }
        public DateTime PublishedDate { get; set; }
        public string AuthorName { get; set; }
        public IPublishedContent AuthorPortraitImage { get; set; }
    }

    public class NewsDetailResultItem : SearchResultItem
    {
        public NewsDetailResultItem(SearchResult result) : base(result)
        {
        }

        public IPublishedContent Image { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}