-- Create a new database named NordicNestDB
CREATE DATABASE NordicNestDB;
GO

-- Switch context to the NordicNestDB database
USE NordicNestDB;
GO

-- Create a read-only login for the database
GO
-- Create a new login named Navbar with SQL Server authentication
CREATE LOGIN FullAccess WITH PASSWORD = 'Kode1234!', DEFAULT_DATABASE = NordicNestDB;
GO
-- Switch to NordicNestDB database
USE NordicNestDB;
GO
-- Create a user for the Navbar login
CREATE USER FullAccess FOR LOGIN FullAccess;
GO
-- Grant read-only access to the Navbar user by adding it to db_datareader role
ALTER ROLE db_owner ADD MEMBER FullAccess;
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
