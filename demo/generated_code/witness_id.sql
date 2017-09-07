


CREATE PROCEDURE dbo.a_LineupText_Create
  @Name varchar(100),
  @Content varchar(4000),
  @Rank int,
  @ModifiedBy varchar(50)
AS

IF EXISTS (SELECT * FROM LineupText WHERE Name = @Name)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO LineupText
(
	Name, 
	Content, 
	Rank, 
	Modified, 
	ModifiedBy
)
VALUES
(
	@Name, 
	@Content, 
	@Rank, 
	getDate(), 
	@ModifiedBy
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_LineupText_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM LineupText
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_LineupText_Update
  @Id int,
  @Name varchar(100),
  @Content varchar(4000),
  @Rank int,
  @ModifiedBy varchar(50)
AS

IF EXISTS (SELECT * FROM LineupText WHERE Name = @Name AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE LineupText
SET
	Name = @Name,
	Content = @Content,
	Rank = @Rank,
	Modified = getDate(),
	ModifiedBy = @ModifiedBy
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_LineupText_GetRecord
  @Id int
AS

SELECT
	LineupText.Id AS LineupText_Id,
	LineupText.Name AS LineupText_Name,
	LineupText.Content AS LineupText_Content,
	LineupText.Rank AS LineupText_Rank,
	LineupText.Modified AS LineupText_Modified,
	LineupText.ModifiedBy AS LineupText_ModifiedBy
FROM LineupText
WHERE LineupText.Id = @Id

CREATE PROCEDURE dbo.a_LineupText_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		LineupText.Id AS LineupText_Id,
		LineupText.Name AS LineupText_Name,
		LineupText.Content AS LineupText_Content,
		LineupText.Rank AS LineupText_Rank,
		LineupText.Modified AS LineupText_Modified,
		LineupText.ModifiedBy AS LineupText_ModifiedBy
	FROM LineupText
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_LineupText_GetList

AS

SELECT
	LineupText.Id AS LineupText_Id,
	LineupText.Name AS LineupText_Name
FROM LineupText
ORDER BY LineupText.Id


CREATE PROCEDURE dbo.a_LineupText_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempLineupText
(
	TempId int IDENTITY PRIMARY KEY,
	LineupText_Id int,
	LineupText_Name varchar(100),
	LineupText_Content varchar(4000),
	LineupText_Rank int,
	LineupText_Modified datetime,
	LineupText_ModifiedBy varchar(50)
)

INSERT INTO #TempLineupText
(
	LineupText_Id,
	LineupText_Name,
	LineupText_Content,
	LineupText_Rank,
	LineupText_Modified,
	LineupText_ModifiedBy
)
EXEC
('
	SELECT
		LineupText.Id AS LineupText_Id,
		LineupText.Name AS LineupText_Name,
		LineupText.Content AS LineupText_Content,
		LineupText.Rank AS LineupText_Rank,
		LineupText.Modified AS LineupText_Modified,
		LineupText.ModifiedBy AS LineupText_ModifiedBy
	FROM LineupText
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	LineupText_Id,
	LineupText_Name,
	LineupText_Content,
	LineupText_Rank,
	LineupText_Modified,
	LineupText_ModifiedBy
FROM #TempLineupText
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_LineupText_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempLineupText
(
	TempId int IDENTITY PRIMARY KEY,
	LineupText_Id int,
	LineupText_Name varchar(100),
	LineupText_Content varchar(4000),
	LineupText_Rank int,
	LineupText_Modified datetime,
	LineupText_ModifiedBy varchar(50)
)

INSERT INTO #TempLineupText
(
	LineupText_Id,
	LineupText_Name,
	LineupText_Content,
	LineupText_Rank,
	LineupText_Modified,
	LineupText_ModifiedBy
)
EXEC
('
	SELECT
		LineupText.Id AS LineupText_Id,
		LineupText.Name AS LineupText_Name,
		LineupText.Content AS LineupText_Content,
		LineupText.Rank AS LineupText_Rank,
		LineupText.Modified AS LineupText_Modified,
		LineupText.ModifiedBy AS LineupText_ModifiedBy
	FROM LineupText
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
	LineupText_Id,
	LineupText_Name,
	LineupText_Content,
	LineupText_Rank,
	LineupText_Modified,
	LineupText_ModifiedBy
FROM #TempLineupText
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_Create
  @CaseId int,
  @Gender char(1),
  @RaceId int,
  @HairId int,
  @AgeId int,
  @WeightId int,
  @Notes varchar(1000),
  @ModifiedBy varchar(50)
AS

INSERT INTO Suspect
(
	CaseId, 
	Gender, 
	RaceId, 
	HairId, 
	AgeId, 
	WeightId, 
	Notes, 
	Created, 
	Modified, 
	ModifiedBy
)
VALUES
(
	@CaseId, 
	@Gender, 
	@RaceId, 
	@HairId, 
	@AgeId, 
	@WeightId, 
	@Notes, 
	getDate(), 
	getDate(), 
	@ModifiedBy
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_Suspect_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM Lineup WHERE SuspectId = @Id) > 0)
		RETURN -16

	IF ((SELECT COUNT(*) FROM PhotoView WHERE SuspectId = @Id) > 0)
		RETURN -17

	END 
ELSE 
	BEGIN

	DELETE FROM Lineup WHERE SuspectId = @Id

	DELETE FROM PhotoView WHERE SuspectId = @Id

	END 

DELETE FROM Suspect
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_Suspect_Update
  @Id int,
  @CaseId int,
  @Gender char(1),
  @RaceId int,
  @HairId int,
  @AgeId int,
  @WeightId int,
  @Notes varchar(1000),
  @ModifiedBy varchar(50)
AS

UPDATE Suspect
SET
	CaseId = @CaseId,
	Gender = @Gender,
	RaceId = @RaceId,
	HairId = @HairId,
	AgeId = @AgeId,
	WeightId = @WeightId,
	Notes = @Notes,
	Modified = getDate(),
	ModifiedBy = @ModifiedBy
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_Suspect_GetRecord
  @Id int
AS

SELECT
	Suspect.Id AS Suspect_Id,
	Suspect.CaseId AS Suspect_CaseId,
	Suspect.Gender AS Suspect_Gender,
	Suspect.RaceId AS Suspect_RaceId,
	Suspect.HairId AS Suspect_HairId,
	Suspect.AgeId AS Suspect_AgeId,
	Suspect.WeightId AS Suspect_WeightId,
	Suspect.Notes AS Suspect_Notes,
	Suspect.Created AS Suspect_Created,
	Suspect.Modified AS Suspect_Modified,
	Suspect.ModifiedBy AS Suspect_ModifiedBy,
	[Case].Number AS Case_Number,
	Race.Description AS Race_Description,
	Hair.Description AS Hair_Description,
	Age.Description AS Age_Description,
	Weight.Description AS Weight_Description
FROM Suspect
LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
WHERE Suspect.Id = @Id

CREATE PROCEDURE dbo.a_Suspect_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Suspect_GetList

AS

SELECT
	Suspect.Id AS Suspect_Id
FROM Suspect
ORDER BY Suspect.Id


CREATE PROCEDURE dbo.a_Suspect_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
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
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByCaseField
  @CaseId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.CaseId = ' + @CaseId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByCaseFieldP
  @CaseId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.CaseId = ' + @CaseId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByCaseFieldPS
  @CaseId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Suspect.CaseId = ' + @CaseId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByRaceField
  @RaceId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.RaceId = ' + @RaceId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByRaceFieldP
  @RaceId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.RaceId = ' + @RaceId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByRaceFieldPS
  @RaceId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Suspect.RaceId = ' + @RaceId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByHairField
  @HairId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.HairId = ' + @HairId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByHairFieldP
  @HairId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.HairId = ' + @HairId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByHairFieldPS
  @HairId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Suspect.HairId = ' + @HairId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByAgeField
  @AgeId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.AgeId = ' + @AgeId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByAgeFieldP
  @AgeId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.AgeId = ' + @AgeId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByAgeFieldPS
  @AgeId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Suspect.AgeId = ' + @AgeId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByWeightField
  @WeightId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.WeightId = ' + @WeightId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByWeightFieldP
  @WeightId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE Suspect.WeightId = ' + @WeightId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Suspect_GetRecordsByWeightFieldPS
  @WeightId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempSuspect
(
	TempId int IDENTITY PRIMARY KEY,
	Suspect_Id int,
	Suspect_CaseId int,
	Suspect_Gender char(1),
	Suspect_RaceId int,
	Suspect_HairId int,
	Suspect_AgeId int,
	Suspect_WeightId int,
	Suspect_Notes varchar(1000),
	Suspect_Created datetime,
	Suspect_Modified datetime,
	Suspect_ModifiedBy varchar(50),
	Case_Number varchar(10),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempSuspect
(
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Suspect.Id AS Suspect_Id,
		Suspect.CaseId AS Suspect_CaseId,
		Suspect.Gender AS Suspect_Gender,
		Suspect.RaceId AS Suspect_RaceId,
		Suspect.HairId AS Suspect_HairId,
		Suspect.AgeId AS Suspect_AgeId,
		Suspect.WeightId AS Suspect_WeightId,
		Suspect.Notes AS Suspect_Notes,
		Suspect.Created AS Suspect_Created,
		Suspect.Modified AS Suspect_Modified,
		Suspect.ModifiedBy AS Suspect_ModifiedBy,
		[Case].Number AS Case_Number,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Suspect
	LEFT OUTER JOIN [Case] ON Suspect.CaseId = [Case].Id 
	LEFT OUTER JOIN Race ON Suspect.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Suspect.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Suspect.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Suspect.WeightId = Weight.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Suspect.WeightId = ' + @WeightId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Suspect_Id,
	Suspect_CaseId,
	Suspect_Gender,
	Suspect_RaceId,
	Suspect_HairId,
	Suspect_AgeId,
	Suspect_WeightId,
	Suspect_Notes,
	Suspect_Created,
	Suspect_Modified,
	Suspect_ModifiedBy,
	Case_Number,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempSuspect
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Case_Create
  @Number varchar(10),
  @Description varchar(1000),
  @ModifiedBy varchar(50)
AS

IF EXISTS (SELECT * FROM [Case] WHERE Number = @Number)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO [Case]
(
	Number, 
	Description, 
	Created, 
	Modified, 
	ModifiedBy
)
VALUES
(
	@Number, 
	@Description, 
	getDate(), 
	getDate(), 
	@ModifiedBy
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_Case_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM Suspect WHERE CaseId = @Id) > 0)
		RETURN -16

	IF ((SELECT COUNT(*) FROM Lineup WHERE CaseId = @Id) > 0)
		RETURN -17

	END 
ELSE 
	BEGIN

	DELETE FROM Suspect WHERE CaseId = @Id

	DELETE FROM Lineup WHERE CaseId = @Id

	END 

DELETE FROM UserCaseLink WHERE CaseId = @Id
DELETE FROM [Case]
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_Case_Update
  @Id int,
  @Number varchar(10),
  @Description varchar(1000),
  @ModifiedBy varchar(50)
AS

IF EXISTS (SELECT * FROM [Case] WHERE Number = @Number AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE [Case]
SET
	Number = @Number,
	Description = @Description,
	Modified = getDate(),
	ModifiedBy = @ModifiedBy
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_Case_GetRecord
  @Id int
AS

SELECT
	[Case].Id AS Case_Id,
	[Case].Number AS Case_Number,
	[Case].Description AS Case_Description,
	[Case].Created AS Case_Created,
	[Case].Modified AS Case_Modified,
	[Case].ModifiedBy AS Case_ModifiedBy
FROM [Case]
WHERE [Case].Id = @Id

CREATE PROCEDURE dbo.a_Case_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Case].Id AS Case_Id,
		[Case].Number AS Case_Number,
		[Case].Description AS Case_Description,
		[Case].Created AS Case_Created,
		[Case].Modified AS Case_Modified,
		[Case].ModifiedBy AS Case_ModifiedBy
	FROM [Case]
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Case_GetList

AS

SELECT
	[Case].Id AS Case_Id,
	[Case].Number AS Case_Number
FROM [Case]
ORDER BY [Case].Id


CREATE PROCEDURE dbo.a_Case_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempCase
(
	TempId int IDENTITY PRIMARY KEY,
	Case_Id int,
	Case_Number varchar(10),
	Case_Description varchar(1000),
	Case_Created datetime,
	Case_Modified datetime,
	Case_ModifiedBy varchar(50)
)

INSERT INTO #TempCase
(
	Case_Id,
	Case_Number,
	Case_Description,
	Case_Created,
	Case_Modified,
	Case_ModifiedBy
)
EXEC
('
	SELECT
		[Case].Id AS Case_Id,
		[Case].Number AS Case_Number,
		[Case].Description AS Case_Description,
		[Case].Created AS Case_Created,
		[Case].Modified AS Case_Modified,
		[Case].ModifiedBy AS Case_ModifiedBy
	FROM [Case]
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Case_Id,
	Case_Number,
	Case_Description,
	Case_Created,
	Case_Modified,
	Case_ModifiedBy
FROM #TempCase
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Case_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempCase
(
	TempId int IDENTITY PRIMARY KEY,
	Case_Id int,
	Case_Number varchar(10),
	Case_Description varchar(1000),
	Case_Created datetime,
	Case_Modified datetime,
	Case_ModifiedBy varchar(50)
)

INSERT INTO #TempCase
(
	Case_Id,
	Case_Number,
	Case_Description,
	Case_Created,
	Case_Modified,
	Case_ModifiedBy
)
EXEC
('
	SELECT
		[Case].Id AS Case_Id,
		[Case].Number AS Case_Number,
		[Case].Description AS Case_Description,
		[Case].Created AS Case_Created,
		[Case].Modified AS Case_Modified,
		[Case].ModifiedBy AS Case_ModifiedBy
	FROM [Case]
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
	Case_Id,
	Case_Number,
	Case_Description,
	Case_Created,
	Case_Modified,
	Case_ModifiedBy
FROM #TempCase
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Case_GetRecordsByUserLink
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Case].Id AS Case_Id,
		[Case].Number AS Case_Number,
		[Case].Description AS Case_Description,
		[Case].Created AS Case_Created,
		[Case].Modified AS Case_Modified,
		[Case].ModifiedBy AS Case_ModifiedBy
	FROM [Case]
	JOIN UserCaseLink ON UserCaseLink.CaseId = [Case].Id AND UserCaseLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Case_GetRecordsByUserLinkP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempCase
(
	TempId int IDENTITY PRIMARY KEY,
	Case_Id int,
	Case_Number varchar(10),
	Case_Description varchar(1000),
	Case_Created datetime,
	Case_Modified datetime,
	Case_ModifiedBy varchar(50)
)

INSERT INTO #TempCase
(
	Case_Id,
	Case_Number,
	Case_Description,
	Case_Created,
	Case_Modified,
	Case_ModifiedBy
)
EXEC
('
	SELECT
		[Case].Id AS Case_Id,
		[Case].Number AS Case_Number,
		[Case].Description AS Case_Description,
		[Case].Created AS Case_Created,
		[Case].Modified AS Case_Modified,
		[Case].ModifiedBy AS Case_ModifiedBy
	FROM [Case]
	JOIN UserCaseLink ON UserCaseLink.CaseId = [Case].Id AND UserCaseLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Case_Id,
	Case_Number,
	Case_Description,
	Case_Created,
	Case_Modified,
	Case_ModifiedBy
FROM #TempCase
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Case_GetRecordsByUserLinkPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempCase
(
	TempId int IDENTITY PRIMARY KEY,
	Case_Id int,
	Case_Number varchar(10),
	Case_Description varchar(1000),
	Case_Created datetime,
	Case_Modified datetime,
	Case_ModifiedBy varchar(50)
)

INSERT INTO #TempCase
(
	Case_Id,
	Case_Number,
	Case_Description,
	Case_Created,
	Case_Modified,
	Case_ModifiedBy
)
EXEC
('
	SELECT
		[Case].Id AS Case_Id,
		[Case].Number AS Case_Number,
		[Case].Description AS Case_Description,
		[Case].Created AS Case_Created,
		[Case].Modified AS Case_Modified,
		[Case].ModifiedBy AS Case_ModifiedBy
	FROM [Case]
	JOIN UserCaseLink ON UserCaseLink.CaseId = [Case].Id AND UserCaseLink.UserId = ' + @UserId + ' 
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
	Case_Id,
	Case_Number,
	Case_Description,
	Case_Created,
	Case_Modified,
	Case_ModifiedBy
FROM #TempCase
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Case_GetUserLinks
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Case].Id AS Case_Id,
		[Case].Number AS Case_Number,
		CAST (ISNULL(UserCaseLink.UserId, 0) as bit) AS Selected
	FROM [Case]
	LEFT OUTER JOIN UserCaseLink ON UserCaseLink.CaseId = [Case].Id AND UserCaseLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Case_GetUserLinksP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempCase
(
	TempId int IDENTITY PRIMARY KEY,
	Case_Id int,
	Case_Number varchar(10),
	selected bit
)

INSERT INTO #TempCase
(
	Case_Id,
	Case_Number,
	selected
)
EXEC
('
	SELECT
		[Case].Id AS Case_Id,
		[Case].Number AS Case_Number,
		CAST (ISNULL(UserCaseLink.UserId, 0) as bit) AS Selected
	FROM [Case]
	LEFT OUTER JOIN UserCaseLink ON UserCaseLink.CaseId = [Case].Id AND UserCaseLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Case_Id,
	Case_Number,
	selected
FROM #TempCase
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Case_GetUserLinksPS
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempCase
(
	TempId int IDENTITY PRIMARY KEY,
	Case_Id int,
	Case_Number varchar(10),
	selected bit
)

INSERT INTO #TempCase
(
	Case_Id,
	Case_Number,
	selected
)
EXEC
('
	SELECT
		[Case].Id AS Case_Id,
		[Case].Number AS Case_Number,
		CAST (ISNULL(UserCaseLink.UserId, 0) as bit) AS Selected
	FROM [Case]
	LEFT OUTER JOIN UserCaseLink ON UserCaseLink.CaseId = [Case].Id AND UserCaseLink.UserId = ' + @UserId + ' 
	WHERE [Case].' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Case_Id,
	Case_Number,
	selected
FROM #TempCase
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Race_Create
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM Race WHERE Description = @Description)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO Race
(
	Description
)
VALUES
(
	@Description
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_Race_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM Suspect WHERE RaceId = @Id) > 0)
		RETURN -16

	IF ((SELECT COUNT(*) FROM Photo WHERE RaceId = @Id) > 0)
		RETURN -17

	END 
ELSE 
	BEGIN

	DELETE FROM Suspect WHERE RaceId = @Id

	DELETE FROM Photo WHERE RaceId = @Id

	END 

DELETE FROM Race
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_Race_Update
  @Id int,
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM Race WHERE Description = @Description AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE Race
SET
	Description = @Description
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_Race_GetRecord
  @Id int
AS

SELECT
	Race.Id AS Race_Id,
	Race.Description AS Race_Description
FROM Race
WHERE Race.Id = @Id

CREATE PROCEDURE dbo.a_Race_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Race.Id AS Race_Id,
		Race.Description AS Race_Description
	FROM Race
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Race_GetList

AS

SELECT
	Race.Id AS Race_Id,
	Race.Description AS Race_Description
FROM Race
ORDER BY Race.Id


CREATE PROCEDURE dbo.a_Race_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempRace
(
	TempId int IDENTITY PRIMARY KEY,
	Race_Id int,
	Race_Description varchar(25)
)

INSERT INTO #TempRace
(
	Race_Id,
	Race_Description
)
EXEC
('
	SELECT
		Race.Id AS Race_Id,
		Race.Description AS Race_Description
	FROM Race
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Race_Id,
	Race_Description
FROM #TempRace
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Race_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempRace
(
	TempId int IDENTITY PRIMARY KEY,
	Race_Id int,
	Race_Description varchar(25)
)

INSERT INTO #TempRace
(
	Race_Id,
	Race_Description
)
EXEC
('
	SELECT
		Race.Id AS Race_Id,
		Race.Description AS Race_Description
	FROM Race
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
	Race_Id,
	Race_Description
FROM #TempRace
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Hair_Create
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM Hair WHERE Description = @Description)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO Hair
(
	Description
)
VALUES
(
	@Description
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_Hair_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM Suspect WHERE HairId = @Id) > 0)
		RETURN -16

	IF ((SELECT COUNT(*) FROM Photo WHERE HairId = @Id) > 0)
		RETURN -17

	END 
ELSE 
	BEGIN

	DELETE FROM Suspect WHERE HairId = @Id

	DELETE FROM Photo WHERE HairId = @Id

	END 

DELETE FROM Hair
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_Hair_Update
  @Id int,
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM Hair WHERE Description = @Description AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE Hair
SET
	Description = @Description
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_Hair_GetRecord
  @Id int
AS

SELECT
	Hair.Id AS Hair_Id,
	Hair.Description AS Hair_Description
FROM Hair
WHERE Hair.Id = @Id

CREATE PROCEDURE dbo.a_Hair_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Hair.Id AS Hair_Id,
		Hair.Description AS Hair_Description
	FROM Hair
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Hair_GetList

AS

SELECT
	Hair.Id AS Hair_Id,
	Hair.Description AS Hair_Description
FROM Hair
ORDER BY Hair.Id


CREATE PROCEDURE dbo.a_Hair_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempHair
(
	TempId int IDENTITY PRIMARY KEY,
	Hair_Id int,
	Hair_Description varchar(25)
)

INSERT INTO #TempHair
(
	Hair_Id,
	Hair_Description
)
EXEC
('
	SELECT
		Hair.Id AS Hair_Id,
		Hair.Description AS Hair_Description
	FROM Hair
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Hair_Id,
	Hair_Description
FROM #TempHair
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Hair_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempHair
(
	TempId int IDENTITY PRIMARY KEY,
	Hair_Id int,
	Hair_Description varchar(25)
)

INSERT INTO #TempHair
(
	Hair_Id,
	Hair_Description
)
EXEC
('
	SELECT
		Hair.Id AS Hair_Id,
		Hair.Description AS Hair_Description
	FROM Hair
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
	Hair_Id,
	Hair_Description
FROM #TempHair
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Age_Create
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM Age WHERE Description = @Description)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO Age
(
	Description
)
VALUES
(
	@Description
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_Age_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM Suspect WHERE AgeId = @Id) > 0)
		RETURN -16

	IF ((SELECT COUNT(*) FROM Photo WHERE AgeId = @Id) > 0)
		RETURN -17

	END 
ELSE 
	BEGIN

	DELETE FROM Suspect WHERE AgeId = @Id

	DELETE FROM Photo WHERE AgeId = @Id

	END 

DELETE FROM Age
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_Age_Update
  @Id int,
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM Age WHERE Description = @Description AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE Age
SET
	Description = @Description
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_Age_GetRecord
  @Id int
AS

SELECT
	Age.Id AS Age_Id,
	Age.Description AS Age_Description
FROM Age
WHERE Age.Id = @Id

CREATE PROCEDURE dbo.a_Age_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Age.Id AS Age_Id,
		Age.Description AS Age_Description
	FROM Age
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Age_GetList

AS

SELECT
	Age.Id AS Age_Id,
	Age.Description AS Age_Description
FROM Age
ORDER BY Age.Id


CREATE PROCEDURE dbo.a_Age_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempAge
(
	TempId int IDENTITY PRIMARY KEY,
	Age_Id int,
	Age_Description varchar(25)
)

INSERT INTO #TempAge
(
	Age_Id,
	Age_Description
)
EXEC
('
	SELECT
		Age.Id AS Age_Id,
		Age.Description AS Age_Description
	FROM Age
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Age_Id,
	Age_Description
FROM #TempAge
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Age_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempAge
(
	TempId int IDENTITY PRIMARY KEY,
	Age_Id int,
	Age_Description varchar(25)
)

INSERT INTO #TempAge
(
	Age_Id,
	Age_Description
)
EXEC
('
	SELECT
		Age.Id AS Age_Id,
		Age.Description AS Age_Description
	FROM Age
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
	Age_Id,
	Age_Description
FROM #TempAge
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Weight_Create
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM Weight WHERE Description = @Description)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO Weight
(
	Description
)
VALUES
(
	@Description
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_Weight_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM Suspect WHERE WeightId = @Id) > 0)
		RETURN -16

	IF ((SELECT COUNT(*) FROM Photo WHERE WeightId = @Id) > 0)
		RETURN -17

	END 
ELSE 
	BEGIN

	DELETE FROM Suspect WHERE WeightId = @Id

	DELETE FROM Photo WHERE WeightId = @Id

	END 

DELETE FROM Weight
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_Weight_Update
  @Id int,
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM Weight WHERE Description = @Description AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE Weight
SET
	Description = @Description
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_Weight_GetRecord
  @Id int
AS

SELECT
	Weight.Id AS Weight_Id,
	Weight.Description AS Weight_Description
FROM Weight
WHERE Weight.Id = @Id

CREATE PROCEDURE dbo.a_Weight_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Weight.Id AS Weight_Id,
		Weight.Description AS Weight_Description
	FROM Weight
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Weight_GetList

AS

SELECT
	Weight.Id AS Weight_Id,
	Weight.Description AS Weight_Description
FROM Weight
ORDER BY Weight.Id


CREATE PROCEDURE dbo.a_Weight_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempWeight
(
	TempId int IDENTITY PRIMARY KEY,
	Weight_Id int,
	Weight_Description varchar(25)
)

INSERT INTO #TempWeight
(
	Weight_Id,
	Weight_Description
)
EXEC
('
	SELECT
		Weight.Id AS Weight_Id,
		Weight.Description AS Weight_Description
	FROM Weight
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Weight_Id,
	Weight_Description
FROM #TempWeight
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Weight_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempWeight
(
	TempId int IDENTITY PRIMARY KEY,
	Weight_Id int,
	Weight_Description varchar(25)
)

INSERT INTO #TempWeight
(
	Weight_Id,
	Weight_Description
)
EXEC
('
	SELECT
		Weight.Id AS Weight_Id,
		Weight.Description AS Weight_Description
	FROM Weight
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
	Weight_Id,
	Weight_Description
FROM #TempWeight
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_Create
  @SuspectId int,
  @SuspectPhotoPosition int,
  @CaseId int,
  @Description varchar(500),
  @ModifiedBy varchar(50)
AS

INSERT INTO Lineup
(
	SuspectId, 
	SuspectPhotoPosition, 
	CaseId, 
	Description, 
	Created, 
	Modified, 
	ModifiedBy
)
VALUES
(
	@SuspectId, 
	@SuspectPhotoPosition, 
	@CaseId, 
	@Description, 
	getDate(), 
	getDate(), 
	@ModifiedBy
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_Lineup_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM LineupView WHERE LineupId = @Id) > 0)
		RETURN -16

	END 
ELSE 
	BEGIN

	DELETE FROM LineupView WHERE LineupId = @Id

	END 

DELETE FROM LineupPhotoLink WHERE LineupId = @Id
DELETE FROM Lineup
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_Lineup_Update
  @Id int,
  @SuspectId int,
  @SuspectPhotoPosition int,
  @CaseId int,
  @Description varchar(500),
  @ModifiedBy varchar(50)
AS

UPDATE Lineup
SET
	SuspectId = @SuspectId,
	SuspectPhotoPosition = @SuspectPhotoPosition,
	CaseId = @CaseId,
	Description = @Description,
	Modified = getDate(),
	ModifiedBy = @ModifiedBy
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_Lineup_GetRecord
  @Id int
AS

SELECT
	Lineup.Id AS Lineup_Id,
	Lineup.SuspectId AS Lineup_SuspectId,
	Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
	Lineup.CaseId AS Lineup_CaseId,
	Lineup.Description AS Lineup_Description,
	Lineup.Created AS Lineup_Created,
	Lineup.Modified AS Lineup_Modified,
	Lineup.ModifiedBy AS Lineup_ModifiedBy,
	[Case].Number AS Case_Number
FROM Lineup
LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
WHERE Lineup.Id = @Id

CREATE PROCEDURE dbo.a_Lineup_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Lineup_GetList

AS

SELECT
	Lineup.Id AS Lineup_Id,
	Lineup.Description AS Lineup_Description
FROM Lineup
ORDER BY Lineup.Id


CREATE PROCEDURE dbo.a_Lineup_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_SuspectId int,
	Lineup_SuspectPhotoPosition int,
	Lineup_CaseId int,
	Lineup_Description varchar(500),
	Lineup_Created datetime,
	Lineup_Modified datetime,
	Lineup_ModifiedBy varchar(50),
	Case_Number varchar(10)
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_SuspectId int,
	Lineup_SuspectPhotoPosition int,
	Lineup_CaseId int,
	Lineup_Description varchar(500),
	Lineup_Created datetime,
	Lineup_Modified datetime,
	Lineup_ModifiedBy varchar(50),
	Case_Number varchar(10)
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
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
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_GetRecordsBySuspectField
  @SuspectId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	WHERE Lineup.SuspectId = ' + @SuspectId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Lineup_GetRecordsBySuspectFieldP
  @SuspectId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_SuspectId int,
	Lineup_SuspectPhotoPosition int,
	Lineup_CaseId int,
	Lineup_Description varchar(500),
	Lineup_Created datetime,
	Lineup_Modified datetime,
	Lineup_ModifiedBy varchar(50),
	Case_Number varchar(10)
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	WHERE Lineup.SuspectId = ' + @SuspectId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_GetRecordsBySuspectFieldPS
  @SuspectId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_SuspectId int,
	Lineup_SuspectPhotoPosition int,
	Lineup_CaseId int,
	Lineup_Description varchar(500),
	Lineup_Created datetime,
	Lineup_Modified datetime,
	Lineup_ModifiedBy varchar(50),
	Case_Number varchar(10)
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Lineup.SuspectId = ' + @SuspectId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_GetRecordsByCaseField
  @CaseId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	WHERE Lineup.CaseId = ' + @CaseId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Lineup_GetRecordsByCaseFieldP
  @CaseId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_SuspectId int,
	Lineup_SuspectPhotoPosition int,
	Lineup_CaseId int,
	Lineup_Description varchar(500),
	Lineup_Created datetime,
	Lineup_Modified datetime,
	Lineup_ModifiedBy varchar(50),
	Case_Number varchar(10)
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	WHERE Lineup.CaseId = ' + @CaseId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_GetRecordsByCaseFieldPS
  @CaseId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_SuspectId int,
	Lineup_SuspectPhotoPosition int,
	Lineup_CaseId int,
	Lineup_Description varchar(500),
	Lineup_Created datetime,
	Lineup_Modified datetime,
	Lineup_ModifiedBy varchar(50),
	Case_Number varchar(10)
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Lineup.CaseId = ' + @CaseId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_GetRecordsByPhotoLink
  @PhotoId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	JOIN LineupPhotoLink ON LineupPhotoLink.LineupId = Lineup.Id AND LineupPhotoLink.PhotoId = ' + @PhotoId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Lineup_GetRecordsByPhotoLinkP
  @PhotoId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_SuspectId int,
	Lineup_SuspectPhotoPosition int,
	Lineup_CaseId int,
	Lineup_Description varchar(500),
	Lineup_Created datetime,
	Lineup_Modified datetime,
	Lineup_ModifiedBy varchar(50),
	Case_Number varchar(10)
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	JOIN LineupPhotoLink ON LineupPhotoLink.LineupId = Lineup.Id AND LineupPhotoLink.PhotoId = ' + @PhotoId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_GetRecordsByPhotoLinkPS
  @PhotoId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_SuspectId int,
	Lineup_SuspectPhotoPosition int,
	Lineup_CaseId int,
	Lineup_Description varchar(500),
	Lineup_Created datetime,
	Lineup_Modified datetime,
	Lineup_ModifiedBy varchar(50),
	Case_Number varchar(10)
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.SuspectId AS Lineup_SuspectId,
		Lineup.SuspectPhotoPosition AS Lineup_SuspectPhotoPosition,
		Lineup.CaseId AS Lineup_CaseId,
		Lineup.Description AS Lineup_Description,
		Lineup.Created AS Lineup_Created,
		Lineup.Modified AS Lineup_Modified,
		Lineup.ModifiedBy AS Lineup_ModifiedBy,
		[Case].Number AS Case_Number
	FROM Lineup
	LEFT OUTER JOIN Suspect ON Lineup.SuspectId = Suspect.Id 
	LEFT OUTER JOIN [Case] ON Lineup.CaseId = [Case].Id 
	JOIN LineupPhotoLink ON LineupPhotoLink.LineupId = Lineup.Id AND LineupPhotoLink.PhotoId = ' + @PhotoId + ' 
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
	Lineup_Id,
	Lineup_SuspectId,
	Lineup_SuspectPhotoPosition,
	Lineup_CaseId,
	Lineup_Description,
	Lineup_Created,
	Lineup_Modified,
	Lineup_ModifiedBy,
	Case_Number
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_GetPhotoLinks
  @PhotoId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.Description AS Lineup_Description,
		CAST (ISNULL(LineupPhotoLink.PhotoId, 0) as bit) AS Selected
	FROM Lineup
	LEFT OUTER JOIN LineupPhotoLink ON LineupPhotoLink.LineupId = Lineup.Id AND LineupPhotoLink.PhotoId = ' + @PhotoId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Lineup_GetPhotoLinksP
  @PhotoId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_Description varchar(500),
	selected bit
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_Description,
	selected
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.Description AS Lineup_Description,
		CAST (ISNULL(LineupPhotoLink.PhotoId, 0) as bit) AS Selected
	FROM Lineup
	LEFT OUTER JOIN LineupPhotoLink ON LineupPhotoLink.LineupId = Lineup.Id AND LineupPhotoLink.PhotoId = ' + @PhotoId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Lineup_Id,
	Lineup_Description,
	selected
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Lineup_GetPhotoLinksPS
  @PhotoId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempLineup
(
	TempId int IDENTITY PRIMARY KEY,
	Lineup_Id int,
	Lineup_Description varchar(500),
	selected bit
)

INSERT INTO #TempLineup
(
	Lineup_Id,
	Lineup_Description,
	selected
)
EXEC
('
	SELECT
		Lineup.Id AS Lineup_Id,
		Lineup.Description AS Lineup_Description,
		CAST (ISNULL(LineupPhotoLink.PhotoId, 0) as bit) AS Selected
	FROM Lineup
	LEFT OUTER JOIN LineupPhotoLink ON LineupPhotoLink.LineupId = Lineup.Id AND LineupPhotoLink.PhotoId = ' + @PhotoId + ' 
	WHERE Lineup.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Lineup_Id,
	Lineup_Description,
	selected
FROM #TempLineup
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_Create
  @ExternalId varchar(20),
  @Gender char(1),
  @RaceId int,
  @HairId int,
  @AgeId int,
  @WeightId int,
  @ModifiedBy varchar(50)
AS

IF EXISTS (SELECT * FROM Photo WHERE ExternalId = @ExternalId)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO Photo
(
	ExternalId, 
	Gender, 
	RaceId, 
	HairId, 
	AgeId, 
	WeightId, 
	Created, 
	Modified, 
	ModifiedBy
)
VALUES
(
	@ExternalId, 
	@Gender, 
	@RaceId, 
	@HairId, 
	@AgeId, 
	@WeightId, 
	getDate(), 
	getDate(), 
	@ModifiedBy
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_Photo_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM PhotoView WHERE PhotoId = @Id) > 0)
		RETURN -16

	END 
ELSE 
	BEGIN

	DELETE FROM PhotoView WHERE PhotoId = @Id

	END 

DELETE FROM LineupPhotoLink WHERE PhotoId = @Id
DELETE FROM Photo
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_Photo_Update
  @Id int,
  @ExternalId varchar(20),
  @Gender char(1),
  @RaceId int,
  @HairId int,
  @AgeId int,
  @WeightId int,
  @ModifiedBy varchar(50)
AS

IF EXISTS (SELECT * FROM Photo WHERE ExternalId = @ExternalId AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE Photo
SET
	ExternalId = @ExternalId,
	Gender = @Gender,
	RaceId = @RaceId,
	HairId = @HairId,
	AgeId = @AgeId,
	WeightId = @WeightId,
	Modified = getDate(),
	ModifiedBy = @ModifiedBy
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_Photo_GetRecord
  @Id int
AS

SELECT
	Photo.Id AS Photo_Id,
	Photo.ExternalId AS Photo_ExternalId,
	Photo.Gender AS Photo_Gender,
	Photo.RaceId AS Photo_RaceId,
	Photo.HairId AS Photo_HairId,
	Photo.AgeId AS Photo_AgeId,
	Photo.WeightId AS Photo_WeightId,
	Photo.Created AS Photo_Created,
	Photo.Modified AS Photo_Modified,
	Photo.ModifiedBy AS Photo_ModifiedBy,
	Race.Description AS Race_Description,
	Hair.Description AS Hair_Description,
	Age.Description AS Age_Description,
	Weight.Description AS Weight_Description
FROM Photo
LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
WHERE Photo.Id = @Id

CREATE PROCEDURE dbo.a_Photo_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Photo_GetList

AS

SELECT
	Photo.Id AS Photo_Id
FROM Photo
ORDER BY Photo.Id


CREATE PROCEDURE dbo.a_Photo_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
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
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByRaceField
  @RaceId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE Photo.RaceId = ' + @RaceId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Photo_GetRecordsByRaceFieldP
  @RaceId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE Photo.RaceId = ' + @RaceId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByRaceFieldPS
  @RaceId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Photo.RaceId = ' + @RaceId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByHairField
  @HairId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE Photo.HairId = ' + @HairId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Photo_GetRecordsByHairFieldP
  @HairId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE Photo.HairId = ' + @HairId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByHairFieldPS
  @HairId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Photo.HairId = ' + @HairId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByAgeField
  @AgeId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE Photo.AgeId = ' + @AgeId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Photo_GetRecordsByAgeFieldP
  @AgeId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE Photo.AgeId = ' + @AgeId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByAgeFieldPS
  @AgeId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Photo.AgeId = ' + @AgeId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByWeightField
  @WeightId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE Photo.WeightId = ' + @WeightId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Photo_GetRecordsByWeightFieldP
  @WeightId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE Photo.WeightId = ' + @WeightId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByWeightFieldPS
  @WeightId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND Photo.WeightId = ' + @WeightId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByLineupLink
  @LineupId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	JOIN LineupPhotoLink ON LineupPhotoLink.PhotoId = Photo.Id AND LineupPhotoLink.LineupId = ' + @LineupId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Photo_GetRecordsByLineupLinkP
  @LineupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	JOIN LineupPhotoLink ON LineupPhotoLink.PhotoId = Photo.Id AND LineupPhotoLink.LineupId = ' + @LineupId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetRecordsByLineupLinkPS
  @LineupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	Photo_ExternalId varchar(20),
	Photo_Gender char(1),
	Photo_RaceId int,
	Photo_HairId int,
	Photo_AgeId int,
	Photo_WeightId int,
	Photo_Created datetime,
	Photo_Modified datetime,
	Photo_ModifiedBy varchar(50),
	Race_Description varchar(25),
	Hair_Description varchar(25),
	Age_Description varchar(25),
	Weight_Description varchar(25)
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		Photo.ExternalId AS Photo_ExternalId,
		Photo.Gender AS Photo_Gender,
		Photo.RaceId AS Photo_RaceId,
		Photo.HairId AS Photo_HairId,
		Photo.AgeId AS Photo_AgeId,
		Photo.WeightId AS Photo_WeightId,
		Photo.Created AS Photo_Created,
		Photo.Modified AS Photo_Modified,
		Photo.ModifiedBy AS Photo_ModifiedBy,
		Race.Description AS Race_Description,
		Hair.Description AS Hair_Description,
		Age.Description AS Age_Description,
		Weight.Description AS Weight_Description
	FROM Photo
	LEFT OUTER JOIN Race ON Photo.RaceId = Race.Id 
	LEFT OUTER JOIN Hair ON Photo.HairId = Hair.Id 
	LEFT OUTER JOIN Age ON Photo.AgeId = Age.Id 
	LEFT OUTER JOIN Weight ON Photo.WeightId = Weight.Id 
	JOIN LineupPhotoLink ON LineupPhotoLink.PhotoId = Photo.Id AND LineupPhotoLink.LineupId = ' + @LineupId + ' 
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
	Photo_Id,
	Photo_ExternalId,
	Photo_Gender,
	Photo_RaceId,
	Photo_HairId,
	Photo_AgeId,
	Photo_WeightId,
	Photo_Created,
	Photo_Modified,
	Photo_ModifiedBy,
	Race_Description,
	Hair_Description,
	Age_Description,
	Weight_Description
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetLineupLinks
  @LineupId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		CAST (ISNULL(LineupPhotoLink.LineupId, 0) as bit) AS Selected
	FROM Photo
	LEFT OUTER JOIN LineupPhotoLink ON LineupPhotoLink.PhotoId = Photo.Id AND LineupPhotoLink.LineupId = ' + @LineupId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Photo_GetLineupLinksP
  @LineupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	selected bit
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	selected
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		CAST (ISNULL(LineupPhotoLink.LineupId, 0) as bit) AS Selected
	FROM Photo
	LEFT OUTER JOIN LineupPhotoLink ON LineupPhotoLink.PhotoId = Photo.Id AND LineupPhotoLink.LineupId = ' + @LineupId + ' 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	selected
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Photo_GetLineupLinksPS
  @LineupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhoto
(
	TempId int IDENTITY PRIMARY KEY,
	Photo_Id int,
	selected bit
)

INSERT INTO #TempPhoto
(
	Photo_Id,
	selected
)
EXEC
('
	SELECT
		Photo.Id AS Photo_Id,
		CAST (ISNULL(LineupPhotoLink.LineupId, 0) as bit) AS Selected
	FROM Photo
	LEFT OUTER JOIN LineupPhotoLink ON LineupPhotoLink.PhotoId = Photo.Id AND LineupPhotoLink.LineupId = ' + @LineupId + ' 
	WHERE Photo.' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	Photo_Id,
	selected
FROM #TempPhoto
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_LineupView_Create
  @LineupId int,
  @WitnessFirstName varchar(25),
  @WitnessLastName varchar(25),
  @Relevance varchar(4000),
  @CreatedBy varchar(50),
  @IsCompleted bit
AS

INSERT INTO LineupView
(
	LineupId, 
	WitnessFirstName, 
	WitnessLastName, 
	Relevance, 
	Created, 
	CreatedBy, 
	IsCompleted
)
VALUES
(
	@LineupId, 
	@WitnessFirstName, 
	@WitnessLastName, 
	@Relevance, 
	getDate(), 
	@CreatedBy, 
	@IsCompleted
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_LineupView_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM PhotoView WHERE LineupViewId = @Id) > 0)
		RETURN -16

	END 
ELSE 
	BEGIN

	DELETE FROM PhotoView WHERE LineupViewId = @Id

	END 

DELETE FROM LineupView
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_LineupView_Update
  @Id int,
  @LineupId int,
  @WitnessFirstName varchar(25),
  @WitnessLastName varchar(25),
  @Relevance varchar(4000),
  @CreatedBy varchar(50),
  @IsCompleted bit
AS

UPDATE LineupView
SET
	LineupId = @LineupId,
	WitnessFirstName = @WitnessFirstName,
	WitnessLastName = @WitnessLastName,
	Relevance = @Relevance,
	CreatedBy = @CreatedBy,
	IsCompleted = @IsCompleted
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_LineupView_GetRecord
  @Id int
AS

SELECT
	LineupView.Id AS LineupView_Id,
	LineupView.LineupId AS LineupView_LineupId,
	LineupView.WitnessFirstName AS LineupView_WitnessFirstName,
	LineupView.WitnessLastName AS LineupView_WitnessLastName,
	LineupView.Relevance AS LineupView_Relevance,
	LineupView.Created AS LineupView_Created,
	LineupView.CreatedBy AS LineupView_CreatedBy,
	LineupView.IsCompleted AS LineupView_IsCompleted,
	[LineupView].WitnessLastName + ', ' + [LineupView].WitnessFirstName AS LineupView_FullName
FROM LineupView
LEFT OUTER JOIN Lineup ON LineupView.LineupId = Lineup.Id 
WHERE LineupView.Id = @Id

CREATE PROCEDURE dbo.a_LineupView_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		LineupView.Id AS LineupView_Id,
		LineupView.LineupId AS LineupView_LineupId,
		LineupView.WitnessFirstName AS LineupView_WitnessFirstName,
		LineupView.WitnessLastName AS LineupView_WitnessLastName,
		LineupView.Relevance AS LineupView_Relevance,
		LineupView.Created AS LineupView_Created,
		LineupView.CreatedBy AS LineupView_CreatedBy,
		LineupView.IsCompleted AS LineupView_IsCompleted,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM LineupView
	LEFT OUTER JOIN Lineup ON LineupView.LineupId = Lineup.Id 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_LineupView_GetList

AS

SELECT
	LineupView.Id AS LineupView_Id
FROM LineupView
ORDER BY WitnessLastName


CREATE PROCEDURE dbo.a_LineupView_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempLineupView
(
	TempId int IDENTITY PRIMARY KEY,
	LineupView_Id int,
	LineupView_LineupId int,
	LineupView_WitnessFirstName varchar(25),
	LineupView_WitnessLastName varchar(25),
	LineupView_Relevance varchar(4000),
	LineupView_Created datetime,
	LineupView_CreatedBy varchar(50),
	LineupView_IsCompleted bit,
	LineupView_FullName varchar(50)
)

INSERT INTO #TempLineupView
(
	LineupView_Id,
	LineupView_LineupId,
	LineupView_WitnessFirstName,
	LineupView_WitnessLastName,
	LineupView_Relevance,
	LineupView_Created,
	LineupView_CreatedBy,
	LineupView_IsCompleted,
	LineupView_FullName
)
EXEC
('
	SELECT
		LineupView.Id AS LineupView_Id,
		LineupView.LineupId AS LineupView_LineupId,
		LineupView.WitnessFirstName AS LineupView_WitnessFirstName,
		LineupView.WitnessLastName AS LineupView_WitnessLastName,
		LineupView.Relevance AS LineupView_Relevance,
		LineupView.Created AS LineupView_Created,
		LineupView.CreatedBy AS LineupView_CreatedBy,
		LineupView.IsCompleted AS LineupView_IsCompleted,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM LineupView
	LEFT OUTER JOIN Lineup ON LineupView.LineupId = Lineup.Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	LineupView_Id,
	LineupView_LineupId,
	LineupView_WitnessFirstName,
	LineupView_WitnessLastName,
	LineupView_Relevance,
	LineupView_Created,
	LineupView_CreatedBy,
	LineupView_IsCompleted,
	LineupView_FullName
FROM #TempLineupView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_LineupView_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempLineupView
(
	TempId int IDENTITY PRIMARY KEY,
	LineupView_Id int,
	LineupView_LineupId int,
	LineupView_WitnessFirstName varchar(25),
	LineupView_WitnessLastName varchar(25),
	LineupView_Relevance varchar(4000),
	LineupView_Created datetime,
	LineupView_CreatedBy varchar(50),
	LineupView_IsCompleted bit,
	LineupView_FullName varchar(50)
)

INSERT INTO #TempLineupView
(
	LineupView_Id,
	LineupView_LineupId,
	LineupView_WitnessFirstName,
	LineupView_WitnessLastName,
	LineupView_Relevance,
	LineupView_Created,
	LineupView_CreatedBy,
	LineupView_IsCompleted,
	LineupView_FullName
)
EXEC
('
	SELECT
		LineupView.Id AS LineupView_Id,
		LineupView.LineupId AS LineupView_LineupId,
		LineupView.WitnessFirstName AS LineupView_WitnessFirstName,
		LineupView.WitnessLastName AS LineupView_WitnessLastName,
		LineupView.Relevance AS LineupView_Relevance,
		LineupView.Created AS LineupView_Created,
		LineupView.CreatedBy AS LineupView_CreatedBy,
		LineupView.IsCompleted AS LineupView_IsCompleted,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM LineupView
	LEFT OUTER JOIN Lineup ON LineupView.LineupId = Lineup.Id 
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
	LineupView_Id,
	LineupView_LineupId,
	LineupView_WitnessFirstName,
	LineupView_WitnessLastName,
	LineupView_Relevance,
	LineupView_Created,
	LineupView_CreatedBy,
	LineupView_IsCompleted,
	LineupView_FullName
FROM #TempLineupView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_LineupView_GetRecordsByLineupField
  @LineupId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		LineupView.Id AS LineupView_Id,
		LineupView.LineupId AS LineupView_LineupId,
		LineupView.WitnessFirstName AS LineupView_WitnessFirstName,
		LineupView.WitnessLastName AS LineupView_WitnessLastName,
		LineupView.Relevance AS LineupView_Relevance,
		LineupView.Created AS LineupView_Created,
		LineupView.CreatedBy AS LineupView_CreatedBy,
		LineupView.IsCompleted AS LineupView_IsCompleted,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM LineupView
	LEFT OUTER JOIN Lineup ON LineupView.LineupId = Lineup.Id 
	WHERE LineupView.LineupId = ' + @LineupId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_LineupView_GetRecordsByLineupFieldP
  @LineupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempLineupView
(
	TempId int IDENTITY PRIMARY KEY,
	LineupView_Id int,
	LineupView_LineupId int,
	LineupView_WitnessFirstName varchar(25),
	LineupView_WitnessLastName varchar(25),
	LineupView_Relevance varchar(4000),
	LineupView_Created datetime,
	LineupView_CreatedBy varchar(50),
	LineupView_IsCompleted bit,
	LineupView_FullName varchar(50)
)

INSERT INTO #TempLineupView
(
	LineupView_Id,
	LineupView_LineupId,
	LineupView_WitnessFirstName,
	LineupView_WitnessLastName,
	LineupView_Relevance,
	LineupView_Created,
	LineupView_CreatedBy,
	LineupView_IsCompleted,
	LineupView_FullName
)
EXEC
('
	SELECT
		LineupView.Id AS LineupView_Id,
		LineupView.LineupId AS LineupView_LineupId,
		LineupView.WitnessFirstName AS LineupView_WitnessFirstName,
		LineupView.WitnessLastName AS LineupView_WitnessLastName,
		LineupView.Relevance AS LineupView_Relevance,
		LineupView.Created AS LineupView_Created,
		LineupView.CreatedBy AS LineupView_CreatedBy,
		LineupView.IsCompleted AS LineupView_IsCompleted,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM LineupView
	LEFT OUTER JOIN Lineup ON LineupView.LineupId = Lineup.Id 
	WHERE LineupView.LineupId = ' + @LineupId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	LineupView_Id,
	LineupView_LineupId,
	LineupView_WitnessFirstName,
	LineupView_WitnessLastName,
	LineupView_Relevance,
	LineupView_Created,
	LineupView_CreatedBy,
	LineupView_IsCompleted,
	LineupView_FullName
FROM #TempLineupView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_LineupView_GetRecordsByLineupFieldPS
  @LineupId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempLineupView
(
	TempId int IDENTITY PRIMARY KEY,
	LineupView_Id int,
	LineupView_LineupId int,
	LineupView_WitnessFirstName varchar(25),
	LineupView_WitnessLastName varchar(25),
	LineupView_Relevance varchar(4000),
	LineupView_Created datetime,
	LineupView_CreatedBy varchar(50),
	LineupView_IsCompleted bit,
	LineupView_FullName varchar(50)
)

INSERT INTO #TempLineupView
(
	LineupView_Id,
	LineupView_LineupId,
	LineupView_WitnessFirstName,
	LineupView_WitnessLastName,
	LineupView_Relevance,
	LineupView_Created,
	LineupView_CreatedBy,
	LineupView_IsCompleted,
	LineupView_FullName
)
EXEC
('
	SELECT
		LineupView.Id AS LineupView_Id,
		LineupView.LineupId AS LineupView_LineupId,
		LineupView.WitnessFirstName AS LineupView_WitnessFirstName,
		LineupView.WitnessLastName AS LineupView_WitnessLastName,
		LineupView.Relevance AS LineupView_Relevance,
		LineupView.Created AS LineupView_Created,
		LineupView.CreatedBy AS LineupView_CreatedBy,
		LineupView.IsCompleted AS LineupView_IsCompleted,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM LineupView
	LEFT OUTER JOIN Lineup ON LineupView.LineupId = Lineup.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND LineupView.LineupId = ' + @LineupId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	LineupView_Id,
	LineupView_LineupId,
	LineupView_WitnessFirstName,
	LineupView_WitnessLastName,
	LineupView_Relevance,
	LineupView_Created,
	LineupView_CreatedBy,
	LineupView_IsCompleted,
	LineupView_FullName
FROM #TempLineupView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_PhotoView_Create
  @LineupViewId int,
  @PhotoId int,
  @SuspectId int,
  @IsSuspect bit,
  @Result varchar(10),
  @Certainty varchar(1000)
AS

INSERT INTO PhotoView
(
	LineupViewId, 
	PhotoId, 
	SuspectId, 
	IsSuspect, 
	[Result], 
	Certainty
)
VALUES
(
	@LineupViewId, 
	@PhotoId, 
	@SuspectId, 
	@IsSuspect, 
	@Result, 
	@Certainty
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_PhotoView_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM PhotoView
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_PhotoView_GetRecord
  @Id int
AS

SELECT
	PhotoView.Id AS PhotoView_Id,
	PhotoView.LineupViewId AS PhotoView_LineupViewId,
	PhotoView.PhotoId AS PhotoView_PhotoId,
	PhotoView.SuspectId AS PhotoView_SuspectId,
	PhotoView.IsSuspect AS PhotoView_IsSuspect,
	PhotoView.[Result] AS PhotoView_Result,
	PhotoView.Certainty AS PhotoView_Certainty,
	[LineupView].WitnessLastName + ', ' + [LineupView].WitnessFirstName AS LineupView_FullName
FROM PhotoView
LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
WHERE PhotoView.Id = @Id

CREATE PROCEDURE dbo.a_PhotoView_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_PhotoView_GetList

AS

SELECT
	PhotoView.Id AS PhotoView_Id
FROM PhotoView
ORDER BY PhotoView.Id


CREATE PROCEDURE dbo.a_PhotoView_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhotoView
(
	TempId int IDENTITY PRIMARY KEY,
	PhotoView_Id int,
	PhotoView_LineupViewId int,
	PhotoView_PhotoId int,
	PhotoView_SuspectId int,
	PhotoView_IsSuspect bit,
	PhotoView_Result varchar(10),
	PhotoView_Certainty varchar(1000),
	LineupView_FullName varchar(50)
)

INSERT INTO #TempPhotoView
(
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
)
EXEC
('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
FROM #TempPhotoView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsPS
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhotoView
(
	TempId int IDENTITY PRIMARY KEY,
	PhotoView_Id int,
	PhotoView_LineupViewId int,
	PhotoView_PhotoId int,
	PhotoView_SuspectId int,
	PhotoView_IsSuspect bit,
	PhotoView_Result varchar(10),
	PhotoView_Certainty varchar(1000),
	LineupView_FullName varchar(50)
)

INSERT INTO #TempPhotoView
(
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
)
EXEC
('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
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
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
FROM #TempPhotoView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsByLineupViewField
  @LineupViewId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	WHERE PhotoView.LineupViewId = ' + @LineupViewId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsByLineupViewFieldP
  @LineupViewId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhotoView
(
	TempId int IDENTITY PRIMARY KEY,
	PhotoView_Id int,
	PhotoView_LineupViewId int,
	PhotoView_PhotoId int,
	PhotoView_SuspectId int,
	PhotoView_IsSuspect bit,
	PhotoView_Result varchar(10),
	PhotoView_Certainty varchar(1000),
	LineupView_FullName varchar(50)
)

INSERT INTO #TempPhotoView
(
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
)
EXEC
('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	WHERE PhotoView.LineupViewId = ' + @LineupViewId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
FROM #TempPhotoView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsByLineupViewFieldPS
  @LineupViewId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhotoView
(
	TempId int IDENTITY PRIMARY KEY,
	PhotoView_Id int,
	PhotoView_LineupViewId int,
	PhotoView_PhotoId int,
	PhotoView_SuspectId int,
	PhotoView_IsSuspect bit,
	PhotoView_Result varchar(10),
	PhotoView_Certainty varchar(1000),
	LineupView_FullName varchar(50)
)

INSERT INTO #TempPhotoView
(
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
)
EXEC
('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND PhotoView.LineupViewId = ' + @LineupViewId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
FROM #TempPhotoView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsByPhotoField
  @PhotoId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	WHERE PhotoView.PhotoId = ' + @PhotoId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsByPhotoFieldP
  @PhotoId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhotoView
(
	TempId int IDENTITY PRIMARY KEY,
	PhotoView_Id int,
	PhotoView_LineupViewId int,
	PhotoView_PhotoId int,
	PhotoView_SuspectId int,
	PhotoView_IsSuspect bit,
	PhotoView_Result varchar(10),
	PhotoView_Certainty varchar(1000),
	LineupView_FullName varchar(50)
)

INSERT INTO #TempPhotoView
(
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
)
EXEC
('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	WHERE PhotoView.PhotoId = ' + @PhotoId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
FROM #TempPhotoView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsByPhotoFieldPS
  @PhotoId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhotoView
(
	TempId int IDENTITY PRIMARY KEY,
	PhotoView_Id int,
	PhotoView_LineupViewId int,
	PhotoView_PhotoId int,
	PhotoView_SuspectId int,
	PhotoView_IsSuspect bit,
	PhotoView_Result varchar(10),
	PhotoView_Certainty varchar(1000),
	LineupView_FullName varchar(50)
)

INSERT INTO #TempPhotoView
(
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
)
EXEC
('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND PhotoView.PhotoId = ' + @PhotoId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
FROM #TempPhotoView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsBySuspectField
  @SuspectId int,
  @SortExp varchar(100)
AS

EXEC('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	WHERE PhotoView.SuspectId = ' + @SuspectId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsBySuspectFieldP
  @SuspectId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempPhotoView
(
	TempId int IDENTITY PRIMARY KEY,
	PhotoView_Id int,
	PhotoView_LineupViewId int,
	PhotoView_PhotoId int,
	PhotoView_SuspectId int,
	PhotoView_IsSuspect bit,
	PhotoView_Result varchar(10),
	PhotoView_Certainty varchar(1000),
	LineupView_FullName varchar(50)
)

INSERT INTO #TempPhotoView
(
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
)
EXEC
('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	WHERE PhotoView.SuspectId = ' + @SuspectId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
FROM #TempPhotoView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_PhotoView_GetRecordsBySuspectFieldPS
  @SuspectId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int,
  @SearchField varchar(25),
  @SearchKeyword varchar(25)
AS



CREATE TABLE #TempPhotoView
(
	TempId int IDENTITY PRIMARY KEY,
	PhotoView_Id int,
	PhotoView_LineupViewId int,
	PhotoView_PhotoId int,
	PhotoView_SuspectId int,
	PhotoView_IsSuspect bit,
	PhotoView_Result varchar(10),
	PhotoView_Certainty varchar(1000),
	LineupView_FullName varchar(50)
)

INSERT INTO #TempPhotoView
(
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
)
EXEC
('
	SELECT
		PhotoView.Id AS PhotoView_Id,
		PhotoView.LineupViewId AS PhotoView_LineupViewId,
		PhotoView.PhotoId AS PhotoView_PhotoId,
		PhotoView.SuspectId AS PhotoView_SuspectId,
		PhotoView.IsSuspect AS PhotoView_IsSuspect,
		PhotoView.[Result] AS PhotoView_Result,
		PhotoView.Certainty AS PhotoView_Certainty,
		[LineupView].WitnessLastName + '', '' + [LineupView].WitnessFirstName AS LineupView_FullName
	FROM PhotoView
	LEFT OUTER JOIN LineupView ON PhotoView.LineupViewId = LineupView.Id 
	LEFT OUTER JOIN Photo ON PhotoView.PhotoId = Photo.Id 
	LEFT OUTER JOIN Suspect ON PhotoView.SuspectId = Suspect.Id 
	WHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''
	AND PhotoView.SuspectId = ' + @SuspectId + '
	ORDER BY ' + @SortExp
)

DECLARE @rows int 
SET @rows = @@ROWCOUNT 

DECLARE @first int 
DECLARE @last int 
SET @first = (@pageNum-1) * @pageSize + 1 
SET @last = @first + @pageSize - 1 

SELECT
	PhotoView_Id,
	PhotoView_LineupViewId,
	PhotoView_PhotoId,
	PhotoView_SuspectId,
	PhotoView_IsSuspect,
	PhotoView_Result,
	PhotoView_Certainty,
	LineupView_FullName
FROM #TempPhotoView
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_User_Create
  @Login varchar(50),
  @Password varbinary(16),
  @FirstName varchar(25),
  @LastName varchar(25),
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
	getDate(), 
	getDate(), 
	@ModifiedBy
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_User_Delete
  @Id int,
  @DeleteDependents bit
AS

IF (@DeleteDependents = 0)
	BEGIN

	IF ((SELECT COUNT(*) FROM UserLog WHERE UserId = @Id) > 0)
		RETURN -16

	END 
ELSE 
	BEGIN

	DELETE FROM UserLog WHERE UserId = @Id

	END 

DELETE FROM UserCaseLink WHERE UserId = @Id
DELETE FROM UserRoleLink WHERE UserId = @Id
DELETE FROM [User]
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_User_Update
  @Id int,
  @Login varchar(50),
  @Password varbinary(16),
  @FirstName varchar(25),
  @LastName varchar(25),
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
	Modified = getDate(),
	ModifiedBy = @ModifiedBy
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_User_GetRecord
  @Id int
AS

SELECT
	[User].Id AS User_Id,
	[User].Login AS User_Login,
	[User].Password AS User_Password,
	[User].FirstName AS User_FirstName,
	[User].LastName AS User_LastName,
	[User].Created AS User_Created,
	[User].Modified AS User_Modified,
	[User].ModifiedBy AS User_ModifiedBy,
	[User].LastName + ', ' + [User].FirstName AS User_FullName
FROM [User]
WHERE [User].Id = @Id

CREATE PROCEDURE dbo.a_User_GetRecords
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
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_User_GetList

AS

SELECT
	[User].Id AS User_Id,
	[User].LastName + ', ' + [User].FirstName AS User_FullName
FROM [User]
ORDER BY LastName


CREATE PROCEDURE dbo.a_User_GetRecordsP
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
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetRecordsPS
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
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetRecordsByCaseLink
  @CaseId int,
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
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_User_GetRecordsByCaseLinkP
  @CaseId int,
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
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
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
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetRecordsByCaseLinkPS
  @CaseId int,
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
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
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
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetRecordsByRoleLink
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
		[User].Created AS User_Created,
		[User].Modified AS User_Modified,
		[User].ModifiedBy AS User_ModifiedBy,
		[User].LastName + '', '' + [User].FirstName AS User_FullName
	FROM [User]
	JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_User_GetRecordsByRoleLinkP
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
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetRecordsByRoleLinkPS
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
	User_Created,
	User_Modified,
	User_ModifiedBy,
	User_FullName
FROM #TempUser
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetCaseLinks
  @CaseId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserCaseLink.CaseId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_User_GetCaseLinksP
  @CaseId int,
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
		CAST (ISNULL(UserCaseLink.CaseId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
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

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetCaseLinksPS
  @CaseId int,
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
		CAST (ISNULL(UserCaseLink.CaseId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
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

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetCaseLinksByRole
  @CaseId int,
  @RoleId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[User].Id AS User_Id,
		[User].LastName + '', '' + [User].FirstName AS User_FullName,
		CAST (ISNULL(UserCaseLink.CaseId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
	INNER JOIN UserRoleLink ON UserRoleLink.UserId = [User].Id AND UserRoleLink.RoleId = ' + @RoleId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_User_GetCaseLinksByRoleP
  @CaseId int,
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
		CAST (ISNULL(UserCaseLink.CaseId, 0) as bit) AS Selected
	FROM [User]
	LEFT OUTER JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
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

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetRoleLinks
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
)

CREATE PROCEDURE dbo.a_User_GetRoleLinksP
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

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetRoleLinksPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_User_GetRoleLinksByCase
  @RoleId int,
  @CaseId int,
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
	INNER JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_User_GetRoleLinksByCaseP
  @RoleId int,
  @CaseId int,
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
	INNER JOIN UserCaseLink ON UserCaseLink.UserId = [User].Id AND UserCaseLink.CaseId = ' + @CaseId + ' 
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

SELECT @rows

CREATE PROCEDURE dbo.a_Role_Create
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM [Role] WHERE Description = @Description)
	BEGIN
		SELECT -17
		RETURN
	END

INSERT INTO [Role]
(
	Description
)
VALUES
(
	@Description
)

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_Role_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM RolePermissionLink WHERE RoleId = @Id
DELETE FROM UserRoleLink WHERE RoleId = @Id
DELETE FROM [Role]
WHERE Id = @Id

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_Role_Update
  @Id int,
  @Description varchar(25)
AS

IF EXISTS (SELECT * FROM [Role] WHERE Description = @Description AND Id !=  @Id)
	BEGIN
		SELECT -17
		RETURN
	END

UPDATE [Role]
SET
	Description = @Description
WHERE Id = @Id

RETURN 0

CREATE PROCEDURE dbo.a_Role_GetRecord
  @Id int
AS

SELECT
	[Role].Id AS Role_Id,
	[Role].Description AS Role_Description
FROM [Role]
WHERE [Role].Id = @Id

CREATE PROCEDURE dbo.a_Role_GetRecords
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description
	FROM [Role]
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Role_GetList

AS

SELECT
	[Role].Id AS Role_Id,
	[Role].Description AS Role_Description
FROM [Role]
ORDER BY [Role].Id


CREATE PROCEDURE dbo.a_Role_GetRecordsP
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25)
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description
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
	Role_Description
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetRecordsPS
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
	Role_Description varchar(25)
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description
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
	Role_Description
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetRecordsByPermissionLink
  @PermissionId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description
	FROM [Role]
	JOIN RolePermissionLink ON RolePermissionLink.RoleId = [Role].Id AND RolePermissionLink.PermissionId = ' + @PermissionId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Role_GetRecordsByPermissionLinkP
  @PermissionId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25)
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description
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
	Role_Description
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetRecordsByPermissionLinkPS
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
	Role_Description varchar(25)
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description
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
	Role_Description
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetRecordsByUserLink
  @UserId int,
  @SortExp varchar(100)
AS

EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description
	FROM [Role]
	JOIN UserRoleLink ON UserRoleLink.RoleId = [Role].Id AND UserRoleLink.UserId = ' + @UserId + ' 
	ORDER BY ' + @SortExp
)

CREATE PROCEDURE dbo.a_Role_GetRecordsByUserLinkP
  @UserId int,
  @SortExp varchar(100),
  @PageSize int,
  @PageNum int
AS



CREATE TABLE #TempRole
(
	TempId int IDENTITY PRIMARY KEY,
	Role_Id int,
	Role_Description varchar(25)
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description
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
	Role_Description
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetRecordsByUserLinkPS
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
	Role_Description varchar(25)
)

INSERT INTO #TempRole
(
	Role_Id,
	Role_Description
)
EXEC
('
	SELECT
		[Role].Id AS Role_Id,
		[Role].Description AS Role_Description
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
	Role_Description
FROM #TempRole
WHERE TempId >= @first AND TempId <= @last 

SELECT @rows

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
)

CREATE PROCEDURE dbo.a_Role_GetPermissionLinksP
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

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetPermissionLinksPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetPermissionLinksByUser
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
)

CREATE PROCEDURE dbo.a_Role_GetPermissionLinksByUserP
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

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetUserLinks
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
)

CREATE PROCEDURE dbo.a_Role_GetUserLinksP
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

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetUserLinksPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_Role_GetUserLinksByPermission
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
)

CREATE PROCEDURE dbo.a_Role_GetUserLinksByPermissionP
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

SELECT @rows

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
WHERE Permission.Id = @Id

CREATE PROCEDURE dbo.a_Permission_GetRecords
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
)

CREATE PROCEDURE dbo.a_Permission_GetList

AS

SELECT
	Permission.Id AS Permission_Id,
	Permission.Description AS Permission_Description
FROM Permission
ORDER BY Permission.Description


CREATE PROCEDURE dbo.a_Permission_GetRecordsP
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

SELECT @rows

CREATE PROCEDURE dbo.a_Permission_GetRecordsPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_Permission_GetRecordsByPermissionCategoryField
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
)

CREATE PROCEDURE dbo.a_Permission_GetRecordsByPermissionCategoryFieldP
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

SELECT @rows

CREATE PROCEDURE dbo.a_Permission_GetRecordsByPermissionCategoryFieldPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_Permission_GetRecordsByRoleLink
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
)

CREATE PROCEDURE dbo.a_Permission_GetRecordsByRoleLinkP
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

SELECT @rows

CREATE PROCEDURE dbo.a_Permission_GetRecordsByRoleLinkPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_Permission_GetRoleLinks
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
)

CREATE PROCEDURE dbo.a_Permission_GetRoleLinksP
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

SELECT @rows

CREATE PROCEDURE dbo.a_Permission_GetRoleLinksPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_PermissionCategory_GetRecord
  @Id int
AS

SELECT
	PermissionCategory.Id AS PermissionCategory_Id,
	PermissionCategory.Description AS PermissionCategory_Description,
	PermissionCategory.Rank AS PermissionCategory_Rank
FROM PermissionCategory
WHERE PermissionCategory.Id = @Id

CREATE PROCEDURE dbo.a_PermissionCategory_GetRecords
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
)

CREATE PROCEDURE dbo.a_PermissionCategory_GetList

AS

SELECT
	PermissionCategory.Id AS PermissionCategory_Id,
	PermissionCategory.Description AS PermissionCategory_Description
FROM PermissionCategory
ORDER BY PermissionCategory.Id


CREATE PROCEDURE dbo.a_PermissionCategory_GetRecordsP
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

SELECT @rows

CREATE PROCEDURE dbo.a_PermissionCategory_GetRecordsPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_UserLog_Create
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

SELECT @@IDENTITY

CREATE PROCEDURE dbo.a_UserLog_Delete
  @Id int,
  @DeleteDependents bit
AS

DELETE FROM UserLog
WHERE Id = @Id

RETURN @@ROWCOUNT

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
WHERE UserLog.Id = @Id

CREATE PROCEDURE dbo.a_UserLog_GetRecords
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
)

CREATE PROCEDURE dbo.a_UserLog_GetList

AS

SELECT
	UserLog.Id AS UserLog_Id
FROM UserLog
ORDER BY UserLog.Id


CREATE PROCEDURE dbo.a_UserLog_GetRecordsP
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

SELECT @rows

CREATE PROCEDURE dbo.a_UserLog_GetRecordsPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_UserLog_GetRecordsByUserField
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
)

CREATE PROCEDURE dbo.a_UserLog_GetRecordsByUserFieldP
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

SELECT @rows

CREATE PROCEDURE dbo.a_UserLog_GetRecordsByUserFieldPS
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

SELECT @rows

CREATE PROCEDURE dbo.a_LineupPhotoLink_Create
  @LineupId int,
  @PhotoId int
AS

IF (SELECT COUNT(*) FROM LineupPhotoLink WHERE LineupId = @LineupId AND PhotoId = @PhotoId) > 0
	RETURN -16

INSERT INTO LineupPhotoLink
(LineupId, PhotoId)
VALUES (@LineupId, @PhotoId)

RETURN 0

CREATE PROCEDURE dbo.a_LineupPhotoLink_CreateAllByLineup
  @LineupId int
AS

INSERT INTO LineupPhotoLink
(LineupId, PhotoId)
SELECT @LineupId, Id FROM Photo WHERE NOT EXISTS (SELECT * FROM LineupPhotoLink WHERE LineupId = @LineupId AND PhotoId = Id)


RETURN 0

CREATE PROCEDURE dbo.a_LineupPhotoLink_CreateAllByPhoto
  @PhotoId int
AS

INSERT INTO LineupPhotoLink
(LineupId, PhotoId)
SELECT Id, @PhotoId FROM Lineup WHERE NOT EXISTS (SELECT * FROM LineupPhotoLink WHERE LineupId = Id AND PhotoId = @PhotoId)


RETURN 0

CREATE PROCEDURE dbo.a_LineupPhotoLink_Delete
  @LineupId int,
  @PhotoId int
AS

DELETE FROM LineupPhotoLink
WHERE LineupId = @LineupId AND PhotoId = @PhotoId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_LineupPhotoLink_DeleteAllByLineup
  @LineupId int
AS

DELETE FROM LineupPhotoLink
WHERE LineupId = @LineupId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_LineupPhotoLink_DeleteAllByPhoto
  @PhotoId int
AS

DELETE FROM LineupPhotoLink
WHERE PhotoId = @PhotoId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_UserRoleLink_Create
  @UserId int,
  @RoleId int
AS

IF (SELECT COUNT(*) FROM UserRoleLink WHERE UserId = @UserId AND RoleId = @RoleId) > 0
	RETURN -16

INSERT INTO UserRoleLink
(UserId, RoleId)
VALUES (@UserId, @RoleId)

RETURN 0

CREATE PROCEDURE dbo.a_UserRoleLink_CreateAllByUser
  @UserId int
AS

INSERT INTO UserRoleLink
(UserId, RoleId)
SELECT @UserId, Id FROM [Role] WHERE NOT EXISTS (SELECT * FROM UserRoleLink WHERE UserId = @UserId AND RoleId = Id)


RETURN 0

CREATE PROCEDURE dbo.a_UserRoleLink_CreateAllByRole
  @RoleId int
AS

INSERT INTO UserRoleLink
(UserId, RoleId)
SELECT Id, @RoleId FROM [User] WHERE NOT EXISTS (SELECT * FROM UserRoleLink WHERE UserId = Id AND RoleId = @RoleId)


RETURN 0

CREATE PROCEDURE dbo.a_UserRoleLink_Delete
  @UserId int,
  @RoleId int
AS

DELETE FROM UserRoleLink
WHERE UserId = @UserId AND RoleId = @RoleId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_UserRoleLink_DeleteAllByUser
  @UserId int
AS

DELETE FROM UserRoleLink
WHERE UserId = @UserId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_UserRoleLink_DeleteAllByRole
  @RoleId int
AS

DELETE FROM UserRoleLink
WHERE RoleId = @RoleId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_RolePermissionLink_Create
  @RoleId int,
  @PermissionId int
AS

IF (SELECT COUNT(*) FROM RolePermissionLink WHERE RoleId = @RoleId AND PermissionId = @PermissionId) > 0
	RETURN -16

INSERT INTO RolePermissionLink
(RoleId, PermissionId)
VALUES (@RoleId, @PermissionId)

RETURN 0

CREATE PROCEDURE dbo.a_RolePermissionLink_CreateAllByRole
  @RoleId int
AS

INSERT INTO RolePermissionLink
(RoleId, PermissionId)
SELECT @RoleId, Id FROM Permission WHERE NOT EXISTS (SELECT * FROM RolePermissionLink WHERE RoleId = @RoleId AND PermissionId = Id)


RETURN 0

CREATE PROCEDURE dbo.a_RolePermissionLink_CreateAllByPermission
  @PermissionId int
AS

INSERT INTO RolePermissionLink
(RoleId, PermissionId)
SELECT Id, @PermissionId FROM [Role] WHERE NOT EXISTS (SELECT * FROM RolePermissionLink WHERE RoleId = Id AND PermissionId = @PermissionId)


RETURN 0

CREATE PROCEDURE dbo.a_RolePermissionLink_Delete
  @RoleId int,
  @PermissionId int
AS

DELETE FROM RolePermissionLink
WHERE RoleId = @RoleId AND PermissionId = @PermissionId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_RolePermissionLink_DeleteAllByRole
  @RoleId int
AS

DELETE FROM RolePermissionLink
WHERE RoleId = @RoleId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_RolePermissionLink_DeleteAllByPermission
  @PermissionId int
AS

DELETE FROM RolePermissionLink
WHERE PermissionId = @PermissionId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_UserCaseLink_Create
  @UserId int,
  @CaseId int
AS

IF (SELECT COUNT(*) FROM UserCaseLink WHERE UserId = @UserId AND CaseId = @CaseId) > 0
	RETURN -16

INSERT INTO UserCaseLink
(UserId, CaseId)
VALUES (@UserId, @CaseId)

RETURN 0

CREATE PROCEDURE dbo.a_UserCaseLink_CreateAllByUser
  @UserId int
AS

INSERT INTO UserCaseLink
(UserId, CaseId)
SELECT @UserId, Id FROM [Case] WHERE NOT EXISTS (SELECT * FROM UserCaseLink WHERE UserId = @UserId AND CaseId = Id)


RETURN 0

CREATE PROCEDURE dbo.a_UserCaseLink_CreateAllByCase
  @CaseId int
AS

INSERT INTO UserCaseLink
(UserId, CaseId)
SELECT Id, @CaseId FROM [User] WHERE NOT EXISTS (SELECT * FROM UserCaseLink WHERE UserId = Id AND CaseId = @CaseId)


RETURN 0

CREATE PROCEDURE dbo.a_UserCaseLink_Delete
  @UserId int,
  @CaseId int
AS

DELETE FROM UserCaseLink
WHERE UserId = @UserId AND CaseId = @CaseId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_UserCaseLink_DeleteAllByUser
  @UserId int
AS

DELETE FROM UserCaseLink
WHERE UserId = @UserId

RETURN @@ROWCOUNT

CREATE PROCEDURE dbo.a_UserCaseLink_DeleteAllByCase
  @CaseId int
AS

DELETE FROM UserCaseLink
WHERE CaseId = @CaseId

RETURN @@ROWCOUNT