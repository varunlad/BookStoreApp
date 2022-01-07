using BookStoreManager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel;

namespace BookStoreApplication.Controller
{
    public class WishListController : ControllerBase
    {
        private readonly IWishListManager manager;

        public WishListController(IWishListManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addwishlist")]
        public IActionResult AddToWishList(int UserId, int BookId)
        {
            try
            {
                string result = this.manager.AddToWishList(UserId, BookId);
                if (result.Equals("Added to WishList"))
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
        [Route("api/Deletewishlist")]
        public IActionResult DeletWishListItem(int WishListId)
        {
            try
            {
                string result = this.manager.DeleteWishListItem(WishListId);
                if (result.Equals("Book Removed From Wishlist"))
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
        [HttpGet]
        [Route("api/getwishlist")]
        public IActionResult GetWishlist(int UserId)
        {
            try
            {
                var result = this.manager.GetWishList(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result, Message = " Retrived Wishlist" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Not Retrived Cart" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
