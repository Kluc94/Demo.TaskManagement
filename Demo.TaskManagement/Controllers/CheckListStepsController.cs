using Demo.TaskManagement.Data;
using Demo.TaskManagement.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckListStepsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CheckListStepsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CheckListSteps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckListStep>>> GetCheckListSteps()
        {
            if (_context.CheckListSteps == null)
            {
                return NotFound();
            }
            return await _context.CheckListSteps.ToListAsync();
        }

        // GET: api/CheckListSteps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckListStep>> GetCheckListStep(int id)
        {
            if (_context.CheckListSteps == null)
            {
                return NotFound();
            }
            var checkListStep = await _context.CheckListSteps.FindAsync(id);

            if (checkListStep == null)
            {
                return NotFound();
            }

            return checkListStep;
        }

        // PUT: api/CheckListSteps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckListStep(int id, CheckListStep checkListStep)
        {
            if (id != checkListStep.Id)
            {
                return BadRequest();
            }

            _context.Entry(checkListStep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckListStepExists(id))
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

        // POST: api/CheckListSteps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CheckListStep>> PostCheckListStep(CheckListStep checkListStep)
        {
            if (_context.CheckListSteps == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CheckListSteps'  is null.");
            }
            _context.CheckListSteps.Add(checkListStep);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckListStep", new { id = checkListStep.Id }, checkListStep);
        }

        // DELETE: api/CheckListSteps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckListStep(int id)
        {
            if (_context.CheckListSteps == null)
            {
                return NotFound();
            }
            var checkListStep = await _context.CheckListSteps.FindAsync(id);
            if (checkListStep == null)
            {
                return NotFound();
            }

            _context.CheckListSteps.Remove(checkListStep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckListStepExists(int id)
        {
            return (_context.CheckListSteps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
