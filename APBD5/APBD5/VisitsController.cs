using Microsoft.AspNetCore.Mvc;

namespace Klinika;

[ApiController]
[Route("[controller]")]
public class VisitsController : ControllerBase
{
    // GET: api/Visits
    [HttpGet]
    public ActionResult<IEnumerable<Visit>> GetVisits()
    {
        return DataStore.Visits;
    }

    // GET: api/Visits/5
    [HttpGet("{id}")]
    public ActionResult<Visit> GetVisit(int id)
    {
        var visit = DataStore.Visits.FirstOrDefault(v => v.Id == id);
        if (visit == null)
        {
            return NotFound();
        }
        return visit;
    }

    // POST: api/Visits
    [HttpPost]
    public ActionResult<Visit> PostVisit(Visit visit)
    {
        DataStore.Visits.Add(visit);
        return CreatedAtAction(nameof(GetVisit), new { id = visit.Id }, visit);
    }

    // PUT: api/Visits/5
    [HttpPut("{id}")]
    public IActionResult PutVisit(int id, Visit visit)
    {
        if (id != visit.Id)
        {
            return BadRequest();
        }

        var existingVisit = DataStore.Visits.FirstOrDefault(v => v.Id == id);
        if (existingVisit == null)
        {
            return NotFound();
        }

        existingVisit.VisitDate = visit.VisitDate;
        existingVisit.Description = visit.Description;
        existingVisit.Cost = visit.Cost;

        return NoContent();
    }

    // DELETE: api/Visits/5
    [HttpDelete("{id}")]
    public IActionResult DeleteVisit(int id)
    {
        var visit = DataStore.Visits.FirstOrDefault(v => v.Id == id);
        if (visit == null)
        {
            return NotFound();
        }

        DataStore.Visits.Remove(visit);

        return NoContent();
    }
}