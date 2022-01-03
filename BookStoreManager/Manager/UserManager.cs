using BookStoreManagers.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using UserModel;
using UserRepository.Interface;

namespace BookStoreManagers.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IuserRepo repository;
        public UserManager(IuserRepo repository)
        {
            this.repository = repository;
        }
        public string Register(SignupModel userData)
        {
            try
            {
                return this.repository.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Login(LoginModel userData)
        {
            try
            {
                return this.repository.Login(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string RestPassword(ResetPasswordModel userData)
        {
            try
            {
                return this.repository.RestPassword(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ForgotPassword(string email)
        {
            try
            {
                return this.repository.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
