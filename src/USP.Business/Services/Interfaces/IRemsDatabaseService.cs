using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Business.Models.Dapper;

namespace USP.Business.Services.Interfaces
{
    public interface IRemsDatabaseService
    {
        List<CourseInformation> GetCourses();
    }
}
