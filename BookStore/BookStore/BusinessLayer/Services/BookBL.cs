using BusinessLayer.Interface;
using CommonLayer.Models;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public string AddBook(BookModel book)
        {
            try
            {
                return bookRL.AddBook(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string UpdateBookDetails(UpdateBookModel update,int BookId)
        {
            try
            {
                return bookRL.UpdateBookDetails(update,BookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string DeleteBook(int BookId)
        {
            try
            {
                return bookRL.DeleteBook(BookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return bookRL.GetAllBooks();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BookModel> GetAllBooksbyBookId(int BookId)
        {
            try
            {
                return bookRL.GetAllBooksbyBookId(BookId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
