use master




if (select count(*) from sys.syslogins where name = 'AdvWorks2012')> 0 
begin
	drop login AdvWorks2012
	use AdventureWorks2012
	DROP USER AdvWorks2012
	USE master


end


-- creating a login with a password 

CREATE LOGIN AdvWorks2012 WITH password = '1234'







use AdventureWorks2012

CREATE USER AdvWorks2012 FOR LOGIN AdvWorks2012






-- change the user to the default database, mapping the user. 

ALTER LOGIN AdvWorks2012 WITH default_database = Adventureworks2012






-- adding the db_datawriter role and the db_datareader role

ALTER ROLE db_datareader ADD member AdvWorks2012
ALTER ROLE db_datawriter ADD member AdvWorks2012
GO 


-- denying all permissions on the schema: Humanresources
DENY SELECT ON SCHEMA :: HumanResources TO AdvWorks2012
DENY INSERT ON SCHEMA :: HumanResources TO AdvWorks2012
DENY UPDATE ON SCHEMA :: HumanResources TO AdvWorks2012
DENY DELETE ON SCHEMA :: HumanResources TO AdvWorks2012
DENY ALTER  ON SCHEMA :: HumanResources TO AdvWorks2012


-- denying all permissions on the schema : person

DENY SELECT ON SCHEMA :: Person TO AdvWorks2012
DENY ALTER  ON SCHEMA :: Person TO AdvWorks2012
DENY UPDATE ON SCHEMA :: Person TO AdvWorks2012
DENY INSERT ON SCHEMA :: Person TO AdvWorks2012
DENY DELETE ON SCHEMA :: Person TO AdvWorks2012
GO 






-- stored procedure that accepts an customerID and returns the salesorderID, orderdate, shipdate, salespersonname, city and state from 
-- shipping address, total amount due on the order. 

CREATE PROC customerid
(
	@customerID INT
)
AS
BEGIN 

	SELECT soh.salesorderid, soh.orderdate, soh.shipdate, a.city, stp.[name], soh.totaldue
	FROM   sales.SalesOrderHeader soh
LEFT JOIN  sales.SalesPerson sp
ON         soh.SalesPersonID = sp.BusinessEntityID
LEFT JOIN  HumanResources.Employee e
ON         soh.SalesPersonID = e.BusinessEntityID
LEFT JOIN  sales.Customer c
ON         soh.CustomerID = c.CustomerID
LEFT JOIN  person.[Address] a
ON         soh.ShipToAddressID = a.AddressID
LEFT JOIN  person.StateProvince stp
ON         stp.StateProvinceID = a.StateProvinceID
WHERE  c.CustomerID = @customerID

END
GO



-------------------------------------------------------------------------




CREATE PROC saleid
AS
BEGIN

	SELECT c.customerID , CONCAT(p.firstname , ',', p.lastname)
	FROM   sales.Customer c
	JOIN   person.person p
	ON     p.BusinessEntityID = c.PersonID

END 
GO 




	










