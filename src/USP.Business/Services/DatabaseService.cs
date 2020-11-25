using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using USP.Business.Models.Dapper;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class DatabaseService : IDatabaseService             
    {
        public ApplicationForm GetApplicationData(Guid userId)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationDb"].ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                return db.Query<ApplicationForm>("GetApplication", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<ApplicationForm> GetSubmittedApplicationData(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationDb"].ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startDate", startDate);
                parameters.Add("@endDate", endDate);
                return db.Query<ApplicationForm>("GetSubmittedApplications", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public int InsertApplicationData(Guid userId, ApplicationForm form)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationDb"].ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                parameters.Add("@IsSubmitted", form.IsSubmitted);
                parameters.Add("@DateSubmitted", form.DateSubmitted);
                parameters.Add("@TypeOfStudy", form.TypeOfStudy);
                parameters.Add("@Campus", form.Campus);
                parameters.Add("@IsStepTwoSubmitted", form.IsStepTwoSubmitted);
                parameters.Add("@IsStepThreeSubmitted", form.IsStepThreeSubmitted);
                parameters.Add("@IsStepFourSubmitted", form.IsStepFourSubmitted);
                parameters.Add("@IsStepFiveSubmitted", form.IsStepFiveSubmitted);
                parameters.Add("@IsStepSixSubmitted", form.IsStepSixSubmitted);
                parameters.Add("@IsStepSevenSubmitted", form.IsStepSevenSubmitted);
                parameters.Add("@IsStepEightSubmitted", form.IsStepEightSubmitted);
                parameters.Add("@IsStepNineSubmitted", form.IsStepNineSubmitted);
                parameters.Add("@IsStepTenSubmitted", form.IsStepTenSubmitted);
                parameters.Add("@IsStepElevenSubmitted", form.IsStepElevenSubmitted);
                parameters.Add("@IsStepTwelveSubmitted", form.IsStepTwelveSubmitted);
                parameters.Add("@StudentsFirstName", form.StudentsFirstName);
                parameters.Add("@StudentsMiddleName", form.StudentsMiddleName);
                parameters.Add("@StudentsLastName", form.StudentsLastName);
                parameters.Add("@StudentsPreferedName", form.StudentsPreferedName);
                parameters.Add("@StudentsDateOfBirth", form.StudentsDateOfBirth);
                parameters.Add("@StudentsGender", form.StudentsGender);
                parameters.Add("@StudentsHomeAddressLine1", form.StudentsHomeAddressLine1);
                parameters.Add("@StudentsHomeAddressLine2", form.StudentsHomeAddressLine2);
                parameters.Add("@StudentsHomePostCode", form.StudentsHomePostCode);
                parameters.Add("@StudentsTownOrCity", form.StudentsTownOrCity);
                parameters.Add("@StudentsMobileNumber", form.StudentsMobileNumber);
                parameters.Add("@StudentsHomeNumber", form.StudentsHomeNumber);
                parameters.Add("@StudentsEmailAddress", form.StudentsEmailAddress);
                parameters.Add("@EmergencyContactFirstName", form.EmergencyContactFirstName);
                parameters.Add("@EmergencyContactLastName", form.EmergencyContactLastName);
                parameters.Add("@EmergencyContactRelationship", form.EmergencyContactRelationship);
                parameters.Add("@EmergencyContactPhoneNumber", form.EmergencyContactPhoneNumber);
                parameters.Add("@EmergencyContactEmailAddress", form.EmergencyContactEmailAddress);
                parameters.Add("@ParentOrCarerFirstName", form.ParentOrCarerFirstName);
                parameters.Add("@ParentOrCarerLastName", form.ParentOrCarerLastName);
                parameters.Add("@ParentOrCarerRelationshipToStudent", form.ParentOrCarerRelationshipToStudent);
                parameters.Add("@ParentOrCarerPhoneNumber", form.ParentOrCarerPhoneNumber);
                parameters.Add("@ParentOrCarerEmailAddress", form.ParentOrCarerEmailAddress);
                parameters.Add("@IsParentOrCarerPrimaryContact", form.IsParentOrCarerPrimaryContact);
                parameters.Add("@PrimaryContactFirstName", form.PrimaryContactFirstName);
                parameters.Add("@PrimaryContactLastName", form.PrimaryContactLastName);
                parameters.Add("@PrimaryContactRelationshipToStudent", form.PrimaryContactRelationshipToStudent);
                parameters.Add("@PrimaryContactPhoneNumber", form.PrimaryContactPhoneNumber);
                parameters.Add("@PrimaryContactEmailAddress", form.PrimaryContactEmailAddress);
                parameters.Add("@AlevelCourseFirstChoice", form.AlevelCourseFirstChoice);
                parameters.Add("@AlevelCourseSecondChoice", form.AlevelCourseSecondChoice);
                parameters.Add("@AlevelCourseThirdChoice", form.AlevelCourseThirdChoice);
                parameters.Add("@IsFirstFullLevel3Qualification", form.IsFirstFullLevel3Qualification);
                parameters.Add("@NameOfCourseOfTheTypeSelected", form.NameOfCourseOfTheTypeSelected);
                parameters.Add("@SportsAcademiesCsv", form.SportsAcademiesCsv);
                parameters.Add("@NameOfLastCollegeOrSchool", form.NameOfLastCollegeOrSchool);
                parameters.Add("@QualificationsJson", form.QualificationsJson);
                parameters.Add("@HasDisabilityDifficulty", form.HasDisabilityDifficulty);
                parameters.Add("@DisabilitiesDifficultiesCsv", form.DisabilitiesDifficultiesCsv);
                parameters.Add("@PrimaryDisability", form.PrimaryDisability);
                parameters.Add("@StudentsEthnicity", form.StudentsEthnicity);
                parameters.Add("@StudentsNationality", form.StudentsNationality);
                parameters.Add("@StudentsReligion", form.StudentsReligion);
                parameters.Add("@StudentsFirstLanguage", form.StudentsFirstLanguage);
                parameters.Add("@ResidentOfUkEuForThreeYears", form.ResidentOfUkEuForThreeYears);
                parameters.Add("@NameOfCountryOutsideUkEu", form.NameOfCountryOutsideUkEu);
                parameters.Add("@DateOfResidenceOutsideUkEuFrom", form.DateOfResidenceOutsideUkEuFrom);
                parameters.Add("@DateOfResidenceOutsideUkEuTo", form.DateOfResidenceOutsideUkEuTo);
                parameters.Add("@AnyCriminalConvictionOrFinalWarning", form.AnyCriminalConvictionOrFinalWarning);
                parameters.Add("@ReceiveMarketingEmails", form.ReceiveMarketingEmails);
                parameters.Add("@HowDidYouHearAboutThisCourse", form.HowDidYouHearAboutThisCourse);
                parameters.Add("@IsALevels", form.IsALevels);
                parameters.Add("@IsAdultLearning", form.IsAdultLearning);
                parameters.Add("@IsApprenticeships", form.IsApprenticeships);
                parameters.Add("@IsProfessionalQualifications", form.IsProfessionalQualifications);
                parameters.Add("@IsHigherEducation", form.IsHigherEducation);
                parameters.Add("@NationalInsuranceNumber", form.NationalInsuranceNumber);
                parameters.Add("@MostInterestedSector", form.MostInterestedSector);

                var rowsAffected = db.Execute("InsertApplication", parameters, commandType: CommandType.StoredProcedure);
                return rowsAffected;
            }
        }
    }
}
