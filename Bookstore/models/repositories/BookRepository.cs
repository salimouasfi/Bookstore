using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.models.repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id= 1,
                    Title=" C# programming ",
                    Description="No description",
                    ImageUrl= "OIP.jpg",
                    Author = new Author
                    {
                        Id = 2
                    }
                },
                new Book
                {
                    Id= 2,
                    Title=" Python programming ",
                    Description="No description",
                    ImageUrl = "python.jpg",
                     Author = new Author
                    {
                        Id = 1
                    }
                },
                new Book
                {
                    Id= 3,
                    Title=" PHP programming ",
                    Description="No description",
                    ImageUrl = "php.jpg",
                    Author = new Author{}
                }
            };
        }

        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) +1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b=> b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            return books.Where(b=> b.Title.Contains(term)).ToList();
        }

        public void Update(int id,Book newBook)
        {
            var book = Find(id);
            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.ImageUrl = newBook.ImageUrl;
        }
    }
}
