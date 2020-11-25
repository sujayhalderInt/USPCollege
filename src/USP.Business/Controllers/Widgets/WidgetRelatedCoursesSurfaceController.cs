using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using Umbraco.Core.Models;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetRelatedCoursesSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetRelatedCourses()
        {
            var model = new WidgetRelatedCourses(Model);
            model.RelatedCourse = GetRelatedCourses(model.Courses);
            return CurrentPartialView(model);
        }

        public IEnumerable<DataRelatedCourses> GetRelatedCourses(IEnumerable<IPublishedContent> courses)
        {
            var results = new List<DataRelatedCourses>();

            if (courses != null)
            {
                foreach (var publishedContent in courses)
                {
                    DataRelatedCourses data;

                    if (publishedContent?.ContentType.Alias == SiteConstants.AliasDataRelatedCoursePicker)
                    {
                        var picker = new DataExistingRelatedCourse(publishedContent);
                        var pickedItem = picker.Course.FirstOrDefault();
                        data = new DataRelatedCourses(pickedItem);
                    }
                    else
                    {
                        data = new DataRelatedCourses(publishedContent);
                    }

                    results.Add(data);
                }
            }
            return results;
        }
    }

}
