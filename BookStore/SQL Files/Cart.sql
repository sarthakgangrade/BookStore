CREATE TABLE Cart
(
	CartID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL
	FOREIGN KEY (UserId) REFERENCES RegisterUser(UserId),
	BookId INT NOT NULL
	FOREIGN KEY (BookId) REFERENCES Books(BookId),	
	OrderQuantity INT default 1
);

select * from Cart

Create PROCEDURE sp_AddingCart(
	@UserId INT,
	@BookId INT,
	@OrderQuantity INT)
AS
BEGIN
	IF (EXISTS(SELECT * FROM Books WHERE BookId=@BookId))		
	begin
		INSERT INTO Cart(UserId,BookId,OrderQuantity)
		VALUES (@UserId,@BookId,@OrderQuantity)
	end
	else
	begin 
		select 2
	end
END



CREATE PROCEDURE sp_DeleteCartDetails
	@CartID INT
AS
BEGIN
	IF EXISTS(SELECT * FROM Cart WHERE CartID = @CartID)
	BEGIN
		DELETE FROM Cart WHERE CartID = @CartID
	END
	ELSE
	BEGIN
		select 1
	END
END


CREATE PROCEDURE sp_GetCartDetails
	@UserId INT
AS
BEGIN
	SELECT
		Cart.CartID,
		Cart.UserId,
		Cart.BookId,
		Cart.OrderQuantity,	
		Books.BookName,
		Books.AuthorName,
		Books.DiscountPrice,
		Books.OriginalPrice, 
		Books.BookDescription ,
		Books.Rating,
		Books.Reviewer,
		Books.Image,
		Books.BookCount
	FROM Cart
	Inner JOIN Books ON Cart.BookId = Books.BookId
	WHERE Cart.UserId = @UserId
END


CREATE PROC sp_UpdateQuantity
	@CartID INT,
	@OrderQuantity INT
AS
BEGIN
	IF (EXISTS(SELECT * FROM Cart WHERE CartID = @CartID))
	BEGIN
			UPDATE Cart
			SET
				OrderQuantity = @OrderQuantity
			WHERE
				CartID = @CartID;
		END
		ELSE
		BEGIN
			Select 1;
		END
END