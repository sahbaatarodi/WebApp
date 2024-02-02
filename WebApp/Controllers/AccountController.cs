using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }


        private readonly ApplicationDbContext _dbContext;

        public AccountController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Account()
        {
            // Fetch all books from the database and pass them to the view
            var allBooks = _dbContext.BooksTable.ToList();
            return View(allBooks);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Fetch the book to be edited from the database
            var book = _dbContext.BooksTable.FirstOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book model)
        {
            if (ModelState.IsValid)
            {
                // Update the book in the database
                _dbContext.Entry(model).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return RedirectToAction("Account");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Fetch the book to be deleted from the database
            var book = _dbContext.BooksTable.FirstOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete the book from the database
            var book = _dbContext.BooksTable.FirstOrDefault(b => b.BookId == id);

            if (book != null)
            {
                _dbContext.BooksTable.Remove(book);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Account");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book model)
        {
            if (ModelState.IsValid)
            {
                // Add the new book to the database
                _dbContext.BooksTable.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction("Account");
            }

            return View(model);
        }
    
}

}
