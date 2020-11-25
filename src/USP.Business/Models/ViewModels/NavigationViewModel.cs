using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Business.Extensions;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Dapper;

namespace USP.Business.Models.ViewModels
{

    public class NavigationBucketItem
    {
        public NavigationBucketItem()
        {
            TertiaryItem = new List<IBaseNavigation>();
        }

        public IBaseNavigation SecondaryItem { get; set; }
        public List<IBaseNavigation> TertiaryItem { get; set; }

        public int BucketCount
        {
            get
            {
                var count = 0;
                if (!TertiaryItem.IsNullOrEmpty())
                {
                    count += TertiaryItem.Count;
                }
                return count;
            }
        }

        public int InclusiveBucketCount
        {
            get
            {
                var count = 1;
                if (!TertiaryItem.IsNullOrEmpty())
                {
                    count += TertiaryItem.Count;
                }
                return count;
            }
        }
    }

    public class NavigationBucket
    {
        public NavigationBucket()
        {
            Buckets = new List<NavigationBucketItem>();
        }

        public List<NavigationBucketItem> Buckets { get; set; }

        public int BucketCount
        {
            get { return Buckets.Sum(b => b.BucketCount); }
        }

        public int InclusiveBucketCount
        {
            get { return Buckets.Sum(b => b.InclusiveBucketCount); }
        }
    }

    public class NavigationMenuItem
    {
        public NavigationMenuItem()
        {
            Buckets = new NavigationBucket[4];
        }

        public IBaseNavigation PrimaryNavigationItem { get; set; }
        public NavigationBucket[] Buckets { get; set; }
    }

    public class NavigationViewModel
    {
        public NavigationViewModel()
        {
            NavigationItems = new List<NavigationMenuItem>();
        }

        public List<NavigationMenuItem> NavigationItems { get; set; }
        public IEnumerable<IBaseNavigation> UtilityLinks { get; set; }
        public SettingsSite Settings { get; set; }
    }

    public class NavigationApplyButtonModel
    {
        public ApplicationForm ApplicationForm { get; set;}
        public string FormUrl { get; set; }
    }
}
