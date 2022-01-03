
using BookStoreManagers.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using UserModel;

namespace BookStoreRepo.Controller
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody] SignupModel userData)
        {
            try
            {
                string result = this.manager.Register(userData);
                if (result.Equals("Registration Successful"))
                {
                    //this.logger.Info(result + Environment.NewLine + DateTime.Now);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    //this.logger.Warn(result + Environment.NewLine + DateTime.Now);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                //this.logger.Error(ex.Message + Environment.NewLine + DateTime.Now);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel userData)
        {
            try
            {
                string result = this.manager.Login(userData);
                if (result.Equals("Login Successful"))
                {
                    //this.logger.Info(result + Environment.NewLine + DateTime.Now);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    //this.logger.Warn(result + Environment.NewLine + DateTime.Now);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                //this.logger.Error(ex.Message + Environment.NewLine + DateTime.Now);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
