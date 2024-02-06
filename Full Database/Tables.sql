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

-- CREATING A TABLE FOR NAVIGATION BARS

-- Define a table named NavBars
CREATE TABLE NavBars (
	-- Unique identifier for each navigation bar
	NavID INT IDENTITY(1,1) PRIMARY KEY,

	-- Name of the navigation bar (maximum length: 15 characters)
	Name VARCHAR(15) NOT NULL,

	-- URL associated with the navigation bar (maximum length: 30 characters)
	URL VARCHAR(30) NOT NULL,

	-- Indicates whether the navigation bar includes an authentication button (1 for true, 0 for false)
	IsAuthBtn BIT NOT NULL
);
-- End of table creation
GO

-- Create a table named Pages to store page information
CREATE TABLE Pages(
    PageID INT IDENTITY(1,1) PRIMARY KEY, -- Primary key with auto-increment starting from 1
    Page VARCHAR(30) NOT NULL -- Name of the page, max 30 characters
);
GO

-- CREATING A TABLE FOR NAVIGATION BAR ASSOCIATIONS WITH PAGES

-- Define a table named NavbarForPages
CREATE TABLE NavbarForPages (
	-- Unique identifier for each navigation bar-page association
	NavbarForPageID INT IDENTITY(1,1) PRIMARY KEY,

	-- Identifier for the associated page
	PageID INT NOT NULL,

	-- Identifier for the associated navigation bar
	NavID INT NOT NULL,

	-- Establish a foreign key relationship with the Pages table using PageID
	FOREIGN KEY (PageID) REFERENCES Pages(PageID),

	-- Establish a foreign key relationship with the NavBars table using NavID
	FOREIGN KEY (NavID) REFERENCES NavBars(NavID)
);
-- End of table creation
GO

-- CREATING A TABLE FOR Clients Contacts

-- Define a table named ContactInquiries
CREATE TABLE ContactInquiries (
	-- Unique identifier for each contact inquiry
	ContactInquiryID INT IDENTITY(1,1) PRIMARY KEY,

	-- First name of the person making the inquiry (maximum length: 30 characters)
	FirstName VARCHAR(30) NOT NULL,

	-- Last name of the person making the inquiry (maximum length: 30 characters)
	LastName VARCHAR(30) NOT NULL,

	-- Indicates whether the person making the inquiry is an existing client (1 for true, 0 for false)
	IsExistingClient BIT NOT NULL,

	-- Date and time when the inquiry was submitted
	SubmissionDateTime DATETIME NOT NULL,

	-- Content of the message or inquiry (maximum length: 255 characters)
	MessageContent VARCHAR(255) NOT NULL,

	-- Indicates whether the inquiry has been completed (1 for true, 0 for false)
	InquiryCompleted BIT NOT NULL
);
-- End of table creation
GO

-- CREATING A TABLE FOR Clients
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
	UserName VARCHAR(150) NOT NULL,
    -- User login password
	Password VARCHAR(150) NOT NULL,
    -- Gender of the client (BIT: 0 for male, 1 for female, for example)
	Gender BIT NOT NULL,
    -- Age of the client
	Age INT NOT NULL,
	-- Check if user have read terms and conditions
	ReadTeams BIT Default 0,
	-- This will store the amount user used on each category
	AmountUsed INT Default 0,
	-- Check if user have completed his Data information
	signup_completed BIT Default 0
);
GO

-- CREATING A TABLE FOR Clients Data
CREATE TABLE ClientData(
	ClientData INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated client
	ClientID INT NOT NULL,
	-- Total Amount he earns each month 
	TotalMonthlyAmount INT Default 0,
	-- Total Amount He can use each month
	UsableAmount INT Default 0,
	-- Total amount used each month
	TotalAmountUsed INT DEFAULT 0,
	-- User Reserve amount
	UserReserved INT,
	-- System Reserve amount
	SystemReserved INT,
	-- Total Amount of Savings (Max 10)
	TotalSavings INT Default 0,
	-- Total Amount of Loans (Max 10)
	TotalLoans INT Default 0 ,
	-- Total Amount of Subscriptions (Max 10)
	TotalSubscriptions INT Default 0 ,
	-- Total Amount of Incomes (Max 5)
	TotalIncomes INT Default 0 ,
	-- Reference to the client associated with this savings record
	FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);

CREATE TABLE Savings(
    -- Unique identifier for each savings record
	SavingID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated client
	ClientID INT NOT NULL,
    -- Name of the savings account
	Name VARCHAR(20) NOT NULL,
    -- Total amount in the savings account
	TotalAmount DECIMAL,
    -- Start date of the savings account
	StartingDate DATETIME NOT NULL,
    -- End date of the savings account
	EndingDate DATETIME,
    -- Current balance in the savings account
	AmountBalance DECIMAL DEFAULT 0,
    -- If true then a date will be fixed for each month else a number will be take that will 
	IsMonthly BIT NOT NULL,
    -- Average input for the savings account
	AverageInput DECIMAL NOT NULL,
    -- User-defined deadline for savings
	UserDeadLine DATETIME,
	-- Is Default saving account
	IsDefault Bit DEFAULT 0,
    -- System deadline for savings
<<<<<<< Updated upstream
	SystemDeadline DATETIME,
=======
	SystemDeadline DATETIME NOT NULL,
	-- this will automatically make the system to add amount each month
	SystemControl BIT DEFAULT 1,
>>>>>>> Stashed changes
    -- Reference to the client associated with this savings record
	FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);
GO

CREATE TABLE SavingSchedule (
    -- Unique identifier for each savings schedule entry
    ScheduleID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated savings account
    SavingID INT NOT NULL,
    -- Number of days before Saving renewal for scheduled activities
    DaysBeforeRenewal INT NOT NULL,
    -- Reference to the associated savings account
    FOREIGN KEY (SavingID) REFERENCES Savings(SavingID)
);
GO

CREATE TABLE Loans(
    -- Unique identifier for each loan record
	LoanID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated client
	ClientID INT NOT NULL,
    -- Name of the loan account
	Name VARCHAR(20) NOT NULL,
    -- Total amount of the loan
	TotalAmount DECIMAL NOT NULL,
    -- Start date of the loan
	StartingDate DATETIME,
    -- End date of the loan
	EndingDate DATETIME,
    -- Current balance of the loan
	AmountBalance DECIMAL NOT NULL,
    -- If true then a date will be fixed for each month else a number will be take that will 
	IsMonthly BIT NOT NULL,
    -- Average payment for the loan
	AverageInput DECIMAL NOT NULL,
    -- User-defined deadline for the loan
	UserDeadLine DATETIME,
	-- Is Default Loan account
	IsDefault Bit DEFAULT 0,
    -- System deadline for the loan
	SystemDeadline DATETIME,
    -- Reference to the client associated with this loan record
	FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);
GO

CREATE TABLE LoanSchedule (
    -- Unique identifier for each loan schedule entry
    ScheduleID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated loan account
    LoanID INT NOT NULL,
    -- Number of days before Loan renewal for scheduled activities
    DaysBeforeRenewal INT NOT NULL,
    -- Reference to the associated loan account
    FOREIGN KEY (LoanID) REFERENCES Loans(LoanID)
);
GO

CREATE TABLE Incomes(
    -- Unique identifier for each income record
    IncomeID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated client
    ClientID INT NOT NULL,
    -- Name of the income source
    Name VARCHAR(20) NOT NULL,
    -- Indicates whether the payment amount is fixed (BIT: 0 for no, 1 for yes)
    IsPaymentAmountFixed BIT NOT NULL,
    -- Indicates whether the payment day is fixed (BIT: 0 for no, 1 for yes)
    IsPaymentDayFixed BIT NOT NULL,
    -- Description of the income source
    Description VARCHAR(100),
    -- Reference to the client associated with this income record
    FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);
GO

CREATE TABLE WorkSchedule (
    -- Unique identifier for each work schedule entry
    ScheduleID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated income source
    IncomeID INT NOT NULL,
    -- Number of days before income renewal for scheduled activities
    DaysBeforeRenewal INT NOT NULL,
    -- Reference to the associated income source
    FOREIGN KEY (IncomeID) REFERENCES Incomes(IncomeID)
);
GO

CREATE TABLE Subscriptions(
    -- Unique identifier for each subscription record
    SubscriptionID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated client
    ClientID INT NOT NULL,
    -- Name of the subscription
    Name VARCHAR(20) NOT NULL,
    -- Indicates whether the subscription is monthly (BIT: 0 for no, 1 for yes)
    IsMonthly BIT NOT NULL,
    -- Amount associated with the subscription
    Amount INT NOT NULL,
    -- Description of the subscription
    Description VARCHAR(100),
    -- Reference to the client associated with this subscription record
    FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);
GO

CREATE TABLE SubscriptionSchedule (
    -- Unique identifier for each work schedule entry
    ScheduleID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated income source
    SubscriptionID INT NOT NULL,
    -- Number of days before Loan renewal for scheduled activities
    DaysBeforeRenewal INT NOT NULL,
    -- Reference to the associated income source
    FOREIGN KEY (SubscriptionID) REFERENCES Subscriptions(SubscriptionID)
);
GO

CREATE TABLE Messages(
    -- Unique identifier for each message
    MessageID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated client
    ClientID INT NOT NULL,
    -- Regarding field indicating the subject of the message
    Regarding VARCHAR(20) NOT NULL,
    -- Message content
    Message VARCHAR(255) NOT NULL,
    -- Reference to the client associated with this message
    FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);
GO

CREATE TABLE Transactions(
    -- Unique identifier for each transaction record
    TransactionID INT IDENTITY(1,1) PRIMARY KEY,
    -- Reference to the associated monthly usage
    ClientID INT NOT NULL,
    -- Recipient of the transaction
    TransactionTo VARCHAR(30) NOT NULL,
    -- Sender of the transaction
    TransactionFrom VARCHAR(30) NOT NULL,
    -- Transaction amount
    Amount DECIMAL NOT NULL,
    -- Timestamp of the transaction
    Time DATETIME NOT NULL,
    -- Reference to the associated monthly usage
    FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);

CREATE TABLE Categories(
	CategoryID INT IDENTITY(1,1) PRIMARY KEY,
	category VARCHAR(20) NOT NULL,
	color VARCHAR(7) NOT NULL
);
GO

CREATE TABLE EachCategoryUsage(
	EachCategoryUsage INT IDENTITY(1,1) PRIMARY KEY,
	TransactionID INT NOT NULL,
	CategoryID INT NOT NULL,
	FOREIGN KEY (TransactionID) REFERENCES Transactions(TransactionID),
	FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);