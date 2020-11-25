using Umbraco.Core.Models;
using USP.Business.Models.MappingClasses;

namespace USP.Business.Services.Interfaces
{
    public interface ISeoService
    {
        SeoMetaData GetSeoMetaData(IPublishedContent content);
    }
}
