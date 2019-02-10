using BookStore.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;

namespace BookStore.Application
{
    public class BookMethod
    {
        public static BookModel GetBook(int id)
        {

            using (var context = new DataBaseModel())
            {
                var Book = (from u in context.Books
                            where u.Id == id
                            select new BookModel
                            {
                                Name = u.Name,
                                Autor = u.Autor,
                                Id = u.Id,
                                Price = u.Price,
                                UserName = u.User.Name,
                                UserId = u.User.Id
                            }).FirstOrDefault();

                return Book;
            }
        }

        public static void BuyBook(int UserId, int BookId)
        {

            using (var context = new DataBaseModel())
            {
                var order = new Order
                {
                    BookId = BookId,
                    UserId = UserId,
                    Date = DateTime.Now
                };


                context.Orders.Add(order);
                context.SaveChanges();

            }
        }
    }
}