namespace M223PunchclockDotnet.Dto
{
    public class EntryDetailDto
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public CategoryDto? Category { get; set; }
        public List<TagDto> Tags { get; set; } = new();
        public ApplicationUserDto? User { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    public class TagDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    public class ApplicationUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}