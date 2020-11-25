using System.Collections.Generic;

namespace USP.Business.Controllers.Base
{
    public class BaseListingController : RenderMvcSurfaceController
    {
        protected virtual Dictionary<string, List<string>> GetTaxonomy(string[] selectedTaxonomy)
        {
            var result = new Dictionary<string, List<string>>();

            if (selectedTaxonomy != null && selectedTaxonomy.Length > 0)
            {
                foreach (var taxonomy in selectedTaxonomy)
                {
                    var data = taxonomy.Split('|');
                    if (result.ContainsKey(data[0]))
                    {
                        result[data[0]].Add(data[1]);
                    }
                    else
                    {
                        result.Add(data[0], new List<string>() { data[1] });
                    }
                }
            }

            return result;
        }
    }
}
