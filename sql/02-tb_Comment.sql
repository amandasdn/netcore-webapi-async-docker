USE db_Blog
GO

PRINT ('Creating the table [tb_Comment]...')
	
IF (OBJECT_ID('dbo.tb_Comment') IS NULL)
BEGIN

	CREATE TABLE tb_Comment (
		IdComment UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
		Content VARCHAR(500) NOT NULL,
		Author VARCHAR(100) NOT NULL,
		CreatedAt DATETIME NOT NULL
	);
	
	PRINT ('Table [tb_Comment] successfully created.')

END
ELSE BEGIN

	PRINT ('Table [tb_Comment] is already created.')
	
END

