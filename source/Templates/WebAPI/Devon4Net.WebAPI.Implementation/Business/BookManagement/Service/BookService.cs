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
            Devon4NetLogger.Debug($"CreateAuthor method from service AuthorService with value : {bookDto.Title}, {bookDto.Summary}, {bookDto.Genere}");

            if (string.IsNullOrEmpty(bookDto.Title) || string.IsNullOrWhiteSpace(bookDto.Title))
            {
                throw new ArgumentException("The 'Title' field can not be null.");
            }

            if (string.IsNullOrEmpty(bookDto.Summary) || string.IsNullOrWhiteSpace(bookDto.Summary))
            {
                throw new ArgumentException("The 'Summary' field can not be null.");
            }

            if (string.IsNullOrEmpty(bookDto.Genere) || string.IsNullOrWhiteSpace(bookDto.Genere))
            {
                throw new ArgumentException("The 'Genere' field can not be null.");
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
            var result = await _bookRepository.Delete(id).ConfigureAwait(false);

            return id;
        }
    }
}
