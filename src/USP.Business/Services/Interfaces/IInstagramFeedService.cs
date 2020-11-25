using Skybrud.Social.Instagram.Objects;

namespace USP.Business.Services.Interfaces
{
    public interface IInstagramFeedService
    {
        InstagramMedia[] GetLatest(string accessToken, int max = 3);
    }
}