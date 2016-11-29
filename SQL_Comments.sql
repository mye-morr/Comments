DROP TABLE IF EXISTS dbo.Simple;
GO
CREATE TABLE Simple (
	numRow bigint NOT NULL IDENTITY(1,1) CONSTRAINT PK_NumRow PRIMARY KEY NONCLUSTERED (numRow),
	datComment datetime NULL CONSTRAINT DEF_datComment DEFAULT GETDATE(),
	vcCommentBy varchar(50) NULL,
	vcComment varchar(MAX) NULL,
	idClaim varchar(100) NULL,
	vcCategory varchar(100) NULL,
	decExpected decimal(38, 2) NULL,
	decVariance decimal(38, 2) NULL,
	vcClient varchar(100) NULL,
	vcInsurance varchar(100) NULL,
	vcContactName varchar(50) NULL,
	vcContactPhone varchar(50) NULL,
	vcContactEmail varchar(100) NULL,
	vcCallRefNo varchar(50) NULL,
	datFollowUp datetime NULL,
	vcNotes varchar(MAX) NULL
	)
GO
CREATE CLUSTERED INDEX idxIdClaim ON Simple(idClaim)
GO