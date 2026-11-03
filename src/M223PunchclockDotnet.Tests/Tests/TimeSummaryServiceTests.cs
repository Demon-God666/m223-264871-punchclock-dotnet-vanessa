using M223PunchclockDotnet.Dto;
using M223PunchclockDotnet.Service;
using NUnit.Framework;

namespace M223PunchclockDotnet.Tests.Tests
{
    public class TimeSummaryServiceTests
    {
        [Test]
        public void TimeSummary_StandardEntry_ReturnsCorrectSummary()
        {
            // Arrange
            var service = new TimeSummaryService(null);

            var entries = new List<EntryDto>
            {
                new EntryDto
                {
                    CheckIn = new DateTime(2026, 11, 3, 8, 0, 0),
                    CheckOut = new DateTime(2026, 11, 3, 17, 0, 0)
                }
            };

            // Act
            List<DateAndDurationDto> result = service.CalculateSummaryPerDay(entries);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Duration, Is.EqualTo(TimeSpan.FromHours(9)));
        }
    }
}