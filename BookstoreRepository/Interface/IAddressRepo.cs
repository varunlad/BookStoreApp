using System.Collections.Generic;
using UserModel;

namespace BookstoreRepository.Interface
{
    public interface IAddressRepo
    {
        string connectionString { get; set; }

        string AddressDetail(AddressModel addressModel);
        string UpdateAddress(AddressModel addressModel);
        IEnumerable<AddressModel> DisplayAllAddress();
        IEnumerable<AddressModel> DisplayAddressById(int UserId);
    }
}