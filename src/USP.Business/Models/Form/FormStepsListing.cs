using System.Collections.Generic;

namespace USP.Business.Models.Form
{
    public class FormStepsListing
    {
        public IEnumerable<FormStep> FormSteps { get; set; }

        public bool IsUserLoggedIn { get; set; }

        public int CurrentFormStep { get; set; }
    }
}
