CREATE PROCEDURE dbo.a_AccCategory_Create
  @UserId int,
  @Description varchar(100),
  @Rank smallint,
  @ModifiedBy varchar(50)
AS

INSERT INTO AccCategory
(
	UserId, 
	Description, 
	Rank, 
	Created, 
	Modified, 
	ModifiedBy
)
VALUES
(
	@UserId, 
	@Description, 
	@Rank, 
	getDate(), 
	getDate(), 
	@ModifiedBy
)

SELECT @@IDENTITYCREATE PROCEDURE dbo.a_AccCategory_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM AccCategoryAccountLink WHERE AccCategoryId = @Id
DELETE FROM AccCategory
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_AccCategory_GetAccountLinks
  @AccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.Description AS AccCategory_Description,
		CAST (ISNULL(AccCategoryAccountLink.AccountId, 0) as bit) AS Selected
	FROM AccCategory
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccCategoryId = AccCategory.Id AND AccCategoryAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_AccCategory_GetAccountLinksP
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccCategory
(
	TempId int IDENTITY PRIMARY KEY,
	AccCategory_Id int,
	AccCategory_Description varchar(100),
	selected bit
)

INSERT INTO #TempAccCategory
(
	AccCategory_Id,
	AccCategory_Description,
	selected
)
EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.Description AS AccCategory_Description,
		CAST (ISNULL(AccCategoryAccountLink.AccountId, 0) as bit) AS Selected
	FROM AccCategory
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccCategoryId = AccCategory.Id AND AccCategoryAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	AccCategory_Id,
	AccCategory_Description,
	selected
FROM #TempAccCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_AccCategory_GetAccountLinksPS
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccCategory
(
	TempId int IDENTITY PRIMARY KEY,
	AccCategory_Id int,
	AccCategory_Description varchar(100),
	selected bit
)

INSERT INTO #TempAccCategory
(
	AccCategory_Id,
	AccCategory_Description,
	selected
)
EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.Description AS AccCategory_Description,
		CAST (ISNULL(AccCategoryAccountLink.AccountId, 0) as bit) AS Selected
	FROM AccCategory
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccCategoryId = AccCategory.Id AND AccCategoryAccountLink.AccountId = ' + @AccountId + ' 
	WHERE AccCategory.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	AccCategory_Id,
	AccCategory_Description,
	selected
FROM #TempAccCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_AccCategory_GetList

AS

SELECT
	AccCategory.Id AS AccCategory_Id,
	AccCategory.Description AS AccCategory_Description
FROM AccCategory
ORDER BY AccCategory.Description
CREATE PROCEDURE dbo.a_AccCategory_GetRecord
  @Id int
AS

SELECT
	AccCategory.Id AS AccCategory_Id,
	AccCategory.UserId AS AccCategory_UserId,
	AccCategory.Description AS AccCategory_Description,
	AccCategory.Rank AS AccCategory_Rank,
	AccCategory.Created AS AccCategory_Created,
	AccCategory.Modified AS AccCategory_Modified,
	AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
	[User].LastName + ', ' + [User].FirstName AS User_FullName
FROM AccCategory
LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
WHERE AccCategory.Id = @IdCREATE PROCEDURE dbo.a_AccCategory_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.UserId AS AccCategory_UserId,
		AccCategory.Description AS AccCategory_Description,
		AccCategory.Rank AS AccCategory_Rank,
		AccCategory.Created AS AccCategory_Created,
		AccCategory.Modified AS AccCategory_Modified,
		AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM AccCategory
	LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_AccCategory_GetRecordsByAccountLink
  @AccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.UserId AS AccCategory_UserId,
		AccCategory.Description AS AccCategory_Description,
		AccCategory.Rank AS AccCategory_Rank,
		AccCategory.Created AS AccCategory_Created,
		AccCategory.Modified AS AccCategory_Modified,
		AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM AccCategory
	LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
	JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccCategoryId = AccCategory.Id AND AccCategoryAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_AccCategory_GetRecordsByAccountLinkP
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccCategory
(
	TempId int IDENTITY PRIMARY KEY,
	AccCategory_Id int,
	AccCategory_UserId int,
	AccCategory_Description varchar(100),
	AccCategory_Rank smallint,
	AccCategory_Created datetime,
	AccCategory_Modified datetime,
	AccCategory_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempAccCategory
(
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.UserId AS AccCategory_UserId,
		AccCategory.Description AS AccCategory_Description,
		AccCategory.Rank AS AccCategory_Rank,
		AccCategory.Created AS AccCategory_Created,
		AccCategory.Modified AS AccCategory_Modified,
		AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM AccCategory
	LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
	JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccCategoryId = AccCategory.Id AND AccCategoryAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
FROM #TempAccCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_AccCategory_GetRecordsByAccountLinkPS
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccCategory
(
	TempId int IDENTITY PRIMARY KEY,
	AccCategory_Id int,
	AccCategory_UserId int,
	AccCategory_Description varchar(100),
	AccCategory_Rank smallint,
	AccCategory_Created datetime,
	AccCategory_Modified datetime,
	AccCategory_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempAccCategory
(
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.UserId AS AccCategory_UserId,
		AccCategory.Description AS AccCategory_Description,
		AccCategory.Rank AS AccCategory_Rank,
		AccCategory.Created AS AccCategory_Created,
		AccCategory.Modified AS AccCategory_Modified,
		AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM AccCategory
	LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
	JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccCategoryId = AccCategory.Id AND AccCategoryAccountLink.AccountId = ' + @AccountId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
FROM #TempAccCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_AccCategory_GetRecordsByUserField
  @UserId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.UserId AS AccCategory_UserId,
		AccCategory.Description AS AccCategory_Description,
		AccCategory.Rank AS AccCategory_Rank,
		AccCategory.Created AS AccCategory_Created,
		AccCategory.Modified AS AccCategory_Modified,
		AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM AccCategory
	LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
	WHERE AccCategory.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_AccCategory_GetRecordsByUserFieldP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccCategory
(
	TempId int IDENTITY PRIMARY KEY,
	AccCategory_Id int,
	AccCategory_UserId int,
	AccCategory_Description varchar(100),
	AccCategory_Rank smallint,
	AccCategory_Created datetime,
	AccCategory_Modified datetime,
	AccCategory_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempAccCategory
(
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.UserId AS AccCategory_UserId,
		AccCategory.Description AS AccCategory_Description,
		AccCategory.Rank AS AccCategory_Rank,
		AccCategory.Created AS AccCategory_Created,
		AccCategory.Modified AS AccCategory_Modified,
		AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM AccCategory
	LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
	WHERE AccCategory.UserId = ' + @UserId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
FROM #TempAccCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_AccCategory_GetRecordsByUserFieldPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccCategory
(
	TempId int IDENTITY PRIMARY KEY,
	AccCategory_Id int,
	AccCategory_UserId int,
	AccCategory_Description varchar(100),
	AccCategory_Rank smallint,
	AccCategory_Created datetime,
	AccCategory_Modified datetime,
	AccCategory_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempAccCategory
(
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.UserId AS AccCategory_UserId,
		AccCategory.Description AS AccCategory_Description,
		AccCategory.Rank AS AccCategory_Rank,
		AccCategory.Created AS AccCategory_Created,
		AccCategory.Modified AS AccCategory_Modified,
		AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM AccCategory
	LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND AccCategory.UserId = ' + @UserId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
FROM #TempAccCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_AccCategory_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccCategory
(
	TempId int IDENTITY PRIMARY KEY,
	AccCategory_Id int,
	AccCategory_UserId int,
	AccCategory_Description varchar(100),
	AccCategory_Rank smallint,
	AccCategory_Created datetime,
	AccCategory_Modified datetime,
	AccCategory_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempAccCategory
(
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.UserId AS AccCategory_UserId,
		AccCategory.Description AS AccCategory_Description,
		AccCategory.Rank AS AccCategory_Rank,
		AccCategory.Created AS AccCategory_Created,
		AccCategory.Modified AS AccCategory_Modified,
		AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM AccCategory
	LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
FROM #TempAccCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_AccCategory_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccCategory
(
	TempId int IDENTITY PRIMARY KEY,
	AccCategory_Id int,
	AccCategory_UserId int,
	AccCategory_Description varchar(100),
	AccCategory_Rank smallint,
	AccCategory_Created datetime,
	AccCategory_Modified datetime,
	AccCategory_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempAccCategory
(
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		AccCategory.Id AS AccCategory_Id,
		AccCategory.UserId AS AccCategory_UserId,
		AccCategory.Description AS AccCategory_Description,
		AccCategory.Rank AS AccCategory_Rank,
		AccCategory.Created AS AccCategory_Created,
		AccCategory.Modified AS AccCategory_Modified,
		AccCategory.ModifiedBy AS AccCategory_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM AccCategory
	LEFT OUTER JOIN [User] ON AccCategory.UserId = [User].Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	AccCategory_Id,
	AccCategory_UserId,
	AccCategory_Description,
	AccCategory_Rank,
	AccCategory_Created,
	AccCategory_Modified,
	AccCategory_ModifiedBy,
	User_FullName
FROM #TempAccCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_AccCategory_Update
  @Id int,
  @UserId int,
  @Description varchar(100),
  @Rank smallint,
  @ModifiedBy varchar(50)
AS

UPDATE AccCategory
SET
	UserId = @UserId,
	Description = @Description,
	Rank = @Rank,
	Modified = getDate(),
	ModifiedBy = @ModifiedBy
WHERE Id = @Id

RETURN 0CREATE PROCEDURE dbo.a_AccCategoryAccountLink_Create
  @AccCategoryId int,
  @AccountId int
AS

IF (SELECT COUNT(*) FROM AccCategoryAccountLink WHERE AccCategoryId = @AccCategoryId AND AccountId = @AccountId) > 0
	RETURN -16

INSERT INTO AccCategoryAccountLink
(AccCategoryId, AccountId)
VALUES (@AccCategoryId, @AccountId)

RETURN 0CREATE PROCEDURE dbo.a_AccCategoryAccountLink_CreateAllByAccCategory
  @AccCategoryId int
AS

INSERT INTO AccCategoryAccountLink
(AccCategoryId, AccountId)
SELECT @AccCategoryId, Id FROM Account WHERE NOT EXISTS (SELECT * FROM AccCategoryAccountLink WHERE AccCategoryId = @AccCategoryId AND AccountId = Id)


RETURN 0CREATE PROCEDURE dbo.a_AccCategoryAccountLink_CreateAllByAccount
  @AccountId int
AS

INSERT INTO AccCategoryAccountLink
(AccCategoryId, AccountId)
SELECT Id, @AccountId FROM AccCategory WHERE NOT EXISTS (SELECT * FROM AccCategoryAccountLink WHERE AccCategoryId = Id AND AccountId = @AccountId)


RETURN 0CREATE PROCEDURE dbo.a_AccCategoryAccountLink_Delete
  @AccCategoryId int,
  @AccountId int
AS

DELETE FROM AccCategoryAccountLink
WHERE AccCategoryId = @AccCategoryId AND AccountId = @AccountId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_AccCategoryAccountLink_DeleteAllByAccCategory
  @AccCategoryId int
AS

DELETE FROM AccCategoryAccountLink
WHERE AccCategoryId = @AccCategoryId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_AccCategoryAccountLink_DeleteAllByAccount
  @AccountId int
AS

DELETE FROM AccCategoryAccountLink
WHERE AccountId = @AccountId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_Account_GetAccCategoryLinks
  @AccCategoryId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(AccCategoryAccountLink.AccCategoryId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetAccCategoryLinksByMasterAccount
  @AccCategoryId int,
  @MasterAccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(AccCategoryAccountLink.AccCategoryId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	INNER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetAccCategoryLinksByMasterAccountP
  @AccCategoryId int,
  @MasterAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(AccCategoryAccountLink.AccCategoryId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	INNER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetAccCategoryLinksByUser
  @AccCategoryId int,
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(AccCategoryAccountLink.AccCategoryId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetAccCategoryLinksByUserP
  @AccCategoryId int,
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(AccCategoryAccountLink.AccCategoryId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetAccCategoryLinksP
  @AccCategoryId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(AccCategoryAccountLink.AccCategoryId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetAccCategoryLinksPS
  @AccCategoryId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(AccCategoryAccountLink.AccCategoryId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	WHERE Account.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetList

AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription
FROM Account
ORDER BY Account.FullAccountNumber
CREATE PROCEDURE dbo.a_Account_GetMasterAccountLinks
  @MasterAccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(MasterAccountAccountLink.MasterAccountId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetMasterAccountLinksByAccCategory
  @MasterAccountId int,
  @AccCategoryId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(MasterAccountAccountLink.MasterAccountId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	INNER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetMasterAccountLinksByAccCategoryP
  @MasterAccountId int,
  @AccCategoryId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(MasterAccountAccountLink.MasterAccountId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	INNER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetMasterAccountLinksByUser
  @MasterAccountId int,
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(MasterAccountAccountLink.MasterAccountId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetMasterAccountLinksByUserP
  @MasterAccountId int,
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(MasterAccountAccountLink.MasterAccountId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetMasterAccountLinksP
  @MasterAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(MasterAccountAccountLink.MasterAccountId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetMasterAccountLinksPS
  @MasterAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(MasterAccountAccountLink.MasterAccountId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	WHERE Account.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetRecord
  @Id int
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountType AS Account_AccountType,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription,
	Account.AcctStat AS Account_AcctStat,
	Account.AcctStatDesc AS Account_AcctStatDesc,
	Account.AdminUnit AS Account_AdminUnit,
	Account.AdminUnitDesc AS Account_AdminUnitDesc,
	Account.AdminUnitStat AS Account_AdminUnitStat,
	Account.RespPerson1 AS Account_RespPerson1,
	Account.RespPerson1CC AS Account_RespPerson1CC,
	Account.RespPerson2 AS Account_RespPerson2,
	Account.RespPerson2CC AS Account_RespPerson2CC,
	Account.RespPerson3 AS Account_RespPerson3,
	Account.RespPerson3CC AS Account_RespPerson3CC,
	Account.Guidelines AS Account_Guidelines,
	Account.Modified AS Account_Modified,
	Account.ModifiedBy AS Account_ModifiedBy
FROM Account
WHERE Account.Id = @IdCREATE PROCEDURE dbo.a_Account_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetRecordsByAccCategoryLink
  @AccCategoryId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetRecordsByAccCategoryLinkP
  @AccCategoryId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountType char(1),
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetRecordsByAccCategoryLinkPS
  @AccCategoryId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountType char(1),
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetRecordsByMasterAccountLink
  @MasterAccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetRecordsByMasterAccountLinkP
  @MasterAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountType char(1),
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetRecordsByMasterAccountLinkPS
  @MasterAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountType char(1),
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetRecordsByUserLink
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetRecordsByUserLinkP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountType char(1),
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetRecordsByUserLinkPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountType char(1),
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountType char(1),
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountType char(1),
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetUserLinks
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetUserLinksByAccCategory
  @UserId int,
  @AccCategoryId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	INNER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetUserLinksByAccCategoryP
  @UserId int,
  @AccCategoryId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	INNER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetUserLinksByMasterAccount
  @UserId int,
  @MasterAccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	INNER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Account_GetUserLinksByMasterAccountP
  @UserId int,
  @MasterAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	INNER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetUserLinksP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Account_GetUserLinksPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	WHERE Account.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Accounts_Payout_GetList

AS

SELECT
	Accounts_Payout.Id AS Accounts_Payout_Id
FROM Accounts_Payout
ORDER BY Accounts_Payout.Id
CREATE PROCEDURE dbo.a_Accounts_Payout_GetRecord
  @Id int
AS

SELECT
	Accounts_Payout.Id AS Accounts_Payout_Id,
	Accounts_Payout.AccountNumber AS Accounts_Payout_AccountNumber,
	Accounts_Payout.Payout AS Accounts_Payout_Payout,
	Accounts_Payout.PayoutDate AS Accounts_Payout_PayoutDate,
	Accounts_Payout.Modified AS Accounts_Payout_Modified,
	Accounts_Payout.ModifiedBy AS Accounts_Payout_ModifiedBy
FROM Accounts_Payout
WHERE Accounts_Payout.Id = @IdCREATE PROCEDURE dbo.a_Accounts_Payout_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Accounts_Payout.Id AS Accounts_Payout_Id,
		Accounts_Payout.AccountNumber AS Accounts_Payout_AccountNumber,
		Accounts_Payout.Payout AS Accounts_Payout_Payout,
		Accounts_Payout.PayoutDate AS Accounts_Payout_PayoutDate,
		Accounts_Payout.Modified AS Accounts_Payout_Modified,
		Accounts_Payout.ModifiedBy AS Accounts_Payout_ModifiedBy
	FROM Accounts_Payout
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Accounts_Payout_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccounts_Payout
(
	TempId int IDENTITY PRIMARY KEY,
	Accounts_Payout_Id int,
	Accounts_Payout_AccountNumber char(10),
	Accounts_Payout_Payout money,
	Accounts_Payout_PayoutDate datetime,
	Accounts_Payout_Modified char(10),
	Accounts_Payout_ModifiedBy int
)

INSERT INTO #TempAccounts_Payout
(
	Accounts_Payout_Id,
	Accounts_Payout_AccountNumber,
	Accounts_Payout_Payout,
	Accounts_Payout_PayoutDate,
	Accounts_Payout_Modified,
	Accounts_Payout_ModifiedBy
)
EXEC
('
	SELECT
		Accounts_Payout.Id AS Accounts_Payout_Id,
		Accounts_Payout.AccountNumber AS Accounts_Payout_AccountNumber,
		Accounts_Payout.Payout AS Accounts_Payout_Payout,
		Accounts_Payout.PayoutDate AS Accounts_Payout_PayoutDate,
		Accounts_Payout.Modified AS Accounts_Payout_Modified,
		Accounts_Payout.ModifiedBy AS Accounts_Payout_ModifiedBy
	FROM Accounts_Payout
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Accounts_Payout_Id,
	Accounts_Payout_AccountNumber,
	Accounts_Payout_Payout,
	Accounts_Payout_PayoutDate,
	Accounts_Payout_Modified,
	Accounts_Payout_ModifiedBy
FROM #TempAccounts_Payout
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Accounts_Payout_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempAccounts_Payout
(
	TempId int IDENTITY PRIMARY KEY,
	Accounts_Payout_Id int,
	Accounts_Payout_AccountNumber char(10),
	Accounts_Payout_Payout money,
	Accounts_Payout_PayoutDate datetime,
	Accounts_Payout_Modified char(10),
	Accounts_Payout_ModifiedBy int
)

INSERT INTO #TempAccounts_Payout
(
	Accounts_Payout_Id,
	Accounts_Payout_AccountNumber,
	Accounts_Payout_Payout,
	Accounts_Payout_PayoutDate,
	Accounts_Payout_Modified,
	Accounts_Payout_ModifiedBy
)
EXEC
('
	SELECT
		Accounts_Payout.Id AS Accounts_Payout_Id,
		Accounts_Payout.AccountNumber AS Accounts_Payout_AccountNumber,
		Accounts_Payout.Payout AS Accounts_Payout_Payout,
		Accounts_Payout.PayoutDate AS Accounts_Payout_PayoutDate,
		Accounts_Payout.Modified AS Accounts_Payout_Modified,
		Accounts_Payout.ModifiedBy AS Accounts_Payout_ModifiedBy
	FROM Accounts_Payout
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Accounts_Payout_Id,
	Accounts_Payout_AccountNumber,
	Accounts_Payout_Payout,
	Accounts_Payout_PayoutDate,
	Accounts_Payout_Modified,
	Accounts_Payout_ModifiedBy
FROM #TempAccounts_Payout
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_College_Create
  @Name varchar(50)
AS

IF EXISTS (SELECT * FROM College WHERE Name = @Name)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO College
(
	Name
)
VALUES
(
	@Name
)

SELECT @@IDENTITYCREATE PROCEDURE dbo.a_College_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM Department WHERE CollegeId = @Id) > 0)
		RETURN -16

	END 
ELSE 
	BEGIN

	DELETE FROM Department WHERE CollegeId = @Id

	END 

DELETE FROM College
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_College_GetList

AS

SELECT
	College.Id AS College_Id,
	College.Name AS College_Name
FROM College
ORDER BY College.Name
CREATE PROCEDURE dbo.a_College_GetRecord
  @Id int
AS

SELECT
	College.Id AS College_Id,
	College.Name AS College_Name
FROM College
WHERE College.Id = @IdCREATE PROCEDURE dbo.a_College_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		College.Id AS College_Id,
		College.Name AS College_Name
	FROM College
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_College_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempCollege
(
	TempId int IDENTITY PRIMARY KEY,
	College_Id int,
	College_Name varchar(50)
)

INSERT INTO #TempCollege
(
	College_Id,
	College_Name
)
EXEC
('
	SELECT
		College.Id AS College_Id,
		College.Name AS College_Name
	FROM College
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	College_Id,
	College_Name
FROM #TempCollege
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_College_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempCollege
(
	TempId int IDENTITY PRIMARY KEY,
	College_Id int,
	College_Name varchar(50)
)

INSERT INTO #TempCollege
(
	College_Id,
	College_Name
)
EXEC
('
	SELECT
		College.Id AS College_Id,
		College.Name AS College_Name
	FROM College
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	College_Id,
	College_Name
FROM #TempCollege
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_College_Update
  @Id int,
  @Name varchar(50)
AS

IF EXISTS (SELECT * FROM College WHERE Name = @Name AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE College
SET
	Name = @Name
WHERE Id = @Id

RETURN 0CREATE PROCEDURE dbo.a_Department_Create
  @CollegeId int,
  @Name varchar(100)
AS

IF EXISTS (SELECT * FROM Department WHERE Name = @Name)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO Department
(
	CollegeId, 
	Name
)
VALUES
(
	@CollegeId, 
	@Name
)

SELECT @@IDENTITYCREATE PROCEDURE dbo.a_Department_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM DepartmentUserLink WHERE DepartmentId = @Id
DELETE FROM Department
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_Department_GetList

AS

SELECT
	Department.Id AS Department_Id,
	Department.Name AS Department_Name
FROM Department
ORDER BY Department.Name
CREATE PROCEDURE dbo.a_Department_GetRecord
  @Id int
AS

SELECT
	Department.Id AS Department_Id,
	Department.CollegeId AS Department_CollegeId,
	Department.Name AS Department_Name,
	College.Name AS College_Name
FROM Department
LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
WHERE Department.Id = @IdCREATE PROCEDURE dbo.a_Department_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.CollegeId AS Department_CollegeId,
		Department.Name AS Department_Name,
		College.Name AS College_Name
	FROM Department
	LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Department_GetRecordsByCollegeField
  @CollegeId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Department.Id AS Department_Id,
		Department.CollegeId AS Department_CollegeId,
		Department.Name AS Department_Name,
		College.Name AS College_Name
	FROM Department
	LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
	WHERE Department.CollegeId = ' + @CollegeId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Department_GetRecordsByCollegeFieldP
  @CollegeId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempDepartment
(
	TempId int IDENTITY PRIMARY KEY,
	Department_Id int,
	Department_CollegeId int,
	Department_Name varchar(100),
	College_Name varchar(50)
)

INSERT INTO #TempDepartment
(
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
)
EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.CollegeId AS Department_CollegeId,
		Department.Name AS Department_Name,
		College.Name AS College_Name
	FROM Department
	LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
	WHERE Department.CollegeId = ' + @CollegeId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
FROM #TempDepartment
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Department_GetRecordsByCollegeFieldPS
  @CollegeId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempDepartment
(
	TempId int IDENTITY PRIMARY KEY,
	Department_Id int,
	Department_CollegeId int,
	Department_Name varchar(100),
	College_Name varchar(50)
)

INSERT INTO #TempDepartment
(
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
)
EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.CollegeId AS Department_CollegeId,
		Department.Name AS Department_Name,
		College.Name AS College_Name
	FROM Department
	LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Department.CollegeId = ' + @CollegeId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
FROM #TempDepartment
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Department_GetRecordsByUserLink
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.CollegeId AS Department_CollegeId,
		Department.Name AS Department_Name,
		College.Name AS College_Name
	FROM Department
	LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
	JOIN DepartmentUserLink ON DepartmentUserLink.DepartmentId = Department.Id AND DepartmentUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Department_GetRecordsByUserLinkP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempDepartment
(
	TempId int IDENTITY PRIMARY KEY,
	Department_Id int,
	Department_CollegeId int,
	Department_Name varchar(100),
	College_Name varchar(50)
)

INSERT INTO #TempDepartment
(
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
)
EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.CollegeId AS Department_CollegeId,
		Department.Name AS Department_Name,
		College.Name AS College_Name
	FROM Department
	LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
	JOIN DepartmentUserLink ON DepartmentUserLink.DepartmentId = Department.Id AND DepartmentUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
FROM #TempDepartment
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Department_GetRecordsByUserLinkPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempDepartment
(
	TempId int IDENTITY PRIMARY KEY,
	Department_Id int,
	Department_CollegeId int,
	Department_Name varchar(100),
	College_Name varchar(50)
)

INSERT INTO #TempDepartment
(
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
)
EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.CollegeId AS Department_CollegeId,
		Department.Name AS Department_Name,
		College.Name AS College_Name
	FROM Department
	LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
	JOIN DepartmentUserLink ON DepartmentUserLink.DepartmentId = Department.Id AND DepartmentUserLink.UserId = ' + @UserId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
FROM #TempDepartment
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Department_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempDepartment
(
	TempId int IDENTITY PRIMARY KEY,
	Department_Id int,
	Department_CollegeId int,
	Department_Name varchar(100),
	College_Name varchar(50)
)

INSERT INTO #TempDepartment
(
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
)
EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.CollegeId AS Department_CollegeId,
		Department.Name AS Department_Name,
		College.Name AS College_Name
	FROM Department
	LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
FROM #TempDepartment
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Department_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempDepartment
(
	TempId int IDENTITY PRIMARY KEY,
	Department_Id int,
	Department_CollegeId int,
	Department_Name varchar(100),
	College_Name varchar(50)
)

INSERT INTO #TempDepartment
(
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
)
EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.CollegeId AS Department_CollegeId,
		Department.Name AS Department_Name,
		College.Name AS College_Name
	FROM Department
	LEFT OUTER JOIN College ON Department.CollegeId = College.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Department_Id,
	Department_CollegeId,
	Department_Name,
	College_Name
FROM #TempDepartment
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Department_GetUserLinks
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.Name AS Department_Name,
		CAST (ISNULL(DepartmentUserLink.UserId, 0) as bit) AS Selected
	FROM Department
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.DepartmentId = Department.Id AND DepartmentUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Department_GetUserLinksP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempDepartment
(
	TempId int IDENTITY PRIMARY KEY,
	Department_Id int,
	Department_Name varchar(100),
	selected bit
)

INSERT INTO #TempDepartment
(
	Department_Id,
	Department_Name,
	selected
)
EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.Name AS Department_Name,
		CAST (ISNULL(DepartmentUserLink.UserId, 0) as bit) AS Selected
	FROM Department
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.DepartmentId = Department.Id AND DepartmentUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Department_Id,
	Department_Name,
	selected
FROM #TempDepartment
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Department_GetUserLinksPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempDepartment
(
	TempId int IDENTITY PRIMARY KEY,
	Department_Id int,
	Department_Name varchar(100),
	selected bit
)

INSERT INTO #TempDepartment
(
	Department_Id,
	Department_Name,
	selected
)
EXEC
('
	SELECT
		Department.Id AS Department_Id,
		Department.Name AS Department_Name,
		CAST (ISNULL(DepartmentUserLink.UserId, 0) as bit) AS Selected
	FROM Department
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.DepartmentId = Department.Id AND DepartmentUserLink.UserId = ' + @UserId + ' 
	WHERE Department.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Department_Id,
	Department_Name,
	selected
FROM #TempDepartment
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Department_Update
  @Id int,
  @CollegeId int,
  @Name varchar(100)
AS

IF EXISTS (SELECT * FROM Department WHERE Name = @Name AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE Department
SET
	CollegeId = @CollegeId,
	Name = @Name
WHERE Id = @Id

RETURN 0CREATE PROCEDURE dbo.a_DepartmentUserLink_Create
  @DepartmentId int,
  @UserId int
AS

IF (SELECT COUNT(*) FROM DepartmentUserLink WHERE DepartmentId = @DepartmentId AND UserId = @UserId) > 0
	RETURN -16

INSERT INTO DepartmentUserLink
(DepartmentId, UserId)
VALUES (@DepartmentId, @UserId)

RETURN 0CREATE PROCEDURE dbo.a_DepartmentUserLink_CreateAllByDepartment
  @DepartmentId int
AS

INSERT INTO DepartmentUserLink
(DepartmentId, UserId)
SELECT @DepartmentId, Id FROM [User] WHERE NOT EXISTS (SELECT * FROM DepartmentUserLink WHERE DepartmentId = @DepartmentId AND UserId = Id)


RETURN 0CREATE PROCEDURE dbo.a_DepartmentUserLink_CreateAllByUser
  @UserId int
AS

INSERT INTO DepartmentUserLink
(DepartmentId, UserId)
SELECT Id, @UserId FROM Department WHERE NOT EXISTS (SELECT * FROM DepartmentUserLink WHERE DepartmentId = Id AND UserId = @UserId)


RETURN 0CREATE PROCEDURE dbo.a_DepartmentUserLink_Delete
  @DepartmentId int,
  @UserId int
AS

DELETE FROM DepartmentUserLink
WHERE DepartmentId = @DepartmentId AND UserId = @UserId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_DepartmentUserLink_DeleteAllByDepartment
  @DepartmentId int
AS

DELETE FROM DepartmentUserLink
WHERE DepartmentId = @DepartmentId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_DepartmentUserLink_DeleteAllByUser
  @UserId int
AS

DELETE FROM DepartmentUserLink
WHERE UserId = @UserId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_Email_Create
  @Subject varchar(250),
  @Message varchar(4000),
  @SentBy varchar(50)
AS

INSERT INTO Email
(
	Subject, 
	Message, 
	Sent, 
	SentBy
)
VALUES
(
	@Subject, 
	@Message, 
	getDate(), 
	@SentBy
)

SELECT @@IDENTITYCREATE PROCEDURE dbo.a_Email_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM EmailUserLink WHERE EmailId = @Id
DELETE FROM Email
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_Email_GetList

AS

SELECT
	Email.Id AS Email_Id,
	Email.Subject AS Email_Subject
FROM Email
ORDER BY Email.Subject
CREATE PROCEDURE dbo.a_Email_GetRecord
  @Id int
AS

SELECT
	Email.Id AS Email_Id,
	Email.Subject AS Email_Subject,
	Email.Message AS Email_Message,
	Email.Sent AS Email_Sent,
	Email.SentBy AS Email_SentBy
FROM Email
WHERE Email.Id = @IdCREATE PROCEDURE dbo.a_Email_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Email.Id AS Email_Id,
		Email.Subject AS Email_Subject,
		Email.Sent AS Email_Sent,
		Email.SentBy AS Email_SentBy
	FROM Email
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Email_GetRecordsByUserLink
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Email.Id AS Email_Id,
		Email.Subject AS Email_Subject,
		Email.Sent AS Email_Sent,
		Email.SentBy AS Email_SentBy
	FROM Email
	JOIN EmailUserLink ON EmailUserLink.EmailId = Email.Id AND EmailUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Email_GetRecordsByUserLinkP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempEmail
(
	TempId int IDENTITY PRIMARY KEY,
	Email_Id int,
	Email_Subject varchar(250),
	Email_Sent datetime,
	Email_SentBy varchar(50)
)

INSERT INTO #TempEmail
(
	Email_Id,
	Email_Subject,
	Email_Sent,
	Email_SentBy
)
EXEC
('
	SELECT
		Email.Id AS Email_Id,
		Email.Subject AS Email_Subject,
		Email.Sent AS Email_Sent,
		Email.SentBy AS Email_SentBy
	FROM Email
	JOIN EmailUserLink ON EmailUserLink.EmailId = Email.Id AND EmailUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Email_Id,
	Email_Subject,
	Email_Sent,
	Email_SentBy
FROM #TempEmail
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Email_GetRecordsByUserLinkPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempEmail
(
	TempId int IDENTITY PRIMARY KEY,
	Email_Id int,
	Email_Subject varchar(250),
	Email_Sent datetime,
	Email_SentBy varchar(50)
)

INSERT INTO #TempEmail
(
	Email_Id,
	Email_Subject,
	Email_Sent,
	Email_SentBy
)
EXEC
('
	SELECT
		Email.Id AS Email_Id,
		Email.Subject AS Email_Subject,
		Email.Sent AS Email_Sent,
		Email.SentBy AS Email_SentBy
	FROM Email
	JOIN EmailUserLink ON EmailUserLink.EmailId = Email.Id AND EmailUserLink.UserId = ' + @UserId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Email_Id,
	Email_Subject,
	Email_Sent,
	Email_SentBy
FROM #TempEmail
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Email_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempEmail
(
	TempId int IDENTITY PRIMARY KEY,
	Email_Id int,
	Email_Subject varchar(250),
	Email_Sent datetime,
	Email_SentBy varchar(50)
)

INSERT INTO #TempEmail
(
	Email_Id,
	Email_Subject,
	Email_Sent,
	Email_SentBy
)
EXEC
('
	SELECT
		Email.Id AS Email_Id,
		Email.Subject AS Email_Subject,
		Email.Sent AS Email_Sent,
		Email.SentBy AS Email_SentBy
	FROM Email
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Email_Id,
	Email_Subject,
	Email_Sent,
	Email_SentBy
FROM #TempEmail
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Email_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempEmail
(
	TempId int IDENTITY PRIMARY KEY,
	Email_Id int,
	Email_Subject varchar(250),
	Email_Sent datetime,
	Email_SentBy varchar(50)
)

INSERT INTO #TempEmail
(
	Email_Id,
	Email_Subject,
	Email_Sent,
	Email_SentBy
)
EXEC
('
	SELECT
		Email.Id AS Email_Id,
		Email.Subject AS Email_Subject,
		Email.Sent AS Email_Sent,
		Email.SentBy AS Email_SentBy
	FROM Email
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Email_Id,
	Email_Subject,
	Email_Sent,
	Email_SentBy
FROM #TempEmail
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Email_GetUserLinks
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Email.Id AS Email_Id,
		Email.Subject AS Email_Subject,
		CAST (ISNULL(EmailUserLink.UserId, 0) as bit) AS Selected
	FROM Email
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.EmailId = Email.Id AND EmailUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Email_GetUserLinksP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempEmail
(
	TempId int IDENTITY PRIMARY KEY,
	Email_Id int,
	Email_Subject varchar(250),
	selected bit
)

INSERT INTO #TempEmail
(
	Email_Id,
	Email_Subject,
	selected
)
EXEC
('
	SELECT
		Email.Id AS Email_Id,
		Email.Subject AS Email_Subject,
		CAST (ISNULL(EmailUserLink.UserId, 0) as bit) AS Selected
	FROM Email
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.EmailId = Email.Id AND EmailUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Email_Id,
	Email_Subject,
	selected
FROM #TempEmail
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Email_GetUserLinksPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempEmail
(
	TempId int IDENTITY PRIMARY KEY,
	Email_Id int,
	Email_Subject varchar(250),
	selected bit
)

INSERT INTO #TempEmail
(
	Email_Id,
	Email_Subject,
	selected
)
EXEC
('
	SELECT
		Email.Id AS Email_Id,
		Email.Subject AS Email_Subject,
		CAST (ISNULL(EmailUserLink.UserId, 0) as bit) AS Selected
	FROM Email
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.EmailId = Email.Id AND EmailUserLink.UserId = ' + @UserId + ' 
	WHERE Email.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Email_Id,
	Email_Subject,
	selected
FROM #TempEmail
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_EmailUserLink_Create
  @EmailId int,
  @UserId int
AS

IF (SELECT COUNT(*) FROM EmailUserLink WHERE EmailId = @EmailId AND UserId = @UserId) > 0
	RETURN -16

INSERT INTO EmailUserLink
(EmailId, UserId)
VALUES (@EmailId, @UserId)

RETURN 0CREATE PROCEDURE dbo.a_EmailUserLink_CreateAllByEmail
  @EmailId int
AS

INSERT INTO EmailUserLink
(EmailId, UserId)
SELECT @EmailId, Id FROM [User] WHERE NOT EXISTS (SELECT * FROM EmailUserLink WHERE EmailId = @EmailId AND UserId = Id)


RETURN 0CREATE PROCEDURE dbo.a_EmailUserLink_CreateAllByUser
  @UserId int
AS

INSERT INTO EmailUserLink
(EmailId, UserId)
SELECT Id, @UserId FROM Email WHERE NOT EXISTS (SELECT * FROM EmailUserLink WHERE EmailId = Id AND UserId = @UserId)


RETURN 0CREATE PROCEDURE dbo.a_EmailUserLink_Delete
  @EmailId int,
  @UserId int
AS

DELETE FROM EmailUserLink
WHERE EmailId = @EmailId AND UserId = @UserId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_EmailUserLink_DeleteAllByEmail
  @EmailId int
AS

DELETE FROM EmailUserLink
WHERE EmailId = @EmailId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_EmailUserLink_DeleteAllByUser
  @UserId int
AS

DELETE FROM EmailUserLink
WHERE UserId = @UserId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_MasterAccount_Create
  @AccountId int
AS

INSERT INTO MasterAccount
(
	AccountId
)
VALUES
(
	@AccountId
)

SELECT @@IDENTITYCREATE PROCEDURE dbo.a_MasterAccount_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM MasterAccountAccountLink WHERE MasterAccountId = @Id
DELETE FROM MasterAccount
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_MasterAccount_GetAccountLinks
  @LinkedAccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		CAST (ISNULL(MasterAccountAccountLink.LinkedAccountId, 0) as bit) AS Selected
	FROM MasterAccount
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.MasterAccountId = MasterAccount.Id AND MasterAccountAccountLink.LinkedAccountId = ' + @LinkedAccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_MasterAccount_GetAccountLinksP
  @LinkedAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempMasterAccount
(
	TempId int IDENTITY PRIMARY KEY,
	MasterAccount_Id int,
	selected bit
)

INSERT INTO #TempMasterAccount
(
	MasterAccount_Id,
	selected
)
EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		CAST (ISNULL(MasterAccountAccountLink.LinkedAccountId, 0) as bit) AS Selected
	FROM MasterAccount
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.MasterAccountId = MasterAccount.Id AND MasterAccountAccountLink.LinkedAccountId = ' + @LinkedAccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	MasterAccount_Id,
	selected
FROM #TempMasterAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_MasterAccount_GetAccountLinksPS
  @LinkedAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempMasterAccount
(
	TempId int IDENTITY PRIMARY KEY,
	MasterAccount_Id int,
	selected bit
)

INSERT INTO #TempMasterAccount
(
	MasterAccount_Id,
	selected
)
EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		CAST (ISNULL(MasterAccountAccountLink.LinkedAccountId, 0) as bit) AS Selected
	FROM MasterAccount
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.MasterAccountId = MasterAccount.Id AND MasterAccountAccountLink.LinkedAccountId = ' + @LinkedAccountId + ' 
	WHERE MasterAccount.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	MasterAccount_Id,
	selected
FROM #TempMasterAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_MasterAccount_GetList

AS

SELECT
	MasterAccount.Id AS MasterAccount_Id
FROM MasterAccount
ORDER BY MasterAccount.Id
CREATE PROCEDURE dbo.a_MasterAccount_GetRecord
  @Id int
AS

SELECT
	MasterAccount.Id AS MasterAccount_Id,
	MasterAccount.AccountId AS MasterAccount_AccountId,
	Account.AccountType AS Account_AccountType,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription,
	Account.AcctStat AS Account_AcctStat,
	Account.AdminUnit AS Account_AdminUnit,
	Account.Modified AS Account_Modified,
	Account.ModifiedBy AS Account_ModifiedBy
FROM MasterAccount
LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
WHERE MasterAccount.Id = @IdCREATE PROCEDURE dbo.a_MasterAccount_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_MasterAccount_GetRecordsByAccountField
  @AccountId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	WHERE MasterAccount.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_MasterAccount_GetRecordsByAccountFieldP
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempMasterAccount
(
	TempId int IDENTITY PRIMARY KEY,
	MasterAccount_Id int,
	MasterAccount_AccountId int,
	Account_AccountType char(1),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempMasterAccount
(
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	WHERE MasterAccount.AccountId = ' + @AccountId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempMasterAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_MasterAccount_GetRecordsByAccountFieldPS
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempMasterAccount
(
	TempId int IDENTITY PRIMARY KEY,
	MasterAccount_Id int,
	MasterAccount_AccountId int,
	Account_AccountType char(1),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempMasterAccount
(
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND MasterAccount.AccountId = ' + @AccountId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempMasterAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_MasterAccount_GetRecordsByAccountLink
  @LinkedAccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	JOIN MasterAccountAccountLink ON MasterAccountAccountLink.MasterAccountId = MasterAccount.Id AND MasterAccountAccountLink.LinkedAccountId = ' + @LinkedAccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_MasterAccount_GetRecordsByAccountLinkP
  @LinkedAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempMasterAccount
(
	TempId int IDENTITY PRIMARY KEY,
	MasterAccount_Id int,
	MasterAccount_AccountId int,
	Account_AccountType char(1),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempMasterAccount
(
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	JOIN MasterAccountAccountLink ON MasterAccountAccountLink.MasterAccountId = MasterAccount.Id AND MasterAccountAccountLink.LinkedAccountId = ' + @LinkedAccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempMasterAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_MasterAccount_GetRecordsByAccountLinkPS
  @LinkedAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempMasterAccount
(
	TempId int IDENTITY PRIMARY KEY,
	MasterAccount_Id int,
	MasterAccount_AccountId int,
	Account_AccountType char(1),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempMasterAccount
(
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	JOIN MasterAccountAccountLink ON MasterAccountAccountLink.MasterAccountId = MasterAccount.Id AND MasterAccountAccountLink.LinkedAccountId = ' + @LinkedAccountId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempMasterAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_MasterAccount_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempMasterAccount
(
	TempId int IDENTITY PRIMARY KEY,
	MasterAccount_Id int,
	MasterAccount_AccountId int,
	Account_AccountType char(1),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempMasterAccount
(
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempMasterAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_MasterAccount_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempMasterAccount
(
	TempId int IDENTITY PRIMARY KEY,
	MasterAccount_Id int,
	MasterAccount_AccountId int,
	Account_AccountType char(1),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempMasterAccount
(
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempMasterAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_MasterAccount_Update
  @Id int,
  @AccountId int
AS

UPDATE MasterAccount
SET
	AccountId = @AccountId
WHERE Id = @Id

RETURN 0CREATE PROCEDURE dbo.a_MasterAccountAccountLink_Create
  @MasterAccountId int,
  @LinkedAccountId int
AS

IF (SELECT COUNT(*) FROM MasterAccountAccountLink WHERE MasterAccountId = @MasterAccountId AND LinkedAccountId = @LinkedAccountId) > 0
	RETURN -16

INSERT INTO MasterAccountAccountLink
(MasterAccountId, LinkedAccountId)
VALUES (@MasterAccountId, @LinkedAccountId)

RETURN 0CREATE PROCEDURE dbo.a_MasterAccountAccountLink_CreateAllByAccount
  @LinkedAccountId int
AS

INSERT INTO MasterAccountAccountLink
(MasterAccountId, LinkedAccountId)
SELECT Id, @LinkedAccountId FROM MasterAccount WHERE NOT EXISTS (SELECT * FROM MasterAccountAccountLink WHERE MasterAccountId = Id AND LinkedAccountId = @LinkedAccountId)


RETURN 0CREATE PROCEDURE dbo.a_MasterAccountAccountLink_CreateAllByMasterAccount
  @MasterAccountId int
AS

INSERT INTO MasterAccountAccountLink
(MasterAccountId, LinkedAccountId)
SELECT @MasterAccountId, Id FROM Account WHERE NOT EXISTS (SELECT * FROM MasterAccountAccountLink WHERE MasterAccountId = @MasterAccountId AND LinkedAccountId = Id)


RETURN 0CREATE PROCEDURE dbo.a_MasterAccountAccountLink_Delete
  @MasterAccountId int,
  @LinkedAccountId int
AS

DELETE FROM MasterAccountAccountLink
WHERE MasterAccountId = @MasterAccountId AND LinkedAccountId = @LinkedAccountId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_MasterAccountAccountLink_DeleteAllByAccount
  @LinkedAccountId int
AS

DELETE FROM MasterAccountAccountLink
WHERE LinkedAccountId = @LinkedAccountId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_MasterAccountAccountLink_DeleteAllByMasterAccount
  @MasterAccountId int
AS

DELETE FROM MasterAccountAccountLink
WHERE MasterAccountId = @MasterAccountId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_Payouts_GetList

AS

SELECT
	Payouts.Id AS Payouts_Id
FROM Payouts
ORDER BY Payouts.Id
CREATE PROCEDURE dbo.a_Payouts_GetRecord
  @Id int
AS

SELECT
	Payouts.Id AS Payouts_Id,
	Payouts.Account AS Payouts_Account,
	Payouts.Payout AS Payouts_Payout,
	Payouts.[Date] AS Payouts_Date
FROM Payouts
WHERE Payouts.Id = @IdCREATE PROCEDURE dbo.a_Payouts_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Payouts.Id AS Payouts_Id,
		Payouts.Account AS Payouts_Account,
		Payouts.Payout AS Payouts_Payout,
		Payouts.[Date] AS Payouts_Date
	FROM Payouts
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Payouts_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempPayouts
(
	TempId int IDENTITY PRIMARY KEY,
	Payouts_Id int,
	Payouts_Account char(10),
	Payouts_Payout money,
	Payouts_Date smalldatetime
)

INSERT INTO #TempPayouts
(
	Payouts_Id,
	Payouts_Account,
	Payouts_Payout,
	Payouts_Date
)
EXEC
('
	SELECT
		Payouts.Id AS Payouts_Id,
		Payouts.Account AS Payouts_Account,
		Payouts.Payout AS Payouts_Payout,
		Payouts.[Date] AS Payouts_Date
	FROM Payouts
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Payouts_Id,
	Payouts_Account,
	Payouts_Payout,
	Payouts_Date
FROM #TempPayouts
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Payouts_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempPayouts
(
	TempId int IDENTITY PRIMARY KEY,
	Payouts_Id int,
	Payouts_Account char(10),
	Payouts_Payout money,
	Payouts_Date smalldatetime
)

INSERT INTO #TempPayouts
(
	Payouts_Id,
	Payouts_Account,
	Payouts_Payout,
	Payouts_Date
)
EXEC
('
	SELECT
		Payouts.Id AS Payouts_Id,
		Payouts.Account AS Payouts_Account,
		Payouts.Payout AS Payouts_Payout,
		Payouts.[Date] AS Payouts_Date
	FROM Payouts
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Payouts_Id,
	Payouts_Account,
	Payouts_Payout,
	Payouts_Date
FROM #TempPayouts
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Permission_GetList

AS

SELECT
	Permission.Id AS Permission_Id,
	Permission.Description AS Permission_Description
FROM Permission
ORDER BY Permission.Description
CREATE PROCEDURE dbo.a_Permission_GetRecord
  @Id int
AS

SELECT
	Permission.Id AS Permission_Id,
	Permission.CategoryId AS Permission_CategoryId,
	Permission.Description AS Permission_Description,
	Permission.Rank AS Permission_Rank
FROM Permission
LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
WHERE Permission.Id = @IdCREATE PROCEDURE dbo.a_Permission_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.CategoryId AS Permission_CategoryId,
		Permission.Description AS Permission_Description,
		Permission.Rank AS Permission_Rank
	FROM Permission
	LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Permission_GetRecordsByPermissionCategoryField
  @CategoryId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.CategoryId AS Permission_CategoryId,
		Permission.Description AS Permission_Description,
		Permission.Rank AS Permission_Rank
	FROM Permission
	LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
	WHERE Permission.CategoryId = ' + @CategoryId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Permission_GetRecordsByPermissionCategoryFieldP
  @CategoryId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempPermission
(
	TempId int IDENTITY PRIMARY KEY,
	Permission_Id int,
	Permission_CategoryId int,
	Permission_Description varchar(100),
	Permission_Rank smallint
)

INSERT INTO #TempPermission
(
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
)
EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.CategoryId AS Permission_CategoryId,
		Permission.Description AS Permission_Description,
		Permission.Rank AS Permission_Rank
	FROM Permission
	LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
	WHERE Permission.CategoryId = ' + @CategoryId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
FROM #TempPermission
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Permission_GetRecordsByPermissionCategoryFieldPS
  @CategoryId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempPermission
(
	TempId int IDENTITY PRIMARY KEY,
	Permission_Id int,
	Permission_CategoryId int,
	Permission_Description varchar(100),
	Permission_Rank smallint
)

INSERT INTO #TempPermission
(
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
)
EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.CategoryId AS Permission_CategoryId,
		Permission.Description AS Permission_Description,
		Permission.Rank AS Permission_Rank
	FROM Permission
	LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Permission.CategoryId = ' + @CategoryId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
FROM #TempPermission
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Permission_GetRecordsByRoleLink
  @RoleId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.CategoryId AS Permission_CategoryId,
		Permission.Description AS Permission_Description,
		Permission.Rank AS Permission_Rank
	FROM Permission
	LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
	JOIN RolePermissionLink ON RolePermissionLink.PermissionId = Permission.Id AND RolePermissionLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Permission_GetRecordsByRoleLinkP
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempPermission
(
	TempId int IDENTITY PRIMARY KEY,
	Permission_Id int,
	Permission_CategoryId int,
	Permission_Description varchar(100),
	Permission_Rank smallint
)

INSERT INTO #TempPermission
(
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
)
EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.CategoryId AS Permission_CategoryId,
		Permission.Description AS Permission_Description,
		Permission.Rank AS Permission_Rank
	FROM Permission
	LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
	JOIN RolePermissionLink ON RolePermissionLink.PermissionId = Permission.Id AND RolePermissionLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
FROM #TempPermission
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Permission_GetRecordsByRoleLinkPS
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempPermission
(
	TempId int IDENTITY PRIMARY KEY,
	Permission_Id int,
	Permission_CategoryId int,
	Permission_Description varchar(100),
	Permission_Rank smallint
)

INSERT INTO #TempPermission
(
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
)
EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.CategoryId AS Permission_CategoryId,
		Permission.Description AS Permission_Description,
		Permission.Rank AS Permission_Rank
	FROM Permission
	LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
	JOIN RolePermissionLink ON RolePermissionLink.PermissionId = Permission.Id AND RolePermissionLink.RoleId = ' + @RoleId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
FROM #TempPermission
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Permission_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempPermission
(
	TempId int IDENTITY PRIMARY KEY,
	Permission_Id int,
	Permission_CategoryId int,
	Permission_Description varchar(100),
	Permission_Rank smallint
)

INSERT INTO #TempPermission
(
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
)
EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.CategoryId AS Permission_CategoryId,
		Permission.Description AS Permission_Description,
		Permission.Rank AS Permission_Rank
	FROM Permission
	LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
FROM #TempPermission
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Permission_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempPermission
(
	TempId int IDENTITY PRIMARY KEY,
	Permission_Id int,
	Permission_CategoryId int,
	Permission_Description varchar(100),
	Permission_Rank smallint
)

INSERT INTO #TempPermission
(
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
)
EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.CategoryId AS Permission_CategoryId,
		Permission.Description AS Permission_Description,
		Permission.Rank AS Permission_Rank
	FROM Permission
	LEFT OUTER JOIN PermissionCategory ON Permission.CategoryId = PermissionCategory.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Permission_Id,
	Permission_CategoryId,
	Permission_Description,
	Permission_Rank
FROM #TempPermission
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Permission_GetRoleLinks
  @RoleId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.Description AS Permission_Description,
		CAST (ISNULL(RolePermissionLink.RoleId, 0) as bit) AS Selected
	FROM Permission
	LEFT OUTER JOIN RolePermissionLink ON RolePermissionLink.PermissionId = Permission.Id AND RolePermissionLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Permission_GetRoleLinksP
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempPermission
(
	TempId int IDENTITY PRIMARY KEY,
	Permission_Id int,
	Permission_Description varchar(100),
	selected bit
)

INSERT INTO #TempPermission
(
	Permission_Id,
	Permission_Description,
	selected
)
EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.Description AS Permission_Description,
		CAST (ISNULL(RolePermissionLink.RoleId, 0) as bit) AS Selected
	FROM Permission
	LEFT OUTER JOIN RolePermissionLink ON RolePermissionLink.PermissionId = Permission.Id AND RolePermissionLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Permission_Id,
	Permission_Description,
	selected
FROM #TempPermission
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Permission_GetRoleLinksPS
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempPermission
(
	TempId int IDENTITY PRIMARY KEY,
	Permission_Id int,
	Permission_Description varchar(100),
	selected bit
)

INSERT INTO #TempPermission
(
	Permission_Id,
	Permission_Description,
	selected
)
EXEC
('
	SELECT
		Permission.Id AS Permission_Id,
		Permission.Description AS Permission_Description,
		CAST (ISNULL(RolePermissionLink.RoleId, 0) as bit) AS Selected
	FROM Permission
	LEFT OUTER JOIN RolePermissionLink ON RolePermissionLink.PermissionId = Permission.Id AND RolePermissionLink.RoleId = ' + @RoleId + ' 
	WHERE Permission.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Permission_Id,
	Permission_Description,
	selected
FROM #TempPermission
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_PermissionCategory_GetList

AS

SELECT
	PermissionCategory.Id AS PermissionCategory_Id,
	PermissionCategory.Description AS PermissionCategory_Description
FROM PermissionCategory
ORDER BY PermissionCategory.Id
CREATE PROCEDURE dbo.a_PermissionCategory_GetRecord
  @Id int
AS

SELECT
	PermissionCategory.Id AS PermissionCategory_Id,
	PermissionCategory.Description AS PermissionCategory_Description,
	PermissionCategory.Rank AS PermissionCategory_Rank
FROM PermissionCategory
WHERE PermissionCategory.Id = @IdCREATE PROCEDURE dbo.a_PermissionCategory_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		PermissionCategory.Id AS PermissionCategory_Id,
		PermissionCategory.Description AS PermissionCategory_Description,
		PermissionCategory.Rank AS PermissionCategory_Rank
	FROM PermissionCategory
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_PermissionCategory_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempPermissionCategory
(
	TempId int IDENTITY PRIMARY KEY,
	PermissionCategory_Id int,
	PermissionCategory_Description varchar(50),
	PermissionCategory_Rank smallint
)

INSERT INTO #TempPermissionCategory
(
	PermissionCategory_Id,
	PermissionCategory_Description,
	PermissionCategory_Rank
)
EXEC
('
	SELECT
		PermissionCategory.Id AS PermissionCategory_Id,
		PermissionCategory.Description AS PermissionCategory_Description,
		PermissionCategory.Rank AS PermissionCategory_Rank
	FROM PermissionCategory
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	PermissionCategory_Id,
	PermissionCategory_Description,
	PermissionCategory_Rank
FROM #TempPermissionCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_PermissionCategory_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempPermissionCategory
(
	TempId int IDENTITY PRIMARY KEY,
	PermissionCategory_Id int,
	PermissionCategory_Description varchar(50),
	PermissionCategory_Rank smallint
)

INSERT INTO #TempPermissionCategory
(
	PermissionCategory_Id,
	PermissionCategory_Description,
	PermissionCategory_Rank
)
EXEC
('
	SELECT
		PermissionCategory.Id AS PermissionCategory_Id,
		PermissionCategory.Description AS PermissionCategory_Description,
		PermissionCategory.Rank AS PermissionCategory_Rank
	FROM PermissionCategory
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	PermissionCategory_Id,
	PermissionCategory_Description,
	PermissionCategory_Rank
FROM #TempPermissionCategory
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_Create
  @Description varchar(25),
  @Rank smallint
AS

IF EXISTS (SELECT * FROM [Role] WHERE Description = @Description)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO [Role]
(
	Description, 
	Rank
)
VALUES
(
	@Description, 
	@Rank
)

SELECT @@IDENTITYCREATE PROCEDURE dbo.a_Role_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM RolePermissionLink WHERE RoleId = @Id
DELETE FROM UserRoleLink WHERE RoleId = @Id
DELETE FROM [Role]
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_Role_GetList

AS

SELECT
	[Role].Id AS Role_Id,
	[Role].Description AS Role_Description
FROM [Role]
ORDER BY [Role].Id
CREATE PROCEDURE dbo.a_Role_GetPermissionLinks
  @PermissionId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(RolePermissionLink.PermissionId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Role_GetPermissionLinksByUser
  @PermissionId int,
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(RolePermissionLink.PermissionId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Role_GetPermissionLinksByUserP
  @PermissionId int,
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	selected bit
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	selected
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(RolePermissionLink.PermissionId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	selected
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetPermissionLinksP
  @PermissionId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	selected bit
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	selected
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(RolePermissionLink.PermissionId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	selected
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetPermissionLinksPS
  @PermissionId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	selected bit
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	selected
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(RolePermissionLink.PermissionId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	WHERE [Role].' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	selected
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetRecord
  @Id int
AS

SELECT
	[Role].Id AS Role_Id,
	[Role].Description AS Role_Description,
	[Role].Rank AS Role_Rank
FROM [Role]
WHERE [Role].Id = @IdCREATE PROCEDURE dbo.a_Role_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		[Role].Rank AS Role_Rank
	FROM [Role]
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Role_GetRecordsByPermissionLink
  @PermissionId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		[Role].Rank AS Role_Rank
	FROM [Role]
	JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Role_GetRecordsByPermissionLinkP
  @PermissionId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	Role_Rank smallint
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	Role_Rank
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		[Role].Rank AS Role_Rank
	FROM [Role]
	JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	Role_Rank
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetRecordsByPermissionLinkPS
  @PermissionId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	Role_Rank smallint
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	Role_Rank
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		[Role].Rank AS Role_Rank
	FROM [Role]
	JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	Role_Rank
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetRecordsByUserLink
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		[Role].Rank AS Role_Rank
	FROM [Role]
	JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Role_GetRecordsByUserLinkP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	Role_Rank smallint
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	Role_Rank
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		[Role].Rank AS Role_Rank
	FROM [Role]
	JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	Role_Rank
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetRecordsByUserLinkPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	Role_Rank smallint
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	Role_Rank
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		[Role].Rank AS Role_Rank
	FROM [Role]
	JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	Role_Rank
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	Role_Rank smallint
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	Role_Rank
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		[Role].Rank AS Role_Rank
	FROM [Role]
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	Role_Rank
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	Role_Rank smallint
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	Role_Rank
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		[Role].Rank AS Role_Rank
	FROM [Role]
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	Role_Rank
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetUserLinks
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(UserRoleLink.UserId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Role_GetUserLinksByPermission
  @UserId int,
  @PermissionId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(UserRoleLink.UserId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	INNER JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Role_GetUserLinksByPermissionP
  @UserId int,
  @PermissionId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	selected bit
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	selected
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(UserRoleLink.UserId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	INNER JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	selected
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetUserLinksP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	selected bit
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	selected
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(UserRoleLink.UserId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	selected
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_GetUserLinksPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25),
	selected bit
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description,
	selected
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description,
		CAST (ISNULL(UserRoleLink.UserId, 0) as bit) AS Selected
	FROM [Role]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	WHERE [Role].' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Role_Id,
	Role_Description,
	selected
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Role_Update
  @Id int,
  @Description varchar(25),
  @Rank smallint
AS

IF EXISTS (SELECT * FROM [Role] WHERE Description = @Description AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE [Role]
SET
	Description = @Description,
	Rank = @Rank
WHERE Id = @Id

RETURN 0CREATE PROCEDURE dbo.a_RolePermissionLink_Create
  @RoleId int,
  @PermissionId int
AS

IF (SELECT COUNT(*) FROM RolePermissionLink WHERE RoleId = @RoleId AND PermissionId = @PermissionId) > 0
	RETURN -16

INSERT INTO RolePermissionLink
(RoleId, PermissionId)
VALUES (@RoleId, @PermissionId)

RETURN 0CREATE PROCEDURE dbo.a_RolePermissionLink_CreateAllByPermission
  @PermissionId int
AS

INSERT INTO RolePermissionLink
(RoleId, PermissionId)
SELECT Id, @PermissionId FROM [Role] WHERE NOT EXISTS (SELECT * FROM RolePermissionLink WHERE RoleId = Id AND PermissionId = @PermissionId)


RETURN 0CREATE PROCEDURE dbo.a_RolePermissionLink_CreateAllByRole
  @RoleId int
AS

INSERT INTO RolePermissionLink
(RoleId, PermissionId)
SELECT @RoleId, Id FROM Permission WHERE NOT EXISTS (SELECT * FROM RolePermissionLink WHERE RoleId = @RoleId AND PermissionId = Id)


RETURN 0CREATE PROCEDURE dbo.a_RolePermissionLink_Delete
  @RoleId int,
  @PermissionId int
AS

DELETE FROM RolePermissionLink
WHERE RoleId = @RoleId AND PermissionId = @PermissionId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_RolePermissionLink_DeleteAllByPermission
  @PermissionId int
AS

DELETE FROM RolePermissionLink
WHERE PermissionId = @PermissionId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_RolePermissionLink_DeleteAllByRole
  @RoleId int
AS

DELETE FROM RolePermissionLink
WHERE RoleId = @RoleId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_Transaction_GetList

AS

SELECT
	[Transaction].Id AS Transaction_Id
FROM [Transaction]
ORDER BY [Transaction].Id
CREATE PROCEDURE dbo.a_Transaction_GetRecord
  @Id int
AS

SELECT
	[Transaction].Id AS Transaction_Id,
	[Transaction].AccountNumber AS Transaction_AccountNumber,
	[Transaction].DonorPayee AS Transaction_DonorPayee,
	[Transaction].TransDescription AS Transaction_TransDescription,
	[Transaction].TransDate AS Transaction_TransDate,
	[Transaction].TransAmount AS Transaction_TransAmount
FROM [Transaction]
WHERE [Transaction].Id = @IdCREATE PROCEDURE dbo.a_Transaction_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Transaction].Id AS Transaction_Id,
		[Transaction].AccountNumber AS Transaction_AccountNumber,
		[Transaction].DonorPayee AS Transaction_DonorPayee,
		[Transaction].TransDescription AS Transaction_TransDescription,
		[Transaction].TransDate AS Transaction_TransDate,
		[Transaction].TransAmount AS Transaction_TransAmount
	FROM [Transaction]
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_Transaction_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempTransaction
(
	TempId int IDENTITY PRIMARY KEY,
	Transaction_Id int,
	Transaction_AccountNumber varchar(20),
	Transaction_DonorPayee varchar(60),
	Transaction_TransDescription varchar(60),
	Transaction_TransDate datetime,
	Transaction_TransAmount money
)

INSERT INTO #TempTransaction
(
	Transaction_Id,
	Transaction_AccountNumber,
	Transaction_DonorPayee,
	Transaction_TransDescription,
	Transaction_TransDate,
	Transaction_TransAmount
)
EXEC
('
	SELECT
		[Transaction].Id AS Transaction_Id,
		[Transaction].AccountNumber AS Transaction_AccountNumber,
		[Transaction].DonorPayee AS Transaction_DonorPayee,
		[Transaction].TransDescription AS Transaction_TransDescription,
		[Transaction].TransDate AS Transaction_TransDate,
		[Transaction].TransAmount AS Transaction_TransAmount
	FROM [Transaction]
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Transaction_Id,
	Transaction_AccountNumber,
	Transaction_DonorPayee,
	Transaction_TransDescription,
	Transaction_TransDate,
	Transaction_TransAmount
FROM #TempTransaction
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_Transaction_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempTransaction
(
	TempId int IDENTITY PRIMARY KEY,
	Transaction_Id int,
	Transaction_AccountNumber varchar(20),
	Transaction_DonorPayee varchar(60),
	Transaction_TransDescription varchar(60),
	Transaction_TransDate datetime,
	Transaction_TransAmount money
)

INSERT INTO #TempTransaction
(
	Transaction_Id,
	Transaction_AccountNumber,
	Transaction_DonorPayee,
	Transaction_TransDescription,
	Transaction_TransDate,
	Transaction_TransAmount
)
EXEC
('
	SELECT
		[Transaction].Id AS Transaction_Id,
		[Transaction].AccountNumber AS Transaction_AccountNumber,
		[Transaction].DonorPayee AS Transaction_DonorPayee,
		[Transaction].TransDescription AS Transaction_TransDescription,
		[Transaction].TransDate AS Transaction_TransDate,
		[Transaction].TransAmount AS Transaction_TransAmount
	FROM [Transaction]
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Transaction_Id,
	Transaction_AccountNumber,
	Transaction_DonorPayee,
	Transaction_TransDescription,
	Transaction_TransDate,
	Transaction_TransAmount
FROM #TempTransaction
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_Create
  @Login varchar(50),
  @Password varbinary(16),
  @FirstName varchar(25),
  @LastName varchar(25),
  @CampusCode varchar(10),
  @UniId varchar(10),
  @ConAgree bit,
  @OnlineAccess bit,
  @ModifiedBy varchar(50)
AS

IF EXISTS (SELECT * FROM [User] WHERE Login = @Login)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO [User]
(
	Login, 
	Password, 
	FirstName, 
	LastName, 
	CampusCode, 
	UniId, 
	ConAgree, 
	OnlineAccess, 
	Created, 
	Modified, 
	ModifiedBy
)
VALUES
(
	@Login, 
	@Password, 
	@FirstName, 
	@LastName, 
	@CampusCode, 
	@UniId, 
	@ConAgree, 
	@OnlineAccess, 
	getDate(), 
	getDate(), 
	@ModifiedBy
)

SELECT @@IDENTITYCREATE PROCEDURE dbo.a_User_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM AccCategory WHERE UserId = @Id) > 0)
		RETURN -16

	IF ((SELECT COUNT(*) FROM UserLog WHERE UserId = @Id) > 0)
		RETURN -17

	END 
ELSE 
	BEGIN

	DELETE FROM AccCategory WHERE UserId = @Id

	DELETE FROM UserLog WHERE UserId = @Id

	END 

DELETE FROM UserRoleLink WHERE UserId = @Id
DELETE FROM DepartmentUserLink WHERE UserId = @Id
DELETE FROM UserAccountLink WHERE UserId = @Id
DELETE FROM EmailUserLink WHERE UserId = @Id
DELETE FROM UsrGroupUserLink WHERE UserId = @Id
DELETE FROM [User]
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_User_GetAccountLinks
  @AccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetAccountLinksByDepartment
  @AccountId int,
  @DepartmentId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	INNER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetAccountLinksByDepartmentP
  @AccountId int,
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	INNER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetAccountLinksByEmail
  @AccountId int,
  @EmailId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	INNER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetAccountLinksByEmailP
  @AccountId int,
  @EmailId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	INNER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetAccountLinksByRole
  @AccountId int,
  @RoleId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetAccountLinksByRoleP
  @AccountId int,
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetAccountLinksByUsrGroup
  @AccountId int,
  @UsrGroupId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	INNER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetAccountLinksByUsrGroupP
  @AccountId int,
  @UsrGroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	INNER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetAccountLinksP
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetAccountLinksPS
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	WHERE [User].' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetDepartmentLinks
  @DepartmentId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetDepartmentLinksByAccount
  @DepartmentId int,
  @AccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetDepartmentLinksByAccountP
  @DepartmentId int,
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetDepartmentLinksByEmail
  @DepartmentId int,
  @EmailId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	INNER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetDepartmentLinksByEmailP
  @DepartmentId int,
  @EmailId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	INNER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetDepartmentLinksByRole
  @DepartmentId int,
  @RoleId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetDepartmentLinksByRoleP
  @DepartmentId int,
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetDepartmentLinksByUsrGroup
  @DepartmentId int,
  @UsrGroupId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	INNER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetDepartmentLinksByUsrGroupP
  @DepartmentId int,
  @UsrGroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	INNER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetDepartmentLinksP
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetDepartmentLinksPS
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	WHERE [User].' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetEmailLinks
  @EmailId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetEmailLinksByAccount
  @EmailId int,
  @AccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetEmailLinksByAccountP
  @EmailId int,
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetEmailLinksByDepartment
  @EmailId int,
  @DepartmentId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	INNER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetEmailLinksByDepartmentP
  @EmailId int,
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	INNER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetEmailLinksByRole
  @EmailId int,
  @RoleId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetEmailLinksByRoleP
  @EmailId int,
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetEmailLinksByUsrGroup
  @EmailId int,
  @UsrGroupId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	INNER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetEmailLinksByUsrGroupP
  @EmailId int,
  @UsrGroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	INNER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetEmailLinksP
  @EmailId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetEmailLinksPS
  @EmailId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(EmailUserLink.EmailId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	WHERE [User].' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetList

AS

SELECT
	[User].Id AS User_Id,
	[User].LastName + ', ' + [User].FirstName AS User_FullName
FROM [User]
ORDER BY LastName
CREATE PROCEDURE dbo.a_User_GetRecord
  @Id int
AS

SELECT
	[User].Id AS User_Id,
	[User].Login AS User_Login,
	[User].Password AS User_Password,
	[User].FirstName AS User_FirstName,
	[User].LastName AS User_LastName,
	[User].CampusCode AS User_CampusCode,
	[User].UniId AS User_UniId,
	[User].ConAgree AS User_ConAgree,
	[User].OnlineAccess AS User_OnlineAccess,
	[User].Created AS User_Created,
	[User].Modified AS User_Modified,
	[User].ModifiedBy AS User_ModifiedBy,
	[User].LastName + ', ' + [User].FirstName AS User_FullName
FROM [User]
WHERE [User].Id = @IdCREATE PROCEDURE dbo.a_User_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRecordsByAccountLink
  @AccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRecordsByAccountLinkP
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsByAccountLinkPS
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsByDepartmentLink
  @DepartmentId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRecordsByDepartmentLinkP
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsByDepartmentLinkPS
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsByEmailLink
  @EmailId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRecordsByEmailLinkP
  @EmailId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsByEmailLinkPS
  @EmailId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsByRoleLink
  @RoleId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRecordsByRoleLinkP
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsByRoleLinkPS
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsByUsrGroupLink
  @UsrGroupId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRecordsByUsrGroupLinkP
  @UsrGroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsByUsrGroupLinkPS
  @UsrGroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_Login varchar(50),
	User_Password varbinary(16),
	User_FirstName varchar(25),
	User_LastName varchar(25),
	User_CampusCode varchar(10),
	User_UniId varchar(10),
	User_ConAgree bit,
	User_OnlineAccess bit,
	User_Created datetime,
	User_Modified datetime,
	User_ModifiedBy varchar(50),
	User_FullName varchar(50)
)

INSERT INTO #TempUser
(
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].Login AS User_Login,
		[User].Password AS User_Password,
		[User].FirstName AS User_FirstName,
		[User].LastName AS User_LastName,
		[User].CampusCode AS User_CampusCode,
		[User].UniId AS User_UniId,
		[User].ConAgree AS User_ConAgree,
		[User].OnlineAccess AS User_OnlineAccess,
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_Login,
	User_Password,
	User_FirstName,
	User_LastName,
	User_CampusCode,
	User_UniId,
	User_ConAgree,
	User_OnlineAccess,
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRoleLinks
  @RoleId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRoleLinksByAccount
  @RoleId int,
  @AccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRoleLinksByAccountP
  @RoleId int,
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRoleLinksByDepartment
  @RoleId int,
  @DepartmentId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	INNER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRoleLinksByDepartmentP
  @RoleId int,
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	INNER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRoleLinksByEmail
  @RoleId int,
  @EmailId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	INNER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRoleLinksByEmailP
  @RoleId int,
  @EmailId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	INNER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRoleLinksByUsrGroup
  @RoleId int,
  @UsrGroupId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	INNER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetRoleLinksByUsrGroupP
  @RoleId int,
  @UsrGroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	INNER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRoleLinksP
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetRoleLinksPS
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	WHERE [User].' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetUsrGroupLinks
  @UsrGroupId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetUsrGroupLinksByAccount
  @UsrGroupId int,
  @AccountId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetUsrGroupLinksByAccountP
  @UsrGroupId int,
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetUsrGroupLinksByDepartment
  @UsrGroupId int,
  @DepartmentId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	INNER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetUsrGroupLinksByDepartmentP
  @UsrGroupId int,
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	INNER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetUsrGroupLinksByEmail
  @UsrGroupId int,
  @EmailId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	INNER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetUsrGroupLinksByEmailP
  @UsrGroupId int,
  @EmailId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	INNER JOIN EmailUserLink ON EmailUserLink.UserId = [User].Id AND EmailUserLink.EmailId = ' + @EmailId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetUsrGroupLinksByRole
  @UsrGroupId int,
  @RoleId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_User_GetUsrGroupLinksByRoleP
  @UsrGroupId int,
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetUsrGroupLinksP
  @UsrGroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_GetUsrGroupLinksPS
  @UsrGroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UsrGroupId + ' 
	WHERE [User].' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_User_Update
  @Id int,
  @Login varchar(50),
  @Password varbinary(16),
  @FirstName varchar(25),
  @LastName varchar(25),
  @CampusCode varchar(10),
  @UniId varchar(10),
  @ConAgree bit,
  @OnlineAccess bit,
  @ModifiedBy varchar(50)
AS

IF EXISTS (SELECT * FROM [User] WHERE Login = @Login AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE [User]
SET
	Login = @Login,
	Password = @Password,
	FirstName = @FirstName,
	LastName = @LastName,
	CampusCode = @CampusCode,
	UniId = @UniId,
	ConAgree = @ConAgree,
	OnlineAccess = @OnlineAccess,
	Modified = getDate(),
	ModifiedBy = @ModifiedBy
WHERE Id = @Id

RETURN 0CREATE PROCEDURE dbo.a_UserAccountLink_Create
  @UserId int,
  @AccountId int
AS

IF (SELECT COUNT(*) FROM UserAccountLink WHERE UserId = @UserId AND AccountId = @AccountId) > 0
	RETURN -16

INSERT INTO UserAccountLink
(UserId, AccountId)
VALUES (@UserId, @AccountId)

RETURN 0CREATE PROCEDURE dbo.a_UserAccountLink_CreateAllByAccount
  @AccountId int
AS

INSERT INTO UserAccountLink
(UserId, AccountId)
SELECT Id, @AccountId FROM [User] WHERE NOT EXISTS (SELECT * FROM UserAccountLink WHERE UserId = Id AND AccountId = @AccountId)


RETURN 0CREATE PROCEDURE dbo.a_UserAccountLink_CreateAllByUser
  @UserId int
AS

INSERT INTO UserAccountLink
(UserId, AccountId)
SELECT @UserId, Id FROM Account WHERE NOT EXISTS (SELECT * FROM UserAccountLink WHERE UserId = @UserId AND AccountId = Id)


RETURN 0CREATE PROCEDURE dbo.a_UserAccountLink_Delete
  @UserId int,
  @AccountId int
AS

DELETE FROM UserAccountLink
WHERE UserId = @UserId AND AccountId = @AccountId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UserAccountLink_DeleteAllByAccount
  @AccountId int
AS

DELETE FROM UserAccountLink
WHERE AccountId = @AccountId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UserAccountLink_DeleteAllByUser
  @UserId int
AS

DELETE FROM UserAccountLink
WHERE UserId = @UserId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UserLog_Create
  @UserId int
AS

INSERT INTO UserLog
(
	UserId, 
	Created
)
VALUES
(
	@UserId, 
	getDate()
)

SELECT @@IDENTITYCREATE PROCEDURE dbo.a_UserLog_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM UserLog
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UserLog_GetList

AS

SELECT
	UserLog.Id AS UserLog_Id
FROM UserLog
ORDER BY UserLog.Id
CREATE PROCEDURE dbo.a_UserLog_GetRecord
  @Id int
AS

SELECT
	UserLog.Id AS UserLog_Id,
	UserLog.UserId AS UserLog_UserId,
	UserLog.Created AS UserLog_Created,
	[User].LastName + ', ' + [User].FirstName AS User_FullName
FROM UserLog
LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
WHERE UserLog.Id = @IdCREATE PROCEDURE dbo.a_UserLog_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_UserLog_GetRecordsByUserField
  @UserId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	WHERE UserLog.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_UserLog_GetRecordsByUserFieldP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUserLog
(
	TempId int IDENTITY PRIMARY KEY,
	UserLog_Id int,
	UserLog_UserId int,
	UserLog_Created datetime,
	User_FullName varchar(50)
)

INSERT INTO #TempUserLog
(
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
)
EXEC
('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	WHERE UserLog.UserId = ' + @UserId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
FROM #TempUserLog
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UserLog_GetRecordsByUserFieldPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUserLog
(
	TempId int IDENTITY PRIMARY KEY,
	UserLog_Id int,
	UserLog_UserId int,
	UserLog_Created datetime,
	User_FullName varchar(50)
)

INSERT INTO #TempUserLog
(
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
)
EXEC
('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND UserLog.UserId = ' + @UserId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
FROM #TempUserLog
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UserLog_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUserLog
(
	TempId int IDENTITY PRIMARY KEY,
	UserLog_Id int,
	UserLog_UserId int,
	UserLog_Created datetime,
	User_FullName varchar(50)
)

INSERT INTO #TempUserLog
(
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
)
EXEC
('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
FROM #TempUserLog
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UserLog_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUserLog
(
	TempId int IDENTITY PRIMARY KEY,
	UserLog_Id int,
	UserLog_UserId int,
	UserLog_Created datetime,
	User_FullName varchar(50)
)

INSERT INTO #TempUserLog
(
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
)
EXEC
('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
FROM #TempUserLog
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UserRoleLink_Create
  @UserId int,
  @RoleId int
AS

IF (SELECT COUNT(*) FROM UserRoleLink WHERE UserId = @UserId AND RoleId = @RoleId) > 0
	RETURN -16

INSERT INTO UserRoleLink
(UserId, RoleId)
VALUES (@UserId, @RoleId)

RETURN 0CREATE PROCEDURE dbo.a_UserRoleLink_CreateAllByRole
  @RoleId int
AS

INSERT INTO UserRoleLink
(UserId, RoleId)
SELECT Id, @RoleId FROM [User] WHERE NOT EXISTS (SELECT * FROM UserRoleLink WHERE UserId = Id AND RoleId = @RoleId)


RETURN 0CREATE PROCEDURE dbo.a_UserRoleLink_CreateAllByUser
  @UserId int
AS

INSERT INTO UserRoleLink
(UserId, RoleId)
SELECT @UserId, Id FROM [Role] WHERE NOT EXISTS (SELECT * FROM UserRoleLink WHERE UserId = @UserId AND RoleId = Id)


RETURN 0CREATE PROCEDURE dbo.a_UserRoleLink_Delete
  @UserId int,
  @RoleId int
AS

DELETE FROM UserRoleLink
WHERE UserId = @UserId AND RoleId = @RoleId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UserRoleLink_DeleteAllByRole
  @RoleId int
AS

DELETE FROM UserRoleLink
WHERE RoleId = @RoleId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UserRoleLink_DeleteAllByUser
  @UserId int
AS

DELETE FROM UserRoleLink
WHERE UserId = @UserId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UsrGroup_Create
  @Name varchar(50)
AS

IF EXISTS (SELECT * FROM UsrGroup WHERE Name = @Name)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO UsrGroup
(
	Name
)
VALUES
(
	@Name
)

SELECT @@IDENTITYCREATE PROCEDURE dbo.a_UsrGroup_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM UsrGroupUserLink WHERE UsrGroupId = @Id
DELETE FROM UsrGroup
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UsrGroup_GetList

AS

SELECT
	UsrGroup.Id AS UsrGroup_Id,
	UsrGroup.Name AS UsrGroup_Name
FROM UsrGroup
ORDER BY UsrGroup.Name
CREATE PROCEDURE dbo.a_UsrGroup_GetRecord
  @Id int
AS

SELECT
	UsrGroup.Id AS UsrGroup_Id,
	UsrGroup.Name AS UsrGroup_Name
FROM UsrGroup
WHERE UsrGroup.Id = @IdCREATE PROCEDURE dbo.a_UsrGroup_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		UsrGroup.Id AS UsrGroup_Id,
		UsrGroup.Name AS UsrGroup_Name
	FROM UsrGroup
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_UsrGroup_GetRecordsByUserLink
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		UsrGroup.Id AS UsrGroup_Id,
		UsrGroup.Name AS UsrGroup_Name
	FROM UsrGroup
	JOIN UsrGroupUserLink ON UsrGroupUserLink.UsrGroupId = UsrGroup.Id AND UsrGroupUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_UsrGroup_GetRecordsByUserLinkP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUsrGroup
(
	TempId int IDENTITY PRIMARY KEY,
	UsrGroup_Id int,
	UsrGroup_Name varchar(50)
)

INSERT INTO #TempUsrGroup
(
	UsrGroup_Id,
	UsrGroup_Name
)
EXEC
('
	SELECT
		UsrGroup.Id AS UsrGroup_Id,
		UsrGroup.Name AS UsrGroup_Name
	FROM UsrGroup
	JOIN UsrGroupUserLink ON UsrGroupUserLink.UsrGroupId = UsrGroup.Id AND UsrGroupUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UsrGroup_Id,
	UsrGroup_Name
FROM #TempUsrGroup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UsrGroup_GetRecordsByUserLinkPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUsrGroup
(
	TempId int IDENTITY PRIMARY KEY,
	UsrGroup_Id int,
	UsrGroup_Name varchar(50)
)

INSERT INTO #TempUsrGroup
(
	UsrGroup_Id,
	UsrGroup_Name
)
EXEC
('
	SELECT
		UsrGroup.Id AS UsrGroup_Id,
		UsrGroup.Name AS UsrGroup_Name
	FROM UsrGroup
	JOIN UsrGroupUserLink ON UsrGroupUserLink.UsrGroupId = UsrGroup.Id AND UsrGroupUserLink.UserId = ' + @UserId + ' 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UsrGroup_Id,
	UsrGroup_Name
FROM #TempUsrGroup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UsrGroup_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUsrGroup
(
	TempId int IDENTITY PRIMARY KEY,
	UsrGroup_Id int,
	UsrGroup_Name varchar(50)
)

INSERT INTO #TempUsrGroup
(
	UsrGroup_Id,
	UsrGroup_Name
)
EXEC
('
	SELECT
		UsrGroup.Id AS UsrGroup_Id,
		UsrGroup.Name AS UsrGroup_Name
	FROM UsrGroup
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UsrGroup_Id,
	UsrGroup_Name
FROM #TempUsrGroup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UsrGroup_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUsrGroup
(
	TempId int IDENTITY PRIMARY KEY,
	UsrGroup_Id int,
	UsrGroup_Name varchar(50)
)

INSERT INTO #TempUsrGroup
(
	UsrGroup_Id,
	UsrGroup_Name
)
EXEC
('
	SELECT
		UsrGroup.Id AS UsrGroup_Id,
		UsrGroup.Name AS UsrGroup_Name
	FROM UsrGroup
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UsrGroup_Id,
	UsrGroup_Name
FROM #TempUsrGroup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UsrGroup_GetUserLinks
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		UsrGroup.Id AS UsrGroup_Id,
		UsrGroup.Name AS UsrGroup_Name,
		CAST (ISNULL(UsrGroupUserLink.UserId, 0) as bit) AS Selected
	FROM UsrGroup
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UsrGroupId = UsrGroup.Id AND UsrGroupUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)CREATE PROCEDURE dbo.a_UsrGroup_GetUserLinksP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUsrGroup
(
	TempId int IDENTITY PRIMARY KEY,
	UsrGroup_Id int,
	UsrGroup_Name varchar(50),
	selected bit
)

INSERT INTO #TempUsrGroup
(
	UsrGroup_Id,
	UsrGroup_Name,
	selected
)
EXEC
('
	SELECT
		UsrGroup.Id AS UsrGroup_Id,
		UsrGroup.Name AS UsrGroup_Name,
		CAST (ISNULL(UsrGroupUserLink.UserId, 0) as bit) AS Selected
	FROM UsrGroup
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UsrGroupId = UsrGroup.Id AND UsrGroupUserLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UsrGroup_Id,
	UsrGroup_Name,
	selected
FROM #TempUsrGroup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UsrGroup_GetUserLinksPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS

CREATE TABLE #TempUsrGroup
(
	TempId int IDENTITY PRIMARY KEY,
	UsrGroup_Id int,
	UsrGroup_Name varchar(50),
	selected bit
)

INSERT INTO #TempUsrGroup
(
	UsrGroup_Id,
	UsrGroup_Name,
	selected
)
EXEC
('
	SELECT
		UsrGroup.Id AS UsrGroup_Id,
		UsrGroup.Name AS UsrGroup_Name,
		CAST (ISNULL(UsrGroupUserLink.UserId, 0) as bit) AS Selected
	FROM UsrGroup
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UsrGroupId = UsrGroup.Id AND UsrGroupUserLink.UserId = ' + @UserId + ' 
	WHERE UsrGroup.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UsrGroup_Id,
	UsrGroup_Name,
	selected
FROM #TempUsrGroup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.a_UsrGroup_Update
  @Id int,
  @Name varchar(50)
AS

IF EXISTS (SELECT * FROM UsrGroup WHERE Name = @Name AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE UsrGroup
SET
	Name = @Name
WHERE Id = @Id

RETURN 0CREATE PROCEDURE dbo.a_UsrGroupUserLink_Create
  @UsrGroupId int,
  @UserId int
AS

IF (SELECT COUNT(*) FROM UsrGroupUserLink WHERE UsrGroupId = @UsrGroupId AND UserId = @UserId) > 0
	RETURN -16

INSERT INTO UsrGroupUserLink
(UsrGroupId, UserId)
VALUES (@UsrGroupId, @UserId)

RETURN 0CREATE PROCEDURE dbo.a_UsrGroupUserLink_CreateAllByUser
  @UserId int
AS

INSERT INTO UsrGroupUserLink
(UsrGroupId, UserId)
SELECT Id, @UserId FROM UsrGroup WHERE NOT EXISTS (SELECT * FROM UsrGroupUserLink WHERE UsrGroupId = Id AND UserId = @UserId)


RETURN 0CREATE PROCEDURE dbo.a_UsrGroupUserLink_CreateAllByUsrGroup
  @UsrGroupId int
AS

INSERT INTO UsrGroupUserLink
(UsrGroupId, UserId)
SELECT @UsrGroupId, Id FROM [User] WHERE NOT EXISTS (SELECT * FROM UsrGroupUserLink WHERE UsrGroupId = @UsrGroupId AND UserId = Id)


RETURN 0CREATE PROCEDURE dbo.a_UsrGroupUserLink_Delete
  @UsrGroupId int,
  @UserId int
AS

DELETE FROM UsrGroupUserLink
WHERE UsrGroupId = @UsrGroupId AND UserId = @UserId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UsrGroupUserLink_DeleteAllByUser
  @UserId int
AS

DELETE FROM UsrGroupUserLink
WHERE UserId = @UserId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.a_UsrGroupUserLink_DeleteAllByUsrGroup
  @UsrGroupId int
AS

DELETE FROM UsrGroupUserLink
WHERE UsrGroupId = @UsrGroupId

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.Account_GetAccCategoryLinksWithQuery
  @UserId int,
  @Query varchar(1000),
  @AccCategoryId int
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(AccCategoryAccountLink.AccCategoryId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	' + @Query
)CREATE PROCEDURE dbo.Account_GetAccCategoryLinksWithQueryP
  @UserId int,
  @Query varchar(1000),
  @AccCategoryId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(AccCategoryAccountLink.AccCategoryId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN AccCategoryAccountLink ON AccCategoryAccountLink.AccountId = Account.Id AND AccCategoryAccountLink.AccCategoryId = ' + @AccCategoryId + ' 
	INNER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	' + @Query + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetAdminUnits

AS

SELECT DISTINCT	
	AdminUnit, 
	AdminUnit + ' - ' + AdminUnitDesc AS Description
FROM Account
ORDER BY AdminUnitCREATE PROCEDURE dbo.Account_GetAlumni
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription
FROM Account
WHERE Account.AccountType = 'A'CREATE PROCEDURE dbo.Account_GetAlumniByActiveStatus

AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription
FROM Account
WHERE Account.AcctStat = 'D' AND Account.AccountType = 'A'CREATE PROCEDURE dbo.Account_GetAlumniByActiveStatusP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription
	FROM Account
	WHERE Account.AcctStat = ''D'' AND Account.AccountType = ''A''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetAlumniP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription
	FROM Account
	WHERE Account.AccountType = ''A''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetAlumniSummary
@AccountNumber varchar(10),
@StartDate datetime,
@EndDate datetime

AS

select 
sum(case when [A_obj_cat]+[A_Object]='0300' and [Post Date]=@StartDate then -[Actual Post Amount_GLTran] else 0 end) - 
sum(case when [A_obj_cat]+[A_Object]='0175' and [Post Date]=@StartDate and [Tran Type]='C' then [Actual Post Amount_GLTran] else 0 end) as Beginning_Cash_Balance, 
sum(case when [A_obj_cat]+[A_Object]<>'3792' and [Tran Type]<>'X' and [A_Sys Attr]='RE' then -[Actual Post Amount_GLTran] else 0 end) as Total_Revenue, 
sum(case when [A_obj_cat]+[A_Object]<>'3792' and [Tran Type]<>'X' and [A_Sys Attr]='EX' then [Actual Post Amount_GLTran] else 0 end) as Total_Expense,
(sum(case when [A_obj_cat]+[A_Object]='0300' and [Post Date]=@StartDate then -[Actual Post Amount_GLTran] else 0 end) -
sum(case when [A_obj_cat]+[A_Object]='0175' and [Post Date]=@StartDate and [Tran Type]='C' then [Actual Post Amount_GLTran] else 0 end))+ 
sum(case when [A_obj_cat]+[A_Object]<>'3792' and [Tran Type]<>'X' and [A_Sys Attr]='RE' then -[Actual Post Amount_GLTran] else 0 end)-
sum(case when [A_obj_cat]+[A_Object]<>'3792' and [Tran Type]<>'X' and [A_Sys Attr]='EX' then [Actual Post Amount_GLTran] else 0 end)as Ending_Cash_Balance
from idatadiv03.dbo.vwGLTransaction
where
[A_Fund]+ [A_event]=@AccountNumber
and
[Post Date] between @StartDate and @EndDate
group by [A_Fund]+[A_event]CREATE PROCEDURE dbo.Account_GetByActiveStatus
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription
FROM Account
WHERE Account.AcctStat = 'D'CREATE PROCEDURE dbo.Account_GetByActiveStatusP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription
	FROM Account
	WHERE Account.AcctStat = ''D''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetByAdminUnit
  @AdminUnit varchar(60)
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription
FROM Account
WHERE Account.AdminUnit = @AdminUnitCREATE PROCEDURE dbo.Account_GetByAdminUnitP
  @AdminUnit varchar(60),
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription
	FROM Account
	WHERE Account.AdminUnit = ''' + @AdminUnit + '''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE procedure Account_GetEndowmentAccounts as
select EndowmentAccount as Account_AccountNumber
from Uni6Data.dbo.EndowmentSpendingJoin
order by EndowmentAccountCREATE PROCEDURE dbo.Account_GetExpenses
@Account varchar (20),
@Start datetime,
@End datetime,
@Column int,
@DonorPayee varchar (50),
@Description varchar (50)

as


if (@Column = 0)
	select  [F_Account Number] as Account, [Payee_DI_GLTran] as DonorPayee, [Transaction Description] as Description, [Post Date] as Date, [Actual Post Amount_GLTran] as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'
	order by  Account, Date desc, DonorPayee

else if (@Column = 1)
	select [F_Account Number] as Account, [Payee_DI_GLTran] as DonorPayee, [Transaction Description] as Description, [Post Date] as Date, [Actual Post Amount_GLTran] as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'
	order by DonorPayee, Date desc

else if (@Column = 2)
	select [F_Account Number] as Account, [Payee_DI_GLTran] as DonorPayee, [Transaction Description] as Description, [Post Date] as Date, [Actual Post Amount_GLTran] as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'
	order by Description, Date desc, DonorPayee 

else if (@Column = 3)
	select [F_Account Number] as Account, [Payee_DI_GLTran] as DonorPayee, [Transaction Description] as Description, [Post Date] as Date, [Actual Post Amount_GLTran] as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'
	order by Date asc, Account, DonorPayee

else if (@Column = 4)
	select [F_Account Number] as Account, [Payee_DI_GLTran] as DonorPayee, [Transaction Description] as Description, [Post Date] as Date, [Actual Post Amount_GLTran] as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'
	order by Amount, Date desc, DonorPayee

else
	select [F_Account Number] as Account, [Payee_DI_GLTran] as DonorPayee, [Transaction Description] as Description, [Post Date] as Date, [Actual Post Amount_GLTran] as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'
	order by  Account, Date desc, DonorPayee


select @@rowcount as Count


if (@Column = 1)
	select [Payee_DI_GLTran] as ColumnValue, sum([Actual Post Amount_GLTran]) as SubTotal, count([Payee_DI_GLTran]) as GroupCounter
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' CREATE PROCEDURE dbo.Account_GetFoundation
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription
FROM Account
WHERE Account.AccountType = 'F'CREATE PROCEDURE dbo.Account_GetFoundationByActiveStatus

AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription
FROM Account
WHERE Account.AcctStat = 'D' AND Account.AccountType = 'F'CREATE PROCEDURE dbo.Account_GetFoundationByActiveStatusP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription
	FROM Account
	WHERE Account.AcctStat = ''D'' AND Account.AccountType = ''F''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetFoundationP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription
	FROM Account
	WHERE Account.AccountType = ''F''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetListOfAllAccounts

AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountType AS Account_AccountType,
	Account.FullAccountNumber +  ' --- ' + Account.AccountDescription AS Account_NumberAndDescription
FROM Account 
ORDER BY Account.FullAccountNumber

--can be used in any other app - to puopulate account listsCREATE PROCEDURE dbo.Account_GetMasterAccountLinksWithQueryP
  @Query varchar(1000),
  @MasterAccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(MasterAccountAccountLink.MasterAccountId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN MasterAccountAccountLink ON MasterAccountAccountLink.LinkedAccountId = Account.Id AND MasterAccountAccountLink.MasterAccountId = ' + @MasterAccountId + ' 
	' + @Query + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetNonMasterAccounts
AS

SELECT
	a.Id AS Account_Id,
	a.FullAccountNumber AS Account_FullAccountNumber
FROM Account a
WHERE NOT EXISTS (SELECT m.AccountId FROM MasterAccount m where m.AccountId = a.Id)CREATE PROCEDURE dbo.Account_GetRevenues
@Account varchar (20),
@Start datetime,
@End datetime,
@Column int,
@DonorPayee varchar (50),
@Description varchar (50)

as


if (@Column = 0)
	select [F_Account Number] as Account, [Transaction Description] as DonorPayee, [Trans  Desc _DI_GLTran] as Description, [Post Date] as Date, -([Actual Post Amount_GLTran]) as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'
	order by  [F_Account Number], [Post Date] desc,  [Transaction Description]

else if (@Column = 1)
	select [F_Account Number] as Account, [Transaction Description] as DonorPayee, [Trans  Desc _DI_GLTran] as Description, [Post Date] as Date, -([Actual Post Amount_GLTran]) as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'
	order by  [Transaction Description], [Post Date] desc

else if (@Column = 2)
	select [F_Account Number] as Account, [Transaction Description] as DonorPayee, [Trans  Desc _DI_GLTran] as Description, [Post Date] as Date, -([Actual Post Amount_GLTran]) as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'
	order by [Trans  Desc _DI_GLTran], [Post Date] desc,  [Transaction Description] 

else if (@Column = 3)
	select [F_Account Number] as Account, [Transaction Description] as DonorPayee, [Trans  Desc _DI_GLTran] as Description, [Post Date] as Date, -([Actual Post Amount_GLTran]) as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'
	order by [Post Date] asc, [F_Account Number],  [Transaction Description]

else if (@Column = 4)
	select [F_Account Number] as Account, [Transaction Description] as DonorPayee, [Trans  Desc _DI_GLTran] as Description, [Post Date] as Date, -([Actual Post Amount_GLTran]) as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'
	order by -([Actual Post Amount_GLTran]), [Post Date] desc,  [Transaction Description]

else
	select [F_Account Number] as Account, [Transaction Description] as DonorPayee, [Trans  Desc _DI_GLTran] as Description, [Post Date] as Date, -([Actual Post Amount_GLTran]) as Amount
	from idatadiv02.dbo.vwGLTransaction 
	where ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'
	order by  [F_Account Number], [Post Date] desc,  [Transaction Description]


select @@rowcount as Count


if (@Column = 1)
	select top 100000CREATE PROCEDURE dbo.Account_GetSummary
@AccountNumber varchar(10),
@StartDate datetime,
@EndDate datetime

AS

select 
sum(case when [F_Obj__Cat_]+[F_Object]='3500' and [Post Date]=@StartDate then -[Actual Post Amount_GLTran] else 0 end) - 
sum(case when [F_Obj__Cat_]+[F_Object]='1030' and [Post Date]=@StartDate and [Tran Type]='C' then [Actual Post Amount_GLTran] else 0 end) as Beginning_Cash_Balance, 
sum(case when [F_Obj__Cat_]+[F_Object]<>'3550' and [Tran Type]<>'X' and [F_Sys Attr]='RE' then -[Actual Post Amount_GLTran] else 0 end) as Total_Revenue, 
sum(case when [F_Obj__Cat_]+[F_Object]<>'3550' and [Tran Type]<>'X' and [F_Sys Attr]='EX' then [Actual Post Amount_GLTran] else 0 end) as Total_Expense,
(sum(case when [F_Obj__Cat_]+[F_Object]='3500' and [Post Date]=@StartDate then -[Actual Post Amount_GLTran] else 0 end) -
sum(case when [F_Obj__Cat_]+[F_Object]='1030' and [Post Date]=@StartDate and [Tran Type]='C' then [Actual Post Amount_GLTran] else 0 end))+ 
sum(case when [F_Obj__Cat_]+[F_Object]<>'3550' and [Tran Type]<>'X' and [F_Sys Attr]='RE' then -[Actual Post Amount_GLTran] else 0 end)-
sum(case when [F_Obj__Cat_]+[F_Object]<>'3550' and [Tran Type]<>'X' and [F_Sys Attr]='EX' then [Actual Post Amount_GLTran] else 0 end)as Ending_Cash_Balance
from idatadiv02.dbo.vwGLTransaction
where
[F_Fund]+[F_Acct]=@AccountNumber
and
[Post Date] between @StartDate and @EndDate
group by [F_Fund]+[F_Acct]CREATE PROCEDURE dbo.Account_GetUserLinksAlumni
  @UserId int
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription,
	CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
FROM Account
LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = @UserId
WHERE Account.AccountType = 'A'CREATE PROCEDURE dbo.Account_GetUserLinksAlumniByActiveStatus
  @UserId int
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription,
	CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
FROM Account
LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = @UserId
WHERE Account.AcctStat = 'D' AND Account.AccountType = 'A'CREATE PROCEDURE dbo.Account_GetUserLinksAlumniByActiveStatusP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	WHERE Account.AcctStat = ''D'' AND Account.AccountType = ''A''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetUserLinksAlumniP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	WHERE Account.AccountType = ''A''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetUserLinksByActiveStatus
  @UserId int
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription,
	CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
FROM Account
LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = @UserId
WHERE Account.AcctStat = 'D'CREATE PROCEDURE dbo.Account_GetUserLinksByActiveStatusP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	WHERE Account.AcctStat = ''D''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetUserLinksByAdminUnit
  @UserId int,
  @AdminUnit varchar(60)
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription,
	CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
FROM Account
LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = @UserId
WHERE Account.AdminUnit = @AdminUnitCREATE PROCEDURE dbo.Account_GetUserLinksByAdminUnitP
  @UserId int,
  @AdminUnit varchar(60),
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	WHERE Account.AdminUnit = ''' + @AdminUnit + '''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetUserLinksFoundation
  @UserId int
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription,
	CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
FROM Account
LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = @UserId
WHERE Account.AccountType = 'F'CREATE PROCEDURE dbo.Account_GetUserLinksFoundationByActiveStatus
  @UserId int
AS

SELECT
	Account.Id AS Account_Id,
	Account.AccountNumber AS Account_AccountNumber,
	Account.FullAccountNumber AS Account_FullAccountNumber,
	Account.AccountDescription AS Account_AccountDescription,
	CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
FROM Account
LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = @UserId
WHERE Account.AcctStat = 'D' AND Account.AccountType = 'F'CREATE PROCEDURE dbo.Account_GetUserLinksFoundationByActiveStatusP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	WHERE Account.AcctStat = ''D'' AND Account.AccountType = ''F''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetUserLinksFoundationP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	WHERE Account.AccountType = ''F''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetUserLinksWithQuery
  @Query varchar(1000),
  @UserId int
AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	' + @Query
)
CREATE PROCEDURE dbo.Account_GetUserLinksWithQueryP
  @Query varchar(1000),
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	selected bit
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		CAST (ISNULL(UserAccountLink.UserId, 0) as bit) AS Selected
	FROM Account
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.AccountId = Account.Id AND UserAccountLink.UserId = ' + @UserId + ' 
	' + @Query + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	selected
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Account_GetUsersDepartments
  @AccountId int
AS

SELECT 
	u.Id, 
	u.LastName + ', ' + u.FirstName AS FullName, 
	d.Name AS DepartmentName
FROM [User] u
JOIN UserAccountLink ua ON u.Id = ua.UserId AND ua.AccountId = @AccountId
LEFT OUTER JOIN DepartmentUserLink du ON u.Id = du.UserId
LEFT OUTER JOIN Department d on d.Id = du.DepartmentId        			--JOIN Department d on d.Id = du.DepartmentId  changed to left outer join on 1/30/07 to fix elimination of people without a department.
ORDER BY u.LastName

SELECT COUNT(*)
FROM [User] u
JOIN UserAccountLink ua ON u.Id = ua.UserId AND ua.AccountId = @AccountIdCREATE PROCEDURE dbo.Account_GetWithQuery
  @Query varchar(1000)

AS

EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy,
		Account.FullAccountNumber +  '' --- '' + Account.AccountDescription AS Account_NumberAndDescription
	FROM Account '
	 + @Query  
	+ ' order by Account.AccountNumber' -- line added on 03/30/2007 to order Account # drop down list box by Account #
)CREATE PROCEDURE dbo.Account_GetWithQueryP
  @Query varchar(1000),
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int

AS

CREATE TABLE #TempAccount
(
	TempId int IDENTITY PRIMARY KEY,
	Account_Id int,
	Account_AccountType char(1),
	Account_AccountNumber varchar(20),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempAccount
(
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		Account.Id AS Account_Id,
		Account.AccountType AS Account_AccountType,
		Account.AccountNumber AS Account_AccountNumber,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM Account '
	 + @Query + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Account_Id,
	Account_AccountType,
	Account_AccountNumber,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.AccountsPayout_GetPayout
@AccountNumber varchar(10),
@StartDate datetime,
@EndDate datetime

AS

DECLARE @payout real


SET @payout = 
(
	SELECT SUM(Payout)
	FROM dbo.Accounts_Payout
	WHERE AccountNumber = @AccountNumber and PayoutDate between @StartDate and @EndDate
)

IF @payout = NULL
	SELECT 0
ELSE
	SELECT @payoutCREATE PROCEDURE dbo.AdminUnits_GetList
AS

SELECT
	1 as ID,
	Uni6Data.dbo.[Accounts_Admin_Units].AdminCode AS AdminCode,
	Uni6Data.dbo.[Accounts_Admin_Units].AdminDescription AS AdminDescription
FROM Uni6Data.dbo.[Accounts_Admin_Units]
ORDER BY Uni6Data.dbo.[Accounts_Admin_Units].AdminCodeCREATE PROCEDURE AdminUnits_GetRecords 
AS
SELECT
1 as ID,Uni6Data.dbo.[Accounts_Admin_Units].AdminCode as AdminCode,Uni6Data.dbo.[Accounts_Admin_Units].AdminDescription
FROM Uni6Data.dbo.[Accounts_Admin_Units]
ORDER BY Uni6Data.dbo.[Accounts_Admin_Units].AdminCodeCREATE PROCEDURE dbo.GeneralUse_GetList

AS

SELECT
	1 as Id,
	Uni6Data.dbo.[Accounts_General_Use].GeneralUse AS GeneralUse
FROM Uni6Data.dbo.[Accounts_General_Use]
ORDER BY GeneralUseCREATE PROCEDURE GeneralUse_GetRecords 
AS
SELECT
1 as Id,
Uni6Data.dbo.Accounts_General_Use.GeneralUse
FROM Uni6Data.dbo.Accounts_General_Use
ORDER BY GeneralUseCREATE PROCEDURE dbo.MasterAccount_GetIdByAccount
  @AccountId int

AS

DECLARE @Id int

SET @Id = (SELECT Id 
FROM MasterAccount
WHERE AccountId = @AccountId)

if @Id = null
	SELECT -1 AS Id
else
	SELECT @Id AS IdCREATE PROCEDURE dbo.MasterAccount_GetWithQueryP
  @Query varchar(1000),
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempMasterAccount
(
	TempId int IDENTITY PRIMARY KEY,
	MasterAccount_Id int,
	MasterAccount_AccountId int,
	Account_AccountType char(1),
	Account_FullAccountNumber varchar(60),
	Account_AccountDescription varchar(60),
	Account_AcctStat varchar(60),
	Account_AdminUnit varchar(60),
	Account_Modified datetime,
	Account_ModifiedBy varchar(60)
)

INSERT INTO #TempMasterAccount
(
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
)
EXEC
('
	SELECT
		MasterAccount.Id AS MasterAccount_Id,
		MasterAccount.AccountId AS MasterAccount_AccountId,
		Account.AccountType AS Account_AccountType,
		Account.FullAccountNumber AS Account_FullAccountNumber,
		Account.AccountDescription AS Account_AccountDescription,
		Account.AcctStat AS Account_AcctStat,
		Account.AdminUnit AS Account_AdminUnit,
		Account.Modified AS Account_Modified,
		Account.ModifiedBy AS Account_ModifiedBy
	FROM MasterAccount
	LEFT OUTER JOIN Account ON MasterAccount.AccountId = Account.Id 
	 ' + @Query + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	MasterAccount_Id,
	MasterAccount_AccountId,
	Account_AccountType,
	Account_FullAccountNumber,
	Account_AccountDescription,
	Account_AcctStat,
	Account_AdminUnit,
	Account_Modified,
	Account_ModifiedBy
FROM #TempMasterAccount
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.Permission_GetAllRecordsByPermCatByRole
  @CategoryId int,
  @RoleId int
AS

SELECT 
	p.Id, 
	p.Description,
	CAST (ISNULL(rp.RoleId, 0) as bit) AS Selected
FROM Permission p
LEFT OUTER JOIN RolePermissionLink rp ON rp.PermissionId = p.Id AND rp.RoleId = @RoleId
WHERE p.CategoryId = @CategoryId
ORDER BY p.Rank, p.DescriptionCREATE PROCEDURE dbo.Permission_GetPermissionCodesByUser
@UserId int
AS
select distinct p.Id
from Permission p
inner join RolePermissionLink rp on p.Id = rp.PermissionId
inner join UserRoleLink ur on ur.RoleId = rp.RoleId
where ur.UserId = @UserIdCREATE PROCEDURE dbo.Transaction_GetAlumniExpensesGroupByAccountP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,AccountNumber, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY AccountNumber

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount , Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY AccountNumber, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records 
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetAlumniExpensesGroupByDateP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, null, null, TransDate, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDate

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransDate, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetAlumniExpensesGroupByDescriptionP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, null, TransDescription, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransDescription, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetAlumniExpensesGroupByDonorPayeeP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, DonorPayee, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY DonorPayee

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY DonorPayee, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetAlumniExpensesSortByAmountP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records 
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransAmount, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetAlumniRevenuesGroupByAccountP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,AccountNumber, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY AccountNumber

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY AccountNumber, Sort 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetAlumniRevenuesGroupByDateP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, null, null, TransDate, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDate

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransDate, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetAlumniRevenuesGroupByDescriptionP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, null, TransDescription, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransDescription, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetAlumniRevenuesGroupByDonorPayeeP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, DonorPayee, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY DonorPayee

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY DonorPayee, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetAlumniRevenuesSortByAmountP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[A_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv03.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [A_Account Number] like @Account + '%'  
	and not  [A_Account Number] like '%37 92' and [A_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransAmount, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetExpensesGroupByAccountP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,AccountNumber, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY AccountNumber

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount , Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY AccountNumber, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records 
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetExpensesGroupByDateP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, null, null, TransDate, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDate

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransDate, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetExpensesGroupByDescriptionP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, null, TransDescription, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransDescription, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetExpensesGroupByDonorPayeeP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, DonorPayee, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY DonorPayee

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY DonorPayee, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetExpensesSortByAmountP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number],
	[Payee_DI_GLTran],
	[Transaction Description],
	[Post Date],
	[Actual Post Amount_GLTran],
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'EX' and not [Tran Type] = 'X'
	and [Payee_DI_GLTran] like '%' + @DonorPayee + '%' and [Transaction Description] like '%' + @Description + '%'

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records 
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransAmount, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetRevenuesGroupByAccountP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,AccountNumber, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY AccountNumber

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY AccountNumber, Sort 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetRevenuesGroupByDateP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, null, null, TransDate, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDate

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransDate, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetRevenuesGroupByDescriptionP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, null, TransDescription, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransDescription, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetRevenuesGroupByDonorPayeeP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1,null, DonorPayee, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY DonorPayee

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY DonorPayee, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.Transaction_GetRevenuesSortByAmountP
@Account varchar (20),
@Start datetime,
@End datetime,
@DonorPayee varchar(50),
@Description varchar(50),
@PageSize int,
@PageNum int

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	[F_Account Number], 
	[Transaction Description], 
	[Trans  Desc _DI_GLTran],
	[Post Date], 
	-([Actual Post Amount_GLTran]),
	0
FROM idatadiv02.dbo.vwGLTransaction 
WHERE ([Post Date] between @Start and @End) and [F_Account Number] like @Account + '%'  
	and not  [F_Account Number] like '%35 50' and [F_Sys Attr] = 'RE' and not [Tran Type] = 'X'
	and [Transaction Description] like '%' + @DonorPayee + '%' and [Trans  Desc _DI_GLTran] like '%' + @Description + '%'

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	AccountNumber varchar(20),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY TransAmount, Sort

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, AccountNumber, DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.User_CopyUserAccounts
  @SourceUserId int,
  @DestinationUserId int
AS

INSERT INTO UserAccountLink
(UserId, AccountId)
SELECT @DestinationUserId, Id FROM Account
WHERE NOT EXISTS (SELECT * FROM UserAccountLink WHERE UserId = @DestinationUserId AND AccountId = Id)
AND EXISTS (SELECT * FROM UserAccountLink WHERE UserId = @SourceUserId AND AccountId = Id)

SELECT @@ROWCOUNTCREATE PROCEDURE dbo.User_DeleteWithDependents
  @Id int

AS

DELETE FROM AccCategoryAccountLink WHERE EXISTS
(
	SELECT AccCategoryId FROM AccCategoryAccountLink JOIN AccCategory ON AccCategory.Id = AccCategoryAccountLink.AccCategoryId AND AccCategory.UserId = @Id
)


DELETE FROM AccCategory WHERE UserId = @Id
DELETE FROM UserLog WHERE UserId = @Id

DELETE FROM UserRoleLink WHERE UserId = @Id
DELETE FROM DepartmentUserLink WHERE UserId = @Id
DELETE FROM UserAccountLink WHERE UserId = @Id
DELETE FROM EmailUserLink WHERE UserId = @Id
DELETE FROM UsrGroupUserLink WHERE UserId = @Id
DELETE FROM [User]
WHERE Id = @Id

RETURN @@ROWCOUNTCREATE PROCEDURE dbo.User_GetAccountLinksWithQuery
  @Query varchar(1000),
  @AccountId int
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	' + @Query

)CREATE PROCEDURE dbo.User_GetAccountLinksWithQueryP
  @Query varchar(1000),
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserAccountLink.AccountId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserAccountLink ON UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	' + @Query + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.User_GetByCollege
  @CollegeId int
AS

SELECT DISTINCT 
	u.Id,
	u.LastName + ', ' + u.FirstName as FullName
FROM [User] u
JOIN DepartmentUserLink du on du.UserId = u.Id
JOIN Department d on du.Departmentid = d.Id
WHERE d.CollegeId = @CollegeIdCREATE PROCEDURE dbo.User_GetDepartmentLinksWithQuery
  @Query varchar(1000),
  @DepartmentId int
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	' + @Query

)CREATE PROCEDURE dbo.User_GetDepartmentLinksWithQueryP
  @Query varchar(1000),
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(DepartmentUserLink.DepartmentId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN DepartmentUserLink ON DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	' + @Query + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.User_GetRoleLinksWithQuery
  @Query varchar(1000),
  @RoleId int
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.Roleid = ' + @RoleId + ' 
	' + @Query

)CREATE PROCEDURE dbo.User_GetRoleLinksWithQueryP
  @Query varchar(1000),
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserRoleLink.RoleId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.Roleid = ' + @RoleId + ' 
	' + @Query + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.User_GetUserGroupLinksWithQuery
  @Query varchar(1000),
  @UserGroupId int
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UserGroupId + ' 
	' + @Query

)CREATE PROCEDURE dbo.User_GetUserGroupLinksWithQueryP
  @Query varchar(1000),
  @UserGroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUser
(
	TempId int IDENTITY PRIMARY KEY,
	User_Id int,
	User_FullName varchar(50),
	selected bit
)

INSERT INTO #TempUser
(
	User_Id,
	User_FullName,
	selected
)
EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UsrGroupUserLink.UsrGroupId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UsrGroupUserLink ON UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @UserGroupId + ' 
	' + @Query + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	User_Id,
	User_FullName,
	selected
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.User_GetUserId
  @Login varchar(50)
AS

DECLARE @id int
SET @id = (SELECT Id FROM [User] WHERE Login = @Login)

IF @id > 0
	SELECT @id
ELSE
	SELECT -1CREATE PROCEDURE dbo.User_LogUser
  @UserId int

AS

INSERT INTO UserLog
VALUES(@UserId, getDate())CREATE PROCEDURE dbo.User_ReplaceUserAccounts
  @SourceUserId int,
  @DestinationUserId int
AS

DELETE FROM UserAccountLink WHERE UserId = @DestinationUserId

INSERT INTO UserAccountLink
(UserId, AccountId)
SELECT @DestinationUserId, Id FROM Account
WHERE EXISTS (SELECT * FROM UserAccountLink WHERE UserId = @SourceUserId AND AccountId = Id)

SELECT @@ROWCOUNTCREATE PROCEDURE dbo.User_ValidateUser
@Login varchar(50),
@Password varbinary(16)
AS
declare @UserId int
set @UserId = (select  Id FROM [User] WHERE Login = @Login and Password = @Password)
if @UserId = null
	select 0
else
	select  @UserIdCREATE PROCEDURE dbo.UserLog_GetRecordsByAccountP
  @AccountId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUserLog
(
	TempId int IDENTITY PRIMARY KEY,
	UserLog_Id int,
	UserLog_UserId int,
	UserLog_Created datetime,
	User_FullName varchar(50)
)

INSERT INTO #TempUserLog
(
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
)
EXEC
('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	JOIN UserAccountLink on UserAccountLink.UserId = [User].Id AND UserAccountLink.AccountId = ' + @AccountId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
FROM #TempUserLog
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.UserLog_GetRecordsByDepartmentP
  @DepartmentId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUserLog
(
	TempId int IDENTITY PRIMARY KEY,
	UserLog_Id int,
	UserLog_UserId int,
	UserLog_Created datetime,
	User_FullName varchar(50)
)

INSERT INTO #TempUserLog
(
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
)
EXEC
('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	JOIN DepartmentUserLink on DepartmentUserLink.UserId = [User].Id AND DepartmentUserLink.DepartmentId = ' + @DepartmentId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
FROM #TempUserLog
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.UserLog_GetRecordsByGroupP
  @GroupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUserLog
(
	TempId int IDENTITY PRIMARY KEY,
	UserLog_Id int,
	UserLog_UserId int,
	UserLog_Created datetime,
	User_FullName varchar(50)
)

INSERT INTO #TempUserLog
(
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
)
EXEC
('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	JOIN UsrGroupUserLink on UsrGroupUserLink.UserId = [User].Id AND UsrGroupUserLink.UsrGroupId = ' + @GroupId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
FROM #TempUserLog
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.UserLog_GetRecordsByRoleP
  @RoleId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS

CREATE TABLE #TempUserLog
(
	TempId int IDENTITY PRIMARY KEY,
	UserLog_Id int,
	UserLog_UserId int,
	UserLog_Created datetime,
	User_FullName varchar(50)
)

INSERT INTO #TempUserLog
(
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
)
EXEC
('
	SELECT
		UserLog.Id AS UserLog_Id,
		UserLog.UserId AS UserLog_UserId,
		UserLog.Created AS UserLog_Created,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM UserLog
	LEFT OUTER JOIN [User] ON UserLog.UserId = [User].Id 
	JOIN UserRoleLink on UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	UserLog_Id,
	UserLog_UserId,
	UserLog_Created,
	User_FullName
FROM #TempUserLog
WHERE TempId >= @first AND TempId <= @last 

SELECT @rowsCREATE PROCEDURE dbo.w_AccountSummary_GetSummary
@AccountId int,
@StartDate datetime,
@EndDate datetime

AS

select  BeginningBalance, TotalRevenues, TotalExpenses, EndingBalance, AccountNumber, CreatedDate, AccountDescription, Guidelines, RespPerson1, RespPerson2, RespPerson3
from uni6data.dbo.AccountSummary
where AccountId = @AccountId
and StartDate = @StartDate
and EndDate = @EndDateCREATE PROCEDURE dbo.w_Payout_GetPayoutDate
@AccountId int,
@StartDate datetime,
@EndDate datetime

AS

select  PayoutDate
from uni6data.dbo.Payout
where AccountId = @AccountId and PayoutDate between @StartDate and @EndDateCREATE PROCEDURE dbo.w_Payout_GetPayouts
@AccountId int,
@StartDate datetime,
@EndDate datetime

AS

select  PayoutAmount
from uni6data.dbo.Payout
where AccountId = @AccountId and PayoutDate between @StartDate and @EndDateCREATE PROCEDURE dbo.w_Payout_GetSummary
@AccountId int,
@StartDate datetime,
@EndDate datetime

AS

select  *
from uni6data.dbo.Payout
where AccountId = @AccountId and PayoutDate between @StartDate and @EndDateCREATE PROCEDURE dbo.w_Transaction_GetExpensesGroupByAccount
@AccountId int, 
@Start smalldatetime,
@End smalldatetime

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	Payee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int,
	ObjectCodeDescription varchar(60)
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Payee, 
	TransDescription,
	TransDate,
	TransAmount,
	0,
	ObjectCodeDescription
FROM Uni6data.dbo.AccountExpense
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId

--insert subtotals
INSERT INTO #Temp1
SELECT 1, ObjectCode, 'TOTAL for ' + ObjectCode + ' (' + ObjectCodeDescription + ')' , null, null, sum(TransAmount), count(Sort), ObjectCodeDescription
FROM #Temp1
GROUP BY ObjectCode, ObjectCodeDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, 'TOTAL for account #' + substring(str(@AccountId, 7), 2, 6), null, null, sum(TransAmount), count(Sort), null
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	Payee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, ObjectCode, Payee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY ObjectCode, Sort, TransDate desc, TransAmount desc 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  Payee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

SELECT * FROM #Temp2CREATE PROCEDURE dbo.w_Transaction_GetExpensesGroupByAccountP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int,
	ObjectCodeDescription varchar(60)
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Payee, 
	TransDescription,
	TransDate,
	TransAmount,
	0,
	ObjectCodeDescription
FROM Uni6data.dbo.AccountExpense
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Payee like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1, ObjectCode, ObjectCodeDescription, null, null, sum(TransAmount), count(Sort), null
FROM #Temp1
GROUP BY ObjectCode, ObjectCodeDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort), null
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY ObjectCode, Sort, TransDate desc, TransAmount desc 
else
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY ObjectCode desc, Sort, TransDate desc, TransAmount desc 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.w_Transaction_GetExpensesGroupByDateP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Payee, 
	TransDescription,
	TransDate,
	TransAmount,
	0
FROM Uni6data.dbo.AccountExpense
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Payee like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1, null, null, null, TransDate, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDate

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransDate desc, Sort, TransAmount desc 
else
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransDate asc, Sort, TransAmount desc 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.w_Transaction_GetExpensesGroupByDescriptionP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Payee, 
	TransDescription,
	TransDate,
	TransAmount,
	0
FROM Uni6data.dbo.AccountExpense
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Payee like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1, null, null , TransDescription, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransDescription, Sort, TransDate desc, TransAmount desc 
else
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransDescription desc, Sort, TransDate desc, TransAmount desc 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.w_Transaction_GetExpensesGroupByDonorPayeeP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Payee, 
	TransDescription,
	TransDate,
	TransAmount,
	0
FROM Uni6data.dbo.AccountExpense
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Payee like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1, null, DonorPayee , null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY DonorPayee

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY DonorPayee, Sort, TransDate desc, TransAmount desc 
else
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY DonorPayee desc, Sort, TransDate desc, TransAmount desc 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.w_Transaction_GetExpensesSortByAmountP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Payee, 
	TransDescription,
	TransDate,
	TransAmount,
	0
FROM Uni6data.dbo.AccountExpense
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Payee like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransAmount desc, TransDate desc, ObjectCode 
else
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransAmount asc, TransDate desc, ObjectCode 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.w_Transaction_GetRevenuesGroupByAccount
@AccountId int, 
@Start smalldatetime,
@End smalldatetime

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	Donor varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int,
	ObjectCodeDescription varchar(60)
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Donor, 
	TransDescription,
	TransDate,
	TransAmount,
	0,
	ObjectCodeDescription
FROM Uni6data.dbo.AccountRevenue
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId

--insert subtotals
INSERT INTO #Temp1
SELECT 1, ObjectCode, 'TOTAL for ' + ObjectCode + ' (' + ObjectCodeDescription + ')' , null, null, sum(TransAmount), count(Sort), ObjectCodeDescription
FROM #Temp1
GROUP BY ObjectCode, ObjectCodeDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, 'TOTAL for account #' + substring(str(@AccountId, 7), 2, 6), null, null, sum(TransAmount), count(Sort), null
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	Donor varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp2
SELECT Sort, ObjectCode, Donor, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort != 2
ORDER BY ObjectCode, Sort, TransDate desc, TransAmount desc 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  Donor, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2

SELECT * FROM #Temp2CREATE PROCEDURE dbo.w_Transaction_GetRevenuesGroupByAccountP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int,
	ObjectCodeDescription varchar(60)
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Donor, 
	TransDescription,
	TransDate,
	TransAmount,
	0,
	ObjectCodeDescription
FROM Uni6data.dbo.AccountRevenue
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Donor like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1, ObjectCode, ObjectCodeDescription, null, null, sum(TransAmount), count(Sort), null
FROM #Temp1
GROUP BY ObjectCode, ObjectCodeDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort), null
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY ObjectCode, Sort, TransDate desc, TransAmount desc 
else 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY ObjectCode desc, Sort, TransDate desc, TransAmount desc 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.w_Transaction_GetRevenuesGroupByDateP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Donor, 
	TransDescription,
	TransDate,
	TransAmount,
	0
FROM Uni6data.dbo.AccountRevenue
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Donor like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1, null, null, null, TransDate, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDate

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransDate desc, Sort, TransAmount desc 
else
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransDate asc, Sort, TransAmount desc 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.w_Transaction_GetRevenuesGroupByDescriptionP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Donor, 
	TransDescription,
	TransDate,
	TransAmount,
	0
FROM Uni6data.dbo.AccountRevenue
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Donor like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1, null, null , TransDescription, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY TransDescription

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransDescription, Sort, TransDate desc, TransAmount desc 
else
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransDescription desc, Sort, TransDate desc, TransAmount desc 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.w_Transaction_GetRevenuesGroupByDonorPayeeP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Donor, 
	TransDescription,
	TransDate,
	TransAmount,
	0
FROM Uni6data.dbo.AccountRevenue
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Donor like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

--insert subtotals
INSERT INTO #Temp1
SELECT 1, null, DonorPayee , null, null, sum(TransAmount), count(Sort)
FROM #Temp1
GROUP BY DonorPayee

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY DonorPayee, Sort, TransDate desc, TransAmount desc 
else
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY DonorPayee desc, Sort, TransDate desc, TransAmount desc 

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRowsCREATE PROCEDURE dbo.w_Transaction_GetRevenuesSortByAmountP
@AccountId int, 
@Start smalldatetime,
@End smalldatetime,
@ObjectCode varchar(4),
@DonorPayee varchar(60),
@Description varchar(60),
@PageSize int,
@PageNum int,
@IsAscendingSort bit

AS

--insert all transactions into temp table
CREATE TABLE #Temp1
(
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

INSERT INTO #Temp1
SELECT 		
	0,
	ObjectCode,
	Donor, 
	TransDescription,
	TransDate,
	TransAmount,
	0
FROM Uni6data.dbo.AccountRevenue
WHERE 
(TransDate between @Start and @End) and AccountId = @AccountId
and ObjectCode like '%' + @ObjectCode + '%'
and Donor like '%' + @DonorPayee + '%' 
and TransDescription like '%' + @Description + '%'

-- insert grandtotal
INSERT INTO #Temp1
SELECT 2, null, null, null, null, sum(TransAmount), count(Sort)
FROM #Temp1
WHERE Sort = 0

--process paging
CREATE TABLE #Temp2
(
	Id int IDENTITY,
	Sort int,
	ObjectCode char(4),
	DonorPayee varchar(60),
	TransDescription varchar(60),
	TransDate datetime,
	TransAmount money,
	Records int
)

if @IsAscendingSort = 1 
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransAmount desc, TransDate desc, ObjectCode
else
	INSERT INTO #Temp2
	SELECT Sort, ObjectCode, DonorPayee, TransDescription, TransDate, TransAmount, Records
	FROM #Temp1 
	WHERE Sort != 2
	ORDER BY TransAmount asc, TransDate desc, ObjectCode

DECLARE @rows int 
SET @rows = @@ROWCOUNT + 1 -- + 1 for the grand total 

INSERT INTO #Temp2
SELECT Sort, ObjectCode,  DonorPayee, TransDescription, TransDate, TransAmount, Records
FROM #Temp1 
WHERE Sort = 2


DECLARE @first int 
DECLARE @last int 
SET @first = (@PageNum-1) * @PageSize + 1 
SET @last = @first + @PageSize - 1 

-- select requested page
SELECT * FROM #Temp2
WHERE Id >= @first AND Id <= @last

--select # of total records
SELECT @rows AS TotalRows