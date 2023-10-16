﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using TMSBlazorAPI.Data;
using TMSBlazorAPI.Models.Club;
using TMSBlazorAPI.Static;

namespace TMSBlazorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly TMSDbContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<ClubsController> logger;

        public ClubsController(TMSDbContext context, IMapper mapper, ILogger<ClubsController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Clubs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubReadOnlyDto>>> GetClubs()
        {
            try
            {
                var clubs = await _context.Clubs.ToListAsync();
                var clubDtos = mapper.Map<IEnumerable<ClubReadOnlyDto>>(clubs);

                if (_context.Clubs == null)
                {
                    return NotFound();
                }
                return Ok(clubDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"error on GET in {nameof(GetClubs)}");
                return StatusCode(500, Messages.Error500Message);
            }
          
        }

        // GET: api/Clubs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubReadOnlyDto>> GetClub(int id)
        {
            try
            {
                var club = await _context.Clubs.FindAsync(id);

                if (club == null)
                {
                    return NotFound();
                }

                var clubDto = mapper.Map<ClubReadOnlyDto>(club);

                return Ok(clubDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"error on GET in {nameof(GetClubs)}");
                return StatusCode(500, Messages.Error500Message);
            }


            
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

            try
            {
                var club = mapper.Map<Club>(clubDto);
                if (_context.Clubs == null)
                {
                    return Problem("Entity set 'TMSDbContext.Clubs'  is null.");
                }
                await _context.Clubs.AddAsync(club);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetClubs), new { id = club.ClubId }, club);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"error on POST in {nameof(clubDto)}");
                return StatusCode(500, Messages.Error500Message);
            }


            
        }

        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex, $"error on DELETE in {nameof(DeleteClub)}");
                return StatusCode(500, Messages.Error500Message);
            }




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
