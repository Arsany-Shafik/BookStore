using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.Repos
{
    public class BookDBRepo : IBookStoreRepository<Book>
    {
        BookStoreDbContext db;
        public BookDBRepo(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
            return book;
        }

        public IList<Book> GetAll()
        {
            return db.Books.Include(p => p.Author).ToList();
        }

        public void Update(int id, Book entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }
        public List<Book> Search(string term)
        {
            var result = db.Books.Include(p => p.Author)
                .Where(p => p.Title.Contains(term)
                        || p.Description.Contains(term)
                        || p.Author.Fullname.Contains(term)).ToList();
             
            return result;
        }
    }
}
