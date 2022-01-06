using UserModel;

namespace BookStoreManager.Interface
{
    public interface IAddressManager
    {
        string AddressDetail(AddressModel addressModel);
        string UpdateAddress(AddressModel addressModel);
    }
}