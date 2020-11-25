namespace USP.Business.Models.ContentModels
{
    public partial class  SettingsSite
    {
        public bool HasSocialLinks
        {
            get
            {
                return !(string.IsNullOrWhiteSpace(TwitterLink)
                         && string.IsNullOrWhiteSpace(LinkedInLink)
                         && string.IsNullOrWhiteSpace(FacebookLink)
                         && string.IsNullOrWhiteSpace(YouTubeLink)
                         );
            }
        }
    }
}
