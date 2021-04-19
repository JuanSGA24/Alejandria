using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Controllers;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using Devon4Net.WebAPI.Implementation.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        private readonly AlejandriaOptions _alejandriaOptions;


        public AuthorService(IUnitOfWork<AlejandriaContext> uoW, IOptions<AlejandriaOptions> alejandriaOptions): base(uoW)
        {
            _authorRepository = uoW.Repository<IAuthorRepository>();
            _bookRepository = uoW.Repository<IBookRepository>();
            _authorBookRepository = uoW.Repository<IAuthorBookRepository>();

            _alejandriaOptions = alejandriaOptions.Value;
        }

        /// <summary>
        /// Get All Authors
        /// </summary>
        /// <returns></returns>
        public async Task<IList<AuthorDto>> GetAllAuthors()
        {
            Devon4NetLogger.Debug($"GetAllAuthors method from service Authorservice");

            var authorListDto = new List<AuthorDto>();
            var authorList = await _authorRepository.Get();

            foreach(Author a in authorList)
            {
                authorListDto.Add(AuthorConverter.ModelToDto(a));
            }

            return authorListDto;
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

            if (string.IsNullOrEmpty(authorDto.Name) || string.IsNullOrWhiteSpace(authorDto.Name))
            {
                throw new ArgumentException("The 'Name' field can not be null.");
            }

            if (string.IsNullOrEmpty(authorDto.Surname) || string.IsNullOrWhiteSpace(authorDto.Surname))
            {
                throw new ArgumentException("The 'Surname' field can not be null.");
            }

            if (string.IsNullOrEmpty(authorDto.Email) || string.IsNullOrWhiteSpace(authorDto.Email))
            {
                throw new ArgumentException("The 'Email' field can not be null.");
            }

            var result = await _authorRepository.Create(authorDto).ConfigureAwait(false);

            return AuthorConverter.ModelToDto(result);
        }

        /// <summary>
        /// Delete an Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteAuthor(Guid id)
        {
            var result = await _authorRepository.Delete(id).ConfigureAwait(false);

            return id;
        }
    }
}
