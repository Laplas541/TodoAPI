using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Commands;
using TodoApi.Application.Convey_Queries;
using TodoApi.Application.Validators;
using TodoApi.Domain.Models;
using TodoApi.Infrastructure.Context;

namespace TodoApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ICommandDispatcher _dispatcher;
        private readonly IQueryDispatcher _querydispatcher;
        

        public TodoItemsController(TodoContext context, ICommandDispatcher dispatcher, IQueryDispatcher qdispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
            _querydispatcher = qdispatcher;
            
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems([FromQuery] int pageNumber = 1)
        {
            var query = new GetAllQuery { Pagenumber = pageNumber };
            try
            {
                var todoitems = await _querydispatcher.QueryAsync<GetAllQuery, IEnumerable<TodoItem>>(query);
                return Ok(todoitems);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);

            }
        }
           


            // GET: api/TodoItems/5
            [HttpGet("{id}")]
            public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
            {
                var query = new GetByIdQuery(id);

                try
                {
                    var todoItem = await _querydispatcher.QueryAsync<GetByIdQuery, TodoItem>(query);
                    return Ok(todoItem);
                }
                catch(ValidationException ex)
                {
                return BadRequest(ex.Errors);
                }

                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
            }


            // PUT: api/TodoItems/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutTodoItem(int id, [FromBody] CommandUpdate command)
            {


                if (id != command.Id)
                {
                    return BadRequest("ID in the URL does not match ID in the command");
                }

                try
                {
                    await _dispatcher.SendAsync(command);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex.Errors);
                }


                catch (KeyNotFoundException ex)
                {
                    // Handle not found case
                    return NotFound(ex.Message);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Handle concurrency issue
                    return Conflict(ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle other potential issues
                    return StatusCode(500, ex.Message);
                }

                return NoContent();
            }

            // POST: api/TodoItems
            [HttpPost]
            public async Task<IActionResult> CreateTodoItem([FromBody] CommandCreate command)
            {
                try
                {
                    await _dispatcher.SendAsync(command);
                    return Ok();
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex.Errors);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }


            // DELETE: api/TodoItems/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteTodoItem(long id)
            {
                var command = new CommandDelete(id);
                try
                {
                    await _dispatcher.SendAsync(command);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex.Errors);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
                return NoContent();
            }

            private bool TodoItemExists(long id)
            {
                return _context.TodoItems.Any(e => e.Id == id);
            }
        }
    } 

