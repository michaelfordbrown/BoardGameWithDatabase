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
    [Route("api/QPlayers")]
    public class QPlayersController : Controller
    {
        private readonly BoardGameContext _context;

        public QPlayersController(BoardGameContext context)
        {
            _context = context;
        }

        public partial class QPlayer
        {
            public string Playername { get; set; }
            public string Facingdirection { get; set; }
            public int Colposition { get; set; }
            public int Rowposition { get; set; }
        }        

        // GET: api/QPlayers

        [HttpGet]
        public IEnumerable<QPlayer> GetQPlayers()
        {
            IEnumerable<QPlayer> qval = from p in _context.Tblplayersv2
                       join bs in _context.Tblboardsquaresv2
                       on p.Id equals bs.Playerid
                       select new QPlayer
                       {
                           Playername = p.Playername,
                           Facingdirection = p.Facingdirection,
                           Colposition = bs.Colposition,
                           Rowposition = bs.Rowposition
                       };

            return qval;
        }

        // GET: api/QPlayers/5
        [HttpGet("{playername}")]
        public IEnumerable<QPlayer> GetQPlayer(string playername)
        {

            var qval = from p in _context.Tblplayersv2
                       from bs in _context.Tblboardsquaresv2
                       where (p.Playername == playername) && (bs.Playerid == p.Id)
                       select new QPlayer
                       {
                           Playername = p.Playername,
                           Facingdirection = p.Facingdirection,
                           Colposition = bs.Colposition,
                           Rowposition = bs.Rowposition
                       };

            return qval;
        }

        // PUT: api/QPlayers/5
        [HttpPut("{playername}")]
        public async Task<IActionResult> PutQPlayer([FromRoute] string playername, [FromBody] QPlayer qplayer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tblplayersv2 player = await _context.Tblplayersv2.SingleOrDefaultAsync(p => p.Playername == playername);
            Tblboardsquaresv2 boardsquare = await _context.Tblboardsquaresv2.SingleOrDefaultAsync(bs => bs.Playerid == player.Id);

            if (player != null)
            {
                player.Playername = qplayer.Playername;
                player.Facingdirection = qplayer.Facingdirection;

                /* Has Player Moved ?*/
                if ((boardsquare.Colposition == qplayer.Colposition) && (boardsquare.Rowposition == qplayer.Rowposition))
                {
                    /* Player has NOT moved */

                }
                else
                {
                    boardsquare.Playerid = null;
                    boardsquare.Player = null;
                    Tblboardsquaresv2 newboardsquare = await _context.Tblboardsquaresv2.SingleOrDefaultAsync((bs => (bs.Colposition == qplayer.Colposition) && (bs.Rowposition == qplayer.Rowposition)));
                    newboardsquare.Player = player;
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tblplayersv2Exists(player.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return NoContent();
        }

        // POST: api/Players
        [HttpPost]
        public async Task<IActionResult> PostQPlayer([FromBody] QPlayer qplayer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tblplayersv2 player = new Tblplayersv2
            {
                Playername = qplayer.Playername,
                Facingdirection = qplayer.Facingdirection
            };

            Tblboardsquaresv2 boardsquare = await _context.Tblboardsquaresv2.SingleOrDefaultAsync((bs => (bs.Colposition == qplayer.Colposition) && (bs.Rowposition == qplayer.Rowposition)));
            boardsquare.Player = player;

            _context.Tblplayersv2.Add(player);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblplayersv2", new { id = player.Id }, player);

        }

        // DELETE: api/Players/5
        [HttpDelete("{playername}")]
        public async Task<IActionResult> DeleteQPlayer([FromRoute] string playername)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tblplayersv2 player = await _context.Tblplayersv2.SingleOrDefaultAsync(p => p.Playername == playername);
            Tblboardsquaresv2 boardsquare = await _context.Tblboardsquaresv2.SingleOrDefaultAsync(bs => bs.Playerid == player.Id);


            if (player == null)
            {
                return NotFound();
            }

            boardsquare.Playerid = null;
            boardsquare.Player = null;

            _context.Tblplayersv2.Remove(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }

        private bool Tblplayersv2Exists(int id)
        {
            return _context.Tblplayersv2.Any(e => e.Id == id);
        }
    }
}