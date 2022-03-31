create Table Admin(
AdminId int primary key identity(1,1),
AdminName varchar(15),
MailId varchar(20),
Password varchar(10),
)

Create procedure sp_AddAdmin
@AdminName varchar(15),
@MailId varchar(30),
@Password varchar(10)
As
Begin
Insert into Admin (AdminName,MailId,Password) values (@AdminName,@MailId,@Password)
End


CREATE PROCEDURE sp_LoginAdmin
(
	@MailID varchar(100),
	@Password varchar(400)
)
AS
BEGIN
	SELECT MailID, Password FROM Admin 
	WHERE @MailID=MailId AND @Password=Password
END;


select * from Admin