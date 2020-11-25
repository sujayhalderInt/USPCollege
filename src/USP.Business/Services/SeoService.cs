using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using USP.Business.Models.MappingClasses;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class SeoService : ISeoService
    {
        public SeoMetaData GetSeoMetaData(IPublishedContent content)
        {
            return new SeoMetaData
            {
                MetaDataTitle = content.GetPropertyValue<string>("metadataTitle"),
                MetaDataDescription = content.GetPropertyValue<string>("metadataDescription"),
                MetaDataKeywords = string.Join(",", content.GetPropertyValue<IEnumerable<string>>("metadataKeywords"))
            };
        }
    }
}
