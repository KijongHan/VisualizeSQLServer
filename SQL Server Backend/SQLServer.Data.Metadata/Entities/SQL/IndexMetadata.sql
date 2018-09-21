SELECT 
	sys.objects.[name] AS table_name,
	sys.indexes.[object_id],
	sys.indexes.[name] AS index_name,
	sys.indexes.[index_id],
	sys.indexes.[type],
	sys.indexes.[type_desc],
	sys.indexes.[data_space_id],
	sys.indexes.[is_unique],
	sys.indexes.[is_primary_key]
FROM 
	sys.indexes
LEFT JOIN
	sys.objects ON
	sys.objects.[object_id] = sys.indexes.[object_id]