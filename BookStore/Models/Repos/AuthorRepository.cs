
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Models.Repos
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {
        IList<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author() {
                    Id = 1,
                    Fullname = "Arsany Shafik"
                },
                new Author() {
                    Id = 2,
                    Fullname = "Fady Shafik"
                },
                new Author() {
                    Id = 3,
                    Fullname = "Mario Medhat"
                },
            };
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(p => p.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
           var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(p=>p.Id==id);
            return author;
        }

        public IList<Author> GetAll()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(p => p.Fullname.Contains(term)).ToList();
        }

        public void Update(int id, Author entity)
        {
            var author = Find(id);
            author.Fullname = entity.Fullname;
        }
    }
}
