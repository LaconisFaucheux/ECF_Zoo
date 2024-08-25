//testé OK 26/04/2024
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using API_Arcadia.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ContextArcadia _context;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(ContextArcadia context, ILogger<ReviewsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Reviews
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var req = from r in _context.Reviews
                      where r.IsValidated
                      select r;

            return await req.ToListAsync();
        }

        [HttpGet("unvalidated")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<IEnumerable<Review>>> GetUnfilteredReviews()
        {
            var req = from r in _context.Reviews
                      where !r.IsValidated
                      select r;

            return await req.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            try
            {
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
            }
            catch (DbUpdateException e) 
            {
                ProblemDetails pb = e.ConvertToProblemDetails();
                return Problem(pb.Detail, null, pb.Status, pb.Title);
            }

        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
