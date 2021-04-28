using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {

        public AuthorRepository(AlejandriaContext context) : base(context)
        {

        }

        /// <summary>
        /// Get all authors
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Author>> GetAllAuthors()
        {
            Devon4NetLogger.Debug($"GetAllAuthors method from repository AuthorRepository");

            return await Get().ConfigureAwait(false);
        }

        /// <summary>
        /// Creates an author
        /// </summary>
        /// <param name="authorDto"></param>
        /// <returns></returns>
        public async Task<Author> CreateAuthor(AuthorDto authorDto)
        {
            Devon4NetLogger.Debug($"Create method from repository AuthorRepository with value : {authorDto.Name}, {authorDto.Surname}, {authorDto.Email}, {authorDto.Phone}");

            var res = await Create(new Author { Name = authorDto.Name, Surname = authorDto.Surname, Email = authorDto.Email, Phone = authorDto.Phone });

            return res;
        }

        /// <summary>
        /// Deletes an author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteAuthor(Guid id)
        {
            Devon4NetLogger.Debug($"DeleteAuthor method from repository AuthorRepository with value : {id}");

            bool cond = await Delete(x => x.Id == id).ConfigureAwait(false);

            if (cond)
            {
                return id;
            }
            throw new Exception($"Author with id = {id} could not be deleted.");
        }

        public async Task<Author> UpdateAuthor(Guid authorId, AuthorDto authorDto)
        {
            Devon4NetLogger.Debug($"UpdateAuthor method from service AuthorRepository with id : {authorId} and Name: {authorDto.Name}, Surname: {authorDto.Surname}, Email: {authorDto.Email}, Phone: {authorDto.Phone}");

            var repoAuthor = await GetFirstOrDefault(x => x.Id == authorId).ConfigureAwait(false);
            
            if (repoAuthor == null)
            {
                throw new ArgumentException($"The author with id {authorId} does not exist and can not be finded");
            }

            repoAuthor.Name = authorDto.Name;
            repoAuthor.Surname = authorDto.Surname;
            repoAuthor.Email = authorDto.Email;
            repoAuthor.Phone = authorDto.Phone;

            return await Update(repoAuthor).ConfigureAwait(false);
        }
    }
}
