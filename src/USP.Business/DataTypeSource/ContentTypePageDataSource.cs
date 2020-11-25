using System.Collections.Generic;
using System.Linq;
using nuPickers.Shared.DotNetDataSource;
using Umbraco.Core;

namespace USP.Business.DataTypeSource
{
    public class ContentTypePageDataSource : IDotNetDataSource
    {
        public IEnumerable<KeyValuePair<string, string>> GetEditorDataItems(int contextId)
        {
            var contentTypesToDisplay = new List<KeyValuePair<string, string>>();

            var allContentTypes = ApplicationContext.Current.Services.ContentTypeService.GetAllContentTypes().Where(x=>x.ContentTypeCompositionExists("baseDocTypeSearchFilter"));

            foreach (var contentType in allContentTypes)
            {
                var ct = new KeyValuePair<string, string>(contentType.Alias, contentType.Name);
                contentTypesToDisplay.Add(ct);
            }

            return contentTypesToDisplay.OrderBy(x => x.Value);
        }
    }
}