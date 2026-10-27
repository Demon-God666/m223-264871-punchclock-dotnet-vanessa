namespace M223PunchclockDotnet.Dto
{
    public class EntryDto
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public int? CategoryId { get; set; }
        public List<int> TagIds { get; set; } = new();
    }
}