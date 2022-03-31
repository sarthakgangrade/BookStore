using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IBookRL
    {
        public string AddBook(BookModel book);
        public string UpdateBookDetails(UpdateBookModel update,int BookId);
        public string DeleteBook(int BookId);
        public List<BookModel> GetAllBooks();
        public List<BookModel> GetAllBooksbyBookId(int BookId);
    }
}
