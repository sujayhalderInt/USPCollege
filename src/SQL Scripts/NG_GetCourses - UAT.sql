CREATE PROCEDURE [dbo].[Prec_GetCourses]
AS
BEGIN

  SELECT [PRTX_ISN] ProspectusID
      ,[PRTX_Code] CourseCode
      ,[PRTX_Category] ProspectusCategoryID
         ,[GNCD_Description] ProspectusCategoryDesc
      ,[PRTX_Text] ProspectusCategoryText
         ,[PRTX_Paragraph] ProspectusDisplayOrder
      ,[PRTX_Created_Date] ProspectusCreatedDate
  FROM [172.16.1.134].[NG].[dbo].[PRTXProspectusText]
  left join [172.16.1.134].[NG].[dbo].[GNCDgncodes] PP on PP.GNCD_Code_Type='PP' and PP.GNCD_General_Code=PRTX_Category
  --Wildcard Course Code added as a filter--
  WHERE PRTX_Code like '___-___-_'
  order By [PRTX_Code], [PRTX_Category] Asc

END
GO