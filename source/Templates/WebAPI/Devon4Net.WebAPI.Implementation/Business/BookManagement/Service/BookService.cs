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
        public async Task<BookDto> GetBookByTitle(string title)
        {
            Devon4NetLogger.Debug($"GetBookByTitle method from service Bookservice with value : {title}");

            return BookConverter.ModelToDto(await _bookRepository.GetFirstOrDefault(x => x.Title == title).ConfigureAwait(false));
 
        }

        /// <summary>
        /// Get book by genere
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<BookDto> GetBookByGenere(string genere)
        {
            Devon4NetLogger.Debug($"GetBookByGenere method from service Bookservice with value : {genere}");

            return BookConverter.ModelToDto(await _bookRepository.GetFirstOrDefault(x => x.Genere == genere).ConfigureAwait(false));

        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        public async Task<IList<BookDto>> GetAllBooks()
        {
            var bookListDto = new List<BookDto>();
            var bookList = await _bookRepository.Get().ConfigureAwait(false);

            foreach(Book b in bookList)
            {
                bookListDto.Add(BookConverter.ModelToDto(b));
            }

            return bookListDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<BookDto> Create(BookDto bookDto)
        {
            Devon4NetLogger.Debug($"CreateBook method from service BookService with value : {bookDto.Title}, {bookDto.Summary}, {bookDto.Genere}");

            if (bookDto == null || bookDto.Title == null || bookDto.Summary == null || bookDto.Genere == null)
            {
                throw new ArgumentException("One or more  field can not be null.");
            }

            return BookConverter.ModelToDto(await _bookRepository.Create(bookDto).ConfigureAwait(false));
        }

        /// <summary>
        /// Delete book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteBook(Guid id)
        {
            Devon4NetLogger.Debug($"DeleteBook method from service BookService with id : {id}");
            return await _bookRepository.Delete(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify a book by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        public async Task<BookDto> ModifyBookById(Guid id, BookDto bookDto)
        {
            var repoBook = await _bookRepository.GetFirstOrDefault(x => x.Id == id);

            if (repoBook == null)
            {
                throw new ArgumentException($"The book with id {id} does not exist and can not be finded");
            }

            if (repoBook.Title != null && !repoBook.Title.Equals("string")) repoBook.Title = bookDto.Title;

            if (repoBook.Summary != null && repoBook.Summary.Equals("string")) repoBook.Summary = bookDto.Summary;

            if (repoBook.Genere != null && repoBook.Genere.Equals("string")) repoBook.Genere = bookDto.Genere;

            return BookConverter.ModelToDto(await _bookRepository.Update(repoBook).ConfigureAwait(false));
        }
    }
}
