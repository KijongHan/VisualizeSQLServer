SELECT 
	sys.key_constraints.[name],
	sys.key_constraints.[object_id],
	sys.key_constraints.[parent_object_id],
	sys.key_constraints.[type],
	sys.key_constraints.[unique_index_id]
FROM 
	sys.key_constraints