USE [USP-Application-Form]
GO

/****** Object:  Table [dbo].[Applications]    Script Date: 13/09/2018 16:28:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Applications](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IsSubmitted] [bit] NULL,
	[DateSubmitted] [datetime] NULL,
	[TypeOfStudy] [nvarchar](100) NULL,
	[Campus] [nvarchar](50) NULL,
	[IsStepTwoSubmitted] [bit] NULL,
	[IsStepThreeSubmitted] [bit] NULL,
	[IsStepFourSubmitted] [bit] NULL,
	[IsStepFiveSubmitted] [bit] NULL,
	[IsStepSixSubmitted] [bit] NULL,
	[IsStepSevenSubmitted] [bit] NULL,
	[IsStepEightSubmitted] [bit] NULL,
	[IsStepNineSubmitted] [bit] NULL,
	[IsStepTenSubmitted] [bit] NULL,
	[IsStepElevenSubmitted] [bit] NULL,
	[IsStepTwelveSubmitted] [bit] NULL,
	[StudentsFirstName] [nvarchar](75) NULL,
	[StudentsMiddleName] [nvarchar](75) NULL,
	[StudentsLastName] [nvarchar](75) NULL,
	[StudentsPreferedName] [nvarchar](75) NULL,
	[StudentsDateOfBirth] [datetime] NULL,
	[StudentsGender] [nvarchar](50) NULL,
	[StudentsHomeAddressLine1] [nvarchar](100) NULL,
	[StudentsHomeAddressLine2] [nvarchar](100) NULL,
	[StudentsHomePostCode] [nvarchar](15) NULL,
	[StudentsTownOrCity] [nvarchar](50) NULL,
	[StudentsMobileNumber] [nvarchar](15) NULL,
	[StudentsHomeNumber] [nvarchar](15) NULL,
	[StudentsEmailAddress] [nvarchar](100) NULL,
	[EmergencyContactFirstName] [nvarchar](75) NULL,
	[EmergencyContactLastName] [nvarchar](75) NULL,
	[EmergencyContactRelationship] [nvarchar](50) NULL,
	[EmergencyContactPhoneNumber] [nvarchar](15) NULL,
	[EmergencyContactEmailAddress] [nvarchar](100) NULL,
	[ParentOrCarerFirstName] [nvarchar](75) NULL,
	[ParentOrCarerLastName] [nvarchar](75) NULL,
	[ParentOrCarerRelationshipToStudent] [nvarchar](50) NULL,
	[ParentOrCarerPhoneNumber] [nvarchar](15) NULL,
	[ParentOrCarerEmailAddress] [nvarchar](50) NULL,
	[IsParentOrCarerPrimaryContact] [bit] NULL,
	[PrimaryContactFirstName] [nvarchar](75) NULL,
	[PrimaryContactLastName] [nvarchar](75) NULL,
	[PrimaryContactRelationshipToStudent] [nvarchar](75) NULL,
	[PrimaryContactPhoneNumber] [nvarchar](75) NULL,
	[PrimaryContactEmailAddress] [nvarchar](75) NULL,
	[AlevelCourseFirstChoice] [nvarchar](100) NULL,
	[AlevelCourseSecondChoice] [nvarchar](100) NULL,
	[AlevelCourseThirdChoice] [nvarchar](100) NULL,
	[IsFirstFullLevel3Qualification] [bit] NULL,
	[NameOfCourseOfTheTypeSelected] [nvarchar](100) NULL,
	[SportsAcademiesCsv] [nvarchar](150) NULL,
	[NameOfLastCollegeOrSchool] [nvarchar](100) NULL,
	[QualificationsJson] [nvarchar](max) NULL,
	[HasDisabilityDifficulty] [bit] NULL,
	[DisabilitiesDifficultiesCsv] [nvarchar](max) NULL,
	[PrimaryDisability] [nvarchar](75) NULL,
	[StudentsEthnicity] [nvarchar](100) NULL,
	[StudentsNationality] [nvarchar](75) NULL,
	[StudentsReligion] [nvarchar](75) NULL,
	[StudentsFirstLanguage] [nvarchar](75) NULL,
	[ResidentOfUkEuForThreeYears] [bit] NULL,
	[NameOfCountryOutsideUkEu] [nvarchar](100) NULL,
	[DateOfResidenceOutsideUkEuFrom] [datetime] NULL,
	[DateOfResidenceOutsideUkEuTo] [datetime] NULL,
	[AnyCriminalConvictionOrFinalWarning] [bit] NULL,
	[ReceiveMarketingEmails] [bit] NULL,
	[HowDidYouHearAboutThisCourse] [nvarchar](100) NULL,
	[MostInterestedSector] [nvarchar](100) NULL,
	[IsALevels] [bit] NULL,
	[IsAdultLearning] [bit] NULL,
	[IsApprenticeships] [bit] NULL,
	[IsProfessionalQualifications] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

