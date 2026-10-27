using System;
using System.ComponentModel.DataAnnotations;

namespace M223PunchclockDotnet.Model;

public class Category
{

    public int Id { get; set; }

    public required string Title { get; set; }

    public ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
