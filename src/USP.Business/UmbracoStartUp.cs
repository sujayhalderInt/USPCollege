using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using umbraco;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Trees;
using USP.Business.Binders;
using USP.Business.Constants;
using USP.Business.Controllers.Partials;
using USP.Business.Helpers;
using USP.Business.Mvc;
using USP.Business.Services;
using USP.Business.Services.Interfaces;

namespace USP.Business
{
    public class UmbracoStartUp : IApplicationEventHandler
    {
        /// <summary>
        /// ApplicationContext is created and other static objects that require initialization have been setup
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        /// <summary>
        /// All resolvers have been initialized but resolution is not frozen so they can be modified in this method
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ModelBinders.Binders.Add(typeof(string), new StringTrimModelBinder());

            var umbracoEngine = ViewEngines.Engines.OfType<Umbraco.Web.Mvc.RenderViewEngine>().FirstOrDefault();
            if (umbracoEngine != null)
            {
                // {0} Action Name
                // {1} Controller Name
                // {2} Area Name
                var partialViewLocations = umbracoEngine.PartialViewLocationFormats.ToList();
                partialViewLocations.Add("~/Views/Partials/{0}/{0}.cshtml");
                partialViewLocations.Add("~/Views/Partials/{1}/{0}.cshtml");
                partialViewLocations.Add("~/Views/Partials/Search/{0}.cshtml");
                partialViewLocations.Add("~/Views/Partials/Listing/{0}.cshtml");
                partialViewLocations.Add("~/Views/Widgets/{0}.cshtml");
                umbracoEngine.PartialViewLocationFormats = partialViewLocations.ToArray();

                var viewLocations = umbracoEngine.ViewLocationFormats.ToList();
                viewLocations.Add("~/Views/Pages/{1}/{0}.cshtml");
                viewLocations.Add("~/Views/ApplicationForm/{1}/{0}.cshtml");
                umbracoEngine.ViewLocationFormats = viewLocations.ToArray();
            }

            RouteTable.Routes.MapRoute(
                name: "application",
                url: GlobalSettings.UmbracoMvcArea + "/backoffice/applications/{action}",
                defaults: new
                {
                    controller = "ApplicationApi",
                    action = "GetApplications"
                });
            RouteTable.Routes.MapUmbracoRoute(
                "Sitemap",
                "sitemap",
                new
                {
                    controller = "Sitemap",
                    action = "Index",
                },
                new GetRootNodeByDocType(SiteConstants.AliasHomePage));

        }

        /// <summary>
        /// Bootup is completed, this allows you to perform any other bootup logic required for the application.
        /// Resolution is frozen so now they can be used to resolve instances.
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(SeoController).Assembly);
            builder.RegisterApiControllers(typeof(SeoController).Assembly);

            builder.RegisterType<SeoService>().As<ISeoService>().SingleInstance();
            builder.RegisterType<SearchService>().As<ISearchService>().SingleInstance();
            builder.RegisterType<PredictiveSearchService>().As<IPredictiveSearchService>().SingleInstance();
            builder.RegisterType<SearchMappingService>().As<ISearchMappingService>().SingleInstance();
            builder.RegisterType<TwitterManagerService>().As<ITwitterManagerService>().SingleInstance();
            builder.RegisterType<InstagramFeedService>().As<IInstagramFeedService>().SingleInstance();
            builder.RegisterType<AuthorizationService>().As<IAuthorizationService>().SingleInstance();
            builder.RegisterType<EmailService>().As<IEmailService>().SingleInstance();
            builder.RegisterType<RecaptchaService>().As<IRecaptchaService>().SingleInstance();
            builder.RegisterType<DatabaseService>().As<IDatabaseService>().SingleInstance();

            //Switch to Fake database for testing purposes
            //builder.RegisterType<FakeRemsDatabaseService>().As<IRemsDatabaseService>().SingleInstance();
            builder.RegisterType<RemsDatabaseService>().As<IRemsDatabaseService>().SingleInstance();

            builder.RegisterType<FileTypeService>().As<IFileTypeService>().SingleInstance();
            builder.RegisterType<CourseImportService>().As<ICourseImportService>().SingleInstance();

            //register umbraco webapi controllers used by the admin site
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterApiControllers(typeof(ApplicationTreeController).Assembly);

            //added by bhanu (form)
            builder.RegisterApiControllers(typeof(Umbraco.Forms.Web.Trees.FormTreeController).Assembly);

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}