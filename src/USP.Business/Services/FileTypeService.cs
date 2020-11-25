using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web;
using USP.Business.Extensions;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class FileTypeService : IFileTypeService
    {
        private Dictionary<string, string> _extensions = null;
        private object dlock = new Object();

        public string GetFileType(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return extension;

            if (_extensions == null)
            {
                _extensions = GetExtensions();
            }

            var cleanExtension = extension.Replace(".", "").ToUpper();
            var extensions = _extensions;
            if (extensions.ContainsKey(cleanExtension))
            {
                return extensions[cleanExtension];
            }

            return cleanExtension;
        }

        public void ResetCache()
        {
            lock (dlock)
            {
                var d = GetExtensions();
                _extensions = d;
            }
        }

        private Dictionary<string, string> GetExtensions()
        {
            var extensions = new Dictionary<string, string>();

            var context = UmbracoContext.Current ?? UmbracoContextFake.FakeContext();

            var helper = new UmbracoHelper(context);
            var setting = helper.ContentQuery.TypedContentSingleAtXPath("//settingsSite");
            if (setting == null)
                return extensions;

            var model = new SettingsSite(setting);

            if (model.FileTypeDescriptions.IsNullOrEmpty())
                return extensions;

            foreach (var fileType in model.FileTypeDescriptions.OfType<FileExtension>())
            {
                foreach (var fileExtension in fileType.FileExtensions)
                {
                    var cleanExtension = fileExtension.Replace(".", "").ToUpper();
                    if (!extensions.ContainsKey(cleanExtension))
                    {
                        extensions.Add(cleanExtension, fileType.FileType);
                    }
                }
            }

            return extensions;
        }
    }
}
