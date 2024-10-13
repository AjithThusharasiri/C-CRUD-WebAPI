using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly DetailsContext _context;

        public DetailsController(DetailsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Details>>> GetDetails()
        {
            try
            {
                if (_context.Details == null)
                {
                    return NotFound();
                }

                return await _context.Details.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Details>> GetDetails(int id)
        {
            try
            {
                if (_context.Details == null)
                {
                    return NotFound();
                }

                var details = await _context.Details.FindAsync(id);
                if (details == null)
                {
                    return NotFound();
                }

                return details;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetails(int id, Details details)
        {
            try
            {
                if (id != details.Id)
                {
                    return BadRequest(new { success = false, message = "ID mismatch." });
                }

                _context.Entry(details).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "success" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailsExists(id))
                {
                    return NotFound(new { success = false, message = "Details not found." });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Concurrency error." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Details>> PostDetails(Details details)
        {
            try
            {
                if (_context.Details == null)
                {
                    return Problem("Entity set 'DetailsContext.Details' is null.");
                }

                _context.Details.Add(details);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDetails", new { id = details.Id }, new { success = true, message = "success", data = details });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetails(int id)
        {
            try
            {
                if (_context.Details == null)
                {
                    return NotFound();
                }

                var details = await _context.Details.FindAsync(id);
                if (details == null)
                {
                    return NotFound();
                }

                _context.Details.Remove(details);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        private bool DetailsExists(int id)
        {
            return (_context.Details?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
