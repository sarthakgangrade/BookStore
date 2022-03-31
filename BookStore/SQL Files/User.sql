Create database BookStoreDB;

Use BookStoreDB
Create table RegisterUser
(
UserId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
FullName varchar(50) NOT NULL,
Email varchar(50) NOT NULL,
Password varchar(20) NOT NULL,
PhoneNo varchar(15) NOT NULL,
)

alter procedure sp_AddUsers
(   
    @FullName VARCHAR(50),
    @Email VARCHAR(50), 
	@Password VARCHAR(50),
    @PhoneNo VARCHAR(50)	
)   
as  
Begin    
    Insert into RegisterUser   
	Values (@FullName,@Email,@Password,@PhoneNo)    
End

exec sp_AddUsers 'Harsh Singh','harsh@gmail.com','123456','9155146677';



CREATE PROCEDURE sp_Login
(
	@Email varchar(100),
	@Password varchar(400)
)
AS
BEGIN
	SELECT Email, Password FROM RegisterUser 
	WHERE @Email=Email AND @Password=Password
END;


CREATE PROCEDURE sp_ForgetPassword
(
	@Email varchar(100)
)
AS
BEGIN
	SELECT UserId,Email FROM RegisterUser 
	WHERE @Email=Email 
END

create procedure sp_ResetPassword
 (
    @Email varchar(30),
	@Password varchar(40)
)
 as
 begin
	 Update RegisterUser 
	 SET Password=@Password
	 where Email=@Email
	 Select * from RegisterUser where Email=@Email; 
 End;

 select * from RegisterUser