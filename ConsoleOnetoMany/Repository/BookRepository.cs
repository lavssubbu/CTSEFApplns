using CTSCodeFirstOnetoMany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CTSCodeFirstOnetoMany.Repository
{
    internal class BookRepository
    {
        private readonly BookContext _context;
        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void AddBook(Book bk)
        {
            _context.Books.Add(bk);
            _context.SaveChanges();
        }

        public IEnumerable<Author> GetAuthor()
        {
            return _context.Authors.ToList();
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.Include(b => b.Author).ToList();
        }

        public Book GetBook(int id)
        {
            return _context.Books.FirstOrDefault(x => x.BookId == id)!;
        }

        public IEnumerable<Book> GetBooksbyAuthor(string name)
        {
            IEnumerable<Book> res = _context.Books.Include(x => x.Author)
                                       .Where(x => x.Author.Name == name);
            return res;

        }

        public IEnumerable<Author> GetAuthorsByBook(string name)
        {
            IEnumerable<Author> res = from auth in _context.Authors
                                      where auth.Name == name
                                      select auth;
            return res;
        }
    }
}
