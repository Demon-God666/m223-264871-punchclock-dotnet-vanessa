using M223PunchclockDotnet.Dto;
using M223PunchclockDotnet.Model;

namespace M223PunchclockDotnet.Service;

public class TimeSummaryService
{
    private readonly DatabaseContext _databaseContext;

    public TimeSummaryService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public List<DateAndDurationDto> CalculateSummaryPerDay(List<EntryDto> entries)
    {
        var summary = new List<DateAndDurationDto>();
        foreach (var entry in entries)
        {
            var dateAndTime = new DateAndDurationDto
            {
                Date = entry.CheckIn,
                Duration = entry.CheckOut - entry.CheckIn
            };

            summary.Add(dateAndTime);
        }

        return summary;
    }
}