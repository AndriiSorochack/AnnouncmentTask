using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnnouncmentTask.Models;
using AnnouncmentTask.Data;

namespace AnnouncmentTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncmentsController : ControllerBase
    {
        private readonly IRepository<Announcment> _repository;

        public AnnouncmentsController(IRepository<Announcment> repository)
        {
            _repository = repository;
        }

        // GET: api/Announcments
        [HttpGet]
        public ActionResult<IEnumerable<Announcment>> GetAnnouncment()
        {
            return  Ok(_repository.GetAll());
        }

        // GET: api/Announcments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcment>> GetAnnouncment(int id)
        {
            var announcment = await _repository.GetByIdAsync(id);

            if (announcment == null)
            {
                return NotFound();
            }

            return Ok(announcment);
        }

        // PUT: api/Announcments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        public async Task<IActionResult> PutAnnouncment(int id, [FromBody]Announcment announcment)
        {
            if (id != announcment.Id)
            {
                return BadRequest();
            }

            if (!AnnouncmentExists(id))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(announcment);
            return Accepted();
        }

        // POST: api/Announcments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Announcment>> PostAnnouncment(Announcment announcment)
        {
            await _repository.CreateAsync(announcment);

            return CreatedAtAction("GetAnnouncment", new { id = announcment.Id }, announcment);
        }

        // DELETE: api/Announcments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncment(int id)
        {
            var announcment = await _repository.GetByIdAsync(id);
            if (announcment == null)
            {
                return NotFound();
            }

            await _repository.RemoveAsync(announcment.Id);

            return NoContent();
        }

        private bool AnnouncmentExists(int id)
        {
            
            return _repository.GetAll().Any(e => e.Id == id);
        }
    }
}
