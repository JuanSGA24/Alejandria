using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.JWT.Common.Const;
using Devon4Net.Infrastructure.JWT.Handlers;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.UserManagement.Dto;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IJwtHandler _jwtHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authorService"></param>
        public AuthorController(IAuthorService authorService, IJwtHandler jwtHandler)
        {
            _authorService = authorService;
            _jwtHandler = jwtHandler;
        }

        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(string user, string password)
        {
            Devon4NetLogger.Debug("Executing Login from controller AuthorController");

            var token = _jwtHandler.CreateClientToken(new List<Claim>
            {
                new Claim(ClaimTypes.Role, AuthConst.Author),
                new Claim(ClaimTypes.Role, AuthConst.Librarian),
                new Claim(ClaimTypes.Name,user),
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
            });

            return Ok(new LoginResponse { Token = token });
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
        /// Publishes a new book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpOptions]
        [Route("publish")]
        [Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.Author)]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Publish(Guid authorId, BookDto bookDto)
        {
            Devon4NetLogger.Debug("Executing Publish from controller AuthorController");
            var result = await _authorService.PublishBook(authorId, bookDto).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Delete an Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            Devon4NetLogger.Debug("Executing Delete from controller AuthorController");
            var result = await _authorService.DeleteAuthor(id).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        
        [HttpPut]
        [Route("edit")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ModifyAuthor(Guid authorId, AuthorDto authorDto)
        {
            Devon4NetLogger.Debug("Exectuing ModifyAuthor from controller AuthorController");

            return Ok(await _authorService.ModifyAuthor(authorId, authorDto).ConfigureAwait(false));
        }

        /*
        [HttpPost]
        [Route("createuser")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUser(string userId, string password, string role)
        {
            Devon4NetLogger.Debug($"Executing CreateUser from controller AuthorController withc values: userId = {userId}, password = {password}, userRole = {userRole}");

            return Ok(await _authorService.CreateUser(userId, password, role).ConfigureAwait(false));
        }
        */
    }
}
