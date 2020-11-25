using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Umbraco.Core;
using Umbraco.Core.Services;
using USP.Business.Models.Dapper;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class RemsDatabaseService : IRemsDatabaseService
    {
        private IDataTypeService _service = null;

        public IDataTypeService DataTypeService
        {
            get => _service ?? (ApplicationContext.Current.Services.DataTypeService);
            set => _service = value;
        }

        public List<CourseInformation> GetCourses()
        {
            var results = new List<CourseInformation>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["RemsDb"].ConnectionString))
            {
                var rows = db.Query<CourseInformationRow>("Prec_GetCourses", commandType: CommandType.StoredProcedure).ToList();
                var courses = rows.GroupBy(x => x.CourseCode).ToDictionary(x => x.Key.Trim(), x => x.ToList());

                foreach (var courseData in courses)
                {
                    var courseInfo = Map(courseData);
                    results.Add(courseInfo);
                }
            }

            return results;
        }

        private CourseInformation Map(KeyValuePair<string, List<CourseInformationRow>> courseData)
        {
            var course = new CourseInformation();
            course.CourseCode = courseData.Key;

            foreach (var row in courseData.Value)
            {
                switch (row.ProspectusCategoryID)
                {
                    case 1:
                        course.Title = row.ProspectusCategoryText;
                        break;
                    case 2:
                        course.Qualification = row.ProspectusCategoryText;
                        break;
                    case 3:
                        course.CourseType = row.ProspectusCategoryText;
                        break;
                    case 4:
                        course.Level = row.ProspectusCategoryText;
                        break;
                    case 5:
                        course.AwardingBody = row.ProspectusCategoryText;
                        break;
                    case 6:
                        course.Duration = row.ProspectusCategoryText;
                        break;
                    case 7:
                        course.DaysandTime = row.ProspectusCategoryText;
                        break;
                    case 8:
                        course.StartDate = row.ProspectusCategoryText;
                        break;
                    case 9:
                        course.Fee = row.ProspectusCategoryText;
                        break;
                    case 10:
                        course.CareerSector = row.ProspectusCategoryText;
                        break;
                    case 11:
                        course.Campus = row.ProspectusCategoryText;
                        break;
                    case 12:
                        course.Description = row.ProspectusCategoryText;
                        break;
                    case 13:
                        course.ImageID = row.ProspectusCategoryText;
                        break;
                    case 14:
                        course.OverlayColour = MapColor(row.ProspectusCategoryText);
                        break;
                    case 15:
                        course.BannerHeading = row.ProspectusCategoryText;
                        break;
                    case 16:
                        course.Content = row.ProspectusCategoryText;
                        break;
                }
            }

            return course;
        }

        private string MapColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                return null;
            }

            var statusEditor = DataTypeService.GetAllDataTypeDefinitions().First(x => x.Name == "Overlay Colour Dropdown");
            int preValueId = DataTypeService.GetPreValuesCollectionByDataTypeId(statusEditor.Id)
                .PreValuesAsDictionary.Where(d => d.Value.Value.ToLower() == color.Trim().ToLower())
                .Select(f => f.Value.Id).FirstOrDefault();

            return preValueId.ToString();
        }
    }
}
