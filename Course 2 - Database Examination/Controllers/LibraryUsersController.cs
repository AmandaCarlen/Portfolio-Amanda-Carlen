using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinaltestLibrary.Data;
using FinaltestLibrary.Models;

namespace FinaltestLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryUsersController : ControllerBase
    {
        private readonly Context _context;

        public LibraryUsersController(Context context)
        {
            _context = context;
        }

        // GET: api/LibraryUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryUser>>> GetLibraryUsers()
        {
            return await _context.LibraryUsers.ToListAsync();
        }

        // GET: api/LibraryUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryUser>> GetLibraryUsers(int id)
        {
            // This shows the book(s) the libraryUser has rented by id from Rent         
            var libraryUser = await _context.LibraryUsers
            .Include(b => b.Rent)
            .ThenInclude(x => x.Book)
            .FirstOrDefaultAsync(x => x.LibraryCardID == id);

            if (libraryUser == null)
            {
                return NotFound();
            }

            return libraryUser;
        }

        // PUT: api/LibraryUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibraryUsers(int id, LibraryUser libraryUser)
        {
            if (id != libraryUser.LibraryCardID)
            {
                return BadRequest();
            }

            _context.Entry(libraryUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryUsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LibraryUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LibraryUser>> PostLibraryUsers(LibraryUser libraryUser)
        {
            _context.LibraryUsers.Add(libraryUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibraryUsers", new { id = libraryUser.LibraryCardID }, libraryUser);
        }

        // DELETE: api/LibraryUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibraryUsers(int id)
        {
            var libraryUser = await _context.LibraryUsers.FindAsync(id);
            if (libraryUser == null)
            {
                return NotFound();
            }

            _context.LibraryUsers.Remove(libraryUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibraryUsersExists(int id)
        {
            return _context.LibraryUsers.Any(e => e.LibraryCardID == id);
        }
    }
}
