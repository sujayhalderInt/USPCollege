using System;
using System.Collections.Generic;

namespace USP.Business.Models.ViewModels
{
    public class FormApplicationViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<FormApplicationItem> Results { get; set; }
    }
}