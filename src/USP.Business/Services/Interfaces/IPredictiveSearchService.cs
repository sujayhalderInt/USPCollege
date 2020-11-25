using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Business.Models.MappingClasses.Search;

namespace USP.Business.Services.Interfaces
{
    public interface IPredictiveSearchService
    {
        SearchResultsViewModel PredictiveSearch(PredictiveSearchParameters predictiveSearchParameters);
    }
}
