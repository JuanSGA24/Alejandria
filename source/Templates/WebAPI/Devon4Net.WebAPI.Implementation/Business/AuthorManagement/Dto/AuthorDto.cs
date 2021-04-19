using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto
{
    /// <summary>
    /// Author definition  
    /// </summary>
    public class AuthorDto
    {
        /// <summary>
        /// The Name of an Author
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The Surname of an Author
        /// </summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// The Email of an Author
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// The Phone of an Author
        /// </summary>
        [Required]
        public int Phone { get; set; }
    }
}

