SELECT
	sys.foreign_keys.[name],
	sys.foreign_keys.[object_id],
	sys.foreign_keys.[parent_object_id],
	sys.foreign_keys.[referenced_object_id],
	sys.foreign_keys.[key_index_id]
FROM 
	sys.foreign_keys