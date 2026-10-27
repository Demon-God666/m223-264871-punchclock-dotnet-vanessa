
using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service;

public class ExampleDataService
{
    private readonly DatabaseContext _databaseContext;

    public ExampleDataService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task CreateExampleDataAsync()
    {
        if (await _databaseContext.Categories.AnyAsync() ||
            await _databaseContext.Tags.AnyAsync() ||
            await _databaseContext.Entries.AnyAsync())
        {
            return;
        }

        // Kategorien erstellen
        var category1 = new Category { Title = "Work" };
        var category2 = new Category { Title = "Personal" };

        // Tags erstellen
        var tag1 = new Tag { Title = "Urgent" };
        var tag2 = new Tag { Title = "Important" };

        // Entries erstellen und Beziehungen setzen
        var now = DateTime.UtcNow;

        var entry1 = new Entry
        {
            CheckIn = DateTime.SpecifyKind(
                now.AddHours(-2), DateTimeKind.Utc),
            CheckOut = DateTime.SpecifyKind(
                now, DateTimeKind.Utc),
            Category = category1,
            Tags = new List<Tag> { tag1 }
        };

        var entry2 = new Entry
        {
            CheckIn = DateTime.SpecifyKind(
                now.AddHours(-4), DateTimeKind.Utc),
            CheckOut = DateTime.SpecifyKind(
                now.AddHours(-3), DateTimeKind.Utc),
            Category = category2,
            Tags = new List<Tag> { tag2 }
        };

        // Alle Entitäten vormerken
        _databaseContext.Categories.AddRange(category1, category2);
        _databaseContext.Tags.AddRange(tag1, tag2);
        _databaseContext.Entries.AddRange(entry1, entry2);

        // Alles gemeinsam speichern
        await _databaseContext.SaveChangesAsync();
    }
}
