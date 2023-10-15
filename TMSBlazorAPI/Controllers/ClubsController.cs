using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMSBlazorAPI.Data;
using TMSBlazorAPI.Models.Club;

namespace TMSBlazorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly TMSDbContext _context;
        private readonly IMapper mapper;

        public ClubsController(TMSDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Clubs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubReadOnlyDto>>> GetClubs()
        {
          var clubs = await _context.Clubs.ToListAsync();
          var clubDtos = mapper.Map<IEnumerable<ClubReadOnlyDto>>(clubs);

          if (_context.Clubs == null)
          {
              return NotFound();
          }
            return Ok(clubDtos);
        }

        // GET: api/Clubs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubReadOnlyDto>> GetClub(int id)
        {
            var club = await _context.Clubs.FindAsync(id);

            if (club == null)
            {
                return NotFound();
            }

            var clubDto = mapper.Map<ClubReadOnlyDto>(club);

            return Ok(clubDto);
        }

        // PUT: api/Clubs/5
        //[HttpPut("update by {id}")]
        //public async Task<IActionResult> PutClub(int id, Club club)
        //{
        //    if (id != club.ClubId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(club).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await ClubExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // PUT: api/Clubs/5
        [HttpPut("update by {id}")]
        public async Task<IActionResult> PutClub(int id, ClubUpdateDto clubDto)
        {
            if (id != clubDto.Id)
            {
                return BadRequest();
            }

            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return BadRequest();
            }
            mapper.Map(clubDto, club);

            _context.Entry(club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClubExists(id))
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

        // POST: api/Clubs
        [HttpPost]
        public async Task<ActionResult<ClubCreateDto>> PostClub(ClubCreateDto clubDto)
        {
          var club =  mapper.Map<Club>(clubDto);    
          if (_context.Clubs == null)
          {
              return Problem("Entity set 'TMSDbContext.Clubs'  is null.");
          }
            await _context.Clubs.AddAsync(club);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClubs), new { id = club.ClubId }, club);
        }

        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            if (_context.Clubs == null)
            {
                return NotFound();
            }
            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private async Task<bool> ClubExists(int id)
        {
            return await (_context.Clubs?.AnyAsync(e => e.ClubId == id));
        }

        //private bool ClubExists(int id)
        //{
        //    return (_context.Clubs?.Any(e => e.ClubId == id)).GetValueOrDefault();
        //}
    }
}
