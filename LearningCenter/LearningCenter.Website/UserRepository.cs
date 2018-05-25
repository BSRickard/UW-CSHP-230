using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningCenter.Website.Repository
{
    public interface IUserRepository
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

    public class UserRepository : IUserRepository
    {
        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                && t.UserPassword == password);

            if (null == user)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        public UserModel Register(string email, string password)
        {
            if (null != DatabaseAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()))
            {
                return null;
            }

            var user = DatabaseAccessor.Instance.Users
                .Add(new User { UserEmail = email, UserPassword = password });
            DatabaseAccessor.Instance.SaveChanges();
            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        public UserModel GetUser(int userId)
        {
            var user = DatabaseAccessor.Instance.Users
                .First(t => t.UserId == userId);
            if (null == user)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }
    }
}