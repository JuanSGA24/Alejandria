using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {

        public BookRepository(AlejandriaContext context) : base(context)
        {

        }

        /// <summary>
        /// Get book by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<Book> GetBookByTitle(string title)
        {
            Devon4NetLogger.Debug($"GetBookByTitle method from repository BookRepository with value : {title}");

            return await GetFirstOrDefault(x => x.Title == title).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a book by genere
        /// </summary>
        /// <param name="genere"></param>
        /// <returns></returns>
        public async Task<Book> GetBookByGenere(string genere)
        {
            Devon4NetLogger.Debug($"GetBookByTitle method from repository BookRepository with value : {genere}");

            return await GetFirstOrDefault(x => x.Genere == genere).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Book>> GetAllBooks()
        {
            Devon4NetLogger.Debug($"GetAllBooks method from repository BookRepository");

            return await Get().ConfigureAwait(false);
        }

        /// <summary>
        /// Create a book
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        public async Task<Book> Create(BookDto bookDto)
        {
            Devon4NetLogger.Debug($"Create method from repository AuthorRepository with value : {bookDto.Title}, {bookDto.Summary}, {bookDto.Genere}");

            return await Create(new Book { Title = bookDto.Title, Summary = bookDto.Summary, Genere = bookDto.Genere }).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> Delete(Guid id)
        {
            Devon4NetLogger.Debug($"Delete method from repository BookRepository with value : {id}");

            bool cond = await Delete(x => x.Id == id).ConfigureAwait(false);

            if (cond)
            {
                return id;
            }

            throw new Exception($"The Book with the id = {id} could not be deleted");
        }

    }
}
