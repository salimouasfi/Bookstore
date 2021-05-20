using Bookstore.models;
using Bookstore.models.repositories;
using Bookstore.viewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.controlers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookRepo;
        private readonly IBookstoreRepository<Author> authorRepo;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookstoreRepository<Book> bookRepo,IBookstoreRepository<Author> authorRepo,
            IHostingEnvironment hosting )
        {
            this.bookRepo = bookRepo;
            this.authorRepo = authorRepo;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepo.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepo.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View(LoadAuthorsListModel());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel bookModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = UploadFile(bookModel.File) ?? string.Empty;
                    

                    if (bookModel.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select an author from the list!";

                        return View(LoadAuthorsListModel());
                    }
                    var author = authorRepo.Find(bookModel.AuthorId);
                    Book book = new Book
                    {
                        Id = bookModel.BookId,
                        Title = bookModel.Title,
                        Description = bookModel.Description,
                        Author = author,
                        ImageUrl = fileName
                    };
                    bookRepo.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }
            ModelState.AddModelError("", "Please fill all required fields");
            return View(LoadAuthorsListModel());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepo.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var model = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepo.List().ToList(),
                ImageUrl = book.ImageUrl
            };
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel bookModel)
        {
            try
            {
                string fileName = UploadFile(bookModel.File,bookModel.ImageUrl);
                
                var author = authorRepo.Find(bookModel.AuthorId);
                Book book = new Book
                {
                    Id = bookModel.BookId,
                    Title = bookModel.Title,
                    Description = bookModel.Description,
                    Author = author,
                    ImageUrl = fileName
                };
                bookRepo.Update(bookModel.BookId,book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepo.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult confirmDelete(int id)
        {
            try
            {   bookRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public BookAuthorViewModel LoadAuthorsListModel()
        {
            var model = new BookAuthorViewModel
            {
                Authors = authorRepo.List().ToList()
            };
            model.Authors.Insert(0, new Author { Id = -1, FullName = "--- Please select an author ---" });
            return model;
        }

        string UploadFile(IFormFile File)
        {
            if (File != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                
                string fullPath = Path.Combine(uploads, File.FileName);

                File.CopyTo(new FileStream(fullPath, FileMode.Create));

                return File.FileName;
            }
            return null;
        }

        string UploadFile(IFormFile File,string imageUrl)
        {
            if (File != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, File.FileName);

                string oldPath = Path.Combine(uploads, imageUrl);

                if (newPath != oldPath)
                {
                    System.IO.File.Delete(oldPath);
                    File.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return File.FileName;
            }
            return imageUrl;
        }

        public ActionResult Search(string term)
        {
            var result = bookRepo.Search(term);
            return View("Index", result);
        }
    }
}
