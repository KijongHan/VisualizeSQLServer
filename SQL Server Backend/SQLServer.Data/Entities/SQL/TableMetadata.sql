SELECT 
	[object_id],
	sys.objects.[name],
	sys.schemas.[schema_id],
	sys.schemas.[name] AS [schema_name]
FROM 
	sys.objects
LEFT JOIN
	sys.schemas ON
	sys.objects.[schema_id] = sys.schemas.[schema_id]
WHERE 
	[type] = 'U'