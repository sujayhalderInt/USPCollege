using System.IO;
using System.Web;
using System.Web.Hosting;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace USP.Business.Helpers
{
    public static class UmbracoContextFake
    {
        public static UmbracoContext FakeContext()
        {
            var dummyContext =
                new HttpContextWrapper(
                    new HttpContext(new SimpleWorkerRequest("/", string.Empty, new StringWriter())));
            return UmbracoContext.EnsureContext(
                dummyContext,
                ApplicationContext.Current,
                new WebSecurity(dummyContext, ApplicationContext.Current),
                UmbracoConfig.For.UmbracoSettings(),
                UrlProviderResolver.Current.Providers,
                false);
        }
    }
}