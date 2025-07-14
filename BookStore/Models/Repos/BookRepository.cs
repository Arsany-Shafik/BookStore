
namespace BookStore.Models.Repos
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id = 1,
                    Title = "Java Programming",
                    Description = "No Description",
                    ImageUrl = "Notebook-PNG-Transparent-Photo.png",
                    Author = new Author{Id = 2}
                    
                },
                 new Book
                {
                    Id = 2,
                    Title = "C# Programming",
                    Description = "No Data",
                    ImageUrl = "kindpng_83424.png",
                    Author = new Author {Id = 1 }
                }, new Book
                {
                    Id = 3,
                    Title = "c++ Programming",
                    Description = "Nothing",
                    ImageUrl = "pnghut_paper-and-pencil-game-notebook-clip-art-writing-pen_Pzt2XKq5EJ.png",
                    Author = new Author{Id = 3}
                },
            };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(p => p.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(p => p.Id == id);
            return book;
        }

        public IList<Book> GetAll()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            return books.Where(p => p.Title.Contains(term)
            || p.Description.Contains(term)).ToList();
        }

        public void Update(int id, Book entity)
        {
            var book = Find(id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Author = entity.Author;
            book.ImageUrl = entity.ImageUrl;
        }
    }
}
