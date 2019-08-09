
use [Northwind]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CustInfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CustInfo]
GO

CREATE TABLE [dbo].[CustInfo] (
	[ID] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CreditCardBalance] [int] NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CustInfo] ADD 
	CONSTRAINT [PK_CustInfo] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

INSERT INTO [dbo].[CustInfo]
      VALUES ('1',500)
GO
INSERT INTO [dbo].[CustInfo]
      VALUES ('2',1000)
GO
INSERT INTO [dbo].[CustInfo]
      VALUES ('3',35000)
GO
