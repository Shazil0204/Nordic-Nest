GO

-- Create a stored procedure named CHECKNAVBAR
CREATE PROCEDURE CHECKNAVBAR(
    @PageName VARCHAR(30) -- Input parameter: name of the page
)
AS
BEGIN
    -- Check if the page exists in Pages table
    IF NOT EXISTS (SELECT 1 FROM Pages WHERE Page = @PageName)
    BEGIN
        -- If page does not exist, return -2
        SELECT -2 AS Result;
        RETURN;
    END

    -- If page exists, select corresponding Navbar Name and URL
    SELECT 
        n.Name, 
        n.URL,
		n.IsAuthBtn
    FROM 
        NavBars n
    INNER JOIN 
        NavbarForPages np ON n.NavID = np.NavID
    INNER JOIN 
        Pages p ON p.PageID = np.PageID
    WHERE 
        p.Page = @PageName;
END
GO

CREATE PROCEDURE CHECKIFCLIENTEXISTS
    @FirstName VARCHAR(30) ,
    @ClientNumber INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Check if client exists by identifying (client name and number)
        IF NOT EXISTS (SELECT 1 FROM Clients WHERE FirstName = @FirstName AND ClientNumber = @ClientNumber)
        BEGIN
            SELECT -2 AS Result, '' AS FirstName, '' AS LastName, 0 AS ClientNumber;
            PRINT 'No client with this name and number exists in our database';
            RETURN;
        END

        -- If client exists, get their details and return a success result
        SELECT FirstName, LastName, ClientNumber, 1 AS Result
        FROM Clients
        WHERE FirstName = @FirstName AND ClientNumber = @ClientNumber;

    END TRY
    BEGIN CATCH
        -- Handle errors
        SELECT -99 AS Result, 
               '' AS FirstName, 
               '' AS LastName, 
               0 AS ClientNumber, 
               ERROR_NUMBER() AS ErrorNumber, 
               ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO

CREATE PROCEDURE USERCONTACTFORM(
	@FirstName VARCHAR(30),
	@LastName VARCHAR(30),
	@IsExistingClient BIT,
	@MessageContent VARCHAR(255),
	@UserEmail VARCHAR(75)
)
AS
BEGIN
	SET NOCOUNT ON;
    BEGIN TRY
        -- INSERTING DATA INTO THE TABLE
		INSERT INTO ContactInquiries (FirstName, LastName, IsExistingClient, SubmissionDateTime, MessageContent, UserEmail, InquiryCompleted)
		VALUES (@FirstName, @LastName, @IsExistingClient, GETDATE(), @MessageContent, @UserEmail, 0);
		
		-- DATA INSERT SUCCESSFULLY
		SELECT 1 AS Result;

    END TRY
    BEGIN CATCH
        -- Handle errors
        SELECT -99 AS Result, 
               '' AS FirstName, 
               '' AS LastName, 
               0 AS ClientNumber, 
               ERROR_NUMBER() AS ErrorNumber, 
               ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO

-- Switch context to the NordicNestDB database
USE NordicNestDB;
GO

CREATE PROCEDURE CheckEmailExist
    @Email VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Count INT;

    -- Check if the email already exists in the Clients table
    SELECT @Count = COUNT(*)
    FROM Clients
    WHERE Email = @Email;

    -- If an email is found, return 1; otherwise, return 0
    IF @Count > 0
        SELECT 1 AS EmailExists;
    ELSE
        SELECT 0 AS EmailExists;
END;
GO

USE NordicNestDB;
GO
CREATE PROCEDURE CheckUsernameExist
    @UserName VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Count INT;

    -- Check if the username already exists in the Clients table
    SELECT @Count = COUNT(*)
    FROM Clients
    WHERE UserName = @UserName;

    -- If a username is found, return 1; otherwise, return 0
    IF @Count > 0
        SELECT 1 AS UsernameExists;
    ELSE
        SELECT 0 AS UsernameExists;
END;
GO
CREATE PROCEDURE AddNewClient
    @FirstName VARCHAR(30),
    @LastName VARCHAR(30),
    @Email VARCHAR(255),
    @UserName VARCHAR(255),
    @Password VARCHAR(255),
    @Gender BIT,
    @Age INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ClientNumber INT;
    DECLARE @Count INT;
    DECLARE @ReturnValue INT = -99; -- Default to error value

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @Count = COUNT(*)
        FROM Clients
        WHERE UserName = @UserName;

        IF @Count > 0
        BEGIN
            SET @ReturnValue = 2; -- Username exists
            SELECT @ReturnValue AS ReturnValue;
            RETURN;
        END

        WHILE 1 = 1
        BEGIN
            SELECT @ClientNumber = CAST(RAND() * 89999999 AS INT) + 10000000;

            SELECT @Count = COUNT(*)
            FROM Clients
            WHERE ClientNumber = @ClientNumber;

            IF @Count = 0
                BREAK;
        END

        INSERT INTO Clients (ClientNumber, FirstName, LastName, Email, UserName, Password, Gender, Age)
        VALUES (@ClientNumber, @FirstName, @LastName, @Email, @UserName, @Password, @Gender, @Age);

        SET @ReturnValue = 1; -- Success

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @ReturnValue = -99; -- Error
    END CATCH

    SELECT @ReturnValue AS ReturnValue; -- Return a result set
END;
GO

CREATE PROCEDURE CHECKUSER
    @UserName VARCHAR(255),
    @ClientID INT OUTPUT, -- New output parameter for ClientID
    @Password VARCHAR(255) OUTPUT,
    @Result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Check if the username exists with case-sensitive comparison
        IF EXISTS (SELECT * FROM Clients WHERE UserName COLLATE Latin1_General_BIN = @UserName)
        BEGIN
            -- Get the ClientID and password for the username
            SELECT @ClientID = ClientID, @Password = Password FROM Clients WHERE UserName COLLATE Latin1_General_BIN = @UserName;
            SET @Result = 1; -- Indicate success
        END
        ELSE
        BEGIN
            SET @ClientID = NULL; -- No ClientID to return
            SET @Password = NULL; -- No password to return
            SET @Result = -2; -- Username does not exist or case mismatch
        END
    END TRY
    BEGIN CATCH
        -- If an error occurs, set the result to -99
        SET @ClientID = NULL;
        SET @Password = NULL;
        SET @Result = -99;
    END CATCH
END
GO


CREATE PROCEDURE GetSavingsInfo
    @ClientID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SavingID INT;
    DECLARE @Name VARCHAR(20);
    DECLARE @TotalAmount DECIMAL;
    DECLARE @AmountBalance DECIMAL;
    DECLARE @ReturnValue INT = -99; -- Default to error value

    BEGIN TRY
        -- Check if the client exists
        IF NOT EXISTS (SELECT 1 FROM Clients WHERE ClientID = @ClientID)
        BEGIN
            SET @ReturnValue = -1; -- Client does not exist
            SELECT @ReturnValue AS ReturnValue;
            RETURN;
        END

        -- Retrieve savings information
        SELECT
            SavingID,
            Name,
            TotalAmount,
            AmountBalance
        FROM Savings
        WHERE ClientID = @ClientID
        ORDER BY StartingDate DESC;

        SET @ReturnValue = 1; -- Success

    END TRY
    BEGIN CATCH
        SET @ReturnValue = -99; -- Error
    END CATCH

    -- Return the result set
    SELECT
        @ReturnValue AS ReturnValue;
END;
GO

CREATE PROCEDURE GetLoansInfo
    @ClientID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @LoanID INT;
    DECLARE @Name VARCHAR(20);
    DECLARE @TotalAmount DECIMAL;
    DECLARE @AmountBalance DECIMAL;
    DECLARE @ReturnValue INT = -99; -- Default to error value

    BEGIN TRY
        -- Check if the client exists
        IF NOT EXISTS (SELECT 1 FROM Clients WHERE ClientID = @ClientID)
        BEGIN
            SET @ReturnValue = -1; -- Client does not exist
            SELECT @ReturnValue AS ReturnValue;
            RETURN;
        END

        -- Retrieve savings information
        SELECT
            LoanID,
            Name,
            TotalAmount,
            AmountBalance
        FROM Loans
        WHERE ClientID = @ClientID
        ORDER BY StartingDate DESC;

        SET @ReturnValue = 1; -- Success

    END TRY
    BEGIN CATCH
        SET @ReturnValue = -99; -- Error
    END CATCH

    -- Return the result set
    SELECT
        @ReturnValue AS ReturnValue;
END;
GO