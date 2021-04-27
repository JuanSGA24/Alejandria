using Devon4Net.Infrastructure.JWT.Common.Const;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Controllers;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service
{
    /// <summary>
    /// IAuthorService
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// GetAllAuthors
        /// </summary>
        /// <returns></returns>
        Task<IList<AuthorDto>> GetAllAuthors();

        /// <summary>
        /// CreateAuthor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<AuthorDto> CreateAuthor(AuthorDto authorDto);

        /// <summary>
        /// Publish a book
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        Task<BookDto> PublishBook(Guid authorId, BookDto bookDto);

        /// <summary>
        /// Delete an Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> DeleteAuthor(Guid id);

        /// <summary>
        /// Modify an Author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorDto"></param>
        /// <returns></returns>
        Task<AuthorDto> ModifyAuthor(Guid authorId, AuthorDto authorDto);

        Task<Guid> DeleteBookAsAuthor(Guid bookId, AuthorDto authorDto, BookDto bookDto);

        Task<BookDto> ModifyBookAsAuthor(Guid bookId, Guid authorId, BookDto bookDto);

       // Task<AuthorDto> CreateUser(string userId, string password, string role);
       
    }
}
