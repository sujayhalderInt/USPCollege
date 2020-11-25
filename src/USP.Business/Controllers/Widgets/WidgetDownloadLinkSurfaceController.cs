using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using Umbraco.Core.Models;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;
using USP.Business.Services.Interfaces;
using File = USP.Business.Models.ContentModels.File;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetDownloadLinkSurfaceController : DocTypeGridEditorSurfaceController
    {
        private readonly IFileTypeService _fileTypeService;

        public WidgetDownloadLinkSurfaceController(IFileTypeService fileTypeService)
        {
            _fileTypeService = fileTypeService;
        }

        public ActionResult WidgetDownloadLink()
        {
            var model = new WidgetDownloadLink(Model);
            model.DownloadLinkList = GetDownloadList(model.DownloadLink);
            return CurrentPartialView(model);
        }

        public IEnumerable<DataDownloadLink> GetDownloadList(IEnumerable<IPublishedContent> contacts)
        {
            var results = new List<DataDownloadLink>();

            if (contacts != null)
            {
                foreach (var publishedContent in contacts)
                {
                    DataDownloadLink data;

                    if (publishedContent?.ContentType.Alias == SiteConstants.AliasDataDownloadLinkPicker)
                    {
                        var picker = new DataExistingDownloadLink(publishedContent);
                        var pickedItem = picker.DownloadLink.FirstOrDefault();
                        data = new DataDownloadLink(pickedItem);
                    }
                    else
                    {
                        data = new DataDownloadLink(publishedContent);
                    }

                    if (data.DownloadFile != null)
                    {
                        var file = new File(data.DownloadFile);
                        data.FileType = _fileTypeService.GetFileType(file.UmbracoExtension);
                    }

                    results.Add(data);
                }
            }
            return results;
        }
    }
}
