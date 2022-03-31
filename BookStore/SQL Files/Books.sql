create TABLE Books
(
	BookId int Identity(1,1) PRIMARY KEY,
	BookName varchar(100) not null,
	AuthorName varchar(70) not null,
    DiscountPrice money not null,   
	OriginalPrice  money not null,            
    BookDescription nvarchar(max),
    Rating float default 0,
    Reviewer int default 0 ,
    Image varchar(max)not null,
	BookCount int
	);

select * from Books

Create procedure Sp_AddBooks   
(   
    @BookName VARCHAR(200),    
    @AuthorName varchar(200),
    @DiscountPrice money ,   
	@OriginalPrice  money ,            
    @BookDescription nvarchar(max),
    @Rating float ,
    @Reviewer int  ,
    @Image varchar(max),
	@BookCount int    
)   
as    
Begin 
	Begin try   
		BEGIN TRANSACTION;
		Insert into Books (BookName,AuthorName,DiscountPrice,OriginalPrice,BookDescription,Rating,Reviewer,Image,BookCount)    
		Values (@BookName,@AuthorName,@DiscountPrice,@OriginalPrice,@BookDescription,@Rating,@Reviewer,@Image,@BookCount) 
		COMMIT TRANSACTION; 
	End try
	Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;  
		ROLLBACK TRANSACTION;  
	End catch  
End


create procedure sp_UpdateBooks   
( 
	@BookId int,
    @BookName VARCHAR(200),    
    @AuthorName varchar(200),
    @DiscountPrice money ,   
	@OriginalPrice  money ,            
    @BookDescription nvarchar(max),
    @Rating float ,
    @Reviewer int  ,
    @Image varchar(max),
	@BookCount int    
)   
as    
Begin 
	Begin try   
		BEGIN TRANSACTION;
		IF Exists(select * from Books where BookId = @BookId) 
		begin
			update Books set 
			BookName= @BookName ,
			AuthorName=@AuthorName,
			DiscountPrice=@DiscountPrice,
			OriginalPrice=@OriginalPrice,
			BookDescription=@BookDescription,
			Rating=@Rating,
			Reviewer=@Reviewer,
			Image=@Image,
			BookCount=@BookCount			
			where BookId = @BookId;
		end
		else
		begin
			Select 1;
		end
		COMMIT TRANSACTION; 
	End try
	Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;  
		ROLLBACK TRANSACTION;  
	End catch  
End

Create PROCEDURE spDeleteBooks
  @BookId int
AS
BEGIN
 Begin try
     IF(EXISTS(SELECT * FROM Books WHERE BookId=@BookId))
	 begin
	   delete from Books WHERE BookId=@BookId;
   	 end
	 else
	   Select 1;
 End try
 Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;    
 End catch 
End


Create PROCEDURE spGetBooks
  @BookId int
AS
BEGIN
 Begin try
     IF(EXISTS(SELECT * FROM Books WHERE BookId=@BookId))
	 begin
	   SELECT * FROM Books WHERE BookId=@BookId;
   	 end
 End try
 Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;    
 End catch 
End


Create procedure spGetAllBook     
Begin  
	select * from Books
End