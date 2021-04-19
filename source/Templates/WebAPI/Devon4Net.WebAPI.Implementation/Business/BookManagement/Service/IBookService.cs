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
        Task<BookDto> GetBookByTitle(string title);

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        Task<IList<BookDto>> GetAllBooks();

        /// <summary>
        /// Create a book
        /// </summary>
        /// <returns></returns>
        Task<BookDto> Create(BookDto bookDto);

        /// <summary>
        /// Delete book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> DeleteBook(Guid id);

    }
}
