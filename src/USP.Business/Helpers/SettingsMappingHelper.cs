using System;
using Umbraco.Core.Models;
using USP.Business.Models.ContentModels;

namespace USP.Business.Helpers
{
    public static class SettingsMappingHelper
    {
        public static Func<IPublishedContent, T> GetMapFunc<T>(string docTypeAlias)
        {
            switch (docTypeAlias.ToLower())
            {
                case "sitesettings":
                    return content => (T)(object)new SettingsSite(content);
                case "settingscookiemessage":
                    return content => (T)(object)new SettingsCookieMessage(content);
                default:
                    return null;
            }
        }
    }
}
