-- SELECT the data that was tracked inside the orchestration
-- because of the TrackingProfile
select * from dbo.bam_FromExprSalesMgr_ViewFromExpressionPo_View

-- SELECT the data from the custom orchestration component
select * from dbo.bam_FromExprSalesMgr_ViewFromExpressionP_View

-- SELECT the relationship data that was written via EventStram::AddRelatedActivity
select * from  dbo.bam_FromExpressionPoItem_AllRelationships