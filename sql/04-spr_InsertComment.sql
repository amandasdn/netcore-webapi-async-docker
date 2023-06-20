USE db_Blog
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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