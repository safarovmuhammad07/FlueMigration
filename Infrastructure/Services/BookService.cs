using System.Net;
using Azure;
using Dapper;
using DoMain.Dtos;
using DoMain.Entities;
using Infrastructure.ApiResponce;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class BookService(DapperContext context):IBookService
{
    public async Task<Responce<List<Book>>> GetBooksAsync()
    {
        var sql = @"select * from Books";
        var res = await context.GetConnection().QueryAsync<Book>(sql);
        return new Responce<List<Book>>(res.ToList());
    }

    public async Task<Responce<Book>> GetBookByIdAsync(int id)
    {
        var sql = @"select * from Books where BookId = @id";
        var res = await context.GetConnection().QueryFirstOrDefaultAsync<Book>(sql, new { id });
        return new Responce<Book>(res);
    }

    public async Task<Responce<BookWithAythorDto>> GetBookByAuthorIdAsync(int id)
    {
        const string sql = @"select * from Books as b join  authors as a on b.AuthorId = a.Id"; 
        var res = await context.GetConnection().QueryFirstOrDefaultAsync<BookWithAythorDto>(sql, new { id });
       return res==null
           ? new Responce<BookWithAythorDto>(HttpStatusCode.InternalServerError,"Internal Server Error")
           : new Responce<BookWithAythorDto>(res);
    }

    public async Task<Responce<bool>> AddBookAsync(Book author)
    {
        const string sql = @"insert into Books(Title, AuthorId, PublishedYear, Genre, IsAvailable) values (@Title, @AuthorId, @PublishedYear, @Genre, @IsAvailable)";
        var res = await context.GetConnection().ExecuteAsync(sql, author);
        return res==0
          ? new Responce<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
          : new Responce<bool>(HttpStatusCode.Created, "Added Successfully");
    }

    public async Task<Responce<bool>> UpdateBookAsync(Book book)
    {
        const string sql = @"inser into Books(BookId, Title, AuthorId, PublishedYear, Genre, IsAvailable) values (@BookId, @Title, @AuthorId, @PublishedYear, @Genre, @IsAvailable)";
        var res = await context.GetConnection().ExecuteAsync(sql, book);
        return res == 0
            ? new Responce<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<bool>(HttpStatusCode.OK, "Updated Successfully");
    }

    public async Task<Responce<bool>> DeleteBookAsync(int id)
    {
        var sql = @"delete from Books where BookId = @id";
        var res = await context.GetConnection().ExecuteAsync(sql, new { id });
        return res == 0
          ? new Responce<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
          : new Responce<bool>(HttpStatusCode.OK, "Deleted Successfully");
    }
}