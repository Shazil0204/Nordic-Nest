-- Switch context to the NordicNestDB database
USE NordicNestDB;
GO
PROCEDURE AddNewClient
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

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Check if the email already exists
        SELECT @Count = COUNT(*)
        FROM Clients
        WHERE Email = @Email;

        IF @Count > 0
        BEGIN
            RAISERROR('The email provided is already in use.', 16, 1);
            RETURN;
        END

        -- Check if the username already exists
        SELECT @Count = COUNT(*)
        FROM Clients
        WHERE UserName = @UserName;

        IF @Count > 0
        BEGIN
            RAISERROR('The username provided is already in use.', 16, 1);
            RETURN;
        END

        -- Generate a unique 8-digit ClientNumber
        WHILE 1 = 1
        BEGIN
            SELECT @ClientNumber = CAST(RAND() * 89999999 AS INT) + 10000000;

            SELECT @Count = COUNT(*)
            FROM Clients
            WHERE ClientNumber = @ClientNumber;

            IF @Count = 0
                BREAK;
        END

        -- Insert the new client
        INSERT INTO Clients (ClientNumber, FirstName, LastName, Email, UserName, Password, Gender, Age)
        VALUES (@ClientNumber, @FirstName, @LastName, @Email, @UserName, @Password, @Gender, @Age);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Rethrow the error
        DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT;
        SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY();
        RAISERROR(@ErrMsg, @ErrSeverity, 1);
    END CATCH
END;
GO