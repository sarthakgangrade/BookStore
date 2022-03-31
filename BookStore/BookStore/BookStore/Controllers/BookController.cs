using BusinessLayer.Interface;
using CommonLayer.Models;
using Google.Apis.Admin.Directory.directory_v1.Data;
using ICSharpCode.Decompiler.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("addBooks")]
        public IActionResult AddBook(BookModel book)
        {
            try
            {
                string result = this.bookBL.AddBook(book);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Book Added Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"Book Added UnSuccessfull"});
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize("Admin")]
        [HttpPut]
        [Route("updateBook/{BookId}")]
        public IActionResult UpdateBookDetails(UpdateBookModel update, int BookId)
        {
            try
            {
                string result = this.bookBL.UpdateBookDetails(update,BookId);
                if (result.Equals(true))
                {
                    return this.BadRequest(new { Status = false, Message = $"Book not updated" });     
                }
                else
                {
                    return this.Ok(new { success = true, message = $"Book Update Successfully" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize ("Admin")]
        [HttpDelete]
        [Route("deleteBook/{BookId}")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                string result = this.bookBL.DeleteBook(BookId);
                if (result.Equals(true))
                {
                    return this.BadRequest(new { Status = false, Message = $"Unable To Delete" });
                }
                else
                {
                    return this.Ok(new { success = true, message = $"Delete Successfully" });
                   
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize("Customer")]
        [HttpGet]
        [Route("getAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.bookBL.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = " All book details Given", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No book exists"});
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize ("Admin")]
        [HttpGet]
        [Route("getAllBooksbyBookId/{BookId}")]
        public IActionResult GetAllBooksByBookId(int BookId)
        {
            try
            {
                //BookModel model = bookBL.GetAllBooks().Where(e => e.BookId == BookId).FirstOrDefault();
                //var BookList = new List<BookModel>();
                var BookList = this.bookBL.GetAllBooksbyBookId(BookId);
                if (BookList != null)
                {
                    return this.Ok(new { Status = true, Message = "Book details of given ID is Sucessfull", Data = BookList });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "BookId doesn't exists" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
