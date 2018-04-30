using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardGame.Models;

namespace BoardGame.Controllers
{
    [Produces("application/json")]
    [Route("api/BoardSquares")]
    public class BoardSquares : Controller
    {
        private readonly BoardGameContext _context;

        public BoardSquares(BoardGameContext context)
        {
            _context = context;
        }

        // GET: api/BoardSquares
        [HttpGet]
        public IEnumerable<Tblboardsquaresv2> GetTblboardsquaresv2()
        {
            return _context.Tblboardsquaresv2;
        }

        // GET: api/BoardSquares/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTblboardsquaresv2([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblboardsquaresv2 = await _context.Tblboardsquaresv2.SingleOrDefaultAsync(m => m.Id == id);

            if (tblboardsquaresv2 == null)
            {
                return NotFound();
            }

            return Ok(tblboardsquaresv2);
        }

        // PUT: api/BoardSquares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblboardsquaresv2([FromRoute] int id, [FromBody] Tblboardsquaresv2 tblboardsquaresv2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblboardsquaresv2.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblboardsquaresv2).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tblboardsquaresv2Exists(id))
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

        // POST: api/BoardSquares
        [HttpPost]
        public async Task<IActionResult> PostTblboardsquaresv2([FromBody] Tblboardsquaresv2 tblboardsquaresv2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tblboardsquaresv2.Add(tblboardsquaresv2);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblboardsquaresv2", new { id = tblboardsquaresv2.Id }, tblboardsquaresv2);
        }

        // DELETE: api/BoardSquares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblboardsquaresv2([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblboardsquaresv2 = await _context.Tblboardsquaresv2.SingleOrDefaultAsync(m => m.Id == id);
            if (tblboardsquaresv2 == null)
            {
                return NotFound();
            }

            _context.Tblboardsquaresv2.Remove(tblboardsquaresv2);
            await _context.SaveChangesAsync();

            return Ok(tblboardsquaresv2);
        }

        private bool Tblboardsquaresv2Exists(int id)
        {
            return _context.Tblboardsquaresv2.Any(e => e.Id == id);
        }
    }
}