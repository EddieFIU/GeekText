  declare @bookId int
  declare @userID int

  select top 1 @bookId=BookID from Books
  select top 1 @userID=Users.UserID from USERS
  if (@bookId>0 and @userID>0)
  begin
	insert into rating
	(RatingDate, RatingValue, RatingUser, BookID)
	values(getdate(),5,@userID,@bookId)
	insert into rating
	(RatingDate, RatingValue, RatingUser, BookID)
	values(getdate(),4,@userID,@bookId)
insert into rating
	(RatingDate, RatingValue, RatingUser, BookID)
	values(getdate(),3,@userID,@bookId)
insert into rating
	(RatingDate, RatingValue, RatingUser, BookID)
	values(getdate(),2,@userID,@bookId)
insert into rating
	(RatingDate, RatingValue, RatingUser, BookID)
	values(getdate(),1,@userID,@bookId)

	
	insert into Comment (UserID,RatingID,BookID,Comment,CreateDate)
	select @userID, Rating.RatingID, @bookId, 'this is a test comment for rating ' + convert(varchar(2), Rating.RatingID), GETDATE()
	from Rating

  end

  

  