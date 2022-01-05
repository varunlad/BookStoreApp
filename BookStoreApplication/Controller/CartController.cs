using BookStoreManager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel;

namespace BookStoreApplication.Controller
{
    public class CartController : ControllerBase
    {
        private readonly ICartManager manager;

        public CartController(ICartManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addcart")]
        public IActionResult Register([FromBody] CartModel cartModel)
        {
            try
            {
                string result = this.manager.AddCart(cartModel);
                if (result.Equals("Book is Added to cart"))
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
        [Route("api/deletecartitem")]
        public IActionResult DeleteCart(int id)
        {
            try
            {
                string result = this.manager.DeleteCart(id);
                if (result.Equals("Item is Deleted"))
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
        [Route("api/updatecartitem")]
        public IActionResult UpdateCart(int cartItemId, int QuantityUpdated)
        {
            try
            {
                string result = this.manager.UpdateCart(cartItemId, QuantityUpdated);
                if (result.Equals("Cart is Updated"))
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
        [Route("api/getcart")]
        public IActionResult GetCart(int id)
        {
            try
            {
                var result = this.manager.GetCart(id);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result, Message = " Retrived Cart" });
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
