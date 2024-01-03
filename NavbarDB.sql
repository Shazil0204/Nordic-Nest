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
CREATE LOGIN Navbar WITH PASSWORD = 'Navbar', DEFAULT_DATABASE = NordicNestDB;
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
