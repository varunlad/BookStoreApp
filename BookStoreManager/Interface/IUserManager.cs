using System.Collections.Generic;
using UserModel;

namespace BookStoreManagers.Interface
{
    public interface IUserManager
    {
        string Register(SignupModel userData);
        string Login(LoginModel userData);
        string RestPassword(ResetPasswordModel userData);
        string ForgotPassword(string email);
    }
}