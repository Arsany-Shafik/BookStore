
namespace BookStore.Models.Repos
{
    public class AuthorDBRepo : IBookStoreRepository<Author>
    {
        BookStoreDbContext db;
        public AuthorDBRepo(BookStoreDbContext _db)
        {
           db= _db;
        }
        public void Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = db.Authors.SingleOrDefault(p => p.Id == id);
            return author;
        }

        public IList<Author> GetAll()
        {
            return db.Authors.ToList();
        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(p => p.Fullname.Contains(term)).ToList();
        }

        public void Update(int id, Author entity)
        { 
            db.Update(entity);
            db.SaveChanges();
        }
    }
}

