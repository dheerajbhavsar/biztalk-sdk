
use [Northwind]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PolicyValidity]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[PolicyValidity]
GO

CREATE TABLE [dbo].[PolicyValidity] (
	[ID] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PolicyStatus] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PolicyValidity] ADD 
	CONSTRAINT [PK_PolicyValidity] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

INSERT INTO [dbo].[PolicyValidity]
      VALUES ('1','VALID')
GO
INSERT INTO [dbo].[PolicyValidity]
      VALUES ('2','INVALID')
