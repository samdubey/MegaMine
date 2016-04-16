IF OBJECTPROPERTY(OBJECT_ID('quarry.WidgetQuarryColourMaterialCounts'), N'IsProcedure') = 1
	DROP PROCEDURE [quarry].[WidgetQuarryColourMaterialCounts] 
GO

CREATE PROCEDURE [quarry].[WidgetQuarryColourMaterialCounts] 
(
	@CompanyId int
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @QuarryTop5 AS TABLE(QuarryId int)

	INSERT INTO @QuarryTop5(QuarryId)
	SELECT TOP 5 QuarryId 
	FROM quarry.Material
	WHERE CompanyId = @CompanyId
	  AND DeletedInd = 0
	GROUP BY QuarryId
	ORDER BY COUNT(MaterialId) DESC;

	SELECT QuarryColourId = convert(varchar(10), mat.QuarryId) + '-' + convert(varchar(10), mat.MaterialColourId), mat.QuarryId, qry.QuarryName, mat.MaterialColourId, mc.ColourName, MaterialCount = COUNT(mat.MaterialId)
	FROM quarry.Material mat
	JOIN quarry.Quarry qry on qry.QuarryId = mat.QuarryId
	JOIN quarry.MaterialColour mc on mc.MaterialColourId = mat.MaterialColourId
	WHERE EXISTS(SELECT 1 FROM @QuarryTop5 q5 WHERE q5.QuarryId = mat.QuarryId)
	AND mat.CompanyId = @CompanyId AND mat.DeletedInd = 0
	GROUP BY mat.QuarryId, qry.QuarryName, mat.MaterialColourId, mc.ColourName
	ORDER BY ColourName, QuarryName

	SET NOCOUNT OFF
END
go
--exec [quarry].[WidgetQuarryColourMaterialCounts]  4;