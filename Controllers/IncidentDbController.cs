using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bouajila.Incidents.Api.Models;

[Route("api/[controller]")]
[ApiController]
public class IncidentDbController : ControllerBase
{
    private readonly IncidentsDbContext _context;
    public IncidentDbController(IncidentsDbContext context)
    {
        _context = context;
    }

    // GET: api/Incident
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Incident>>> GetIncident()
    {
        return await _context.Incidents.ToListAsync();
    }

    // GET: api/Incident/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Incident>> GetIncident(int id)
    {
        var incident = await _context.Incidents.FindAsync(id);

        if (incident == null)
        {
            return NotFound();
        }

        return incident;
    }

    // PUT: api/Incident/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutIncident(int? id, Incident incident)
    {
        if (id != incident.Id)
        {
            return BadRequest();
        }

        _context.Entry(incident).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IncidentExists(id))
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

    // POST: api/Incident
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Incident>> PostIncident(Incident incident)
    {
        _context.Incidents.Add(incident);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetIncident", new { id = incident.Id }, incident);
    }

    // DELETE: api/Incident/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncident(int? id)
    {
        var incident = await _context.Incidents.FindAsync(id);
        if (incident == null)
        {
            return NotFound();
        }

        _context.Incidents.Remove(incident);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool IncidentExists(int? id)
    {
        return _context.Incidents.Any(e => e.Id == id);
    }


    [HttpGet("FilterByStatus/{status}")]
    public ActionResult<IEnumerable<Incident>> FilterByStatus(string status)
    {
        var incidents = (from i in _context.Incidents
                         where i.Status.Contains(status)
                         select i).ToList();
        if (incidents == null || incidents.Count == 0)
        {
            return NotFound();
        }
        return incidents;
    }


    [HttpGet("FilterByStatusAsync/{status}")]
    public async Task<ActionResult<IEnumerable<Incident>>> FilterByStatusAsync(string status)
    {
        var incidents = _context.Incidents.Where(i => i.Status.Contains(status)).ToListAsync();

        if (incidents == null || incidents.Result.Count == 0)
        {
            return NotFound();
        }
        return incidents.Result;
    }



    [HttpGet("FilterBySeverity/{severity}")]
    public ActionResult<IEnumerable<Incident>> FilterBySeverity(string severity)
    {
        var incidents = (from i in _context.Incidents
                         where i.Severity.Contains(severity)
                         select i).ToList();

        if (incidents == null || incidents.Count == 0)
        {
            return NotFound();
        }
        return incidents;
    }




}
