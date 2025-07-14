using System.Reflection;
using BookStore.Models;
using BookStore.Models.Repos;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStore.Contrlloers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorRepository;
        private readonly IWebHostEnvironment hosting;

        public BookController(IBookStoreRepository<Book> bookRepository, IBookStoreRepository<Author> authorRepository, IWebHostEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.GetAll();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View(GetAllAuthors());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "There are validation errors! Please check your input.");
                return View(GetAllAuthors());
            }

            try
            {
                if (model.AuthorId == -1)
                {
                    ViewBag.Message = "Please select an author from the list!";

                    return View(GetAllAuthors());

                }
                string filename = UploadFile(model.ImageUrl) ?? string.Empty;

                Book book = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = authorRepository.Find(model.AuthorId),
                    ImageUrl = filename
                };
                bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(GetAllAuthors());
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authorID = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorID,
                Authors = FillSelectList(),
                ImageEdit = book.ImageUrl
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "There are validation errors! Please check your input.");
                return View(GetAllAuthors());
            }
            try
            {
                if (model.AuthorId == -1)
                {
                    ViewBag.Message = "Please select an author from the list!";
                    return View(GetAllAuthors());

                }
                string filename = UploadFile(model.ImageUrl,model.ImageEdit);

                Book book = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = authorRepository.Find(model.AuthorId),
                    ImageUrl = filename
                };
                bookRepository.Update(model.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(GetAllAuthors());
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> FillSelectList()
        {
            var authors = authorRepository.GetAll().ToList();
            authors.Insert(0, new Author { Id = -1, Fullname = "--- Please select an author ---" });
            return authors;
        }
        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList(),

            };
            return vmodel;
        }
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }
            return null;
        }
        string UploadFile(IFormFile file, string oldFileName)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string newPath = Path.Combine(uploads, file.FileName);
                //Delete old image
                string oldPath = Path.Combine(uploads, oldFileName);
                if (newPath != oldPath)
                {
                    System.IO.File.Delete(oldPath);
                    //Save the new image
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return file.FileName;
            }
            return oldFileName;
        }
        public ActionResult Search (string term)
        {
            var result = bookRepository.Search(term);
            return View("Index", result);
        }
    }
}
