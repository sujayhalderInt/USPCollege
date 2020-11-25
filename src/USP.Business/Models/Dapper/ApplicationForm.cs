using System;

namespace USP.Business.Models.Dapper
{
    public class ApplicationForm
    {
        //public int ID { get; set; }
        public Guid UserId { get; set; }
        public bool IsSubmitted { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public string TypeOfStudy { get; set; }
        public string Campus { get; set; }
        public bool IsStepTwoSubmitted { get; set; }
        public bool IsStepThreeSubmitted { get; set; }
        public bool IsStepFourSubmitted { get; set; }
        public bool IsStepFiveSubmitted { get; set; }
        public bool IsStepSixSubmitted { get; set; }
        public bool IsStepSevenSubmitted { get; set; }
        public bool IsStepEightSubmitted { get; set; }
        public bool IsStepNineSubmitted { get; set; }
        public bool IsStepTenSubmitted { get; set; }
        public bool IsStepElevenSubmitted { get; set; }
        public bool IsStepTwelveSubmitted { get; set; }
        public string StudentsFirstName { get; set; }
        public string StudentsMiddleName { get; set; }
        public string StudentsLastName { get; set; }
        public string StudentsPreferedName { get; set; }
        public DateTime? StudentsDateOfBirth { get; set; }
        public string StudentsGender { get; set; }
        public string StudentsHomeAddressLine1 { get; set; }
        public string StudentsHomeAddressLine2 { get; set; }
        public string StudentsHomePostCode { get; set; }
        public string StudentsTownOrCity { get; set; }
        public string StudentsMobileNumber { get; set; }
        public string StudentsHomeNumber { get; set; }
        public string StudentsEmailAddress { get; set; }
        public string EmergencyContactFirstName { get; set; }
        public string EmergencyContactLastName { get; set; }
        public string EmergencyContactRelationship { get; set; }
        public string EmergencyContactPhoneNumber { get; set; }
        public string EmergencyContactEmailAddress { get; set; }
        public string ParentOrCarerFirstName { get; set; }
        public string ParentOrCarerLastName { get; set; }
        public string ParentOrCarerRelationshipToStudent { get; set; }
        public string ParentOrCarerPhoneNumber { get; set; }
        public string ParentOrCarerEmailAddress { get; set; }
        public bool? IsParentOrCarerPrimaryContact { get; set; }
        public string PrimaryContactFirstName { get; set; }
        public string PrimaryContactLastName { get; set; }
        public string PrimaryContactRelationshipToStudent { get; set; }
        public string PrimaryContactPhoneNumber { get; set; }
        public string PrimaryContactEmailAddress { get; set; }
        public string AlevelCourseFirstChoice { get; set; }
        public string AlevelCourseSecondChoice { get; set; }
        public string AlevelCourseThirdChoice { get; set; }
        public bool? IsFirstFullLevel3Qualification { get; set; }
        public string NameOfCourseOfTheTypeSelected { get; set; }
        public string SportsAcademiesCsv { get; set; }
        public string NameOfLastCollegeOrSchool { get; set; }
        public string QualificationsJson { get; set; }
        public bool? HasDisabilityDifficulty { get; set; }
        public string DisabilitiesDifficultiesCsv { get; set; }
        public string PrimaryDisability { get; set; }
        public string StudentsEthnicity { get; set; }
        public string StudentsNationality { get; set; }
        public string StudentsReligion { get; set; }
        public string StudentsFirstLanguage { get; set; }
        public bool? ResidentOfUkEuForThreeYears { get; set; }
        public string NameOfCountryOutsideUkEu { get; set; }
        public DateTime? DateOfResidenceOutsideUkEuFrom { get; set; }
        public DateTime? DateOfResidenceOutsideUkEuTo { get; set; }
        public bool? AnyCriminalConvictionOrFinalWarning { get; set; }
        public bool ReceiveMarketingEmails { get; set; }
        public string HowDidYouHearAboutThisCourse { get; set; }
        public string MostInterestedSector { get; set; }
        public bool IsALevels { get; set; }
        public bool IsAdultLearning { get; set; }
        public bool IsApprenticeships { get; set; }
        public bool IsProfessionalQualifications { get; set; }
        public bool IsHigherEducation { get; set; }
        public string NationalInsuranceNumber { get; set; }
    }
}
