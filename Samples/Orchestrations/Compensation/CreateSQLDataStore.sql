if exists ( select * from dbo.sysdatabases where name='BTSCompensationSampleMailingList' )
	drop database BTSCompensationSampleMailingList
GO

CREATE DATABASE BTSCompensationSampleMailingList
GO

USE BTSCompensationSampleMailingList

if exists (select * from dbo.sysobjects where id = object_id(N'[Mailing List]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Mailing List]
GO

CREATE TABLE [Mailing List] (
	[CustomerID] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ContactName] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Address] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[City] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Region] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[PostalCode] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Country] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EmailAddress] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

DELETE FROM [BTSCompensationSampleMailingList].[dbo].[Mailing List]

INSERT INTO [BTSCompensationSampleMailingList].[dbo].[Mailing List]
	([CustomerID], [ContactName], [Address], [City], [Region], [PostalCode], [Country], [EmailAddress])
	VALUES(
		'ALFKI', 
		'Alfreds Futterkiste', 
		'Obere Str. 57', 
		'Berlin', 
		null, 
		'12209', 
		'Germany', 
		'none'
	)

INSERT INTO [BTSCompensationSampleMailingList].[dbo].[Mailing List]
	([CustomerID], [ContactName], [Address], [City], [Region], [PostalCode], [Country], [EmailAddress])
	VALUES(
		'ANATR', 
		'Ana Trujillo', 
		'Avda. de la Constitución 2222', 
		'Mexico D.F.', 
		null, 
		'5021', 
		'Mexico', 
		'none'
	)

