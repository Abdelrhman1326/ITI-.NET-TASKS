CREATE DATABASE DotNetBankDB;
Use DotNetBankDB;

CREATE TABLE Customer (
	-- primary key for customer:
	customer_id INT PRIMARY KEY IDENTITY(1,1), -- starts at 1 and auto-increment by 1.
	-- The composite 'name' attribute from your ERD is split into two columns
	first_name VARCHAR(100) NOT NULL,
	last_name VARCHAR(100) NOT NULL
);

CREATE TABLE Bankaccount (
	-- primary key for bank account:
	account_id INT PRIMARY KEY IDENTITY(1001, 1),

	-- Accout Balance:
	balance DECIMAL(18, 4) NOT NULL DEFAULT 0.0000,

	-- customer id Foreign key:
	customer_id	INT NOT NULL,
	-- Define the Foreign Key constraint
	CONSTRAINT FK_Bankaccount_Customer
		FOREIGN KEY (customer_id)
		REFERENCES Customer(customer_id)
);

-- 3. CREATE THE TRANSACTION TABLE (Child Table of Bankaccount)
-- This table needs a Foreign Key (FK) referencing the Bankaccount table.
CREATE TABLE [Transaction] (
    -- Primary Key for the Transaction table.
    transaction_id INT PRIMARY KEY IDENTITY(100001,1),
    
    -- Amount of the transaction.
    amount DECIMAL(18, 4) NOT NULL,
    
    -- Date/Time the transaction occurred. GETDATE() sets the default to the current time.
    transaction_date DATETIME NOT NULL DEFAULT GETDATE(),
    
    -- Used for 'deposit' or 'withdraw' from ERD.
    transaction_type VARCHAR(10) NOT NULL CHECK (transaction_type IN ('Deposit', 'Withdrawal')),
    
    -- Foreign Key (FK) to link to the Bankaccount table (M:1 relationship)
    account_id INT NOT NULL,
    
    -- Define the Foreign Key constraint
    CONSTRAINT FK_Transaction_Bankaccount 
        FOREIGN KEY (account_id) 
        REFERENCES Bankaccount(account_id)
);

DROP TABLE Customer;
DROP TABLE Bankaccount;

SELECT * FROM Customer;
SELECT * FROM Bankaccount;