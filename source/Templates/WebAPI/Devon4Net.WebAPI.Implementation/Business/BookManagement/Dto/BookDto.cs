using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto
{
    /// <summary>
    /// Book definition
    /// </summary>
    public class BookDto
    {
        /// <summary>
        /// The Name of a Book
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The Summary of a Book
        /// </summary>
        [Required]
        public string Summary { get; set; }

        /// <summary>
        /// The Genere of a Book
        /// </summary>
        [Required]
        public string Genere { get; set; }

    }
}
