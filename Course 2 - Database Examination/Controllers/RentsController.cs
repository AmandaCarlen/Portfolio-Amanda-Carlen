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
    public class RentedController : ControllerBase
    {
        private readonly Context _context;

        public RentedController(Context context)
        {
            _context = context;
        }

        // GET: api/Rented
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rent>>> GetRented()
        {
            return await _context.Rented.ToListAsync();
        }

        // GET: api/Rented/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rent>> GetRent(int id)
        {
            // Shows the book rented and the libraryuser
            var rent = await _context.Rented
                .Include(b => b.Book)
                .ThenInclude(x => x.Rent)
                .Include(x => x.LibraryCard)
                .ThenInclude(x => x.Rent)
                .FirstOrDefaultAsync(x => x.ID == id);

            if (rent == null)
            {
                return NotFound();
            }

            return rent;
        }

        // PUT: api/Rented/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRent(int id, Rent rent)
        {
            if (id != rent.ID)
            {
                return BadRequest();
            }

            // Sets IsRented bool to false on Book if returned != null
            if(rent.ReturnedDate != null)
            {
                Book book = await _context.Books.FirstOrDefaultAsync(b => b.ID == rent.BookID);
                book.IsRented = false;
                _context.Update(book);

            }

            _context.Entry(rent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentExists(id))
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

        // POST: api/Rented
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rent>> PostRent(Rent rent)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.ID == rent.BookID);
            if (book == null)
            {
                return BadRequest();
            }
            if (!book.IsRented)
            {
            _context.Rented.Add(rent);
            }
            else
            {
                return BadRequest();
            }
            // Sets IsRented bool to true if book is not returned
            if (rent.RentedDate != null && rent.ReturnedDate == null)
            {           
                book.IsRented = true;
                _context.Update(book);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRent", new { id = rent.ID }, rent);
        }

        // DELETE: api/Rented/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRent(int id)
        {
            var rent = await _context.Rented.FindAsync(id);

            Book book = await _context.Books.FirstOrDefaultAsync(b => b.ID == rent.BookID);

            if (rent == null)
            {
                return NotFound();
            }

            if (rent.ReturnedDate==null)
            { 
                book.IsRented = false;
                _context.Update(book);
            }

            _context.Rented.Remove(rent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentExists(int id)
        {
            return _context.Rented.Any(e => e.ID == id);
        }
    }
}
