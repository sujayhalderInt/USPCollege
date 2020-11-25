using System;
using Skybrud.Social.Instagram;
using Skybrud.Social.Instagram.Objects;
using Umbraco.Core;
using Umbraco.Core.Cache;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class InstagramFeedService : IInstagramFeedService
    {
        public InstagramMedia[] GetLatest(string accessToken, int max = 3)
        {
            var cacheKey = $"InstaFeed.{accessToken}.{max}";

            var data = ApplicationContext.Current.ApplicationCache.RuntimeCache.GetCacheItem<InstagramMedia[]>(
                cacheKey,
                () => GetData(accessToken, max),
                TimeSpan.FromHours(6));
            return data;
        }

        private InstagramMedia[] GetData(string accessToken, int max)
        {
            var service = InstagramService.CreateFromAccessToken(accessToken);

            var user = service.Users.GetSelf().Body.Data;
            var data = service.Users.GetRecentMedia(user.Id, max).Body.Data;

            return data;
        }
    }
}
