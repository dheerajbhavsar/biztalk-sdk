Use BAMPrimaryImport

/** =====================> bts_GetOrphanInterchangeAcks =========================== **/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[bts_GetOrphanInterchangeAcks]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[bts_GetOrphanInterchangeAcks]
GO

CREATE PROCEDURE [dbo].[bts_GetOrphanInterchangeAcks]
AS
BEGIN
	select iaaTbl.*, isaTbl.InterchangeControlNo, isaTbl.ReceiverID, isaTbl.SenderID, 
		isaTbl.ReceiverQ, isaTbl.SenderQ, isaTbl.Direction, isaTbl.InterchangeDateTime 
	from bam_InterchangeAckActivity_CompletedInstances iaaTbl
	left outer join bam_InterchangeStatusActivity_CompletedInstances isaTbl
	on iaaTbl.ReceiverID = isaTbl.SenderID AND iaaTbl.SenderID = isaTbl.ReceiverID AND 
	   iaaTbl.ReceiverQ = isaTbl.SenderQ AND iaaTbl.SenderQ = isaTbl.ReceiverQ AND 
	   iaaTbl.InterchangeControlNo = isaTbl.InterchangeControlNo AND 
	   iaaTbl.InterchangeDateTime = iaaTbl.InterchangeDateTime and iaaTbl.Direction <> isaTbl.Direction
	where isaTbl.InterchangeControlNo is null
	order by iaaTbl.TimeCreated
END
GO


/** =====================> bts_GetOrphanFunctionalAcks =========================== **/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[bts_GetOrphanFunctionalAcks]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[bts_GetOrphanFunctionalAcks]
GO

CREATE PROCEDURE [dbo].[bts_GetOrphanFunctionalAcks]
AS
BEGIN
	select * from bam_FunctionalAckActivity_CompletedInstances faa
	left outer join
	( select isa.ReceiverID, isa.SenderID, isa.ReceiverQ, isa.SenderQ, fgi.GroupControlNo, isa.InterchangeDateTime, isa.Direction 
		from bam_FunctionalGroupInfo_CompletedInstances fgi
		inner join bam_InterchangeStatusActivity_CompletedInstances isa
		on (fgi.InterchangeActivityID = isa.ActivityID)
	) fgisa
	on faa.ReceiverID = fgisa.SenderID AND faa.SenderID = fgisa.ReceiverID AND 
		faa.ReceiverQ = fgisa.SenderQ AND faa.SenderQ = fgisa.ReceiverQ AND faa.GroupControlNo = fgisa.GroupControlNo
		AND faa.Direction <> fgisa.Direction
	where fgisa.GroupControlNo is null
	order by faa.TimeCreated
END
GO
