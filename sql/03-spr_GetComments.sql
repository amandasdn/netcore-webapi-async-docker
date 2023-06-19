USE db_Blog
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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