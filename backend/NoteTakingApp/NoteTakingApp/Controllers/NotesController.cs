using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteTakingApp.Data;
using NoteTakingApp.Models;

namespace NoteTakingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly NotesAPIDbContext dbContext;

        public NotesController(NotesAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            return Ok(await dbContext.Notes.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(AddNote addNote) {
            var note = new Note()
            {
                Id = Guid.NewGuid(),
                Body = addNote.Body,
                CreatedDate = DateTime.UtcNow
            };

            await dbContext.Notes.AddAsync(note);
            await dbContext.SaveChangesAsync();

            return Ok(note);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, UpdateNote updateNote)
        {
            var note = await dbContext.Notes.FindAsync(id);
            if (note == null) return NotFound();

            note.Body = updateNote.Body;
            await dbContext.SaveChangesAsync();
            return Ok(note);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteNoteById([FromRoute] Guid id)
        {
            var note = await dbContext.Notes.FindAsync(id);
            if (note == null) return NotFound();

            dbContext.Notes.Remove(note);
            await dbContext.SaveChangesAsync();
            return Ok(note);
        }
    }
}
