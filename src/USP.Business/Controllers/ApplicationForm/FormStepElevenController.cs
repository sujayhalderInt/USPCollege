using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Form;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.ApplicationForm
{
    public class FormStepElevenController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        private readonly string _nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepTwelve);
        private readonly string _formStep3Url = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepThree);
        private readonly string _formStep4Url = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepFour);
        private readonly string _formStep5Url = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepFive);
        private readonly string _formStep6Url = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepSix);
        private readonly string _formStep7Url = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepSeven);
        private readonly string _formStep8Url = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepEight);
        private readonly string _formStep9Url = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepNine);
        private readonly string _formStep10Url = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepTen);


        public FormStepElevenController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepEleven>();

            if (!Members.IsLoggedIn())
            {
                return Redirect(FormHelper.GetLoginPageUrlWithReturnUrlByAlias());
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var userForm = _databaseService.GetApplicationData(userGuid);
            if (userForm.IsSubmitted)
            {
                return Redirect(FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepThirteen));
            }
            var headingsAndLabels = new Step11HeadingsAndLabels
            {
                StudentsPersonalInformationReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingStudentsPersonalInformation, "Student's personal information"),
                StudentsContactInformationReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingStudentsContactInformation, "Student's contact information"),
                EmergencyContactReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingEmergencyContact, "Emergency contact"),
                ParentCarerDetailsReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingParentCarerDetails, "Parent/Carer details"),
                PrimaryContactDetailsReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingPrimaryContactDetails, "Primary contact details"),
                CourseSelectionReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingCourseSelection, "Course selection"),
                YouAreApplyingForReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingYouAreApplyingFor, "You're applying for"),
                SportsAcademiesReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingSportsAcademies, "Sports academies"),
                PreviousQualificationsReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingPreviousQualifications, "Previous qualifications"),
                MedicalConditionsReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingMedicalConditions, "Medical conditions or disabilities"),
                EqualOpportunitiesReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingEqualOpportunities, "Equal opportunities"),
                CriminalConvictionsReviewHeading = FormHelper.GetStringOrSetDefaultString(viewModel.ReviewHeadingCriminalConvictions, "Criminal convictions"),
                LabelFirstName = FormHelper.GetStringOrSetDefaultString(viewModel.LabelFirstName, "First name"),
                LabelMiddleName = FormHelper.GetStringOrSetDefaultString(viewModel.LabelMiddleName, "Middle name"),
                LabelLastName = FormHelper.GetStringOrSetDefaultString(viewModel.LabelLastName, "Last name"),
                LabelPreferredName = FormHelper.GetStringOrSetDefaultString(viewModel.LabelLastName, "Preferred name"),
                LabelDateOfBirth = FormHelper.GetStringOrSetDefaultString(viewModel.LabelDateOfBirth, "Date of birth"),
                LabelGender = FormHelper.GetStringOrSetDefaultString(viewModel.LabelGender, "Gender"),
                LabelHomeAddress = FormHelper.GetStringOrSetDefaultString(viewModel.LabelHomeAddress, "Home address"),
                LabelMobileNumber = FormHelper.GetStringOrSetDefaultString(viewModel.LabelMobileNumber, "Mobile number"),
                LabelHomeNumber = FormHelper.GetStringOrSetDefaultString(viewModel.LabelHomeNumber, "Home number"),
                LabelStudentsEmail = FormHelper.GetStringOrSetDefaultString(viewModel.LabelStudentsEmail, "Students e-mail"),
                LabelEmail = FormHelper.GetStringOrSetDefaultString(viewModel.LabelEmail, "E-mail"),
                LabelRelationshipToStudent = FormHelper.GetStringOrSetDefaultString(viewModel.LabelRelationshipToStudent, "Relationship to student"),
                LabelPhoneNumber = FormHelper.GetStringOrSetDefaultString(viewModel.LabelPhoneNumber, "Phone number"),
                LabelAlevelsFirstChoice = FormHelper.GetStringOrSetDefaultString(viewModel.LabelAlevelsFirstChoice, "The course you are interested in the most is"),
                LabelAlevelsSecondChoice = FormHelper.GetStringOrSetDefaultString(viewModel.LabelsAlevelsSecondChoice, "Your second choice is"),
                LabelAlevelsThirdChoice = FormHelper.GetStringOrSetDefaultString(viewModel.LabelAlevelsThirdChoice, "Your third choice is"),
                LabelAdultLearningFirstChoice = FormHelper.GetStringOrSetDefaultString(viewModel.LabelAdultLearningFirstChoice, "First Choice"),
                LabelAdultLearningSecondChoice = FormHelper.GetStringOrSetDefaultString(viewModel.LabelAdultLearningSecondChoice, "Second Choice"),
                LabelAdultLearningThirdChoice = FormHelper.GetStringOrSetDefaultString(viewModel.LabelAdultLearningThirdChoice, "Third Choice"),
                LabelSportsAcademiesApplyingTo = FormHelper.GetStringOrSetDefaultString(viewModel.LabelSportsAcademiesApplyingTo, "You are also applying to"),
                LabelSubject = FormHelper.GetStringOrSetDefaultString(viewModel.LabelSubject, "Subject"),
                LabelType = FormHelper.GetStringOrSetDefaultString(viewModel.LabelType, "Type"),
                LabelGrade = FormHelper.GetStringOrSetDefaultString(viewModel.LabelGrade, "Grade/Predicted Grade"),
                LabelYear = FormHelper.GetStringOrSetDefaultString(viewModel.LabelYear, "Year"),
                LabelEthinicity = FormHelper.GetStringOrSetDefaultString(viewModel.LabelEthnicity, "Ethnicity"),
                LabelNationality = FormHelper.GetStringOrSetDefaultString(viewModel.LabelNationality, "Nationality"),
                LabelReligion = FormHelper.GetStringOrSetDefaultString(viewModel.LabelReligion, "Religion"),
                LabelFirstLanguage = FormHelper.GetStringOrSetDefaultString(viewModel.LabelFirstLanguage, "First Language"),
                LabelNoLearningDisabilityDifficulty = FormHelper.GetStringOrSetDefaultString(viewModel.LabelNoLearningDisabilityDifficulty, "You do not have any learning difficulty, disability or health problem."),
                LabelHaveLearningDisabilityDifficulty = FormHelper.GetStringOrSetDefaultString(viewModel.LabelHaveLearningDisabilityDifficulty, "You have"),
                LabelPrimaryDisabilityDifficulty = FormHelper.GetStringOrSetDefaultString(viewModel.LabelPrimaryDisabilityDifficulty, "Your primary disability or difficulty is"),
                LabelResidentOfUkEuFor3Years = FormHelper.GetStringOrSetDefaultString(viewModel.LabelResidentOfUkEuFor3Years, "You've been a resident of the UK/European Union in the last three years."),
                LabelNoCriminalConviction = FormHelper.GetStringOrSetDefaultString(viewModel.LabelNoCriminalConviction, "You don't have a criminal conviction, formal conviction, reprimand or final warning from the Police."),
                LabelHaveCriminalConviction = FormHelper.GetStringOrSetDefaultString(viewModel.LabelHaveCriminalConviction, "You have a criminal conviction, formal conviction, reprimand or final warning from the Police. A wellbeing adviser will be in touch to discuss this in confidence with you."),
                LabelNationalInsuranceNumber = FormHelper.GetStringOrSetDefaultString(viewModel.LabelNationalInsuranceNumber, "National Insurance Number")
            };
            viewModel.FormStep3Url = _formStep3Url;
            viewModel.FormStep4Url = _formStep4Url;
            viewModel.FormStep5Url = _formStep5Url;
            viewModel.FormStep6Url = _formStep6Url;
            viewModel.FormStep7Url = _formStep7Url;
            viewModel.FormStep8Url = _formStep8Url;
            viewModel.FormStep9Url = _formStep9Url;
            viewModel.FormStep10Url = _formStep10Url;
            viewModel.ApplicationForm = userForm;
            viewModel.HeadingsAndLabels = headingsAndLabels;
            viewModel.Qualifications = null;
            if (!string.IsNullOrEmpty(userForm.QualificationsJson))
            {
                viewModel.Qualifications = JsonConvert.DeserializeObject<List<FormStepNineQualifications>>(userForm.QualificationsJson);
            }
            return CurrentTemplate(viewModel);
        }

        [HttpPost]
        public ActionResult HandleFormStepElevenSubmit()
        {
            if (!Members.IsLoggedIn())
            {
                if (!Members.IsLoggedIn()) return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url));
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();

            var errorEmergencyContactFieldsEmpty = FormHelper.GetDictionaryItemOrSetDefaultString(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step11Validations.EmergencyContactFieldsEmpty,
                "Emergency Contact fields are empty, please proceed to Step 4 to fill it");
            var errorParentCarerDetailsNotFilledIn = FormHelper.GetDictionaryItemOrSetDefaultString(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step11Validations.ParentCarerFieldsEmpty,
                "Parent/Carer name has not been filled in, please proceed to Step 5 to fill it");
            var errorALevelChoicesNotFilledIn = FormHelper.GetDictionaryItemOrSetDefaultString(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step11Validations.ALevelChoicesNotFilledIn,
                "A level choices has not been filled in, please proceed to Step 6 to fill it");
            var errorAdultLearningCourseNotFilledIn = FormHelper.GetDictionaryItemOrSetDefaultString(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step11Validations.AdultLearningCourseNotFilledIn,
                "Please, atleast choose one course for Adult Learning by proceeding to Step 7 to fill it");
            var errorCourseNotSelected = FormHelper.GetDictionaryItemOrSetDefaultString(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step11Validations.CourseNotSelected,
                "Course to study has not been selected, please proceed to Step 7 to choose it");
            var errorNameOfLastCollegeSchool = FormHelper.GetDictionaryItemOrSetDefaultString(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step11Validations.NameOfLastCollegeSchool,
                "Name of Last College or school has not been filled in, please proceed to Step 9 to fill it");

            if (applicationForm.IsAdultLearning)
            {
                //step 4 : Emergnecy contact field can't be empty
                if (string.IsNullOrEmpty(applicationForm.EmergencyContactFirstName))
                {
                    ModelState.AddModelError("EmergencyContact", FormHelper.StringWithLink(_formStep4Url, errorEmergencyContactFieldsEmpty));
                }
                //step 7
                //Alevel First Choice is being used here as Adult Learning can also take 3 courses like Alevels
                if (string.IsNullOrEmpty(applicationForm.AlevelCourseFirstChoice))
                {
                    ModelState.AddModelError("AdultLearning", FormHelper.StringWithLink(_formStep7Url, errorAdultLearningCourseNotFilledIn));
                }
            }
            else
            {
                //step 5 : can't be empty
                if (string.IsNullOrEmpty(applicationForm.ParentOrCarerFirstName))
                {
                    ModelState.AddModelError("ParentCarerName", FormHelper.StringWithLink(_formStep5Url, errorParentCarerDetailsNotFilledIn));
                }
                //step 9 : Last college or school name can't be empty
                if (string.IsNullOrEmpty(applicationForm.NameOfLastCollegeOrSchool))
                {
                    ModelState.AddModelError("NameOfLastCollegeOrSchool", FormHelper.StringWithLink(_formStep9Url, errorNameOfLastCollegeSchool));
                }
            }
            if (applicationForm.IsALevels)
            {
                //step 6 : Alevels choices can't be empty 
                if (string.IsNullOrEmpty(applicationForm.AlevelCourseFirstChoice))
                {
                    ModelState.AddModelError("AlevelChoices", FormHelper.StringWithLink(_formStep6Url, errorALevelChoicesNotFilledIn));
                }
            }
            else
            {
                if (!applicationForm.IsAdultLearning)
                {
                    //step 7 : Course of the type selected can't be empty
                    if (string.IsNullOrEmpty(applicationForm.NameOfCourseOfTheTypeSelected))
                    {
                        ModelState.AddModelError("NameOfTheTypeOfTheCourseSelected", FormHelper.StringWithLink(_formStep7Url, errorCourseNotSelected));
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            applicationForm.IsStepElevenSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);
            return Redirect(_nextStepUrl);
        }
    }
}
