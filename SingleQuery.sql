-- Create a new database named NordicNestDB
CREATE DATABASE NordicNestDB;
GO

-- Switch context to the NordicNestDB database
USE NordicNestDB;
GO

-- Create a table named NavBars to store navigation bar items
CREATE TABLE NavBars(
    NavID INT IDENTITY(1,1) PRIMARY KEY, -- Primary key with auto-increment starting from 1
    Name VARCHAR(15) NOT NULL, -- Name of the navigation item, max 15 characters
    URL VARCHAR(30) NOT NULL, -- URL associated with the navigation item, max 30 characters
	IsAuthBtn BIT NOT NULL
);
GO

-- Create a table named Pages to store page information
CREATE TABLE Pages(
    PageID INT IDENTITY(1,1) PRIMARY KEY, -- Primary key with auto-increment starting from 1
    Page VARCHAR(30) NOT NULL -- Name of the page, max 30 characters
);
GO

-- Create a table to link NavBars and Pages
CREATE TABLE NavbarForPages(
    NavbarForPageID INT IDENTITY(1,1) PRIMARY KEY, -- Primary key with auto-increment starting from 1
    PageID INT NOT NULL, -- Foreign key referencing PageID in Pages table
    NavID INT NOT NULL, -- Foreign key referencing NavID in NavBars table
    FOREIGN KEY (PageID) REFERENCES Pages(PageID), -- Establishing foreign key relationship
    FOREIGN KEY (NavID) REFERENCES NavBars(NavID) -- Establishing foreign key relationship
);
GO

-- Insert initial data into the NavBars table
INSERT INTO NavBars(Name, URL, IsAuthBtn) VALUES
    ('Home','/Index',0),
    ('About','/About',0),
    ('Services','/Services',0),
    ('Contact','/Contact',0),
	('Manual','/Manual', 0),
	('Login','/Login',1),
	('Register','/Register',1);
GO

-- Insert initial data into the Pages table
INSERT INTO Pages(Page) VALUES
    ('Home'),
    ('About'),
    ('Services'),
    ('Contact');
GO

-- Linking pages with navigation bars in NavbarForPages table
INSERT INTO NavbarForPages(PageID, NavID) VALUES
    (1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),
    (2,1),(2,2),(2,3),(2,4),
    (3,1),(3,2),(3,3),(3,4),
    (4,1),(4,2),(4,3),(4,4);
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

-- Create a read-only login for the database
GO
-- Create a new login named Navbar with SQL Server authentication
CREATE LOGIN Navbar WITH PASSWORD = 'Kode1234!', DEFAULT_DATABASE = NordicNestDB;
GO
-- Switch to NordicNestDB database
USE NordicNestDB;
GO
-- Create a user for the Navbar login
CREATE USER Navbar FOR LOGIN Navbar;
GO
-- Grant read-only access to the Navbar user by adding it to db_datareader role
ALTER ROLE db_datareader ADD MEMBER Navbar;
GO

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

-- Create a read-only login for the database
GO
-- Create a new login named CheckClient with SQL Server authentication
CREATE LOGIN CheckClient WITH PASSWORD = 'Kode1234!', DEFAULT_DATABASE = NordicNestDB;
GO
-- Switch to NordicNestDB database
USE NordicNestDB;
GO
-- Create a user for the CheckClient login
CREATE USER CheckClient FOR LOGIN CheckClient;
GO
-- Grant read-only access to the CheckClient user by adding it to db_datareader role
ALTER ROLE db_datareader ADD MEMBER CheckClient;
GO

-- Switch context to the NordicNestDB database
USE NordicNestDB;
GO

CREATE TABLE ContactInquiries (
	ContactInquiryID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	IsExistingClient BIT NOT NULL,
	SubmissionDateTime DATETIME NOT NULL,
	MessageContent VARCHAR(255) NOT NULL,
	UserEmail VARCHAR(75) NOT NULL,
	InquiryCompleted BIT NOT NULL
);

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

-- exec USERCONTACTFORM 'Shazil','Shahid',0,'Hello my name is Shazil Shahid','Shazilshahid@gmail.com';

-- select * from ContactInquiries
GO
-- Create a new login
CREATE LOGIN ContactInquiriesLogin WITH PASSWORD = 'Kode1234!', DEFAULT_DATABASE = NordicNestDB;
-- GOING INTO THE DATABASE
USE NordicNestDB;
GO
-- Create a user in your database for this login
CREATE USER ContactInquiriesLogin FOR LOGIN ContactInquiriesLogin;
GO
-- Grant insert permissions on the ContactInquiries table
GRANT INSERT ON ContactInquiries TO ContactInquiriesLogin;
GO
-- Explicitly deny select and delete permissions
DENY SELECT, UPDATE, DELETE ON ContactInquiries TO ContactInquiriesLogin;
GO

-- Switch context to the NordicNestDB database
USE NordicNestDB;
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
    SET NOCOUNT ON; -- This line prevents extra result sets from interfering with SELECT statements.

    -- Declare variables for use within the procedure
    DECLARE @ClientNumber INT;
    DECLARE @Count INT;

    BEGIN TRY
        BEGIN TRANSACTION; -- Start a transaction for atomic operation

        -- Check if the email already exists in the Clients table
        SELECT @Count = COUNT(*)
        FROM Clients
        WHERE Email = @Email;

        -- If an email is found, raise an error and stop the procedure
        IF @Count > 0
        BEGIN
            RAISERROR('The email provided is already in use.', 16, 1);
            RETURN;
        END

        -- Check if the username already exists in the Clients table
        SELECT @Count = COUNT(*)
        FROM Clients
        WHERE UserName = @UserName;

        -- If a username is found, raise an error and stop the procedure
        IF @Count > 0
        BEGIN
            RAISERROR('The username provided is already in use.', 16, 1);
            RETURN;
        END

        -- Loop to generate a unique 8-digit ClientNumber
        WHILE 1 = 1
        BEGIN
            -- Generate a random 8-digit number
            SELECT @ClientNumber = CAST(RAND() * 89999999 AS INT) + 10000000;

            -- Check if this ClientNumber already exists
            SELECT @Count = COUNT(*)
            FROM Clients
            WHERE ClientNumber = @ClientNumber;

            -- If no matching ClientNumber is found, exit the loop
            IF @Count = 0
                BREAK;
        END

        -- Insert the new client record into the Clients table
        INSERT INTO Clients (ClientNumber, FirstName, LastName, Email, UserName, Password, Gender, Age)
        VALUES (@ClientNumber, @FirstName, @LastName, @Email, @UserName, @Password, @Gender, @Age);

        COMMIT TRANSACTION; -- Commit the transaction after successful operation
    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of any error during the procedure execution
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Retrieve error information and rethrow the error
        DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT;
        SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY();
        RAISERROR(@ErrMsg, @ErrSeverity, 1);
    END CATCH
END;
GO
