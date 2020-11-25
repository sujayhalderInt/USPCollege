USE [UPS-ApplicationForm]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetSubmittedApplications] 
	@startDate datetime = null,
	@endDate datetime = null
AS
WITH    limits AS
        (
        SELECT  COALESCE(@startDate, MIN([DateSubmitted])) AS startDate, COALESCE(@endDate, MAX([DateSubmitted])) AS endDate
        FROM    [dbo].[Applications]
        )
	SELECT [ID]
		  ,[UserId]
		  ,[IsSubmitted]
		  ,[DateSubmitted]
		  ,[TypeOfStudy]
		  ,[Campus]
		  ,[IsStepTwoSubmitted]
		  ,[IsStepThreeSubmitted]
		  ,[IsStepFourSubmitted] 
		  ,[IsStepFiveSubmitted] 
		  ,[IsStepSixSubmitted] 
		  ,[IsStepSevenSubmitted] 
		  ,[IsStepEightSubmitted] 
		  ,[IsStepNineSubmitted]
		  ,[IsStepTenSubmitted] 
	      ,[IsStepElevenSubmitted] 
		  ,[IsStepTwelveSubmitted] 
		  ,[StudentsFirstName] 
		  ,[StudentsMiddleName] 
		  ,[StudentsLastName] 
		  ,[StudentsPreferedName] 
		  ,[StudentsDateOfBirth] 
		  ,[StudentsGender] 
		  ,[StudentsHomeAddressLine1] 
		  ,[StudentsHomeAddressLine2] 
		  ,[StudentsHomePostCode] 
		  ,[StudentsTownOrCity] 
		  ,[StudentsMobileNumber] 
		  ,[StudentsHomeNumber] 
		  ,[StudentsEmailAddress] 
		  ,[EmergencyContactFirstName] 
		  ,[EmergencyContactLastName] 
		  ,[EmergencyContactRelationship] 
		  ,[EmergencyContactPhoneNumber]
		  ,[EmergencyContactEmailAddress] 
		  ,[ParentOrCarerFirstName] 
		  ,[ParentOrCarerLastName] 
	      ,[ParentOrCarerRelationshipToStudent]
		  ,[ParentOrCarerPhoneNumber] 
		  ,[ParentOrCarerEmailAddress] 
		  ,[IsParentOrCarerPrimaryContact]
		  ,[PrimaryContactFirstName] 
		  ,[PrimaryContactLastName] 
		  ,[PrimaryContactRelationshipToStudent] 
		  ,[PrimaryContactPhoneNumber] 
		  ,[PrimaryContactEmailAddress] 
		  ,[AlevelCourseFirstChoice]
		  ,[AlevelCourseSecondChoice] 
		  ,[AlevelCourseThirdChoice] 
		  ,[IsFirstFullLevel3Qualification] 
		  ,[NameOfCourseOfTheTypeSelected]
		  ,[SportsAcademiesCsv] 
		  ,[NameOfLastCollegeOrSchool] 
		  ,[QualificationsJson] 
		  ,[HasDisabilityDifficulty]
		  ,[DisabilitiesDifficultiesCsv] 
		  ,[PrimaryDisability] 
		  ,[StudentsEthnicity] 
		  ,[StudentsNationality] 
	      ,[StudentsReligion] 
		  ,[StudentsFirstLanguage] 
	      ,[ResidentOfUkEuForThreeYears] 
		  ,[NameOfCountryOutsideUkEu] 
		  ,[DateOfResidenceOutsideUkEuFrom]
		  ,[DateOfResidenceOutsideUkEuTo] 
		  ,[AnyCriminalConvictionOrFinalWarning]
		  ,[ReceiveMarketingEmails] 
		  ,[HowDidYouHearAboutThisCourse]
		  ,[MostInterestedSector]
		  ,[IsALevels]
		  ,[IsAdultLearning]
		  ,[IsApprenticeships] 
		  ,[IsProfessionalQualifications]
	  FROM limits
	  JOIN    [dbo].[Applications] a
	  ON      [DateSubmitted] BETWEEN startDate AND endDate
	  WHERE IsSubmitted = 1
	  ORDER BY DateSubmitted DESC

GO