-- 1-Get the number of employees in each department.

SELECT d.Dname, COUNT(*) AS NumberOfEmployees

FROM dbo.Employee AS e

JOIN dbo.Departments As d

ON e.Dno = d.Dnum

GROUP BY d.Dname;



-- 2-Get the minimum salary in each department.

SELECT d.Dname AS DepartmentName, MIN(e.Salary) AS MinimumSalary

FROM dbo.Employee AS e

JOIN dbo.Departments AS d

ON e.Dno = d.Dnum

GROUP BY d.Dname;



-- 3-Get the average salary for each department.

SELECT d.Dname AS DepartmentName, AVG(e.salary) AS AverageSalary

FROM dbo.Employee as e

JOIN dbo.Departments AS d

ON e.Dno = d.Dnum

GROUP BY d.Dname;



-- 4-Get the departments that have more than 3 employees.

SELECT d.Dname

FROM dbo.Departments AS d

JOIN dbo.Employee AS e

ON e.Dno = d.Dnum

GROUP BY d.Dname

HAVING COUNT(e.Dno) > 3;



-- 5-Get the projects that have more than 2 employees working on them.

SELECT p.Pname AS ProjectName, p.Pnumber AS ProjectNumber

FROM dbo.Project AS p

JOIN dbo.Works_for AS w

ON p.Pnumber = w.Pno

GROUP BY p.Pname, p.Pnumber

HAVING COUNT(w.Essn) > 2;



-- 6-Get the employees who have the maximum salary.

SELECT e.Fname+' '+e.Lname as EmployeeName, e.Salary AS MaxSalary

FROM dbo.Employee AS e

WHERE e.Salary=(SELECT MAX(e.Salary)

FROM dbo.Employee AS e);



-- 7-Get the employees who have a salary greater than the average salary.

SELECT e.Fname+' '+e.Lname as EmployeeName, e.Salary AS Salary

FROM dbo.Employee AS e

WHERE e.Salary > (SELECT AVG(e.Salary)

FROM dbo.Employee AS e);



-- 8-Get the names of employees who work on the same projects as "John Smith".

SELECT DISTINCT e.Fname + ' ' + e.Lname AS EmployeeName

FROM dbo.Employee AS e

JOIN dbo.Works_for AS w

ON e.Ssn = w.Essn

WHERE w.Pno IN (

    SELECT w2.Pno

    FROM dbo.Employee AS e2

    JOIN dbo.Works_for AS w2

    ON e2.Ssn = w2.Essn

    WHERE e2.Fname = 'John' AND e2.Lname = 'Smith'

)

AND e.Fname != 'John' AND e.Lname != 'Smith';



-- 9-Get the departments that control the projects where "Alice" works.

SELECT DISTINCT d.Dname

FROM dbo.Employee AS e

JOIN dbo.Works_for AS w

ON e.Ssn = w.Essn

JOIN dbo.Project AS p

ON w.Pno = p.Pnumber

JOIN dbo.Departments AS d

ON p.Dnum = d.Dnum

WHERE e.Fname = 'Alice';



-- 10-Create a view to display employees with their department name and salary.

CREATE VIEW EmployeeDepartmentSalaryView AS

SELECT e.Fname + ' ' + e.Lname AS EmployeeName, d.Dname AS DepartmentName, e.Salary

FROM dbo.Employee AS e

JOIN dbo.Departments AS d

ON e.Dno = d.Dnum;



-- 11-Select all data from the view.

SELECT * FROM EmployeeDepartmentSalaryView;



-- 12-Create a view for projects with their department name.

CREATE VIEW ProjectDepartmentView AS

SELECT p.Pname AS ProjectName, d.Dname AS DepartmentName

FROM dbo.Project AS p

JOIN dbo.Departments AS d

ON p.Dnum = d.Dnum;



-- 13-Display employees ordered by salary descending.

SELECT e.Fname+' '+e.Lname as EmployeeName, e.Salary as Salary

FROM dbo.Employee as e

ORDER BY Salary DESC;



-- 14-Display projects ordered by project name alphabetically.

SELECT p.Pname as ProjectName

FROM dbo.Project as p

ORDER BY ProjectName;



-- 15-Get the top 3 highest paid employees.

SELECT TOP 3 e.Fname+' '+e.Lname AS EmployeeName, e.Salary AS Salary

FROM dbo.Employee as e

ORDER BY Salary DESC;



-- 16-Get the top 2 departments with the largest number of employees.

SELECT TOP 2 d.Dname AS DepartmentName,

COUNT(e.Ssn) AS NumberOfEmployees

FROM dbo.Departments AS d

JOIN dbo.Employee AS e

ON d.Dnum = e.Dno

GROUP BY d.Dname

ORDER BY NumberOfEmployees DESC;



-- 18-Create a simple view for courses & Delete a project from the view





-- 19-Create a view for employees & Increase salary of 'John Smith' by 10%

CREATE VIEW JohnSmith10PercentSalaryView AS

SELECT Fname, Lname, Salary * 1.10 AS IncreasedSalary

FROM dbo.Employee

WHERE Fname = 'John' AND Lname = 'Smith';



-- 20- Get employees who earn more than the average salary of their own department.

SELECT e.Fname+' '+e.Lname, e.Salary, e.Dno FROM dbo.Employee AS e

WHERE e.Salary > (

        SELECT AVG(e2.Salary)

        FROM dbo.Employee AS e2

        WHERE e2.Dno = e.Dno

    );