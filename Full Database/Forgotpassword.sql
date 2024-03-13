-- FIRST I CALL "RESETPASSWORD1" THIS PROCEDURE THEN IF IT RETURN 1 THEN I CALL "AUTOKEYINSERTER"
-- AND WHEN IT RETURNS 10 THEN I CALL "RESETPASSWORD2" AND IF IT RETURNS 19 THAT MEANS EVERYTHING WORKS

CREATE TABLE ForgotPassword(
	FPID INT IDENTITY(1,1) PRIMARY KEY,
	ClientID INT NOT NULL,
	AGK INT NOT NULL,
	HowManyTimes INT NOT NULL,
	ChangingTime DATETIME,
	FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);
GO

CREATE PROCEDURE RESETPASSWORD1(
	@Username VARCHAR(30),
	@Email VARCHAR(50),
	@Result INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ReturnValue INT;
    BEGIN TRY
		IF NOT EXISTS (SELECT * FROM Clients WHERE UserName = @UserName)
		BEGIN
			SET @Result = 2; -- IT MEANS THAT THE USERNAME IS NOT AVAILABEL IN DATABASE
		END
		ELSE
		BEGIN
			IF NOT EXISTS (SELECT * FROM Clients WHERE Email = @Email)
			BEGIN
				SET @Result = 3; -- IT MEANS THAT THE EMAIL IS NOT THE SAME AS THE PREVIOUS ONE
			END
			ELSE
			BEGIN
				SET @Result = 1; -- THIS MEAN THIS PROCEDURE WORKED PROPERLY
			END
		END
	END TRY
    BEGIN CATCH
        -- Handle errors
        SELECT -99 AS @Result,  
               ERROR_NUMBER() AS ErrorNumber, 
               ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO

CREATE PROCEDURE AUTOKEYINSERTER(
	@AKUsername VARCHAR(30),
	@AGK INT,
	@Result INT OUTPUT
)
AS
BEGIN
	SET NOVOUNT ON;
	BEGIN TRY
		DECLARE @ClientID = SELECT ClientID FROM Clients WHERE Username = @AKUsername;
		DECLARE @COUNTHMT = SELECT HowManyTimes FROM ForgotPassword where ClientID = @ClientID;
		IF @COUNTHMT = 5
			SET @Result = 4; -- THIS MEANS THAT THE PASSWORD HAS BEEN RESET ABOUT 5 TIMES
		ELSE 
			@COUNTHMT = @COUNTHMT + 1;
			INSERT INTO ForgotPassword(ClientID, AGK, HowManyTimes, ChangingTime) 
			VALUES(@ClientID, @AGK, @COUNTHMT, @GETDATE);
			SET @Result = 10 -- THIS MEAN THIS PROCEDURE WORKED PROPERLY
	END TRY
	BEGIN CATCH
        -- Handle errors
        SELECT -99 AS @Result,  
               ERROR_NUMBER() AS ErrorNumber, 
               ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO

CREATE PROCEDURE RESETPASSWORD2(
	@Username VARCHAR(30),
	@Password VARCHAR(150),
	@Result INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		DECLARE @ClientID = SELECT ClientID FROM Clients WHERE Username = @AKUsername;
		UPDATE Clients SET Password = @Password WHERE Username = @Username;
		UPDATE ForgotPassword SET AGK = "" WHERE ClientID = @ClientID;
		SET @Result = 19 -- IT MEANS ALL DONE AND PASSWORD IS CHANGED
	END TRY
	BEGIN CATCH
        -- Handle errors
        SELECT -99 AS @Result,  
               ERROR_NUMBER() AS ErrorNumber, 
               ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO