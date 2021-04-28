using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Service
{
    /// <summary>
    /// IBookService
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Search book by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<IEnumerable<BookDto>> GetBookByTitle(string title);

        /// <summary>
        /// Search a book by genere
        /// </summary>
        /// <param name="genere"></param>
        /// <returns></returns>
        Task<IEnumerable<BookDto>> GetBookByGenere(string genere);

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BookDto>> GetAllBooks();

        /// <summary>
        /// Create a book
        /// </summary>
        /// <returns></returns>
        Task<BookDto> CreateBook(BookDto bookDto);

        /// <summary>
        /// Delete book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> DeleteBookById(Guid id);

        /// <summary>
        /// Modify a book by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        Task<BookDto> ModifyBookById(Guid id, BookDto bookDto);
    }
}
