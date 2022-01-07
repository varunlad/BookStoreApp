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
    }
}
