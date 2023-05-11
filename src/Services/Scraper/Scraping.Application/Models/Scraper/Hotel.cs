using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Scraping.Application.Models.Scraper;

public class Hotel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int ClassificationStars { get; set; }
    public double ReviewPoints { get; set; }
    public int NumberOfReviews { get; set; }
    public string Description { get; set; } 
    public IReadOnlyList<string> RoomCategories { get; set; }
    public IReadOnlyList<string> AlternativeHotels { get; set; }
}
