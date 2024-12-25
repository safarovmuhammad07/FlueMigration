using DoMain.Dtos;
using DoMain.Entities;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface IBookService
{
    Task<Responce<List<Book>>> GetBooksAsync();
    Task<Responce<Book>> GetBookByIdAsync(int id);
    Task<Responce<BookWithAythorDto>> GetBookByAuthorIdAsync(int id);
    Task<Responce<bool>> AddBookAsync(Book book);
    Task<Responce<bool>> UpdateBookAsync(Book book);
    Task<Responce<bool>> DeleteBookAsync(int id);
}