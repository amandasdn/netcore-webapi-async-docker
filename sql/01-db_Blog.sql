PRINT ('Creating the database [db_Blog]...')
	
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'db_Blog')
BEGIN

    CREATE DATABASE [db_Blog]
	
	PRINT ('Database [db_Blog] successfully created.')
	
END
ELSE BEGIN

	PRINT ('Database [db_Blog] is already created.')
	
END
GO