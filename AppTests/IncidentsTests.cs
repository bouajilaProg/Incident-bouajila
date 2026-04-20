using Bouajila.Incidents.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AppTests;

public class IncidentsTests
{
    [Fact]
    public async Task GetIncidents_WhenDataExists_ReturnsAllIncidents()
    {
        using var context = GetDbContext();
        context.Incidents.AddRange(
            new Incident { Title = "Incident1", Description = "First incident description", Status = "OPEN", Severity = "HIGH" },
            new Incident { Title = "Incident2", Description = "Second incident description", Status = "CLOSED", Severity = "LOW" }
        );
        context.SaveChanges();

        var controller = new global::IncidentDbController(context);

        var result = await controller.GetIncident();

        var incidents = Assert.IsType<List<Incident>>(result.Value);
        Assert.Equal(2, incidents.Count);
    }

    [Fact]
    public async Task GetIncident_ExistingId_ReturnsIncident()
    {
        using var context = GetDbContext();
        var incident = new Incident
        {
            Id = 1,
            Title = "Test",
            Description = "Test incident description",
            Status = "OPEN",
            Severity = "HIGH"
        };

        context.Incidents.Add(incident);
        context.SaveChanges();

        var controller = new global::IncidentDbController(context);

        var result = await controller.GetIncident(1);

        Assert.NotNull(result.Value);
        Assert.Equal("Test", result.Value.Title);
    }

    private static global::IncidentsDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<global::IncidentsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new global::IncidentsDbContext(options);
    }
}
