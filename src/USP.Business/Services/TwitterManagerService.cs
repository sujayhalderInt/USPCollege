using System.Collections.Generic;
using TweetSharp;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Data;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class TwitterManagerService : ITwitterManagerService
    {
        public IEnumerable<Tweet> GetTweets(SettingsTwitter settings, int? numberOfPosts = null)
        {
            var tweetList = new List<Tweet>();

            if (settings == null ||
                string.IsNullOrWhiteSpace(settings.ConsumerKey) ||
                string.IsNullOrWhiteSpace(settings.ConsumerSecret) ||
                string.IsNullOrWhiteSpace(settings.AccessToken) ||
                string.IsNullOrWhiteSpace(settings.TokenSecret))
            {
                return tweetList;
            }

            // Set tokens
            var consumerKey = settings.ConsumerKey;
            var consumerSecret = settings.ConsumerSecret;
            var accessToken = settings.AccessToken;
            var accessTokenSecret = settings.TokenSecret;

            // Authenticate
            var service = new TwitterService(consumerKey, consumerSecret);
            service.AuthenticateWith(accessToken, accessTokenSecret);

            var showPosts = numberOfPosts ?? (settings.NoOfPosts == 0 ? 3 : settings.NoOfPosts);
            var tweets = ListTweetsOnUserTimeline(settings.AccountUsername, showPosts, true, service);

            if (tweets == null) return tweetList;

            foreach (var tweet in tweets)
            {
                tweetList.Add(new Tweet()
                {
                    Text = tweet.TextAsHtml,
                    UserName = tweet.User.Name,
                    UserScreenName = tweet.User.ScreenName.ToUpperInvariant(),
                    Id = tweet.Id.ToString(),
                    ImageUrl = tweet.Author.ProfileImageUrl,
                    TimeCreated = tweet.CreatedDate
                });
            }

            return tweetList;
        }

        private IEnumerable<TwitterStatus> ListTweetsOnUserTimeline(string screenName, int numberOfPosts, bool excludeReplies, TwitterService service)
        {
            var tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
            {
                ScreenName = screenName,
                ExcludeReplies = excludeReplies,
                Count = numberOfPosts
            });

            return tweets;
        }
    }
}
