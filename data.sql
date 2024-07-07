create database QLSanBong
go

use QLSanBong
go

-- Account
-- Bill
-- Stadium
-- Customer

drop table Account
create table Account
(
	UserName nvarchar(100) primary key,
	DisplayName nvarchar(100) not null,
	Password Nvarchar(100) not null default 0,
	type int not null default 0
)
go

drop table Stadium
create table Stadium
(
	id int identity primary key,
	Ten nvarchar(100) not null default N'Chưa có tên'
)
go

drop table Customer
create table Customer
(
	id int identity primary key,
	CustomerName nvarchar(100),
	CustomerPhone int,
	price float not null Default 0,
	DateCheckIn nvarchar(100) not null,
	DateCheckOut nvarchar(100) not null,
	idStadium int not null,
	CheckIn Date not null default getdate(),
	CheckOut Date,
	foreign key (idStadium) References dbo.Stadium(id)
)
go

drop table Bill
create table Bill
(	
	id int identity primary key,
    CustomerName nvarchar(100),
    CustomerPhone int,
    price float not null Default 0,
    CheckIn nvarchar(100) not null,
    CheckOut nvarchar(100) not null,
    idStadium int not null,
	DateCheckIn Date not null default getdate(),
	DateCheckOut Date default getdate(),
    foreign key (idStadium) References dbo.Stadium(id)
)
go

select * from Account
insert into dbo.Account(
	UserName,
	DisplayName,
	Password,
	type)
values(N'Staff',
	N'Staff',
	N'1',
	0)

insert into dbo.Account(
	UserName,
	DisplayName,
	Password,
	type)
values(N'Sasimi',
	N'manh',
	N'1',
	1)

drop proc USP_Login
create proc USP_Login
@username nvarchar(100), @passWord nvarchar(100)
as 
begin
 select * from dbo.Account where UserName = @username and Password = @passWord
 end
 go

------ Update acc
create proc USP_UpdateAccount
@userName nvarchar(100), @displayName nvarchar(100), @password nvarchar(100), @newPassword nvarchar(100)
as
begin 
	declare @isRightPass int = 0

	select @isRightPass = count(*) from Account where UserName = @userName and Password = @password

	if(@isRightPass = 1)
	begin
		if(@newPassword = null or @newPassword = '')
			begin 
				update Account set DisplayName = @displayName where UserName = @userName
			end
		else
			begin
				update Account set DisplayName = @displayName, Password = @newPassword where UserName = @userName
			end
	end
end 
go


 ---------------------------------------------
 --Thêm sân
 declare @i int = 1

while @i <= 10
begin
	insert dbo.Stadium(Ten) values (N'Sân ' + Cast (@i as nvarchar(100)))
	set @i = @i + 1
end
go

---------------------------------
-- thêm khách hàng
drop proc AddCustomer
CREATE PROCEDURE AddCustomer
    @CustomerName NVARCHAR(100),
    @CustomerPhone INT,
    @Price FLOAT = 0,
    @DateCheckIn NVARCHAR(100),
    @DateCheckOut NVARCHAR(100),
    @StadiumName NVARCHAR(100)
AS
BEGIN
    DECLARE @StadiumId INT;
    DECLARE @InsertedStadiumName NVARCHAR(100);

    -- Kiểm tra xem sân vận động đã tồn tại hay chưa
    IF NOT EXISTS (SELECT 1 FROM Stadium WHERE Ten = @StadiumName)
    BEGIN
        -- Nếu chưa tồn tại, thêm sân vận động mới
        INSERT INTO Stadium (Ten) VALUES (@StadiumName);
        SET @StadiumId = SCOPE_IDENTITY(); -- Lấy ID của sân vận động vừa được thêm vào
        SET @InsertedStadiumName = @StadiumName; -- Lưu tên sân vận động đã thêm vào
    END
    ELSE
    BEGIN
        -- Nếu sân vận động đã tồn tại, lấy ID của nó
        SELECT @StadiumId = id FROM Stadium WHERE Ten = @StadiumName;
        SET @InsertedStadiumName = @StadiumName; -- Lưu tên sân vận động đã tồn tại
    END

    -- Kiểm tra xem đã có đặt chỗ nào trùng giờ và trùng sân vận động không
    IF NOT EXISTS (
        SELECT 1
        FROM Customer
        WHERE idStadium = @StadiumId
        AND (
            (@DateCheckIn < DateCheckOut AND @DateCheckOut > DateCheckIn) -- Kiểm tra sự trùng lặp của thời gian
        )
    )
    BEGIN
        -- Thêm thông tin khách hàng vào bảng Customer
        INSERT INTO Customer (CustomerName, CustomerPhone, Price, DateCheckIn, DateCheckOut, idStadium)
        VALUES (@CustomerName, @CustomerPhone, @Price, @DateCheckIn, @DateCheckOut, @StadiumId);

        -- Trả về ID của khách hàng vừa được thêm vào và tên sân vận động
        SELECT SCOPE_IDENTITY() AS CustomerID, @InsertedStadiumName AS StadiumName;
    END
    ELSE
    BEGIN
        -- Trả về thông báo lỗi nếu có trùng lặp thời gian và sân vận động
        RAISERROR ('Đã có đặt chỗ trùng giờ cho sân vận động này.', 16, 1);
    END
END;
GO



--------------------------------------
--Sửa
Update Customer set CustomerName = N'', CustomerPhone = 132, price = 100, DateCheckIn = N'', DateCheckOut = N'', idStadium = 2 where id = 4

CREATE PROCEDURE UpdateCustomer
    @CustomerID INT,
    @CustomerName NVARCHAR(100),
    @CustomerPhone INT,
    @Price FLOAT,
    @DateCheckIn NVARCHAR(100),
    @DateCheckOut NVARCHAR(100)
AS
BEGIN
    UPDATE Customer
    SET 
        CustomerName = @CustomerName,
        CustomerPhone = @CustomerPhone,
        price = @Price,
        DateCheckIn = @DateCheckIn,
        DateCheckOut = @DateCheckOut
    WHERE 
        id = @CustomerID;
END;
go

create proc cusAndSta
as
begin
	select * from Customer c, Stadium s where s.id = c.idStadium
end
go

--------------------------------
-- Xóa 
delete Customer where id = 12

------------------------------------
--Thanh Toán
create proc MoveCusToBill
@id int
as
begin
	INSERT INTO Bill (CustomerName, CustomerPhone, price, CheckIn, CheckOut, idStadium)
    SELECT CustomerName, CustomerPhone, price, DateCheckIn, DateCheckOut, idStadium
    FROM Customer where id = @id

	update Bill set DateCheckOut = getdate() where id = @id

    DELETE FROM Customer where id = @id
end
go

----------------------------
-- danh sách hóa đơn

create proc USP_GetListBillByDate
@checkIn date, @checkOut date
as
begin
	select idStadium as N'Số sân', CustomerName as N'Tên', CustomerPhone as N'Số điện thoại', CheckIn as 'Giờ vào', CheckOut as 'Giờ ra', price as 'Giá' 
	from Bill where DateCheckIn >= @checkIn and DateCheckOut <= @checkOut
end 
go