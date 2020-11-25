using System.Collections.Generic;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Data;

namespace USP.Business.Services.Interfaces
{
    public interface ITwitterManagerService
    {
        IEnumerable<Tweet> GetTweets(SettingsTwitter settings, int? numberOfPosts = null);

    }
}
