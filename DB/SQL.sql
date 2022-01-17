USE [WTDotNet]
GO

INSERT INTO [dbo].[DepartmentDetails]
           ([DepartmentID]
           ,[DepartmentName]
           ,[DepartmentCode]
           ,[Description]
           ,[EntryDate]
           ,[SessionYear]
           ,[UserId])
     VALUES
           (1
           ,'IT'
           ,'IT'
           ,'IT'
           ,getdate()
           ,'2021-2022'
           ,1)
GO


USE [WTDotNet]
GO

INSERT INTO [dbo].[UserDetails]
           ([UserId]
           ,[UserName]
           ,[LoginName]
           ,[Password]
           ,[ConfirmPassword]
           ,[Address]
           ,[Email]
           ,[PhoneNo]
           ,[MobileNo]
           ,[EntryDate]
           ,[SessionYear]
           ,[DepartmentId]
           ,[DepartmentHead]
           ,[UserTypeId]
           ,[IsActive])
     VALUES
           (1
           ,'erpadmin'
           ,'erpadmin'
           ,'erpadmin'
           ,'erpadmin'
           ,'erpadmin'
           ,'erpadmin'
           ,'123'
           ,'123'
           ,getdate()
           ,'2020-21'
           ,1
           ,1
           ,1
           ,1)
GO


USE [WTDotNet]
GO

INSERT INTO [dbo].[UserType]
           ([UserTypeId]
           ,[UserTypeName])
     VALUES
           (1
           ,'ERPADMIN')
GO



INSERT INTO [dbo].[UserType]
           ([UserTypeId]
           ,[UserTypeName])
     VALUES
           (2
           ,'Department Head')
GO


INSERT INTO [dbo].[UserType]
           ([UserTypeId]
           ,[UserTypeName])
     VALUES
           (3
           ,'ERP User')
GO

USE [WTDotNet]
GO

INSERT INTO [dbo].[CompanyDetails]
           ([ID]
           ,[CName]
           ,[AddressOffice]
           ,[AddressWork]
           ,[Phone]
           ,[Fax]
           ,[Email]
           ,[Web]
           ,[CSTT]
           ,[UPTT]
           ,[TIN]
           ,[IECode]
           ,[EmpEsiCode1]
           ,[EmpEsiCode2]
           ,[EmpEsiCode3]
           ,[EmpPfEsttCode]
           ,[Excise]
           ,[CustomTeriffNo]
           ,[PANNo]
           ,[POFooter1]
           ,[POFooter2]
           ,[GSTN]
           ,[LogoPath]
           ,[PhotoPath])
     VALUES
           (1
           ,'WTLLP'
           ,'C 41 Sec 58 Noida'
           ,'C 41 Sec 58 Noida'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,0
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-'
           ,'-')
GO


