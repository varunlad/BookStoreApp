using BookStoreManager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel;

namespace BookStoreApplication.Controller
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager manager;

        public OrderController(IOrderManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addorder")]
        public IActionResult Register([FromBody] OrderModel orderModel)
        {
            try
            {
                string result = this.manager.AddOrder(orderModel);
                if (result.Equals("Added to Order"))
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
        [Route("api/getorder")]
        public IActionResult GetOrderList(int UserId)
        {
            try
            {
                var result = this.manager.GetOrderList(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result, Message = " Retrived Order" });
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
