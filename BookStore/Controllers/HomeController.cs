using BookStore.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BookStore.EntityFramework;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (User.Identity.IsAuthenticated)
            {
                var UserId = int.Parse(User.Identity.Name);

                var userDetails = UserMethod.GetUser(UserId);
                ViewBag.UserName = userDetails.Name;

            }
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        DataBaseModel db = new DataBaseModel();

        public ActionResult Index()
        {
            return View(db.Books);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Buy()
        {
            return View(db.Books);
        }

        public ActionResult Book(int id)
        {
            var model = BookMethod.GetBook(id);

            return View(model);
        }

        //[HttpPost]
        //public string BuyBook(Order order)
        //{
        //    order.Date = DateTime.Now;
        //    db.Orders.Add(order);
        //    db.SaveChanges();
        //    return "Спасибо," + order.Person + "за покупку!";

        //}

        public ActionResult BuyBook(int id)
        {
            Book b = db.Books.Find(id);
            if (b != null)
            {
                var UserId = int.Parse(User.Identity.Name);
                b.UserId = UserId;
                db.SaveChanges();

                db.Orders.Add(new Order { UserId = UserId, BookId = b.Id, Date = DateTime.Now, BookName = b.Name });
                db.SaveChanges();


            }


            return RedirectToAction("Index");
        }

        public ActionResult DeleteBook(int id)
        {
            Book b = db.Books.Find(id);
            if (b != null)
            {
                db.Books.Remove(b);
                db.SaveChanges();


            }
            return RedirectToAction("Index");
        }






        //public ActionResult Book(int id, BuyBookModel model)
        //{
        //    using (var db = new DataBaseModel())
        //    {
        //        db.Books.Add(new Book { Name = model.Name, Autor = model.Autor, UserId = User.})
        //        }
        //    return View(db.Books);

        //}

        //        using (var db = new DataBaseModel())
        //{
        //    db.Books.Add(new Book { Name = model.Name, Autor = model.Autor, UserId = User.})
        //}
        //    return View(db.Books);

    }
}
