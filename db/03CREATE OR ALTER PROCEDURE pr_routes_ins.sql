USE BancoMaster
GO
CREATE OR ALTER PROCEDURE pr_routes_ins 
	 @origin nvarchar(3) 
	,@Destintion nvarchar(3) 
	,@Price DECIMAL(5,2)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION;
			INSERT INTO [BancoMaster].[dbo].[tbRoutes]
					 (rtsOrigin	, rtsDestintion, rtsPrice)
			VALUES (@origin	, @Destintion	, @Price); 
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
		THROW;
	END CATCH;
END;