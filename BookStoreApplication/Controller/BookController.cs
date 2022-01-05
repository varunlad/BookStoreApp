using BookStoreManager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel;

namespace BookStoreApplication.Controller
{
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;

        public BookController(IBookManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/bookdetails")]
        public IActionResult Register([FromBody] BookModel userData)
        {
            try
            {
                string result = this.manager.Register(userData);
                if (result.Equals("Book is Added"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("api/deletebook")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                string result = this.manager.DeleteBook(id);
                if (result.Equals("Book is Deleted"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/updatebook")]
        public IActionResult ResetPassword([FromBody] BookModel bookModel)
        {
            try
            {
                string result = this.manager.UpdateBook(bookModel);
                //logger.LogInformation("A new User reset password with email " + reset.Email);

                if (result.Equals("Book is Updated"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                //logger.LogWarning("Exception cought while Reset password" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/getbook")]
        public IActionResult GetBookId(int id)
        {
            try
            {
                object result = this.manager.GetBookId(id);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<object>() { Status = true, Data = result, Message = "Book is Successfully Display" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<object>() { Status = false, Message = "Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<object>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/getallbooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.manager.DisplayAllBooks();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result, Message = "Book is Retrived" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Book Not Retrived" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
