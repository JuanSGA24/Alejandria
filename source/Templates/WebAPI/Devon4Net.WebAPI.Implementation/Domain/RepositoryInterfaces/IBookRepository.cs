using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Get book by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<Book> GetBookByTitle(string title);

        /// <summary>
        /// Get list of all books
        /// </summary>
        /// <returns></returns>
        Task<IList<Book>> GetAllBooks();

        /// <summary>
        /// Create a book
        /// </summary>
        /// <returns></returns>
        Task<Book> Create(BookDto bookDto);

        /// <summary>
        /// Delete a book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> Delete(Guid id);
    }
}
