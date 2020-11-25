using USP.Business.Constants;
using USP.Business.Services.Interfaces;
using USP.Business.Services.WidgetIndexers;

namespace USP.Business.Services
{
    public class WidgetIndexFactory
    {
        public GridComponentIndexer GetGridIndexer()
        {
            return new GridComponentIndexer();
        }

        public IWidgetIndexer GetIndexer(string alias)
        {
            switch (alias)
            {
                case SiteConstants.GridWidgets.AliasRichTextEditor:
                    return new RichTextWidgetIndexer();
                default:
                    return null;
            }
        }
    }
}
