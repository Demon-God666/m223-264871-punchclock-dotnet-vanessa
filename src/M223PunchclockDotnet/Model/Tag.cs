using System;

namespace M223PunchclockDotnet.Model;

public class Tag
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
