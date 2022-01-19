using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using API.Models.Entities;
using API.Models.Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private DataContext _context;

        public ItemController(DataContext context)
        {
            _context = context;
        }

        //POST /api/item/add
        // adds 1 item to db
        [HttpPost("add")]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //POST /api/item/addmany
        // adds list of items to db
        [HttpPost("addmany")]
        public async Task<IActionResult> AddManyItems([FromBody] Item[] item)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Items.AddRangeAsync(item);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //GET /api/item/unsorted
        // gets all items from db
        [HttpGet("unsorted")]
        public async Task<IActionResult> GetAllItems()
        {
            return Ok(_context.Items);
        }

        //GET /api/item/byid
        // gets all items from db sorted by Id
        [HttpGet("byid")]
        public async Task<IActionResult> GetAllItemsSortedById()
        {
            return Ok(_context.Items.OrderBy(x => x.Id));
        }

        //GET /api/item/{id}
        // gets 1 item from db by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneItemById(string id)
        {
            try
            {
                var oneItem = await _context.Items.FindAsync(new Guid(id));

                if (oneItem is null)
                {
                    return NotFound();
                }

                return Ok(oneItem);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        //GET /api/item/{name}
        // gets items from db by name
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetAllItemsByName(string name)
        {
            try
            {
                var allItemsByName = _context.Items.Where(x => x.Name == name);

                if (allItemsByName is null)
                {
                    return NotFound(allItemsByName);
                }

                return Ok(allItemsByName);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        //GET /api/item/bytype/{type}
        // gets list of items from db by type
        [HttpGet("bytype/{type}")]
        public async Task<IActionResult> GetAllItemsByType(string type)
        {
            try
            {
                var allItemsByType = _context.Items.Where(x => x.Type == type);

                if (allItemsByType is null)
                {
                    return NotFound(allItemsByType);
                }

                return Ok(allItemsByType);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        //DELETE /api/item/{id}
        // deletes 1 item from db by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemById(string id)
        {
            try
            {
                var oneItem = await _context.Items.FindAsync(new Guid(id));

                if (oneItem is null)
                {
                    return NotFound();
                }
                _context.Items.Remove(oneItem);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        //DELETE /api/item/deleteall
        // deletes all items from db
        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteAllItems()
        {
            try
            {
                _context.Items.RemoveRange(_context.Items);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}