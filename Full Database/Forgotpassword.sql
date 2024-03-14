CREATE TABLE Clients(
    -- Unique identifier for each client
	ClientID INT IDENTITY(1,1) PRIMARY KEY,
    -- Client-specific number
	ClientNumber INT NOT NULL,
    -- First name of the client
	FirstName VARCHAR(30) NOT NULL,
    -- Last name of the client
	LastName VARCHAR(30) NOT NULL,
    -- Email address of the client
	Email VARCHAR(50) NOT NULL,
    -- User login username
	UserName VARCHAR(30) NOT NULL,
    -- User login password
	Password VARCHAR(150) NOT NULL,
    -- Gender of the client (BIT: 0 for male, 1 for female, for example)
	Gender BIT NOT NULL,
    -- Age of the client
	Age INT NOT NULL,
	-- Check if user have read terms and conditions
	ReadTerms BIT Default 0,
	-- This will store the amount user used on each category
	AmountUsed INT Default 0,
	-- Check if user have completed his Data information
	signup_completed BIT Default 0,
	-- THIS WILL STORE THE AMOUNT OF TIMES USER CHANGED HIS PASSWORD
	HowManyTimes INT Default 0
);
GO

CREATE TABLE PasswordChangingTime(
	FPID INT IDENTITY(1,1) PRIMARY KEY,
	ClientID INT NOT NULL,
	-- this will store auto generated key to check
	AGK INT Default 0,
	ChangingTime DATETIME,
	FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);
GO


-----------------------------------------------------------------------RESETPASSWORD1


CREATE PROCEDURE CHECKUSERNAMEANDEMAIL(
    @Username VARCHAR(30),
    @Email VARCHAR(50),
    @Result INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UsernameExists BIT;

    -- Check if the username exists
    SELECT @UsernameExists = CASE WHEN EXISTS (SELECT 1 FROM Clients WHERE UserName = @Username) THEN 1 ELSE 0 END;

    IF @UsernameExists = 0
    BEGIN
        -- Username does not exist
        SET @Result = 2; -- Username not available in the database
        PRINT 'Username not available in the database';
    END
    ELSE
    BEGIN
        -- Username exists, now check if the provided email matches
        IF NOT EXISTS (SELECT 1 FROM Clients WHERE UserName = @Username AND Email = @Email)
        BEGIN
            -- Email does not match
            SET @Result = 3; -- Provided email does not match the one associated with the username
            PRINT 'Provided email does not match the one associated with the username';
        END
        ELSE
        BEGIN
            -- Username and email match
            SET @Result = 1; -- Procedure executed successfully
            PRINT 'Username and email match';
        END
    END
END;



-----------------------------------------------------------------------AUTOKEYINSERTER


CREATE PROCEDURE AUTOKEYINSERTER(
	@AKUsername VARCHAR(30),
	@AGK INT,
	@Result INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		DECLARE @ClientID INT; 
		DECLARE @COUNTHMT INT;
		SELECT @ClientID = ClientID FROM Clients WHERE Username = @AKUsername;
		SELECT @COUNTHMT = HowManyTimes FROM Clients WHERE ClientID = @ClientID;
		IF @COUNTHMT = 5
		BEGIN
			SET @Result = 4; -- THIS MEANS THAT THE PASSWORD HAS BEEN RESET ABOUT 5 TIMES
			PRINT 'THIS MEANS THAT THE PASSWORD HAS BEEN RESET ABOUT 5 TIMES';
		END
		ELSE 
		BEGIN
			INSERT INTO PasswordChangingTime(ClientID, AGK, ChangingTime) 
			VALUES(@ClientID, @AGK, GETDATE());
			SET @Result = 10; -- THIS MEAN THIS PROCEDURE WORKED PROPERLY
			PRINT 'THIS MEAN THIS PROCEDURE WORKED PROPERLY';
		END
	END TRY
	BEGIN CATCH
		-- Handle errors
		SET @Result = -992;
		PRINT 'Error: ' + ERROR_MESSAGE(); -- Print the error message for debugging
	END CATCH
END;
GO


-----------------------------------------------------------------------RESETPASSWORD2


CREATE PROCEDURE RESETPASSWORDFINAL(
	@Username VARCHAR(30),
	@Password VARCHAR(150),
	@VerifyAGK INT,
	@Result INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		DECLARE @ClientID INT;
		DECLARE @COUNTHMT INT;
		SELECT @COUNTHMT = HowManyTimes FROM Clients;
		SELECT @ClientID = ClientID FROM Clients WHERE Username = @Username; -- Changed AKUsername to Username
		IF NOT EXISTS(SELECT * FROM PasswordChangingTime WHERE AGK = @VerifyAGK)
		BEGIN
			SET @Result = 5; -- The key user inserted is not the same as the one saved in the database
			PRINT 'The key user inserted is not the same as the one saved in the database';
		END
		ELSE
		BEGIN
			SET @COUNTHMT = @COUNTHMT + 1;
			UPDATE Clients SET Password = @Password WHERE Username = @Username;
			UPDATE Clients SET HowManyTimes = @COUNTHMT WHERE  Username = @Username;
			UPDATE PasswordChangingTime SET AGK = '' WHERE ClientID = @ClientID;
			SET @Result = 19; -- All done, password changed
			PRINT 'All done, password changed';
		END
	END TRY
	BEGIN CATCH
		-- Handle errors
		SET @Result = -992;
		PRINT 'Error: ' + ERROR_MESSAGE(); -- Print the error message for debugging
	END CATCH
END;
GO

-- works hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu hu-hu
