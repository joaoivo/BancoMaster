USE BancoMaster
GO
CREATE OR ALTER PROCEDURE pr_routes_sel 
	 @guid uniqueidentifier = null
	,@origin nvarchar(3) = NULL
	,@Destintion nvarchar(3) = NULL

AS BEGIN 
DECLARE @query nvarchar(max)='
	SELECT [rtsGuid]
			,[rtsOrigin]
			,[rtsDestintion]
			,[rtsPrice]
	FROM [BancoMaster].[dbo].[tbRoutes]'

	DECLARE @where nvarchar(max)=null

	IF @guid IS NOT NULL SET @where = CONCAT(' ([rtsGuid]=''',@guid,''') ')

	IF @origin IS NOT NULL 
		BEGIN
		IF @where IS NOT NULL set @where = CONCAT(@where, ' and ')
		SET @where = CONCAT(@where,'([rtsOrigin] = ''',@origin,''')')
		END

	IF @Destintion IS NOT NULL 
		BEGIN
		IF @where IS NOT NULL set @where = CONCAT(@where, ' and ')
		SET @where = CONCAT(@where,'([rtsDestintion] = ''',@Destintion,''')')
		END
	
	IF @where IS NOT NULL set @query = CONCAT(@query , ' where ',@where)

	--exec(@query)
	EXEC sp_executesql @query
END; 
go
