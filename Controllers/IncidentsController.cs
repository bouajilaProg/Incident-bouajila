using Microsoft.AspNetCore.Mvc;
using tp1.Models;


namespace tp1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase
    {

        private static readonly List<Incident> _incidents = new();
        private static int _nextId = 1;
        private static readonly string[] AllowedSeverities =
        { "LOW", "MEDIUM", "HIGH", "CRITICAL" };
        private static readonly string[] AllowedStatuses =
        { "OPEN", "IN_PROGRESS", "RESOLVED" };



        [HttpGet]
        public IActionResult GetIncidents()
        {
            return Ok(_incidents);
        }


        [HttpPost]
        public IActionResult CreateIncident([FromBody] Incident incident)
        {
            if (!AllowedSeverities.Contains(incident.Severity.ToUpper()))
            {
                return BadRequest($"Severity must be : {string.Join(", ", AllowedSeverities)}");
            }


            incident.Id = _nextId++;
            incident.Status = AllowedStatuses[0];
            incident.CreatedAt = DateTime.Now;
            _incidents.Add(incident);


            return Ok(incident);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetIncidentById(int id)
        {
            var incident = _incidents.FirstOrDefault(i => i.Id == id);
            if (incident == null)
            {
                return NotFound();
            }
            return Ok(incident);
        }


        [HttpPut("update-status/{id}")]
        public IActionResult UpdateIncidentStatus(int id, [FromBody] string status)
        {
            var incident = _incidents.FirstOrDefault(i => i.Id == id);
            if (incident == null)
            {
                return NotFound();
            }

            if (!AllowedStatuses.Contains(status.ToUpper()))
            {
                return BadRequest("wrong status ");
            }

            incident.Status = status.ToUpper();
            return Ok(incident);

        }

        [HttpDelete("delete-incident/{id}")]
        public IActionResult DeleteIncident(int id)
        {

            var incident = _incidents.FirstOrDefault(i => i.Id == id);
            if (incident == null)
            {
                return NotFound();
            }

            if (incident.Status == "OPEN" && incident.Severity == "CRITICAL")
            {
                return BadRequest("Cannot delete incidents with OPEN status and CRITICAL severity.");
            }

            _incidents.Remove(incident);
            return NoContent();
        }
