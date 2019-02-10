using BookStore.Application;
using BookStore.EntityFramework;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication5.Exceptions;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                var UserId = UserMethod.Login(model);

                if (UserId != null)
                {
                    FormsAuthentication.SetAuthCookie(UserId.ToString(), true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (var db = new DataBaseModel())
                {
                    user = db.Users.FirstOrDefault(u => u.Name == model.Name);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (var db = new DataBaseModel())
                    {
                        db.Users.Add(new User { Name = model.Name, Email = model.Email, Password = model.Password});
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Name == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }




            return View(model);
        }



        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Setting(SettingModel model)
        {
            var UserId = int.Parse(User.Identity.Name);

            try
            {
                UserMethod.ChangeSettings(UserId, model);
            }
            catch (SettingsException ex)
            {
                ModelState.AddModelError("Password", ex.Message);

            }
            return View();
        }

        public ActionResult Setting()
        {

            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }




        public ActionResult newBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult newBook(NewBookModel model)
        {
            var UserId = int.Parse(User.Identity.Name);


            if (ModelState.IsValid)
            {
                Book book = null;
                //using (var db = new DataBaseModel())
                //{
                //    book = db.Books.FirstOrDefault(u => u.Name == model.Name);
                //}
                //if (book == null)
                //{
                    // создаем новую книгу
                    using (var db = new DataBaseModel())
                    {
                        db.Books.Add(new Book { Name = model.Name, Autor = model.Autor, Price = model.Price, UserId = UserId });
                        db.SaveChanges();


                    }
                    // если пользователь удачно добавлен в бд
                    //if (book != null)
                    //{
                        return RedirectToAction("Buy", "Home");
                    //}

                //}
                //else
                //{
                //    ModelState.AddModelError("", "Такая книга уже существует");
                //}
            }
            return View(model);
        }
        public ActionResult DeleteBook()
        {

            return View();
        }

    }
}

//public ActionResult Register(RegisterModel model)
//{
//    if (ModelState.IsValid)
//    {
//        User user = null;
//        using (var db = new DataBaseModel())
//        {
//            user = db.Users.FirstOrDefault(u => u.Name == model.Name);
//        }
//        if (user == null)
//        {
//            // создаем нового пользователя
//            using (var db = new DataBaseModel())
//            {
//                db.Users.Add(new User { Name = model.Name, Email = model.Email, Password = model.Password });
//                db.SaveChanges();

//                user = db.Users.Where(u => u.Name == model.Name && u.Password == model.Password).FirstOrDefault();
//            }
//            // если пользователь удачно добавлен в бд
//            if (user != null)
//            {
//                FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
//                return RedirectToAction("Index", "Home");
//            }
//        }
//        else
//        {
//            ModelState.AddModelError("", "Пользователь с таким логином уже существует");
//        }
//    }




//    return View(model);
//}
