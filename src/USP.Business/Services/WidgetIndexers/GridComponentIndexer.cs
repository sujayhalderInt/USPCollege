using System.Text;
using USP.Business.Models.Data;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services.WidgetIndexers
{
    public class GridComponentIndexer
    {
        public string IndexGrid(GridLayout gridControl)
        {
            var mainContent = new StringBuilder();
            var factory = new WidgetIndexFactory();
            var data = gridControl;
            foreach (var section in data.Sections)
            {
                foreach (var sectionRow in section.Rows)
                {
                    foreach (var sectionRowArea in sectionRow.Areas)
                    {
                        foreach (var control in sectionRowArea.Controls)
                        {
                            string alias = control.editor.alias;
                            IWidgetIndexer indexer = factory.GetIndexer(alias);
                            var extractedData = indexer?.IndexComponent(control);
                            if (extractedData == null) continue;
                            mainContent.Append(extractedData);
                        }
                    }
                }
            }

            return mainContent.ToString();
        }
    }
}