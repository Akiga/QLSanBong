create database QLSanBong
go

use QLSanBong
go

-- Account
-- Bill
-- BillInfo
-- Table

drop table Account
create table Account
(
	UserName nvarchar(100) primary key,
	DisplayName nvarchar(100) not null,
	Password Nvarchar(100) not null default 0,
	type int not null default 0
)
go

drop table Bill
create table Bill
(	
	id int identity primary key,
	DateCheckIn Date not null default getdate(),
	DateCheckOut Date,
	idTable int not null,
	status int not null

	foreign key (idTable) References dbo.TableFood(id)
)
go

drop table BillInfo
create table BillInfo
(
	id int identity primary key,
	idBill int not null,
	idFood int not null,
	count int not null Default 0

	foreign key (idBill) References dbo.Bill(id)
)

drop table Stadium
create table Stadium
(
	id int identity primary key,
	Ten nvarchar(100) not null default N'Chưa có tên',
	status nvarchar(100) not null Default N'Trống'
)
go


insert into dbo.Account(
	UserName,
	DisplayName,
	Password,
	type
	)
values(N'Staff',
	N'Staff',
	N'1',
	0
)


insert into dbo.Account(
	UserName,
	DisplayName,
	Password,
	type
	)
values(N'Akiga',
	N'manh',
	N'1',
	1
)

select * from dbo.Account

create proc USP_GetAcountByUserName
@userName nvarchar(100)
as
begin
	select * from dbo.Account Where UserName = @userName
end
go

EXEC dbo.USP_GetAcountByUserName @userName = N'Akiga'
go


create proc USP_Login
@username nvarchar(100), @passWord nvarchar(100)
as 
begin
 select * from dbo.Account where UserName = @username and Password = @passWord
 end
 go