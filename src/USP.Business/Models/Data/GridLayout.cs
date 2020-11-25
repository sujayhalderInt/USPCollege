using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace USP.Business.Models.Data
{
    public class GridLayout
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sections")]
        public IEnumerable<Grid> Sections { get; set; }
    }

    public class Grid
    {
        [JsonProperty("grid")]
        public int? Size { get; set; }
        [JsonProperty("rows")]
        public IEnumerable<GridRow> Rows { get; set; }
    }

    public class GridRow
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("areas")]
        public IEnumerable<GridArea> Areas { get; set; }
    }

    public class GridArea
    {
        [JsonProperty("grid")]
        public int? Size { get; set; }
        [JsonProperty("controls")]
        public IEnumerable<dynamic> Controls { get; set; }
    }
}
