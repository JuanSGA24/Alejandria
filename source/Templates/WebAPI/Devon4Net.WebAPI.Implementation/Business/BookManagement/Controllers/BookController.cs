using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Service;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Controllers
{
    /// <summary>
    /// Books controller
    /// </summary>
    [ApiController]
    [Route("/v1/bookmanagement")]
    [EnableCors("CorsPolicy")]
    public class BookController: ControllerBase
    {
        private readonly IBookService _bookService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bookService"></param>
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        /// <summary>
        /// Gets a book by its title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("booksbytitle")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBookByTitle(string title)
        {
            Devon4NetLogger.Debug("Executing GetBooksByTitle from controller BookController");
            return Ok(await _bookService.GetBookByTitle(title).ConfigureAwait(false));
        }

        /// <summary>
        /// Gets the entire list of books
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("books")]
        [ProducesResponseType(typeof(IList<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllBooks()
        {
            Devon4NetLogger.Debug("Executing Create from controller BookController");
            return Ok(await _bookService.GetAllBooks().ConfigureAwait(false));
        }

        /// <summary>
        /// Creates a book
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(BookDto bookDto)
        {
            Devon4NetLogger.Debug("Executing Create from controller BookController");
            return Ok(await _bookService.Create(bookDto).ConfigureAwait(false));
        }

        /// <summary>
        /// Deletes a book by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletebyid")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            Devon4NetLogger.Debug("Executing Delete from controller BookController");
            var result = await _bookService.DeleteBook(id).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut]
        [Route("editbookbyid")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ModifyBook(Guid id, string title, string summary, string genere)
        {
            Devon4NetLogger.Debug("Executing ModifyBook from controller BookController");
            if (id == null ||title == null || summary == null || genere == null)
            {
                return BadRequest("The id of the employee must be provided");
            }
            return Ok(await _employeeService.ModifyEmployeeById(employeeDto.Id, employeeDto.Name, employeeDto.Surname, employeeDto.Mail).ConfigureAwait(false));
        }
    }
}
