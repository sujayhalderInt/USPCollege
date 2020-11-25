ALTER TABLE [dbo].[Applications]
ADD IsHigherEducation bit,
    NationalInsuranceNumber nvarchar(9)
GO

USE [UPS-ApplicationForm]
GO
/****** Object:  StoredProcedure [dbo].[GetApplication]    Script Date: 26/09/2018 16:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetApplication] 
	@UserId uniqueidentifier
AS
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
		  ,[IsHigherEducation]
		  ,[NationalInsuranceNumber]
	  FROM [dbo].[Applications]
	  WHERE UserId = @UserId
GO

USE [UPS-ApplicationForm]
GO
/****** Object:  StoredProcedure [dbo].[GetSubmittedApplications]    Script Date: 26/09/2018 16:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[GetSubmittedApplications] 
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
		  ,[IsHigherEducation]
		  ,[NationalInsuranceNumber]
	  FROM limits
	  JOIN    [dbo].[Applications] a
	  ON      [DateSubmitted] BETWEEN startDate AND endDate
	  WHERE IsSubmitted = 1
	  ORDER BY DateSubmitted DESC

GO

USE [UPS-ApplicationForm]
GO
/****** Object:  StoredProcedure [dbo].[InsertApplication]    Script Date: 26/09/2018 16:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsertApplication] 
	@UserId uniqueidentifier,
    @IsSubmitted bit,
	@DateSubmitted datetime = NULL,
    @TypeOfStudy NVARCHAR (100) = NULL,
	@Campus NVARCHAR (50) = NULL,
	@IsStepTwoSubmitted bit = 0,
	@IsStepThreeSubmitted bit = 0,
	@IsStepFourSubmitted bit = 0,
	@IsStepFiveSubmitted bit = 0,
	@IsStepSixSubmitted bit = 0,
	@IsStepSevenSubmitted bit = 0,
	@IsStepEightSubmitted bit = 0,
	@IsStepNineSubmitted bit = 0,
	@IsStepTenSubmitted bit = 0,
	@IsStepElevenSubmitted bit = 0,
	@IsStepTwelveSubmitted bit = 0,
	@StudentsFirstName NVARCHAR (75) = NULL,
	@StudentsMiddleName NVARCHAR (75) = NULL,
	@StudentsLastName NVARCHAR (75) = NULL,
	@StudentsPreferedName NVARCHAR (75) = NULL,
	@StudentsDateOfBirth datetime = NULL,
	@StudentsGender NVARCHAR (50) = NULL,
	@StudentsHomeAddressLine1 NVARCHAR (100) = NULL,
	@StudentsHomeAddressLine2 NVARCHAR (100) = NULL,
	@StudentsHomePostCode NVARCHAR (15) = NULL,
	@StudentsTownOrCity NVARCHAR (50) = NULL,
	@StudentsMobileNumber NVARCHAR (15) = NULL,
	@StudentsHomeNumber NVARCHAR (15) = NULL,
	@StudentsEmailAddress NVARCHAR (100) = NULL,
	@EmergencyContactFirstName NVARCHAR (75) = NULL,
	@EmergencyContactLastName NVARCHAR (75) = NULL,
	@EmergencyContactRelationship NVARCHAR (50) = NULL,
	@EmergencyContactPhoneNumber NVARCHAR (15) = NULL,
	@EmergencyContactEmailAddress NVARCHAR (100) = NULL,
	@ParentOrCarerFirstName NVARCHAR (75) = NULL,
	@ParentOrCarerLastName NVARCHAR (75) = NULL,
	@ParentOrCarerRelationshipToStudent NVARCHAR (50) = NULL,
	@ParentOrCarerPhoneNumber NVARCHAR (15) = NULL,
	@ParentOrCarerEmailAddress NVARCHAR (50) = NULL,
	@IsParentOrCarerPrimaryContact bit,
	@PrimaryContactFirstName NVARCHAR (75) = NULL,
	@PrimaryContactLastName NVARCHAR (75) = NULL,
	@PrimaryContactRelationshipToStudent NVARCHAR (50) = NULL,
	@PrimaryContactPhoneNumber NVARCHAR (15) = NULL,
	@PrimaryContactEmailAddress NVARCHAR (100) = NULL,
	@AlevelCourseFirstChoice NVARCHAR (100) = NULL,
	@AlevelCourseSecondChoice NVARCHAR (100) = NULL,
	@AlevelCourseThirdChoice NVARCHAR (100) = NULL,
	@IsFirstFullLevel3Qualification bit,
	@NameOfCourseOfTheTypeSelected NVARCHAR (100) = NULL,
	@SportsAcademiesCsv NVARCHAR (500) = NULL,
	@NameOfLastCollegeOrSchool NVARCHAR (100) = NULL,
	@QualificationsJson NVARCHAR (MAX) = NULL,
	@HasDisabilityDifficulty bit = NULL,
	@DisabilitiesDifficultiesCsv NVARCHAR (MAX) = NULL,
	@PrimaryDisability NVARCHAR (75) = NULL,
	@StudentsEthnicity NVARCHAR (100) = NULL,
	@StudentsNationality NVARCHAR (75) = NULL,
	@StudentsReligion NVARCHAR (75) = NULL,
	@StudentsFirstLanguage NVARCHAR (75) = NULL,
	@ResidentOfUkEuForThreeYears bit = NULL,
	@NameOfCountryOutsideUkEu NVARCHAR (100) = NULL,
	@DateOfResidenceOutsideUkEuFrom datetime = NULL,
	@DateOfResidenceOutsideUkEuTo datetime = NULL,
	@AnyCriminalConvictionOrFinalWarning bit,
	@ReceiveMarketingEmails bit,
	@HowDidYouHearAboutThisCourse NVARCHAR (100) = NULL,
	@MostInterestedSector NVARCHAR (100) = NULL,
	@IsALevels bit = 0,
	@IsAdultLearning bit = 0,
	@IsApprenticeships bit = 0,
	@IsProfessionalQualifications bit = 0,
	@IsHigherEducation bit = 0,
	@NationalInsuranceNumber nvarchar(9) = NULL
AS
	IF NOT EXISTS(SELECT UserId From dbo.Applications where UserId=@UserId)
	BEGIN
		INSERT INTO [dbo].[Applications]
				   ([UserId]
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
				   ,[IsHigherEducation] 
				   ,[NationalInsuranceNumber] 
				   )
			 VALUES
				   (@UserId
				   ,@IsSubmitted
				   ,@DateSubmitted
				   ,@TypeOfStudy
				   ,@Campus
				   ,@IsStepTwoSubmitted
				   ,@IsStepThreeSubmitted 
				   ,@IsStepFourSubmitted 
				   ,@IsStepFiveSubmitted 
				   ,@IsStepSixSubmitted 
				   ,@IsStepSevenSubmitted 
				   ,@IsStepEightSubmitted 
				   ,@IsStepNineSubmitted 
				   ,@IsStepTenSubmitted 
				   ,@IsStepElevenSubmitted 
				   ,@IsStepTwelveSubmitted 
				   ,@StudentsFirstName 
				   ,@StudentsMiddleName 
				   ,@StudentsLastName 
				   ,@StudentsPreferedName 
				   ,@StudentsDateOfBirth 
				   ,@StudentsGender 
				   ,@StudentsHomeAddressLine1 
				   ,@StudentsHomeAddressLine2 
				   ,@StudentsHomePostCode 
				   ,@StudentsTownOrCity 
				   ,@StudentsMobileNumber 
				   ,@StudentsHomeNumber 
				   ,@StudentsEmailAddress 
				   ,@EmergencyContactFirstName 
				   ,@EmergencyContactLastName 
				   ,@EmergencyContactRelationship 
				   ,@EmergencyContactPhoneNumber
				   ,@EmergencyContactEmailAddress 
				   ,@ParentOrCarerFirstName 
				   ,@ParentOrCarerLastName 
				   ,@ParentOrCarerRelationshipToStudent
				   ,@ParentOrCarerPhoneNumber 
				   ,@ParentOrCarerEmailAddress 
				   ,@IsParentOrCarerPrimaryContact 
				   ,@PrimaryContactFirstName 
				   ,@PrimaryContactLastName 
				   ,@PrimaryContactRelationshipToStudent 
				   ,@PrimaryContactPhoneNumber 
				   ,@PrimaryContactEmailAddress 
				   ,@AlevelCourseFirstChoice
				   ,@AlevelCourseSecondChoice 
				   ,@AlevelCourseThirdChoice 
				   ,@IsFirstFullLevel3Qualification 
				   ,@NameOfCourseOfTheTypeSelected
				   ,@SportsAcademiesCsv 
				   ,@NameOfLastCollegeOrSchool 
				   ,@QualificationsJson 
				   ,@HasDisabilityDifficulty 
				   ,@DisabilitiesDifficultiesCsv 
				   ,@PrimaryDisability 
				   ,@StudentsEthnicity 
				   ,@StudentsNationality 
				   ,@StudentsReligion 
				   ,@StudentsFirstLanguage 
				   ,@ResidentOfUkEuForThreeYears 
				   ,@NameOfCountryOutsideUkEu 
				   ,@DateOfResidenceOutsideUkEuFrom 
				   ,@DateOfResidenceOutsideUkEuTo 
				   ,@AnyCriminalConvictionOrFinalWarning
				   ,@ReceiveMarketingEmails 
				   ,@HowDidYouHearAboutThisCourse 
				   ,@MostInterestedSector
				   ,@IsALevels
				   ,@IsAdultLearning
				   ,@IsApprenticeships
				   ,@IsProfessionalQualifications 
				   ,@IsHigherEducation
				   ,@NationalInsuranceNumber
				   )
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Applications]
		SET 
			TypeOfStudy = @TypeOfStudy,
			Campus = @Campus,
			IsSubmitted = @IsSubmitted,
			DateSubmitted = @DateSubmitted,
			IsStepTwoSubmitted = @IsStepTwoSubmitted,
			IsStepThreeSubmitted = @IsStepThreeSubmitted,
		    IsStepFourSubmitted = @IsStepFourSubmitted,
			IsStepFiveSubmitted = @IsStepFiveSubmitted,
			IsStepSixSubmitted = @IsStepSixSubmitted,
		    IsStepSevenSubmitted = @IsStepSevenSubmitted,
		    IsStepEightSubmitted = @IsStepEightSubmitted,
			IsStepNineSubmitted = @IsStepNineSubmitted,
		    IsStepTenSubmitted  = @IsStepTenSubmitted,
			IsStepElevenSubmitted = @IsStepElevenSubmitted,
			IsStepTwelveSubmitted = @IsStepTwelveSubmitted,
			StudentsFirstName = @StudentsFirstName,
			StudentsMiddleName = @StudentsMiddleName,
			StudentsLastName = @StudentsLastName,
			StudentsPreferedName = @StudentsPreferedName,
			StudentsDateOfBirth = @StudentsDateOfBirth,
			StudentsGender = @StudentsGender,
			StudentsHomeAddressLine1 = @StudentsHomeAddressLine1,
			StudentsHomeAddressLine2 = @StudentsHomeAddressLine2,
			StudentsHomePostCode = @StudentsHomePostCode,
			StudentsTownOrCity = @StudentsTownOrCity,
			StudentsMobileNumber = @StudentsMobileNumber,
			StudentsHomeNumber = @StudentsHomeNumber,
			StudentsEmailAddress = @StudentsEmailAddress,
			EmergencyContactFirstName  = @EmergencyContactFirstName,
			EmergencyContactLastName = @EmergencyContactLastName,
			EmergencyContactRelationship = @EmergencyContactRelationship,
			EmergencyContactPhoneNumber = @EmergencyContactPhoneNumber,
			EmergencyContactEmailAddress = @EmergencyContactEmailAddress,
			ParentOrCarerFirstName = @ParentOrCarerFirstName,
			ParentOrCarerLastName = @ParentOrCarerLastName,
			ParentOrCarerRelationshipToStudent = @ParentOrCarerRelationshipToStudent,
			ParentOrCarerPhoneNumber = @ParentOrCarerPhoneNumber,
			ParentOrCarerEmailAddress = @ParentOrCarerEmailAddress,
			IsParentOrCarerPrimaryContact = @IsParentOrCarerPrimaryContact,
			PrimaryContactFirstName = @PrimaryContactFirstName,
			PrimaryContactLastName = @PrimaryContactLastName,
			PrimaryContactRelationshipToStudent = @PrimaryContactRelationshipToStudent,
			PrimaryContactPhoneNumber = @PrimaryContactPhoneNumber,
			PrimaryContactEmailAddress = @PrimaryContactEmailAddress,
			AlevelCourseFirstChoice = @AlevelCourseFirstChoice,
			AlevelCourseSecondChoice = @AlevelCourseSecondChoice,
			AlevelCourseThirdChoice = @AlevelCourseThirdChoice,
			IsFirstFullLevel3Qualification = @IsFirstFullLevel3Qualification,
			NameOfCourseOfTheTypeSelected = @NameOfCourseOfTheTypeSelected,
			SportsAcademiesCsv = @SportsAcademiesCsv,
			NameOfLastCollegeOrSchool = @NameOfLastCollegeOrSchool,
			QualificationsJson = @QualificationsJson,
			HasDisabilityDifficulty = @HasDisabilityDifficulty,
			DisabilitiesDifficultiesCsv = @DisabilitiesDifficultiesCsv,
			PrimaryDisability = @PrimaryDisability,
			StudentsEthnicity = @StudentsEthnicity,
			StudentsNationality = @StudentsNationality,
			StudentsReligion = @StudentsReligion,
			StudentsFirstLanguage = @StudentsFirstLanguage,
			ResidentOfUkEuForThreeYears = @ResidentOfUkEuForThreeYears,
			NameOfCountryOutsideUkEu = @NameOfCountryOutsideUkEu,
			DateOfResidenceOutsideUkEuFrom = @DateOfResidenceOutsideUkEuFrom,
			DateOfResidenceOutsideUkEuTo = @DateOfResidenceOutsideUkEuTo,
			AnyCriminalConvictionOrFinalWarning = @AnyCriminalConvictionOrFinalWarning,
			ReceiveMarketingEmails = @ReceiveMarketingEmails,
			HowDidYouHearAboutThisCourse = @HowDidYouHearAboutThisCourse,
			MostInterestedSector = @MostInterestedSector,
			IsALevels = @IsALevels,
			IsAdultLearning = @IsAdultLearning,
			IsApprenticeships = @IsApprenticeships,
			IsProfessionalQualifications = @IsProfessionalQualifications,
			IsHigherEducation = @IsHigherEducation,
			NationalInsuranceNumber = @NationalInsuranceNumber
		WHERE UserId = @UserId
	END

	GO
