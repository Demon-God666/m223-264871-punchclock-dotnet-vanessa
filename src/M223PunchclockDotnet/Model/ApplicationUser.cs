using System;

namespace M223PunchclockDotnet.Model;

public class ApplicationUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
     public ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
