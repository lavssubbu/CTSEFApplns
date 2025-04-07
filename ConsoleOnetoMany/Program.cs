// See https://aka.ms/new-console-template for more information
using CTSCodeFirstOnetoMany.Models;
using CTSCodeFirstOnetoMany.Repository;

using var context = new BookContext();

BookRepository repo = new BookRepository(context);


IEnumerable<Book> res3 = repo.GetBooks();
foreach (Book buk in res3)
{
    Console.WriteLine(buk.Title + " " + buk.Author.Name);
}
Book bk = repo.GetBook(111);
Console.WriteLine(bk.Title + " " + bk.Author.Name);
IEnumerable<Author> res = repo.GetAuthorsByBook("DotNet");
foreach(Author author in res)
{
    Console.WriteLine(author.Name+" "+author.Books);
}
IEnumerable<Book> res2 =repo.GetBooksbyAuthor("Stephen");
foreach (Book buk in res2)
{
    Console.WriteLine(buk.Title+" "+buk.Author.Name);
}




