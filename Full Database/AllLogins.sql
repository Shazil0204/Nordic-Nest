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

-- Create a new login
CREATE LOGIN CreateUser WITH PASSWORD = 'Kode1234!', DEFAULT_DATABASE = NordicNestDB;
-- GOING INTO THE DATABASE
USE NordicNestDB;
GO
-- Create a user in your database for this login
CREATE USER CreateUser FOR LOGIN CreateUser;
GO
-- Grant insert permissions on the ContactInquiries table
GRANT INSERT ON Clients TO CreateUser;
GO
-- Explicitly deny select and delete permissions
DENY SELECT, UPDATE, DELETE ON Clients TO CreateUser;
GO