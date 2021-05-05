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
        public async Task<IList<Book>> GetBookByTitle(string title)
        {
            Devon4NetLogger.Debug($"GetBookByTitle method from repository BookRepository with value : {title}");

            return await Get(x => x.Title == title).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a book by genere
        /// </summary>
        /// <param name="genere"></param>
        /// <returns></returns>
        public async Task<IList<Book>> GetBookByGenere(string genere)
        {
            Devon4NetLogger.Debug($"GetBookByTitle method from repository BookRepository with value : {genere}");

            return await Get(x => x.Genere == genere).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        public Task<IList<Book>> GetAllBooks()
        {
            Devon4NetLogger.Debug($"GetAllBooks method from repository BookRepository");

            return Get();
        }

        /// <summary>
        /// Create a book
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        public  Task<Book> CreateBook(BookDto bookDto)
        {
            Devon4NetLogger.Debug($"Create method from repository BookRepository with values : {bookDto.Title}, {bookDto.Summary}, {bookDto.Genere}");

            return  Create(new Book { Title = bookDto.Title, Summary = bookDto.Summary, Genere = bookDto.Genere });
        }

        public  Task<Book> GetNewBook(BookDto bookDto)
        {
            Devon4NetLogger.Debug($"GetNewBook method from repository BookRepository with values : {bookDto.Title}, {bookDto.Summary}, {bookDto.Genere}");

            return GetFirstOrDefault(x => x.Title == bookDto.Title && x.Summary == bookDto.Summary && x.Genere == bookDto.Genere);
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteBookById(Guid id)
        {
            Devon4NetLogger.Debug($"Delete method from repository BookRepository with value : {id}");

            bool cond = await Delete(x => x.Id == id).ConfigureAwait(false);

            if (cond)
            {
               //var msg = $"The book with id = {id} has been deleted correctly";
                return id;
            }

            throw new Exception($"The Book with the id = {id} could not be deleted");
        }

        public async Task<Book> UpdateBookById(Guid id, BookDto bookDto)
        {
            var repoBook = await GetFirstOrDefault(x => x.Id == id).ConfigureAwait(false);

            if (repoBook == null)
            {
                throw new ArgumentException($"The book with id {id} does not exist and can not be finded");
            }

            repoBook.Title = bookDto.Title;
            repoBook.Summary = bookDto.Summary;
            repoBook.Genere = bookDto.Genere;

            return await Update(repoBook).ConfigureAwait(false);
        }
    }
}
