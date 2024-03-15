SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[pr_routes_ins] 
	 @origin nvarchar(3) 
	,@Destintion nvarchar(3) 
	,@Price DECIMAL(5,2)
	,@id UNIQUEIDENTIFIER =null OUTPUT 
	,@isOK int =0 OUTPUT
	,@message VARCHAR(max) = null OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION;
			set @id = newid()
			INSERT INTO [BancoMaster].[dbo].[tbRoutes]
					(rtsGuid , rtsOrigin	, rtsDestintion, rtsPrice)
			VALUES (@id		, @origin	, @Destintion	, @Price); 
			SET @isOK = 1
			set @message='Rota inserida com sucesso'
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		SET @id = null
		SET @isOK = 0
		set @message=concat('Erro na Inserção: ',ERROR_MESSAGE())
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
		THROW;
	END CATCH;
END;
GO
