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
        public async Task<Author> Create(AuthorDto authorDto)
        {
            Devon4NetLogger.Debug($"Create method from repository AuthorRepository with value : {authorDto.Name}, {authorDto.Surname}, {authorDto.Email}, {authorDto.Phone}");

            return await Create(new Author { Name = authorDto.Name, Surname = authorDto.Surname, Email = authorDto.Email, Phone = authorDto.Phone });
        }

        /// <summary>
        /// Deletes an author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> Delete(Guid id)
        {
            Devon4NetLogger.Debug($"Delete method from repository AuthorRepository with value : {id}");

            bool cond = await Delete(x => x.Id == id).ConfigureAwait(false);

            if (cond)
            {
                return id;
            }
            throw new Exception($"Author with id = {id} could not be deleted.");
        }
    }
}
