using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Data;
using USP.Business.Models.Form;
using USP.Business.Models.ViewModels;
using USP.Business.Mvc;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.API
{
    [Umbraco.Web.Mvc.UmbracoAuthorizeAttribute()]
    public class ApplicationApiController : UmbracoController
    {
        private readonly IDatabaseService _databaseService;

        public ApplicationApiController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public ActionResult GetApplications(DateTime? startDate = null, DateTime? endDate=null)
        {
            if (endDate.HasValue)
            {
                endDate = endDate.Value.AddSeconds(86399);
            }
            var data = _databaseService.GetSubmittedApplicationData(startDate, endDate);

            var step11 = FormHelper.GetFormStepsHolderIPublishedContent()
                ?.Descendant(SiteConstants.AliasApplicationForm.StepEleven);

            if (step11 == null)
            {
                throw new Exception("Cannot find step 11");
            }

            var results = new List<FormApplicationItem>();

            var viewModel = new FormStepEleven(step11);

            foreach (var applicationForm in data)
            {
                var item = new FormApplicationItem
                {
                    HeadingsAndLabels = GetHeaders(viewModel),
                    ApplicationForm = applicationForm,
                    Qualifications = null
                };

                if (!string.IsNullOrEmpty(applicationForm.QualificationsJson))
                {
                    item.Qualifications = JsonConvert.DeserializeObject<List<FormStepNineQualifications>>(applicationForm.QualificationsJson);
                }

                results.Add(item);
            }

            var viewmodel = new FormApplicationViewModel();
            viewmodel.StartDate = startDate;
            viewmodel.EndDate = endDate;
            viewmodel.Results = results;

            return PartialView("~/Views/Partials/Export/Applications.cshtml", viewmodel);
        }

        private Step11HeadingsAndLabels GetHeaders(FormStepEleven viewModel)
        {
            return new Step11HeadingsAndLabels
            {
                StudentsPersonalInformationReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingStudentsPersonalInformation) ? viewModel.ReviewHeadingStudentsPersonalInformation : "Student's personal information",
                StudentsContactInformationReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingStudentsContactInformation) ? viewModel.ReviewHeadingStudentsContactInformation : "Student's contact information",
                EmergencyContactReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingEmergencyContact) ? viewModel.ReviewHeadingEmergencyContact : "Emergency contact",
                ParentCarerDetailsReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingParentCarerDetails) ? viewModel.ReviewHeadingParentCarerDetails : "Parent/Carer details",
                PrimaryContactDetailsReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingPrimaryContactDetails) ? viewModel.ReviewHeadingPrimaryContactDetails : "Primary contact details",
                CourseSelectionReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingCourseSelection) ? viewModel.ReviewHeadingCourseSelection : "Course selection",
                YouAreApplyingForReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingYouAreApplyingFor) ? viewModel.ReviewHeadingYouAreApplyingFor : "You're applying for ",
                SportsAcademiesReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingSportsAcademies) ? viewModel.ReviewHeadingSportsAcademies : "Sports academies",
                PreviousQualificationsReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingPreviousQualifications) ? viewModel.ReviewHeadingPreviousQualifications : "Previous qualifications",
                MedicalConditionsReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingMedicalConditions) ? viewModel.ReviewHeadingMedicalConditions : "Medical conditions or disabilities",
                EqualOpportunitiesReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingEqualOpportunities) ? viewModel.ReviewHeadingEqualOpportunities : "Equal opportunities",
                CriminalConvictionsReviewHeading = !string.IsNullOrEmpty(viewModel.ReviewHeadingCriminalConvictions) ? viewModel.ReviewHeadingCriminalConvictions : "Criminal convictions",
                LabelFirstName = !string.IsNullOrEmpty(viewModel.LabelFirstName) ? viewModel.LabelFirstName : "First name",
                LabelMiddleName = !string.IsNullOrEmpty(viewModel.LabelMiddleName) ? viewModel.LabelMiddleName : "Middle name",
                LabelLastName = !string.IsNullOrEmpty(viewModel.LabelLastName) ? viewModel.LabelLastName : "Last name",
                LabelPreferredName = !string.IsNullOrEmpty(viewModel.LabelLastName) ? viewModel.LabelPreferredName : "Preferred name",
                LabelDateOfBirth = !string.IsNullOrEmpty(viewModel.LabelDateOfBirth) ? viewModel.LabelDateOfBirth : "Date of birth",
                LabelGender = !string.IsNullOrEmpty(viewModel.LabelGender) ? viewModel.LabelGender : "Gender",
                LabelHomeAddress = !string.IsNullOrEmpty(viewModel.LabelHomeAddress) ? viewModel.LabelHomeAddress : "Home address",
                LabelMobileNumber = !string.IsNullOrEmpty(viewModel.LabelMobileNumber) ? viewModel.LabelMobileNumber : "Mobile number",
                LabelHomeNumber = !string.IsNullOrEmpty(viewModel.LabelHomeNumber) ? viewModel.LabelHomeNumber : "Home number",
                LabelStudentsEmail = !string.IsNullOrEmpty(viewModel.LabelStudentsEmail) ? viewModel.LabelStudentsEmail : "Students e-mail",
                LabelEmail = !string.IsNullOrEmpty(viewModel.LabelEmail) ? viewModel.LabelEmail : "E-mail",
                LabelRelationshipToStudent = !string.IsNullOrEmpty(viewModel.LabelRelationshipToStudent) ? viewModel.LabelRelationshipToStudent : "Relationship to student",
                LabelPhoneNumber = !string.IsNullOrEmpty(viewModel.LabelPhoneNumber) ? viewModel.LabelPhoneNumber : "Phone number",
                LabelAlevelsFirstChoice = !string.IsNullOrEmpty(viewModel.LabelAlevelsFirstChoice) ? viewModel.LabelAlevelsFirstChoice : "The course you are interested in the most is",
                LabelAlevelsSecondChoice = !string.IsNullOrEmpty(viewModel.LabelsAlevelsSecondChoice) ? viewModel.LabelsAlevelsSecondChoice : "Your second choice is",
                LabelAlevelsThirdChoice = !string.IsNullOrEmpty(viewModel.LabelAlevelsThirdChoice) ? viewModel.LabelAlevelsThirdChoice : "Your third choice is",
                LabelAdultLearningFirstChoice = !string.IsNullOrEmpty(viewModel.LabelAdultLearningFirstChoice) ? viewModel.LabelAdultLearningFirstChoice : "First Choice",
                LabelAdultLearningSecondChoice = !string.IsNullOrEmpty(viewModel.LabelAdultLearningSecondChoice) ? viewModel.LabelAdultLearningSecondChoice : "Second Choice",
                LabelAdultLearningThirdChoice = !string.IsNullOrEmpty(viewModel.LabelAdultLearningThirdChoice) ? viewModel.LabelAdultLearningThirdChoice : "Third Choice",
                LabelSportsAcademiesApplyingTo = !string.IsNullOrEmpty(viewModel.LabelSportsAcademiesApplyingTo) ? viewModel.LabelSportsAcademiesApplyingTo : "You are also applying to",
                LabelSubject = !string.IsNullOrEmpty(viewModel.LabelSubject) ? viewModel.LabelSubject : "Subject",
                LabelType = !string.IsNullOrEmpty(viewModel.LabelType) ? viewModel.LabelType : "Type",
                LabelGrade = !string.IsNullOrEmpty(viewModel.LabelGrade) ? viewModel.LabelGrade : "Grade/Predicted Grade",
                LabelYear = !string.IsNullOrEmpty(viewModel.LabelYear) ? viewModel.LabelYear : "Year",
                LabelEthinicity = !string.IsNullOrEmpty(viewModel.LabelEthnicity) ? viewModel.LabelEthnicity : "Ethnicity",
                LabelNationality = !string.IsNullOrEmpty(viewModel.LabelNationality) ? viewModel.LabelNationality : "Nationality",
                LabelReligion = !string.IsNullOrEmpty(viewModel.LabelReligion) ? viewModel.LabelReligion : "Religion",
                LabelFirstLanguage = !string.IsNullOrEmpty(viewModel.LabelFirstLanguage) ? viewModel.LabelFirstLanguage : "First Language",
                LabelNoLearningDisabilityDifficulty = !string.IsNullOrEmpty(viewModel.LabelNoLearningDisabilityDifficulty) ? viewModel.LabelNoLearningDisabilityDifficulty : "You do not have any learning difficulty, disability or health problem.",
                LabelHaveLearningDisabilityDifficulty = !string.IsNullOrEmpty(viewModel.LabelHaveLearningDisabilityDifficulty) ? viewModel.LabelHaveLearningDisabilityDifficulty : "You have",
                LabelPrimaryDisabilityDifficulty = !string.IsNullOrEmpty(viewModel.LabelPrimaryDisabilityDifficulty) ? viewModel.LabelPrimaryDisabilityDifficulty : "Your primary disability or difficulty is",
                LabelResidentOfUkEuFor3Years = !string.IsNullOrEmpty(viewModel.LabelResidentOfUkEuFor3Years) ? viewModel.LabelResidentOfUkEuFor3Years : "You've been a resident of the UK/European Union in the last three years.",
                LabelNoCriminalConviction = !string.IsNullOrEmpty(viewModel.LabelNoCriminalConviction) ? viewModel.LabelNoCriminalConviction : "You don't have a criminal conviction, formal conviction, reprimand or final warning from the Police.",
                LabelHaveCriminalConviction = !string.IsNullOrEmpty(viewModel.LabelHaveCriminalConviction) ? viewModel.LabelHaveCriminalConviction : "You have a criminal conviction, formal conviction, reprimand or final warning from the Police. A wellbeing adviser will be in touch to discuss this in confidence with you.",
                LabelNationalInsuranceNumber = !string.IsNullOrEmpty(viewModel.LabelNationalInsuranceNumber) ? viewModel.LabelNationalInsuranceNumber : "National Insurance Number",
                LabelHowDidYouHearAboutUs = "How did you hear about us",
                LabelMostInterestedSector = "Most interested in sector",
                LabelReceiveMarketing = "Receive Marketing Email"
            };
        }
    }
}
