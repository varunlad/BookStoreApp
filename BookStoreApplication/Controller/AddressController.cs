using BookStoreManager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel;

namespace BookStoreApplication.Controller
{
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager manager;

        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addressdetails")]
        public IActionResult AddressDetail([FromBody] AddressModel addressModel)
        {
            try
            {
                string result = this.manager.AddressDetail(addressModel);
                if (result.Equals("Address is Added"))
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
        [Route("api/updateaddress")]
        public IActionResult UpdateAddress([FromBody] AddressModel address)
        {
            try
            {
                string result = this.manager.UpdateAddress(address);
                //logger.LogInformation("A new User reset password with email " + reset.Email);

                if (result.Equals("Address is Updated"))
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
    }
}
