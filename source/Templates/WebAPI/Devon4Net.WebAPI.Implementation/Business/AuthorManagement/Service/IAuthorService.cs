using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Controllers;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service
{
    /// <summary>
    /// IAuthorService
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// GetAllAuthors
        /// </summary>
        /// <returns></returns>
        Task<IList<AuthorDto>> GetAllAuthors();

        /// <summary>
        /// CreateAuthor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<AuthorDto> CreateAuthor(AuthorDto authorDto);

        /// <summary>
        /// DeleteAuthor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> DeleteAuthor(Guid id);
       
    }
}
