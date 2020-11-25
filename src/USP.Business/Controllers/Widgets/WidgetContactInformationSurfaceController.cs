using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using Umbraco.Core.Models;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetContactInformationSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetContactInformation()
        {
            var model = new WidgetContactInformation(Model);
            model.ContactList = GetContactInformation(model.Contacts);
            return CurrentPartialView(model);
        }

        public IEnumerable<DataContactInformation> GetContactInformation(IEnumerable<IPublishedContent> contacts)
        {
            var results = new List<DataContactInformation>();

            if (contacts != null)
            {
                foreach (var publishedContent in contacts)
                {
                    DataContactInformation data;

                    if (publishedContent?.ContentType.Alias == SiteConstants.AliasDataContactInformationPicker)
                    {
                        var picker = new DataSharedContactInformation(publishedContent);
                        var pickedItem = picker.ContactInformationItem.FirstOrDefault();
                        data = new DataContactInformation(pickedItem);
                    }
                    else
                    {
                        data = new DataContactInformation(publishedContent);
                    }

                    results.Add(data);
                }
            }
            return results;
        }
    }

}
