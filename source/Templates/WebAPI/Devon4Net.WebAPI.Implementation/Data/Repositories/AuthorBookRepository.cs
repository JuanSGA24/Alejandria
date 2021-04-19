using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    public class AuthorBookRepository : Repository<AuthorBook>, IAuthorBookRepository
    {

        public AuthorBookRepository(AlejandriaContext context) : base(context)
        {

        }
    }
}
