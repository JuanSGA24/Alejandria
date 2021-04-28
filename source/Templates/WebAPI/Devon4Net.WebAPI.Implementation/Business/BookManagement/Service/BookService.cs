using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Service
{
    public class BookService : Service<AlejandriaContext>, IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IUnitOfWork<AlejandriaContext> uoW) : base(uoW)
        {
            _bookRepository = uoW.Repository<IBookRepository>();
        }

        /// <summary>
        /// Get book by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BookDto>> GetBookByTitle(string title)
        {
            Devon4NetLogger.Debug($"GetBookByTitle method from service Bookservice with value : {title}");

            var res = await _bookRepository.GetBookByTitle(title).ConfigureAwait(false);
            return res.Select(BookConverter.ModelToDto);
        }

        /// <summary>
        /// Get book by genere
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BookDto>> GetBookByGenere(string genere)
        {
            Devon4NetLogger.Debug($"GetBookByGenere method from service Bookservice with value : {genere}");

            var res = await _bookRepository.GetBookByGenere(genere).ConfigureAwait(false);
            return res.Select(BookConverter.ModelToDto);
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            Devon4NetLogger.Debug($"GetAllBooks method from service BookService");
            
            var bookList = await _bookRepository.GetAllBooks().ConfigureAwait(false);
            return bookList.Select(BookConverter.ModelToDto);
        }

        /// <summary>
        /// Create a book
        /// </summary>
        /// <returns></returns>
        public async Task<BookDto> CreateBook(BookDto bookDto)
        {
            Devon4NetLogger.Debug($"CreateBook method from service BookService with value : {bookDto.Title}, {bookDto.Summary}, {bookDto.Genere}");

            if (bookDto == null || bookDto.Title == null || bookDto.Summary == null || bookDto.Genere == null)
            {
                throw new ArgumentException("One or more field can not be null.");
            }

            return BookConverter.ModelToDto(await _bookRepository.CreateBook(bookDto).ConfigureAwait(false));
        }

        /// <summary>
        /// Delete book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteBookById(Guid id)
        {
            Devon4NetLogger.Debug($"DeleteBook method from service BookService with id : {id}");

            return await _bookRepository.DeleteBookById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify a book by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        public async Task<BookDto> ModifyBookById(Guid id, BookDto bookDto)
        {
            Devon4NetLogger.Debug($"ModifyBookById method from service BookService with id: {id} and values : {bookDto.Title}, {bookDto.Summary}, {bookDto.Genere}");

            var res = await _bookRepository.UpdateBookById(id, bookDto).ConfigureAwait(false);
            return BookConverter.ModelToDto(res);
        }
    }
}
