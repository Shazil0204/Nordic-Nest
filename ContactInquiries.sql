-- Switch context to the NordicNestDB database
USE NordicNestDB;
GO

CREATE TABLE ContactInquiries (
	ContactInquiryID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	IsExistingClient BIT NOT NULL,
	SubmissionDateTime DATETIME NOT NULL,
	MessageContent VARCHAR(255) NOT NULL
);

INSERT INTO ContactInquiries (FirstName, LastName, IsExistingClient, SubmissionDateTime, MessageContent)
VALUES 
('John', 'Doe', 0, GETDATE(), 'Interested in product details.'),
('Jane', 'Smith', 1, GETDATE(), 'Issue with recent purchase.'),
('Emily', 'Johnson', 0, GETDATE(), 'Request for more information on services.'),
('Michael', 'Brown', 1, GETDATE(), 'Feedback on customer support.'),
('Sarah', 'Davis', 0, GETDATE(), 'Inquiry about partnership opportunities.'),
('David', 'Wilson', 1, GETDATE(), 'Questions regarding billing.'),
('Emma', 'Martinez', 0, GETDATE(), 'Looking for customized solutions.'),
('Daniel', 'Anderson', 1, GETDATE(), 'Feedback and suggestions for improvements.'),
('Olivia', 'Thomas', 0, GETDATE(), 'General inquiry about the company.'),
('James', 'Jackson', 1, GETDATE(), 'Request for a follow-up on previous discussion.');


