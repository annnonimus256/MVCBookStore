using BookStore.EntityFramework;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Exceptions;

namespace BookStore.Application
{
    public class UserMethod
    {
        public static UserModel GetUser(int id)
        {

            using (var context = new DataBaseModel())
            {
                var user = (from u in context.Users
                            where u.Id == id
                            select new UserModel
                            {
                                Name = u.Name,
                                Email = u.Email,
                                Id = u.Id,

                            }).FirstOrDefault();

                if (user == null)
                    throw new Exception("User not exist");
                return user;
            }

        }

        public static int? Login(LoginModel model)
        {
            using (var db = new DataBaseModel())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == model.Name && u.Password == model.Password);

                return user?.Id;
            }
        }

        public static void ChangeSettings(int userId, SettingModel model)
        {
            using (var db = new DataBaseModel())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);

                if (user.Password != model.Password)
                    throw new SettingsException("Password not match");

                user.Password = model.NewPassword;
                db.SaveChanges();
            }
        }
    }
}