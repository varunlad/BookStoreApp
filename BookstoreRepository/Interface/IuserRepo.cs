using UserModel;

namespace UserRepository.Interface
{
    public interface IuserRepo
    {
        string connectionString { get; set; }

        string Register(SignupModel user);
    }
}