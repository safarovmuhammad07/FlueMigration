using System.Net;
using Azure;
using Dapper;
using DoMain.Entities;
using Infrastructure.ApiResponce;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class AuthorService(DapperContext context):IAuthorService
{
    public async Task<Responce<List<Author>>> GetAuthors()
    {
        const string sql = @"select * from Authors";
        var res = await context.GetConnection().QueryAsync<Author>(sql);
        return new Responce<List<Author>>(res.ToList());
    }

    public async Task<Responce<Author>> GetAuthorById(int id)
    {
        const string sql = @"select * from Authors where Id = @Id";
        var res =await context.GetConnection().QueryFirstOrDefaultAsync<Author>(sql, new { Id = id });
        return res != null ? new Responce<Author>(res) : new Responce<Author>(HttpStatusCode.NotFound,"Author not found");
    }

     public async Task<Responce<bool>> AddAuthor(Author author)
    {
        const string sql = @"insert into Authors (Name, Country) values (@Name, @Country)";
        var res = await context.GetConnection().ExecuteAsync(sql, author);
        return res ==0
            ? new Responce<bool>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<bool>(HttpStatusCode.Created, "Author updated");
    }

    public async Task<Responce<bool>> UpdateAuthor(Author author)
    {
        const string sql = @"update Authors set Name = @Name, Country = @Country where Id = @Id";
        var res = await context.GetConnection().ExecuteAsync(sql, author);
        return res==0  
            ? new Responce<bool>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<bool>(HttpStatusCode.OK, "Author updated");
        
    }

    public async Task<Responce<bool>> DeleteAuthor(int id)
    {
        const string sql = @"delete from Authors where Id = @Id";
        var res = await context.GetConnection().ExecuteAsync(sql, new { Id = id });
        return res==0
            ? new Responce<bool>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<bool>(HttpStatusCode.OK, "Author deleted");
    }

}