namespace USP.Business.Models.Data
{
    public class ListingPostback
    {
        public string[] SelectedTaxonomy { get; set; }
        public string[] SelectedDocTypes { get; set; }


        public string SortBy { get; set; }
        public string SearchText { get; set; }

        public int Page { get; set; }



        public string BtnApplyFilters { get; set; }
        public string BtnClearFilters { get; set; }
        public string BtnLoadMore { get; set; }
        public string BtnSearch { get; set; }
        public string BtnPage { get; set; }
        public string BtnPagePrev { get; set; }
        public string BtnPageNext { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
