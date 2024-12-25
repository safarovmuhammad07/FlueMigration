using DoMain.Entities;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface IAuthorService
{
    Task<Responce<List<Author>>> GetAuthors();
    Task<Responce<Author>> GetAuthorById(int id);
    Task<Responce<bool>> AddAuthor(Author author);
    Task<Responce<bool>> UpdateAuthor(Author author);
    Task<Responce<bool>> DeleteAuthor(int id);
}