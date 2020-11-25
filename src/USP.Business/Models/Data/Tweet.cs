using System;

namespace USP.Business.Models.Data
{
    public class Tweet
    {
        public string Text { get; set; }
        public string UserScreenName { get; set; }
        public string UserName { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime TimeCreated { get; set; }

        public int MinutesAgo
        {
            get
            {
                var variable = DateTime.UtcNow - TimeCreated;
                return variable.Minutes;
            }
        }

        public int HoursAgo
        {
            get
            {
                var variable = DateTime.UtcNow - TimeCreated;
                return variable.Hours;
            }
        }

        public int DaysAgo
        {
            get
            {
                var variable = DateTime.UtcNow - TimeCreated;
                return variable.Days;
            }
        }
    }
}
