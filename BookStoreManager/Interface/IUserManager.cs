using UserModel;

namespace BookStoreManagers.Interface
{
    public interface IUserManager
    {
        string Register(SignupModel userData);
        string Login(LoginModel userData);
    }
}