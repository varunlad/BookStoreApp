using BookStoreManager.Interface;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using UserModel;

namespace BookStoreManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepo repository;
        public AddressManager(IAddressRepo repository)
        {
            this.repository = repository;
        }
        public string AddressDetail(AddressModel addressModel)
        {
            try
            {
                return this.repository.AddressDetail(addressModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
