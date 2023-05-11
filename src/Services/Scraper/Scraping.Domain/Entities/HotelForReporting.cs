namespace Scraping.Domain.Entities;

#nullable disable

public class HotelForReporting
{
    public HotelMetadata Hotel { get; set; }
    public List<HotelRate> HotelRates { get; set; }
}
