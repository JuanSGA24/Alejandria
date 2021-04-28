using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// IAuthorRepository Interface
    /// </summary>
    public interface IAuthorRepository : IRepository<Author>
    {
        /// <summary>
        /// Get all Authors
        /// </summary>
        /// <returns></returns>
        Task<IList<Author>> GetAllAuthors();
        /// <summary>
        /// Create Author
        /// </summary>
        /// <param name="authorDto"></param>
        /// <returns></returns>
        Task<Author> CreateAuthor(AuthorDto authorDto);

        /// <summary>
        /// Delete Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> Delete(Guid id);
    }
}
