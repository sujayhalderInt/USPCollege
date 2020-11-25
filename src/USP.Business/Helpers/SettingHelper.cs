using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;
using umbraco.NodeFactory;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Extensions;

namespace USP.Business.Helpers
{
    public enum SettingType
    {
        [Description("Nodes")] Node,
        [Description("FieldNames")] FieldName,
        [Description("Global")] Global,
        [Description("DocTypeAlias")] DocTypeAlias,
        [Description("MemberTypes")] MemberType,
        [Description("MemberGroups")] MemberGroup,
        [Description("Settings")] Settings
    }

    /// <summary>
    /// Provides main functionality for Settings from an XML file
    /// </summary>
    public class SettingHelper
    {
        private static XDocument _settingsXml;

        private static XDocument SettingsXml
        {
            get
            {
                string path;
                if (HttpContext.Current != null)
                {
                    path = HostingEnvironment.MapPath("~/config/Precedent.Settings.config");
                }
                else
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + "\\config\\Precedent.Settings.config";
                }

                return _settingsXml ?? (_settingsXml = XDocument.Load(path));
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (!attributes.Empty())
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }

        /// <summary>
        /// Get a setting value from the settings xml
        /// </summary>
        /// <param name="settingType">Section of the settings file to look in</param>
        /// <param name="key">Key of value to retrieve</param>
        /// <param name="settingXml">Xml containing settings. If empty will user SettingXml property</param>
        /// <returns>setting value, or null of it failed</returns>
        public static string GetXmlSetting(SettingType settingType, string key, XDocument settingXml = null)
        {
            try
            {
                if (settingXml == null)
                {
                    settingXml = SettingsXml;
                }

                XElement item = settingXml.Descendants(GetEnumDescription(settingType))
                                           .Elements("Item")
                                           .SingleOrDefault(x => (string)x.Attribute("name") == key);

                if (item != null)
                {
                    return item.Attribute("value")?.Value;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(MethodBase.GetCurrentMethod().DeclaringType, 
                    $"###  Website ERROR: GetXMLSetting settingType:{settingType} + key:{key} ", ex);
            }

            return null;
        }

        public static Guid GetGuidXmlSetting(SettingType settingType, string key)
        {
            var setting = GetXmlSetting(settingType, key);
            return setting.AsGuid();
        }

        public static decimal GetDecimalXmlSetting(SettingType settingType, string key)
        {
            var setting = GetXmlSetting(settingType, key);
            return setting.AsDecimal();
        }

        public static double GetDoubleXmlSetting(SettingType settingType, string key)
        {
            var setting = GetXmlSetting(settingType, key);
            return setting.AsDouble();
        }

        public static int GetIntXmlSetting(SettingType settingType, string key)
        {
            var setting = GetXmlSetting(settingType, key);
            return setting.AsInt();
        }

        public static bool GetBooleanXmlSetting(SettingType settingType, string key)
        {
            var setting = GetXmlSetting(settingType, key);
            return setting.AsBool();
        }
        
        public static T GetCurrentByDocType<T>() where T : class
        {
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            var currentNode = Node.GetCurrent();

            var item = umbHelper.TypedContent(currentNode);
            return item.As<T>();
        }

        public static IPublishedContent GetCurrentSiteRoot()
        {
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            return umbHelper.AssignedContentItem.AncestorOrSelf(1);
        }

        public static IPublishedContent GetCurrentNode()
        {
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            return umbHelper.AssignedContentItem;
        }

        public static IEnumerable<IPublishedContent> GetGlobalRoot()
        {
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            return umbHelper.TypedContentAtRoot().ToContentSet();
        }

        public static T GetSettingsByDocType<T>() where T : class
        {
            var docTypeAlias = char.ToLowerInvariant(typeof(T).Name[0]) + typeof(T).Name.Substring(1);

            var settings = GetSettingFolder()
                .DescendantsOrSelf(docTypeAlias)
                .FirstOrDefault();

            Func<IPublishedContent, T> map = SettingsMappingHelper.GetMapFunc<T>(docTypeAlias);

            //return site specific settings else get from the global settings 
            return map(settings);
        }

        public static IPublishedContent GetNode(string nodeName)
        {
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            return umbHelper.TypedContent(GetGuidXmlSetting(SettingType.Node, nodeName));
        }

        private static IPublishedContent GetSettingFolder()
        {
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            return umbHelper.TypedContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == SiteConstants.AliasSiteSettings);
        }
    }
}