using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Business.Models.Dapper;

namespace USP.Business.Services.Interfaces
{
    public interface IDatabaseService
    {
        ApplicationForm GetApplicationData(Guid userId);
        int InsertApplicationData(Guid userId, ApplicationForm form);
        List<ApplicationForm> GetSubmittedApplicationData(DateTime? startDate = null, DateTime? endDate = null);
    }
}
