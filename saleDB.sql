CREATE DATABASE SaleDB
GO

use SaleDB

create table Products(
ProductID int,
ProductName varchar(50),
Quantity decimal,
UnitPrice float,
primary key (ProductID),
)

insert into Products values(1, 'TV', 10, 100)
insert into Products values(2, 'Fridge', 5, 200)
insert into Products values(3, 'PS5', 15, 50)
go