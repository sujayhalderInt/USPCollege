using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DevTrends.MvcDonutCaching;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Deploy;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Sync;
using Umbraco.Web;
using Umbraco.Web.Cache;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Services.Interfaces;

namespace USP.Business.Mvc
{
    public class UmbracoPublishCacheHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            CacheRefresherBase<PageCacheRefresher>.CacheUpdated += CacheRefresherBaseOnCacheUpdated;
            CacheRefresherBase<DictionaryCacheRefresher>.CacheUpdated += CacheRefresherBaseOnCacheUpdated;
            CacheRefresherBase<MediaCacheRefresher>.CacheUpdated += CacheRefresherBaseOnCacheUpdated;
        }

        private void CacheRefresherBaseOnCacheUpdated(MediaCacheRefresher sender, CacheRefresherEventArgs cacheRefresherEventArgs)
        {
            ClearCache(cacheRefresherEventArgs);
        }

        private void CacheRefresherBaseOnCacheUpdated(DictionaryCacheRefresher sender, CacheRefresherEventArgs cacheRefresherEventArgs)
        {
            ClearCache(cacheRefresherEventArgs);
        }

        private void CacheRefresherBaseOnCacheUpdated(PageCacheRefresher sender, CacheRefresherEventArgs cacheRefresherEventArgs)
        {
            ClearCache(cacheRefresherEventArgs);
        }

        private void ClearCache(CacheRefresherEventArgs args)
        {
            if (args?.MessageObject != null)
            {
                var cacheManager = new OutputCacheManager();

                if (args.MessageType == MessageType.RefreshAll)
                {
                    cacheManager.RemoveItems();
                    ClearFileTypes();
                    LogHelper.Debug<UmbracoPublishCacheHandler>("Clearing all Caches");
                    return;
                }

                var contentType = string.Empty;
                var contentId = -99;

                if (args.MessageType == MessageType.RefreshByInstance ||
                    args.MessageType == MessageType.RemoveByInstance)
                {
                    var c = args.MessageObject as Content;
                    contentType = c?.ContentType.Alias;
                    contentId = c?.Id ?? -99;
                }

                if (args.MessageType == MessageType.RefreshById || args.MessageType == MessageType.RemoveById)
                {
                    var id = (int)args.MessageObject;

                    if (UmbracoContext.Current == null)
                    {
                        UmbracoContextFake.FakeContext();
                    }
                    var helper = new UmbracoHelper(UmbracoContext.Current);
                    var publishedNode = helper.TypedContent(id);
                    if (publishedNode != null)
                    {
                        contentType = publishedNode.DocumentTypeAlias;
                        contentId = id;
                    }
                }

                // IF CACHING PAGES UNCOMMENT BELOW

                //if (contentId >= 0)
                //{
                //    var enumerableCache = OutputCache.Instance as IEnumerable<KeyValuePair<string, object>>;

                //    if (enumerableCache == null)
                //    {
                //        return;
                //    }

                //    // get all output cache keys that contain current page id
                //    var keysToDelete = enumerableCache
                //        .Where(x => !string.IsNullOrEmpty(x.Key) && x.Key.Contains($"node={contentId};"))
                //        .Select(x => x.Key)
                //        .ToList();

                //    // remove all caches for current page
                //    foreach (var key in keysToDelete)
                //    {
                //        OutputCache.Instance.Remove(key);
                //    }

                //    LogHelper.Debug<UmbracoPublishCacheHandler>($"Removing Caches for {contentId}: {string.Join(",", keysToDelete)}");
                //}

                if (!string.IsNullOrWhiteSpace(contentType))
                {
                    if (contentType.StartsWith("page") || contentType == SiteConstants.AliasSiteSettings)
                    {
                        cacheManager.RemoveItems("Navigation");
                        LogHelper.Debug<UmbracoPublishCacheHandler>("Clearing Navigation Cache");
                    }

                    if (contentType.StartsWith(SiteConstants.AliasSiteSettings))
                    {
                        cacheManager.RemoveItems("Footer");
                        ClearFileTypes();
                        LogHelper.Debug<UmbracoPublishCacheHandler>("Clearing Footer Cache");
                    }
                }
            }
        }

        public void ClearFileTypes()
        {
            try
            {
                var service = DependencyResolver.Current.GetService<IFileTypeService>();
                if (service != null)
                {
                    service.ResetCache();
                }
                else
                {
                    LogHelper.Warn<UmbracoPublishCacheHandler>("Unable to resolve File Type Service");
                }
            }
            catch (Exception e)
            {
                LogHelper.Error<UmbracoPublishCacheHandler>("Error clearing file types", e);
            }
        }
    }
}