USE BancoMaster
GO
CREATE OR ALTER PROCEDURE pr_routes_upd
	 @guid uniqueidentifier = null
	,@origin nvarchar(3) 
	,@Destintion nvarchar(3) 
	,@Price DECIMAL(5,2)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [BancoMaster].[dbo].[tbRoutes]
				set	 rtsOrigin		=@origin
						,rtsDestintion	=@Destintion
						,rtsPrice		=@Price
				WHERE	[rtsGuid] = @guid
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
		THROW;
	END CATCH;
END;