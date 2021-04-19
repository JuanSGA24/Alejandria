using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Converters
{
    public static class AuthorConverter
    {
        /// <summary>
        /// ModelToDto Author transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static AuthorDto ModelToDto(Author item)
        {
            if (item == null) return new AuthorDto();

            return new AuthorDto
            {
                Name = item.Name,
                Surname = item.Surname,
                Email = item.Email,
                Phone = item.Phone
            };
        }
    }
}
