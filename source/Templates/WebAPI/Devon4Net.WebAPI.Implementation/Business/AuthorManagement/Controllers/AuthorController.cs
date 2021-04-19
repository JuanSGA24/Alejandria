using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Controllers
{
    /// <summary>
    /// Author Controller
    /// </summary>
    [ApiController]
    [Route("v1/authormanagement")]
    [EnableCors("CorsPolicy")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authorService"></param>
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Gets the entire list of authors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("authors")]
        [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllAuthors()
        {
            Devon4NetLogger.Debug("Executing GetAllAuthors from controller AuthorController");
            return Ok(await _authorService.GetAllAuthors().ConfigureAwait(false));
        }

        /// <summary>
        /// Create an Author
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(AuthorDto authorDto)
        {
            Devon4NetLogger.Debug("Executing Create from controller AuthorController");
            var result = await _authorService.CreateAuthor(authorDto).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Delete an Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            Devon4NetLogger.Debug("Executing Delete from controller AuthorController");
            var result = await _authorService.DeleteAuthor(id).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
