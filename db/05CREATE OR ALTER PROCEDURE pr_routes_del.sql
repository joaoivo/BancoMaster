SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[pr_routes_del]
	@guid uniqueidentifier = null

	,@id UNIQUEIDENTIFIER =null OUTPUT 
	,@isOK int =0 OUTPUT
	,@message VARCHAR(max) = null OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION;
			DELETE [BancoMaster].[dbo].[tbRoutes]
				WHERE	[rtsGuid] = @guid
			
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
