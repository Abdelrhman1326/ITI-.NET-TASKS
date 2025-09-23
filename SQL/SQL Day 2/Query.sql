-- Database & Tables
-- Create Database
CREATE DATABASE CompanyTask;
GO
USE CompanyTask;
GO

-- Employees Table
CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Address NVARCHAR(100),
    Gender CHAR(1) CHECK (Gender IN ('M', 'F')),
    BirthDate DATE,
    SupervisorId INT NULL,
    DepartmentNumber INT,
    FOREIGN KEY (SupervisorId) REFERENCES Employees(Id),
    FOREIGN KEY (DepartmentNumber) REFERENCES Departments(DNumber)
);

-- Departments Table
CREATE TABLE Departments (
    DNumber INT PRIMARY KEY,
    DName NVARCHAR(50) NOT NULL,
    ManagerId INT NULL,
    HiringDate DATE
);

-- Projects Table
CREATE TABLE Projects (
    PNumber INT PRIMARY KEY,
    PName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(100),
    City NVARCHAR(50),
    DeptNum INT,
    FOREIGN KEY (DeptNum) REFERENCES Departments(DNumber)
);

-- Employee_Projects (many-to-many relationship)
CREATE TABLE Employee_Projects (
    EId INT,
    PNum INT,
    WorkingHours INT,
    PRIMARY KEY (EId, PNum),
    FOREIGN KEY (EId) REFERENCES Employees(Id),
    FOREIGN KEY (PNum) REFERENCES Projects(PNumber)
);


-- Insert Data
-- Insert Departments
INSERT INTO Departments (DNumber, DName, ManagerId, HiringDate)
VALUES 
(1, 'HR', NULL, '2020-01-01'),
(2, 'IT', NULL, '2021-06-15'),
(3, 'Finance', NULL, '2019-03-10');

-- Insert Employees
INSERT INTO Employees (FirstName, LastName, Address, Gender, BirthDate, SupervisorId, DepartmentNumber)
VALUES
('Ali', 'Hassan', 'Cairo', 'M', '2000-05-15', NULL, 1),
('Mona', 'Adel', 'Alex', 'F', '1998-11-20', 1, 2),
('Omar', 'Khaled', 'Cairo', 'M', '2001-03-12', 1, 2),
('Mariam', 'Nabil', 'Giza', 'F', '1999-07-25', 2, 3),
('Mostafa', 'Zaki', 'Cairo', 'M', '2002-09-18', 2, 1);

-- Insert Projects
INSERT INTO Projects (PNumber, PName, Location, City, DeptNum)
VALUES
(101, 'Website Revamp', 'HQ', 'Cairo', 2),
(102, 'Recruitment Drive', 'Branch1', 'Alex', 1),
(103, 'Audit 2025', 'HQ', 'Giza', 3);

-- Link Employees to Projects
INSERT INTO Employee_Projects (EId, PNum, WorkingHours)
VALUES
(1, 101, 20),
(2, 101, 30),
(2, 103, 10),
(3, 102, 25),
(4, 103, 40),
(5, 101, 15);


-- Queries
-- 1. Employees in Department 1
SELECT * FROM Employees WHERE DepartmentNumber = 1;

-- 2. Full names of employees in Cairo
SELECT FirstName + ' ' + LastName AS FullName 
FROM Employees WHERE Address = 'Cairo';

-- 3. Employees born between 1999 and 2002
SELECT * FROM Employees 
WHERE BirthDate BETWEEN '1999-01-01' AND '2002-12-31';

-- 4. Projects of employee with Id=2
SELECT P.PName 
FROM Projects P
JOIN Employee_Projects EP ON P.PNumber = EP.PNum
WHERE EP.EId = 2;

-- 5. Employees ordered by LastName DESC
SELECT * FROM Employees ORDER BY LastName DESC;

-- 6. Employees with no Supervisor
SELECT * FROM Employees WHERE SupervisorId IS NULL;


-- Update & Delete
-- Update address of employee 3
UPDATE Employees SET Address = 'Alex' WHERE Id = 3;

-- Delete employee with Id = 5
DELETE FROM Employees WHERE Id = 5;


-- Extra Queries
-- 1. FirstName starts with 'M'
SELECT * FROM Employees WHERE FirstName LIKE 'M%';

-- 2. Unique addresses
SELECT DISTINCT Address FROM Employees;

-- 3. Order by multiple columns
SELECT * FROM Employees ORDER BY FirstName ASC, LastName ASC;

-- 4. FirstName is exactly 4 chars
SELECT * FROM Employees WHERE LEN(FirstName) = 4;

-- 5. FirstName starts with A and 3rd letter is 'm'
SELECT * FROM Employees WHERE FirstName LIKE 'Am_';

-- 6. FirstName starts with letters A–M
SELECT * FROM Employees WHERE FirstName LIKE '[A-M]%';

-- 7. Address does not start with 'C'
SELECT * FROM Employees WHERE Address NOT LIKE 'C%';


-- SchoolDB + Backup/Restore
-- Create new DB
CREATE DATABASE SchoolDB;
GO
USE SchoolDB;
GO

-- Example Table
CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    Age INT,
    City NVARCHAR(50)
);

INSERT INTO Students (Name, Age, City)
VALUES
('Ahmed', 20, 'Cairo'),
('Laila', 22, 'Alex'),
('Hassan', 19, 'Giza');

-- Backup SchoolDB
BACKUP DATABASE SchoolDB
TO DISK = 'C:\Backups\SchoolDB.bak';

-- Restore SchoolDB
RESTORE DATABASE SchoolDB_Restore
FROM DISK = 'C:\Backups\SchoolDB.bak'
WITH MOVE 'SchoolDB' TO 'C:\Data\SchoolDB_Restore.mdf',
     MOVE 'SchoolDB_log' TO 'C:\Data\SchoolDB_Restore.ldf';
