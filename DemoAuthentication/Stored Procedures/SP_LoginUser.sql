CREATE PROCEDURE [dbo].[SP_LoginUser]
    @Email NVARCHAR(320), 
    @Passwd NVARCHAR(20)
AS
Begin
    Select Id, Nom, Prenom, Email, IsAdmin from [User] where Email = @Email and Passwd = HASHBYTES('SHA2_512', dbo.F_GetPreSalt() + @Passwd + dbo.F_GetPostSalt());    
End
