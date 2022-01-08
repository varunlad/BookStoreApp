using BookStoreManager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel;

namespace BookStoreApplication.Controller
{
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackManager manager;

        public FeedBackController(IFeedBackManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addfeedback")]
        public IActionResult AddFeedback([FromBody] FeedBackModel feedBack)
        {
            try
            {
                string result = this.manager.AddFeedBack(feedBack);
                if (result.Equals("FeedBack is Recorder"))
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
