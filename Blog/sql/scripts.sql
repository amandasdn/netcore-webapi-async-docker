USE master
GO

PRINT ('*** scripts.sql ***')

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

-----------------------------------------------------------------------------

USE db_Blog
GO

-----------------------------------------------------------------------------

PRINT ('Creating the table [tb_Comment]...')
	
IF (OBJECT_ID('dbo.tb_Comment') IS NULL)
BEGIN

	CREATE TABLE tb_Comment (
		IdComment UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
		Content VARCHAR(500) NOT NULL,
		Author VARCHAR(100) NOT NULL,
		CreatedAt DATETIME NOT NULL,
		StoredAt DATETIME NULL
	);
	
	PRINT ('Table [tb_Comment] successfully created.')

END
ELSE BEGIN

	PRINT ('Table [tb_Comment] is already created.')
	
END

GO

-----------------------------------------------------------------------------

PRINT ('Inserting values to the table [tb_Comment]...')

DELETE FROM tb_Comment
GO

INSERT INTO tb_Comment (IdComment, Content, Author, CreatedAt, StoredAt)
VALUES ('ac14f698-0afe-47fe-841d-22f1b01bd4bb', 'What a nice content!', 'Amanda', GETDATE()-1, GETDATE()-1)
GO

INSERT INTO tb_Comment (IdComment, Content, Author, CreatedAt, StoredAt)
VALUES ('6c206dd6-2914-4ad9-9d5e-e30a7af66136', 'I loved this post.', 'Amanda', GETDATE()-1, GETDATE()-1)
GO

INSERT INTO tb_Comment (IdComment, Content, Author, CreatedAt, StoredAt)
VALUES ('ce4e1570-687d-4d42-a60f-89cd947681fa', 'Cool!', 'Ana', GETDATE()-1, GETDATE()-1)
GO

INSERT INTO tb_Comment (IdComment, Content, Author, CreatedAt, StoredAt)
VALUES ('5c519671-23e6-488e-9a83-8bc95785a0f4', 'What does it mean?', 'Josh', GETDATE()-1, GETDATE()-1)
GO

INSERT INTO tb_Comment (IdComment, Content, Author, CreatedAt, StoredAt)
VALUES ('647f4f0e-2e2e-4c25-bfa3-a0076864bd3a', 'LOL', 'Ana', GETDATE()-1, GETDATE()-1)
GO

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-----------------------------------------------------------------------------

PRINT ('Creating procedure [spr_GetComments]...')

IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'spr_GetComments'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.spr_GetComments
GO

/*******************************************************************
* Procedure: spr_GetComments
* Description: Get all comments of a post - with pagination.
* Created At: 19/06/2023 - Amanda Nascimento
* Changes		Date			Developer					Description
* #001			__/__/____		_____________________		___________________________________
*******************************************************************/

CREATE PROCEDURE [spr_GetComments] (
	@page_size INT,
	@page_number INT,
	@author VARCHAR(100) = NULL
) AS

BEGIN

	SELECT
		IdComment, Content, Author, CreatedAt,
		(Count(*) OVER() + @page_size - 1) / @page_size AS PageCount
	FROM
		tb_Comment
	WHERE
		@author IS NULL OR Author = @author
	ORDER BY
		CreatedAt DESC

	OFFSET (@page_number - 1) * @page_size ROWS
	FETCH NEXT @page_size ROWS ONLY;

END

GO

-----------------------------------------------------------------------------

PRINT ('Creating procedure [spr_InsertComment]...')

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'spr_InsertComment'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.spr_InsertComment
GO

/*******************************************************************
* Procedure: spr_InsertComment
* Description: Insert a comment to the table.
* Created At: 20/06/2023 - Amanda Nascimento
* Changes		Date			Developer					Description
* #001			__/__/____		_____________________		___________________________________
*******************************************************************/

CREATE PROCEDURE [spr_InsertComment] (
	@id_comment UNIQUEIDENTIFIER,
	@content VARCHAR(500),
	@author VARCHAR(100),
	@createdAt DATETIME,
	@storedAt DATETIME
) AS

BEGIN

	INSERT INTO tb_Comment (IdComment, Content, Author, CreatedAt, StoredAt)
	VALUES (@id_comment, @content, @author, @createdAt, @storedAt)

END