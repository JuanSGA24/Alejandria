using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Controllers;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using Devon4Net.WebAPI.Implementation.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service
{
    /// <summary>
    /// Author service implementation
    /// </summary>
    public class AuthorService : Service<AlejandriaContext>, IAuthorService
    {

        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorBookRepository _authorBookRepository;
        // private readonly IUserRepository _userRepository;
        private IHttpClientHandler _httpClientHandler;

        private readonly AlejandriaOptions _alejandriaOptions;


        public AuthorService(IUnitOfWork<AlejandriaContext> uoW, IOptions<AlejandriaOptions> alejandriaOptions, IHttpClientHandler httpClientHandler) : base(uoW)
        {
            _authorRepository = uoW.Repository<IAuthorRepository>();
            _bookRepository = uoW.Repository<IBookRepository>();
            _authorBookRepository = uoW.Repository<IAuthorBookRepository>();
            // _userRepository = uoW.Repository<IUserRepository>();
            _httpClientHandler = httpClientHandler;

            _alejandriaOptions = alejandriaOptions.Value;
        }

        /// <summary>
        /// Get All Authors
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AuthorDto>> GetAllAuthors()
        {
            Devon4NetLogger.Debug($"GetAllAuthors method from service Authorservice");

            var authorList = await _authorRepository.GetAllAuthors().ConfigureAwait(false);

            return authorList.Select(AuthorConverter.ModelToDto);
        }

        /// <summary>
        /// Creates the Author
        /// </summadry>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<AuthorDto> CreateAuthor(AuthorDto authorDto)
        {
            Devon4NetLogger.Debug($"CreateAuthor method from service AuthorService with value : {authorDto.Name}, {authorDto.Surname}, {authorDto.Email}, {authorDto.Phone}");

            if (authorDto == null || authorDto.Name == null || authorDto.Surname == null || authorDto.Email == null)
            {
                throw new ArgumentException("One or more field can not be null.");
            }

            var res = await _authorRepository.CreateAuthor(authorDto).ConfigureAwait(false);

            return AuthorConverter.ModelToDto(res);
        }

        public async Task<BookDto> PublishBook(Guid authorId, BookDto bookDto)
        {
            Devon4NetLogger.Debug($"PublishBook method from service AuthorService with id : {authorId} and values: {bookDto.Title}, {bookDto.Summary}, {bookDto.Genere}");

            var newBookDto = await _httpClientHandler.Send<BookDto>(HttpMethod.Post, "Books", "/v1/bookmanagement/createbook", bookDto, MediaType.ApplicationJson, useCamelCase:true).ConfigureAwait(false);
            var newBook = await _bookRepository.GetNewBook(bookDto).ConfigureAwait(false);
            var authorBook = await _authorBookRepository.CreateAuthorBook(authorId, newBook.Id, DateTime.Now, DateTime.Now.AddYears(_alejandriaOptions.Validity)).ConfigureAwait(false);

            return newBookDto;
        }

        /// <summary>
        /// Delete an Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteAuthor(Guid id)
        {
            Devon4NetLogger.Debug($"DeleteAuthor method from service AuthorService with id : {id}");

            return await _authorRepository.DeleteAuthor(id).ConfigureAwait(false);
        }

        public async Task<AuthorDto> ModifyAuthor(Guid authorId, AuthorDto authorDto)
        {
            Devon4NetLogger.Debug($"ModifyAuthor method from service AuthorService with id : {authorId} and Name: {authorDto.Name}, Surname: {authorDto.Surname}, Email: {authorDto.Email}, Phone: {authorDto.Phone}");

            var res = await _authorRepository.UpdateAuthor(authorId, authorDto);
            return AuthorConverter.ModelToDto(res);
        }

        public Task<Guid> DeleteBookAsAuthor(Guid bookId, AuthorDto authorDto, BookDto bookDto)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto> ModifyBookAsAuthor(Guid bookId, Guid authorId, BookDto bookDto)
        {
            throw new NotImplementedException();
        }

        /*public Task<AuthorDto> CreateUser(string userId, string password, string role)
        {
            throw new NotImplementedException();
        }*/

    }
}
