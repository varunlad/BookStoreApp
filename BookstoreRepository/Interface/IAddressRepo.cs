using UserModel;

namespace BookstoreRepository.Interface
{
    public interface IAddressRepo
    {
        string connectionString { get; set; }

        string AddressDetail(AddressModel addressModel);
    }
}