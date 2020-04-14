CREATE PROCEDURE [dbo].[SP_RegisterUser]
	@Nom NVARCHAR(75), 
    @Prenom NVARCHAR(75), 
    @Email NVARCHAR(320), 
    @Passwd NVARCHAR(20)
AS
Begin
	Declare @IsAdmin Bit = 0;

    if NOT EXISTS (Select * from [User])
        Set @IsAdmin = 1;

    Insert into [User] (Nom, Prenom, Email, Passwd, IsAdmin) values (@Nom, @Prenom, @Email, HASHBYTES('SHA2_512', dbo.F_GetPreSalt() + @Passwd + dbo.F_GetPostSalt()), @IsAdmin);    
End
