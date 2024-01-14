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
    }
}
