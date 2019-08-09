SELECT     *
FROM         dtav_FindMessageFacts
WHERE     ([ServiceInstance/ServiceName] LIKE 'Bam%')
ORDER BY [ServiceInstance/InstanceID], [Event/Timestamp]


SELECT     *
FROM         bam_EndToEnd_ViewEndToEndActivit0_View
ORDER BY Received DESC


