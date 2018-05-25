using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.Website.Repository;

namespace LearningCenter.Website.Business
{
    public interface IUserManager
    {
        UserModel LogIn   (string email, string password);
        UserModel Register(string email, string password);
        UserModel GetUser (int userId);
    }

    public class UserModel
    {
        public int    Id   { get; set; }
        public string Name { get; set; }
    }

    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel LogIn(string email, string password)
        {
            var user = userRepository.LogIn(email, password);

            if (null == user)
            {
                return null;
            }

            return new UserModel { Id = user.Id, Name = user.Name };
        }

        public UserModel Register(string email, string password)
        {
            var user = userRepository.Register(email, password);

            if (null == user)
            {
                return null;
            }

            return new UserModel { Id = user.Id, Name = user.Name };
        }

        public UserModel GetUser(int userId)
        {
            var user = userRepository.GetUser(userId);

            if (null == user)
            {
                return null;
            }

            return new UserModel { Id = user.Id, Name = user.Name };
        }
    }
}