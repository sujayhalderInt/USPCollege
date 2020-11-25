using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace USP.Business.Mvc
{
    public class GetRootNodeByDocType : UmbracoVirtualNodeRouteHandler
    {
        private readonly string _alias;

        protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            var h = new UmbracoHelper(umbracoContext);
            var home = h.TypedContentAtRoot().First(x => x.DocumentTypeAlias == _alias);
            return home;
        }

        public GetRootNodeByDocType(string docTypeAlias)
        {
            _alias = docTypeAlias;
        }
    }
}
