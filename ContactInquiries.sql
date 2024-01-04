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
CREATE LOGIN WriteOnlyLogin WITH PASSWORD = 'Kode1234!', DEFAULT_DATABASE = NordicNestDB;
-- GOING INTO THE DATABASE
USE NordicNestDB;
GO
-- Create a user in your database for this login
CREATE USER WriteOnlyUser FOR LOGIN WriteOnlyLogin;
GO
-- Grant insert permissions on the ContactInquiries table
GRANT INSERT ON ContactInquiries TO WriteOnlyUser;
GO
-- Explicitly deny select and delete permissions
DENY SELECT, UPDATE, DELETE ON ContactInquiries TO WriteOnlyUser;
GO
