using System.Collections.Generic;
using UserModel;

namespace BookStoreManager.Interface
{
    public interface IAddressManager
    {
        string AddressDetail(AddressModel addressModel);
        string UpdateAddress(AddressModel addressModel);
        IEnumerable<AddressModel> DisplayAllAddress();
        IEnumerable<AddressModel> DisplayAddressById(int UserId);
    }
}