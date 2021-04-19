using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Converters
{
    public class BookConverter
    {
        /// <summary>
        /// ModelToDto Book transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static BookDto ModelToDto(Book item)
        {
            if (item == null) return new BookDto();

            return new BookDto
            {
                Title = item.Title,
                Genere = item.Genere,
                Summary = item.Summary,
            };
        }
    }
}

