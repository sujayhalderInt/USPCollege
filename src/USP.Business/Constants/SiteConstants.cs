using umbraco;

namespace USP.Business.Constants
{
    public static class SiteConstants
    {
        public const string TwitterUrl = "https://twitter.com/";
        public const string CookieName = "CookieAgreementUSP";
        public const string MainEventCtaText = "See All Events";


        //Doctype Aliases
        public const string AliasDataContactInformationPicker = "dataSharedContactInformation";
        public const string AliasDataDownloadLinkPicker = "dataExistingDownloadLink";
        public const string AliasBaseArticleGrid = "baseArticleGrid";
        public const string AliasHomePage = "pageHome";
        public const string AliasNotFound = "pageNotFound";
        public const string AliasDataSpotlightTreePicker = "dataSpotlightTreePicker";
        public const string AliasDataExistingTimelinePicker = "dataExistingTimelineItem";
        public const string AliasSiteSettings = "settingsSite";
        public const string AliasMemberType = "Member";
        public const string AliasApplicationFormStepsHolder = "applicationFormStepsHolder";
        public const string AliasCourseDetail = "pageCourseDetail";
        public const string AliasCareerDetail = "pageCareerDetail";
        public const string AliasCourseFilterSettings = "settingsCourseListingFilters";
        public const string AliasBlogFilterSettings = "settingsBlogListingFilters";
        public const string AliasNewsFilterSettings = "settingsNewsListingFilters";
        public const string AliasDataRelatedCoursePicker = "dataExistingRelatedCourse";
        public const string AliasDataProfileTreePicker = "dataProfileTreePicker";

        //Grid Aliases
        public static class GridWidgets
        {
            public const string AliasRichTextEditor = "docTypeWidgetRichTextEditor";
        }

        public static class DropDownLists
        {
            public const string NotSelectedText = "Pick One...";
        }

        //Doctype Aliases Application Forms
        public static class AliasApplicationForm
        {
            public const string Contains = "formStep";
            public const string StepOne = "pageApplicationFormLogin";
            public const string StepTwo = "formStepTwo";
            public const string StepThree = "formStepThree";
            public const string StepFour = "formStepFour";
            public const string StepFive = "formStepFive";
            public const string StepSix = "formStepSix";
            public const string StepSeven = "formStepSeven";
            public const string StepEight = "formStepEight";
            public const string StepNine = "formStepNine";
            public const string StepTen = "formStepTen";
            public const string StepEleven = "formStepEleven";
            public const string StepTwelve = "formStepTwelve";
            public const string StepThirteen = "formStepThirteen";
        }
        public static class Fields
        {
            public const string NodeTypeAlias = "nodeTypeAlias";
            public const string MainContentPlaceHolder = "mainContentPlaceholder";

            public const string CourseType = "courseType";
            public const string CourseLevel = "courseLevel";
            public const string AwardingBody = "awardingBody";
            public const string CareerSector = "careerSector";
            public const string Campus = "campus";
            public const string EventType = "eventType";
            public const string TaxonomyId = "taxonomyId";
            public const string TaxonomyName = "taxonomyName";

            public const string HideFromSearch = "hideFromSearch";
            public const string MainContent = "mainContent";
            public const string BannerSummary = "bannerSummary";
            public const string BannerHeading = "bannerHeading";
            public const string BannerImage = "bannerImage";
            public const string Qualification = "qualification";
            public const string SortCourseLevel = "sortCourseLevel";
            public const string SortSubjectArea = "sortSubjectArea";
            public const string SortDate = "sortDate";
            public const string SortCampus = "sortCampus";
            public const string SortEventType = "sortEventType";
            public const string SortBannerHeadingDesc = "sortBannerHeadingDesc";
            public const string SortBlogAuthorLastName = "sortAuthor";
            public const string Id = "id";
            public const string StartDate = "startDate";
            public const string EndDate = "endDate";

            public const string FilterStartDate = "filterStartDate";
            public const string FilterEndDate = "filterEndDate";

            public const string NewsTopics = "newsTopics";
            public const string BlogTopics = "blogTopics";
            public const string BlogAuthor = "blogAuthor";
            public const string PublishedDate = "publishedDate";
            public const string ExpandedPath = "expandedPath";

            public const string DaysAndTimes = "daysAndTimes";
            public const string Fee = "fee";
            public const string Duration = "duration";
            public const string OverlayColour = "overlay";
            public const string RemsCourseCode = "rEMSCourseCode";
            public const string MetaTitle = "metadataTitle";

        }





        //Application Form Steps Progress Width
        public static class WidthApplicationProgress
        {
            public const string StepTwo = "5";
            public const string StepThree = "10";
            public const string StepFour = "20";
            public const string StepFive = "30";
            public const string StepSix = "40";
            public const string StepSeven = "50";
            public const string StepEight = "60";
            public const string StepNine = "70";
            public const string StepTen = "80";
            public const string StepEleven = "85";
            public const string StepTwelve = "95";
            public const string StepThirteen = "100";
        }
        public static class SearchIndex
        {
            public const string GlobalSearchIndexer = "GlobalSearchIndexer";
        }




        //Umbraco Dictionary Keys
        public const string LoginValidationEmailRequiredKey = "Login Validation Email Required";
        public const string LoginValidationInvalidEmailKey = "Login Validation Invalid Email";
        public const string LoginValidationPasswordRequiredKey = "Login Validation Password Required";
        public const string RegistrationValidationEmailRequiredKey = "Registration Validation Email Required";
        public const string RegistrationValidationEmailInvalidKey = "Registration Validation Invalid Email";
        public const string RegistrationValidationPasswordRequiredKey = "Registration Validation Password Required";
        public const string RegistrationValidationPasswordInvalidKey = "Registration Validation Invalid Password";
        public const string RegistrationValidationPasswordRegexKey = "Registration Validation Password Regex";
        public const string RegistrationValidationRepeatPasswordRequiredKey = "Registration Validation Repeat Password Required";
        public const string RegistrationValidationPasswordsNoMatchKey = "Registration Validation Passwords No Match";
        public const string RegistrationValidationAgreeToPrivacyStatementKey = "Registration Validation Agree To Privacy Statement";
        public const string RegistrationValidationRecaptchaInvalidKey = "Registration Validation Recaptcha Invalid";
        public const string RegistrationFailedUserExistsKey = "Registration Failed User Exists";
        public const string RegistrationFailedErrorKey = "Registration Failed Error";
        public const string ForgottenPasswordEmailRequiredKey = "Forgotten Password Email Required";
        public const string ForgottenPasswordEmailInvalidKey = "Forgotten Password Invalid Email";
        public const string ForgottenPasswordUserNotExistsKey = "Forgotten Password User Not Exists";
        public const string ForgottenPasswordErrorKey = "Forgotten Password Error";
        public const string ForgottenPasswordEmailSentKey = "Forgotten Password Email Sent";
        public const string ResetPasswordEmailRequiredKey = "Reset Password Email Required";
        public const string ResetPasswordPasswordRequiredKey = "Reset Password Password Required";
        public const string ResetPasswordConfirmPasswordRequiredKey = "Reset Password Confirm Password Required";
        public const string ResetPasswordPasswordsNoMatchKey = "Reset Password Passwords No Match";
        public const string ResetPasswordUserNotExistsKey = "Reset Password User Not Exists";
        public const string ResetPasswordInvalidTokenKey = "Reset Password Invalid Token";
        public const string ResetPasswordTokenExpiredKey = "Reset Password Token Expired";
        public const string ResetPasswordSuccessMessageKey = "Reset Password Success Message";
        public const string ApplicationFormFieldRequiredMessageKey = "Application Form Field Required";
        public const string ApplicationFormStep3PlaceholderFirstName = "Application Form Step 3 Placeholder First Name";
        public const string ApplicationFormStep3PlaceholderMiddleName = "Application Form Step 3 Placeholder Middle Name";
        public const string ApplicationFormStep3PlaceholderSurname = "Application Form Step 3 Placeholder Surname";
        public const string ApplicationFormStep3PlaceholderAnotherName = "Application Form Step 3 Placeholder Another Name";
        public const string ApplicationFormStep3PlaceholderDateOfBirth = "Application Form Step 3 Placeholder Date Of Birth";
        public const string ApplicationFormStep3PlaceholderNI = "Application Form Step 3 Placeholder NI";
        public const string ApplicationFormStep4PlaceholderHomeAddress = "Application Form Step 4 Placeholder Home Address";
        public const string ApplicationFormStep4PlaceholderSecondLineOfAddress = "Application Form Step 4 Placeholder Second Line Of Address";
        public const string ApplicationFormStep4PlaceholderTownOrCity = "Application Form Step 4 Placeholder Town Or City";
        public const string ApplicationFormStep4PlaceholderEmailAddress = "Application Form Step 4 Placeholder Email Address";
        public const string ApplicationFormStep4PlaceholderPhoneNumber = "Application Form Step 4 Placeholder Phone Number";
        public const string ApplicationFormStep4PlaceholderPostCode = "Application Form Step 4 Placeholder Postcode";
        public const string ApplicationFormStep4PlaceholderEmergencyContactFirstName = "Application Form Step 4 Placeholder Emergency Contact First Name";
        public const string ApplicationFormStep4PlaceholderEmergencyContactLastName = "Application Form Step 4 Placeholder Emergency Contact Last Name";
        public const string ApplicationFormStep4PlaceholderEmergencyContactRelationshipToStudent = "Application Form Step 4 Placeholder Emergency Contact Relationship To Student";
        public const string ApplicationFormStep4PlaceholderEmergencyContactPhoneNumber = "Application Form Step 4 Placeholder Emergency Contact Phone Number";
        public const string ApplicationFormStep4PlaceholderEmergencyContactEmailAddress = "Application Form Step 4 Placeholder Emergency Contact Email Address";
        public const string ApplicationFormStep5PlaceholderParentOrCarerFirstName = "Application Form Step 5 Placeholder Parent / Carer First Name";
        public const string ApplicationFormStep5PlaceholderParentOrCarerLastName = "Application Form Step 5 Placeholder Parent / Carer Last Name";
        public const string ApplicationFormStep5PlaceholderParentOrCarerRelationshipToStudent = "Application Form Step 4 Placeholder Emergency Contact Relationship To Student";
        public const string ApplicationFormStep5PlaceholderParentOrCarerPhoneNumber = "Application Form Step 5 Placeholder Parent / Carer Phone Number";
        public const string ApplicationFormStep5PlaceholderParentOrCarerContactEmail = "Application Form Step 5 Placeholder Parent / Carer Contact Email";
        public const string ApplicationFormStep5PlaceholderPrimaryContactFirstName = "Application Form Step 5 Placeholder Primary Contact First Name";
        public const string ApplicationFormStep5PlaceholderPrimaryContactLastName = "Application Form Step 5 Placeholder Primary Contact Last Name";
        public const string ApplicationFormStep5PlaceholderPrimaryContactRelationshipToStudent = "Application Form Step 5 Placeholder Primary Contact Relationship To Student";
        public const string ApplicationFormStep5PlaceholderPrimaryContactEmail = "Application Form Step 5 Placeholder Primary Contact Email";






        public const string EmailServiceSmtpFromKey = "Email Service SMTP From";
        public const string EmailServiceSmtpHostKey = "Email Service SMTP Host";

        public const string RecaptchaSecretKeyKey = "Recaptcha Secret Key";
        public static class Crops
        {
            //Crops for Images
            public const string CropBanner = "Banner";

            public const string CropSpotlightSecondary = "Spotlight-Secondary";
            public const string CropQuote = "Quote";
            public const string CropContact = "Quote";
            public const string CropLoader = "Loader";
            public const string CropFeaturesWidget = "Features-Widget";
            public const string RelatedCourse = "RelatedCourse";
            public const string Author = "Author-Portrait";
            public const string GridEditorPreview = "GridEditorPreview";

            public static class AspectRatio
            {
                public const string Square = "Square";
                public const string FourToThree = "43";
                public const string SixteenToNine = "169";
                public const string TwentyOneToNine = "219";
            }
        }

        public static class ApplicationFormGeneral
        {
            public static class Step10
            {
                public const string PreferNotToSayLabel = "Prefer not to say";
                public const string PreferNotToSayValue = "Don't want to disclose";
            }

            public static class Step11
            {
                public const string Edit = "Edit";
            }
        }

        public static class UmbracoDictionaryKey
        {
            public static class ListingPages
            {
                public static class BlogListing
                {
                    public const string SearchPlaceholder = "BlogListing.SearchPlaceHolder";
                    public const string SearchHeader = "BlogListing.SearchHeader";
                    public const string Search = "BlogListing.Search";
                    public const string SearchButton = "BlogListing.SearchButton";
                    public const string ApplyFilters= "BlogListing.ApplyFilters";
                    public const string ClearFilters= "BlogListing.ClearFilters";
                    public const string SortyByDateDescendingText= "BlogListing.SortBy.Date.NewerFirst.Text";
                    public const string SortyByDateAscendingText = "BlogListing.SortBy.Date.OlderFirst.Text";
                    public const string SortyByAuthorDescendingText = "BlogListing.SortBy.Author.Descending.Text";
                    public const string SortyByAuthorAscendingText = "BlogListing.SortBy.Author.Ascending.Text";
                }

                //Jira Issue SEEV-497 manage text from Dictionary
                public static class EventListing
                {
                    public const string SearchPlaceholder = "EventListing.SearchPlaceHolder";
                    public const string SearchHeader = "EventListing.SearchHeader";
                    public const string Search = "EventListing.Search";
                    public const string SearchButton = "EventListing.SearchButton";
                    public const string ApplyFilters = "EventListing.ApplyFilters";
                    public const string ClearFilters = "EventListing.ClearFilters";
                    public const string SortyByDateDescendingText = "EventListing.SortBy.Date.NewerFirst.Text";
                    public const string SortyByDateAscendingText = "EventListing.SortBy.Date.OlderFirst.Text";
                    public const string SortyByCampusDescendingText = "EventListing.SortBy.Campus.Descending.Text";
                    public const string SortyByCampusAscendingText = "EventListing.SortBy.Campus.Ascending.Text";
                }

                public static class NewsListing
                {
                    public const string SearchButton = "NewsListing.SearchButton";
                    public const string ApplyFilters = "NewsListing.ApplyFilters";
                    public const string ClearFilters = "NewsListing.ClearFilters";
                    public const string SortyByDateDescendingText = "NewsListing.SortBy.Date.NewerFirst.Text";
                    public const string SortyByDateAscendingText = "NewsListing.SortBy.Date.OlderFirst.Text";
                }
            }

            public static class ApplyButton
            {
                public const string ContinueApplication = "ApplyButton.ContinueApplication";
                public const string SignInButton = "ApplyButton.SignInButton";
                public const string ContinueApplicationMenuText = "ApplyButton.ContinueApplicationMenuText";
                public const string ApplyNow = "ApplyButton.ApplyNow";
                public const string ApplicationSubmitted = "ApplyButton.ApplicationSubmitted";
            }

            public static class Login
            {
                public const string EmailRequired = "Login Validation Email Required";
                public const string PasswordRequired = "Login Validation Password Required";
                public const string InvalidPassword = "Login Validation Invalid Email";
                public const string Failed = "Login Validation Login Failed";
            }

            public static class ApplicationForm
            {
                public const string SaveButtonText = "Application Form Save Text";
                public const string PickOne = "Application Form Pick One";
                public const string InvalidEmail = "Application Form Steps Invalid Email";


                public static class Step2Validations
                {
                    public const string SeevicCampusOnly = "Application Form Step 2 Seevic Campus Only";
                    public const string CourseNotAvailable = "Application Form Step 2 Course Not Available";
                }

                public static class Dependencies
                {
                    public const string AdultLearning = "Application Form Adult Learning";
                    public const string ALevels = "Application Form A Levels";
                }

                public static class Step6PlaceHolder
                {
                    public const string FirstChoice = "Application Form Step 6 Placeholder First Choice";
                    public const string SecondChoice = "Application Form Step 6 Placeholder Second Choice";
                    public const string ThirdChoice = "Application Form Step 6 Placeholder Third Choice";
                }

                public static class Step7PlaceHolder
                {
                    public const string CourseSelect = "Application Form Step 7 Placeholder Drop Down Course Select";
                }

                public static class Step9PlaceHolder
                {
                    public const string LastSchoolOrCollege = "Application Form Step 9 Placeholder Last School/College";
                    public const string Year = "Application Form Step 9 Placeholder Year";
                    public const string Type = "Application Form Step 9 Placeholder Type";
                    public const string GradePredictedGrade = "Application Form Step 9 Placeholder Grade/Predicted Grade";
                    public const string Subject = "Application Form Step 9 Placeholder Subject";
                }

                public static class Step10PlaceHolder
                {
                    public const string FirstLanguage = "Application Form Step 10 Placeholder First Language";
                    public const string Nationality = "Application Form Step 10 Placeholder Nationality";
                    public const string Religion = "Application Form Step 10 Placeholder Religion";
                    public const string Ethnicity = "Application Form Step 10 Placeholder Ethnicity";
                    public const string PrimaryDisabilityDifficulty = "Application Form Step 10 Placeholder Primary Disability / Difficulty";
                    public const string WhichCountry = "Application Form Step 10 Placeholder Which Country";
                    public const string PreferNotToSay = "Application Form Step 10 Placeholder Disability Prefer Not To Say";
                }

                public static class Step10Errors
                {
                    public const string DisabilitiesDifficultiesRequired = "Application Form Error Step 10 Disabilities Difficulties Required";
                    public const string PrimaryDisabilitiesDifficultiesRequired = "Application Form Step 10 Placeholder Primary Disability / Difficulty Required";

                }

                public static class Step11Validations
                {
                    public const string EmergencyContactFieldsEmpty = "Application Form Emergency Contact Fields Empty";
                    public const string ALevelChoicesNotFilledIn = "Application Form A Level Choices Not Filled In";
                    public const string ParentCarerFieldsEmpty = "Application Form Parent Or Carer Fields Empty";
                    public const string CourseNotSelected = "Application Form Course Not Selected";
                    public const string NameOfLastCollegeSchool = "Application Form Name Of Last College Or School";
                    public const string AdultLearningCourseNotFilledIn = "Application Form Adult Learning Course Not Selected";
                }
            }
        }

        public static class CssClass
        {
            public const string FormCheckLabel = "form__check__label";
        }

        public static class HtmlId
        {
            public const string SingleSelect = "single-select";
        }

        public static class Sort
        {
            public const string Title = "title";
            public const string Level = "level";
            public const string SubjectArea = "subject";
            public const string Date = "date";
            public const string DateDesc = "date-desc";
            public const string Campus = "campus";
            public const string CampusDesc = "campus-desc";
            public const string EventType = "event-type";
            public const string BlogAuthor = "blog-author";
            public const string BlogAuthorDesc = "blog-author-desc";

        }

        public static class SearchType
        {
            public const string CourseSearch = "course";
            public const string EventSearch = "event";
            public const string GlobalSearch = "global";
            public const string BlogSearch = "blog";
            public const string NewsSearch = "news";
        }

    }
}


