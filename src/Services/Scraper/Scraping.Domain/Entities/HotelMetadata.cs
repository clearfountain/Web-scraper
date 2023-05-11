namespace Scraping.Domain.Entities;

#nullable disable

public class HotelMetadata
{
    public int Classification { get; set; }
    public int HotelID { get; set; }
    public string Name { get; set; }
    public double ReviewScore { get; set; }
}
