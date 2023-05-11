using HtmlAgilityPack;
using Scraping.Application.Models.Scraper;

namespace Scraping.Application.Contracts.Infrastructure;

public interface IScraperService
{
    Hotel ExtractHotelFromFile(string fileName);
    string GetHotelName(HtmlDocument htmlDoc);
    string GetHotelAddress(HtmlDocument htmlDoc);
    int GetHotelClassification(HtmlDocument htmlDoc);
    double GetHotelReview(HtmlDocument htmlDoc);
    int GetReviewsCount(HtmlDocument htmlDoc);
    string GetDescription(HtmlDocument htmlDoc);
    IReadOnlyList<string> GetRoomCategories(HtmlDocument htmlDoc);
    IReadOnlyList<string> GetAlternativeHotels(HtmlDocument htmlDoc);
}
