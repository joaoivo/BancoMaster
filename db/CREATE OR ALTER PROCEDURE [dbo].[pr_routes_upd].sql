SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[pr_routes_upd]
	 @guid uniqueidentifier = null
	,@origin nvarchar(3) 
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
			UPDATE [BancoMaster].[dbo].[tbRoutes]
				set	 rtsOrigin		=@origin
						,rtsDestintion	=@Destintion
						,rtsPrice		=@Price
				WHERE	[rtsGuid] = @guid
			SET @isOK = 1
			set @message='Rota atualizada com sucesso'
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		SET @id = null
		SET @isOK = 0
		set @message=concat('Erro na atualização: ',ERROR_MESSAGE())
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
	END CATCH;
END;
GO
