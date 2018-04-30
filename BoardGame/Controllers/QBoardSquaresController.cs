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
    [Route("api/QBoardSquares")]
    public class QBoardSquaresController : Controller
    {
        private readonly BoardGameContext _context;

        public QBoardSquaresController(BoardGameContext context)
        {
            _context = context;
        }

        public partial class QBoardSquare
        {
            public string Playername { get; set; }
            public string Facingdirection { get; set; }
            public int? Playerid { get; set; }
            public int Northwall { get; set; }
            public int Southwall { get; set; }
            public int Westwall { get; set; }
            public int Eastwall { get; set; }
            public int Colposition { get; set; }
            public int Rowposition { get; set; }
        }

        // GET: api/QBoardSquares
        [HttpGet]
        public IEnumerable<QBoardSquare> GetQBoardSquares()
        {
            IEnumerable<QBoardSquare> qval = from bs in _context.Tblboardsquaresv2
                                             join p in _context.Tblplayersv2
                                        on bs.Playerid equals p.Id into joined
                                             from p in joined.DefaultIfEmpty()
                                             select new QBoardSquare
                                             {
                                                 Playername = p.Playername,
                                                 Facingdirection = p.Facingdirection,
                                                 Colposition = bs.Colposition,
                                                 Rowposition = bs.Rowposition,
                                                 Northwall = bs.Northwall,
                                                 Southwall = bs.Southwall,
                                                 Westwall = bs.Westwall,
                                                 Eastwall = bs.Eastwall
                                             };

            return qval;
        }

        // GET: api/QBoardSquares/0/1
        [HttpGet("{col}/{row}")]
        public IEnumerable<QBoardSquare> GetQBoardSquare(int col, int row)
        {
            var qval = from bs in _context.Tblboardsquaresv2
                       join p in _context.Tblplayersv2
                       on bs.Playerid equals p.Id
                       where (bs.Colposition == col) && (bs.Rowposition == row)
                       select new QBoardSquare
                       {
                           Playername = p.Playername,
                           Facingdirection = p.Facingdirection,
                           Northwall = bs.Northwall,
                           Southwall = bs.Southwall,
                           Westwall = bs.Westwall,
                           Eastwall = bs.Eastwall,
                           Colposition = bs.Colposition,
                           Rowposition = bs.Rowposition
                       };

            return qval;
        }

        // PUT: api/QBoardSquares/0/1
        [HttpPut("{col}/{row}")]
        public async Task<IActionResult> PutQBoardSquare([FromRoute] int col, [FromRoute]int row, [FromBody] QBoardSquare qboardsquare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tblboardsquaresv2 boardsquare = await _context.Tblboardsquaresv2.SingleOrDefaultAsync(bs => (bs.Colposition == col) && (bs.Rowposition == row));
            Tblplayersv2 player = await _context.Tblplayersv2.SingleOrDefaultAsync(p => p.Id == boardsquare.Playerid);

            if (boardsquare != null)
            {
                boardsquare.Northwall = qboardsquare.Northwall;
                boardsquare.Southwall = qboardsquare.Southwall;
                boardsquare.Westwall = qboardsquare.Westwall;
                boardsquare.Eastwall = qboardsquare.Eastwall;
                boardsquare.Colposition = col;
                boardsquare.Rowposition = row;
                if (player != null)
                    boardsquare.Playerid = player.Id;
                else
                    boardsquare.Playerid = null;
            }
            else
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tblboardsquaresv2Exists(boardsquare.Id))
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

        // POST: api/QBoardSquares
        [HttpPost]
        public async Task<IActionResult> PostQBoardSquare([FromBody] QBoardSquare qboardsquare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tblboardsquaresv2 boardsquare = new Tblboardsquaresv2
            {
                Northwall = qboardsquare.Northwall,
                Southwall = qboardsquare.Southwall,
                Westwall = qboardsquare.Westwall,
                Eastwall = qboardsquare.Eastwall
            };

            Tblplayersv2 player = await _context.Tblplayersv2.SingleOrDefaultAsync(p => (p.Id == boardsquare.Playerid));

            _context.Tblboardsquaresv2.Add(boardsquare);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblboardsquaresv2", new { id = boardsquare.Id }, boardsquare);
        }

        // DELETE: api/QBoardSquares/5
        [HttpDelete("{id}")]
        public IActionResult DeleteQBoardSquares([FromRoute] int id)
        {
            return BadRequest(ModelState);
        }

        private bool Tblboardsquaresv2Exists(int id)
        {
            return _context.Tblboardsquaresv2.Any(e => e.Id == id);
        }
    }
}