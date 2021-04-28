using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    public interface IAuthorBookRepository
    {

        Task<AuthorBook> CreateAuthorBook(Guid authorId, Guid bookId, DateTime publishDate, DateTime validityDate);
    }
}
