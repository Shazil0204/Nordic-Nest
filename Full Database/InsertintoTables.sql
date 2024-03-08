GO
-- Insert initial data into the NavBars table
INSERT INTO NavBars(Name, URL, IsAuthBtn) VALUES
    ('Home','/Index',0),---------------------------------- 1 	*
    ('About','/About',0),--------------------------------- 2 	*
    ('Services','/Services',0),--------------------------- 3 	*
    ('Contact','/Contact',0),----------------------------- 4 	*
	('Manual','/Manual', 0),------------------------------ 5 	*
	('Login','/Login',1),--------------------------------- 6 	*
	('Register','/Register',1),--------------------------- 7 	*
	('sign out','/Index',1),------------------------------ 8 	*
	('Dashboard','/Dashboard',0),------------------------- 9 	*
	('Savings','/Savings',0),----------------------------- 10	
	('Loans','/Loans',0),--------------------------------- 11	
	('subscriptions','/Subscriptions',0),----------------- 12	
	('message','/message',0),----------------------------- 13	
	('Charts','/Charts',0),------------------------------- 14	
	('Chat','/Chat',0),----------------------------------- 15	
	('Contact Form','/Contact_Form',0),------------------- 16	*
	('Create Saving','/CreateSaving',0),------------------ 17	
	('Update Saving','/UpdateSaving',0),------------------ 18	
	('Delete Saving','/DeleteSaving',0),------------------ 19	
	('Create Loan','/CreateLoan',0),---------------------- 20	
	('Update Loan','/UpdateLoan',0),---------------------- 21	
	('Delete Loan','/DeleteLoan',0),---------------------- 22	
	('Create subscription','/CreateSubscription',0),------ 23	
	('Update subscription','/UpdateSubscription',0),------ 24	
	('Delete subscription','/DeleteSubscription',0);------ 25		
	
GO
INSERT INTO Pages(Page) VALUES
('Home'               ),
('About'              ),
('Services'           ),
('Contact'            ),
('Manual'             ),
('Login'              ),
('Register'           ),
('sign'               ),
('Dashboard'          ),
('Savings'            ),
('Loans'              ),
('subscriptions'      ),
('message'            ),
('Charts'             ),
('Chat'               ),
('Contact'            ),
('Create Saving'      ),
('Update Saving'      ),
('Delete Saving'      ),
('Create Loan'        ),
('Update Loan'        ),
('Delete Loan'        ),
('Create subscription'),
('Update subscription'),
('Delete subscription');

GO
-- Linking pages with navigation bars in NavbarForPages table
INSERT INTO NavbarForPages(PageID, NavID) VALUES
	(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),
	(2,1),(2,3),(2,4),(2,5),(2,6),(2,7),
	(3,1),(3,2),(3,4),(3,5),(3,6),(3,7),
	(4,1),(4,2),(4,3),(4,5),(4,16),(4,6),(4,7),
	(5,1),(5,2),(5,3),(5,4),(5,6),(5,7),
	(9,10),(9,11),(9,12),(9,13),(9,14),(9,15),(9,8),
	(10,17),(10,18),(10,19),(10,9),(10,8),
	(11,20),(11,21),(11,22),(11,9),(11,8),
	(12,23),(12,24),(12,25),(12,9),(12,8);

--------------------------------------------------------------  DUMMY DATA
-- Dummy data for Clients table
INSERT INTO Clients (ClientNumber, FirstName, LastName, Email, UserName, Password, Gender, Age, ReadTeams, signup_completed)
VALUES 
(1001, 'John', 'Doe', 'john.doe@example.com', 'john_doe', '$2a$10$v7bhGx/F2n6i8OjhuMgFPOQ4zE1qLRTWbCqWw69zsYPuolsKSa3ku', 0, 25, 0, 1),
(1002, 'Jane', 'Smith', 'jane.smith@example.com', 'jane_smith', '$2a$10$8jwys1sNieC4gTYbzWyLJOYkFdsriV5PFeAy96MwcffVfqWWHZLFu', 1, 30, 0, 0);

-- Dummy data for ClientData table
INSERT INTO ClientData (ClientID, TotalMonthlyAmount, UsableAmount, UserReserved, SystemReserved, TotalSavings, TotalLoans, TotalSubscriptions, TotalIncomes)
VALUES 
(1, 5000, 4000, 500, 200, 2000, 1000, 500, 2000),
(2, 8000, 6000, 700, 300, 2500, 1500, 800, 3000);

-- Dummy data for Savings table
INSERT INTO Savings (ClientID, Name, TotalAmount, StartingDate, EndingDate, AmountBalance, IsMonthly, AverageInput, UserDeadLine, SystemDeadline, IsDefault)
VALUES 
(1, 'Emergency Fund', 1000, '2024-01-01', '2025-01-01', 500, 1, 200, '2024-12-31', '2025-01-05', 1),
(1, 'Vacation Fund', 2000, '2024-01-01', '2024-12-31', 1000, 0, 300, '2024-12-31', '2025-01-05', 0),
(2, 'Education Fund', 3000, '2024-01-01', '2024-12-31', 1500, 0, 400, '2024-12-31', '2025-01-05', 0);

-- Dummy data for SavingSchedule table
INSERT INTO SavingSchedule (SavingID, DaysBeforeRenewal)
VALUES 
(1, 7),
(2, 14);

-- Dummy data for Loans table
INSERT INTO Loans (ClientID, Name, TotalAmount, StartingDate, EndingDate, AmountBalance, IsMonthly, AverageInput, UserDeadLine, SystemDeadline, IsDefault)
VALUES 
(1, 'Car Loan', 10000, '2024-01-01', '2025-01-01', 5000, 0, 800, '2024-12-31', '2025-01-05', 0),
(2, 'Home Loan', 50000, '2024-01-01', '2025-01-01', 25000, 1, 1500, '2024-12-31', '2025-01-05', 1);

-- Dummy data for LoanSchedule table
INSERT INTO LoanSchedule (LoanID, DaysBeforeRenewal)
VALUES 
(1, 7),
(2, 14);

-- Dummy data for Incomes table
INSERT INTO Incomes (ClientID, Name, IsPaymentAmountFixed, IsPaymentDayFixed, Description)
VALUES 
(1, 'Salary', 1, 1, 'Monthly income from employment'),
(2, 'Freelance', 0, 1, 'Irregular income from freelance work');

-- Dummy data for WorkSchedule table
INSERT INTO WorkSchedule (IncomeID, DaysBeforeRenewal)
VALUES 
(1, 30),
(2, 15);

-- Dummy data for Subscriptions table
INSERT INTO Subscriptions (ClientID, Name, IsMonthly, Amount, Description)
VALUES 
(1, 'Netflix', 1, 10, 'Monthly subscription for streaming service'),
(2, 'Gym Membership', 1, 30, 'Monthly gym subscription');

-- Dummy data for SubscriptionSchedule table
INSERT INTO SubscriptionSchedule (SubscriptionID, DaysBeforeRenewal)
VALUES 
(1, 5),
(2, 7);

-- Dummy data for Messages table
INSERT INTO Messages (ClientID, Regarding, Message)
VALUES 
(1, 'Payment Reminder', 'Your loan installment is due on 2024-02-01. Please ensure sufficient balance.'),
(2, 'Subscription Renewal', 'Your Netflix subscription will renew on 2024-02-05. Make sure your payment method is up to date.');

-- Dummy data for Transactions table
INSERT INTO Transactions (ClientID, TransactionTo, TransactionFrom, Amount, Time)
VALUES 
(1, 'Landlord', 'John Doe', 1200, '2024-02-03 12:30:00'),
(2, 'Freelance Client', 'Jane Smith', 2000, '2024-02-05 14:45:00');

-- Dummy data for Categories table
INSERT INTO Categories (category, color)
VALUES 
('Simple Transaction', '#4CAF50'),
('Budget', '#2196F3'),
('Subscription', '#FF9800'),
('Loan', '#FF5252'),
('Saving', '#8BC34A');

-- Dummy data for EachCategoryUsage table
INSERT INTO EachCategoryUsage (TransactionID, CategoryID)
VALUES 
(1, 1),
(2, 3);
