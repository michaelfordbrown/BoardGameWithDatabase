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
    [Route("api/Players")]
    public class Players : Controller
    {
        private readonly BoardGameContext _context;

        public Players(BoardGameContext context)
        {
            _context = context;
        }

        // GET: api/Players

        [HttpGet]
        public IEnumerable<Tblplayersv2> GetTblplayersv2()
        {
             return _context.Tblplayersv2;
        }

        /*        

        public class QPlayer
        {
            public int Id { get; set; }
            public string Playername { get; set; }
            public string Facingdirection { get; set; }
            public int Colposition { get; set; }
            public int Rowposition { get; set; }
        }

        public DbSet<QPlayer> QPlayers { get; set; }     
        // GET: api/QPlayers        
        [HttpGet]
        public IEnumerable<QPlayers> GetQPlayers()
        {
            return _context.QPlayers;
        }

        /*
        public IEnumerable<QPlayer> GetTblplayersv2()
        {
            IQueryable<QPlayer> players = from p in _context.Tblplayersv2
                                          join bs in _context.Tblboardsquaresv2
                                          on p.Id equals bs.Playerid
                                          select new QPlayer()
                                          {
                                              Id = p.Id,
                                              Playername = p.Playername,
                                              Facingdirection = p.Facingdirection,
                                              Colposition = bs.Colposition,
                                              Rowposition = bs.Rowposition
                                          };

            return View(await players.AsNoTracking().ToListAsync());
            return players;
        }
        */

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTblplayersv2([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblplayersv2 = await _context.Tblplayersv2.SingleOrDefaultAsync(m => m.Id == id);

            if (tblplayersv2 == null)
            {
                return NotFound();
            }

            return Ok(tblplayersv2);
        }

        // PUT: api/Players/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblplayersv2([FromRoute] int id, [FromBody] Tblplayersv2 tblplayersv2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblplayersv2.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblplayersv2).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tblplayersv2Exists(id))
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

        // POST: api/Players
        [HttpPost]
        public async Task<IActionResult> PostTblplayersv2([FromBody] Tblplayersv2 tblplayersv2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tblplayersv2.Add(tblplayersv2);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblplayersv2", new { id = tblplayersv2.Id }, tblplayersv2);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblplayersv2([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblplayersv2 = await _context.Tblplayersv2.SingleOrDefaultAsync(m => m.Id == id);
            if (tblplayersv2 == null)
            {
                return NotFound();
            }

            _context.Tblplayersv2.Remove(tblplayersv2);
            await _context.SaveChangesAsync();

            return Ok(tblplayersv2);
        }

        private bool Tblplayersv2Exists(int id)
        {
            return _context.Tblplayersv2.Any(e => e.Id == id);
        }
    }
}