namespace USP.Business.Models.Form
{
    public class FormStep
    {
        public string StepName { get; set; }
        public string StepUrl { get; set; }
        public int StepListingOrder { get; set; }
        public bool IsSubmitted { get; set; }
        public string StepDoctypeAlias { get; set; }
    }
}
