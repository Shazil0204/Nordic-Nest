-- Switch context to the NordicNestDB database
USE NordicNestDB;
GO

-- CREATING A TABLE FOR Clients
CREATE TABLE Clients(
	ClientID INT IDENTITY(1,1) PRIMARY KEY,
	ClientNumber INT NOT NULL,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	Email VARCHAR(255) NOT NULL,
	UserName VARCHAR(255) NOT NULL,
	Password VARCHAR(255) NOT NULL,
	Gender BIT,
	Age INT
);
GO

INSERT INTO Clients(ClientNumber, FirstName, LastName, Email, UserName, Password, Gender, Age) VALUES(100180, 'Shiz', 'Shiz', 'Shazilshahid04@gmail.com', 'Shiz', 'Shiz', 1, 19);
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