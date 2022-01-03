using UserModel;

namespace UserRepository.Interface
{
    public interface IuserRepo
    {
        string connectionString { get; set; }

        string Register(SignupModel user);
        string Login(LoginModel login);
        string RestPassword(ResetPasswordModel resetPassword);
    }
}