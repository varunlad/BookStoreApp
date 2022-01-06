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
    }
}
