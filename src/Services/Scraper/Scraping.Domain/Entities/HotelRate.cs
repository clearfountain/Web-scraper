namespace Scraping.Domain.Entities;

#nullable disable

public class HotelRate
{
    public int Adults { get; set; }
    public int Los { get; set; }
    public string RateDescription { get; set; }
    public int RateID { get; set; }
    public string RateName { get; set; }
    public DateTime TargetDay { get; set; }
    public Price Price { get; set; }
    public List<RateTag> RateTags { get; set; }
}
