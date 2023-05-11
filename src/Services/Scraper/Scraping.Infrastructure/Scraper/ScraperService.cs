using HtmlAgilityPack;
using Scraping.Application.Contracts.Infrastructure;
using Scraping.Application.Models.Scraper;
using System.Text;

namespace Scraping.Infrastructure.Scraper;

public class ScraperService : IScraperService
{
    public Hotel ExtractHotelFromFile(string fileName)
    {
        Hotel hotel = new();

        var htmlDoc = LoadDocument(fileName);

        hotel.Name = GetHotelName(htmlDoc);
        hotel.Address = GetHotelAddress(htmlDoc);
        hotel.ClassificationStars = GetHotelClassification(htmlDoc);
        hotel.ReviewPoints = GetHotelReview(htmlDoc);
        hotel.NumberOfReviews = GetReviewsCount(htmlDoc);
        hotel.Description = GetDescription(htmlDoc);
        hotel.RoomCategories = GetRoomCategories(htmlDoc);
        hotel.AlternativeHotels = GetAlternativeHotels(htmlDoc);

        return hotel;
    }

    public HtmlDocument LoadDocument(string inputFilePath)
    {
        string path = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.FullName;

        var filePath = Path.Combine(path, inputFilePath);

        //using (StreamReader streamReader = new(filePath, Encoding.UTF8))
        //{
        //    string? fileContent = streamReader.ReadToEnd();
        //}

        HtmlDocument htmlDoc = new();

        htmlDoc.Load(filePath);

        return htmlDoc;
    }
    public string GetHotelName(HtmlDocument htmlDoc)
    {
        var hotelNameNode = htmlDoc.DocumentNode.SelectSingleNode("//span[@id='hp_hotel_name']");
        var hotelName = hotelNameNode?.InnerText ?? null;
        hotelName = hotelName?.Trim()?.Replace("\n", "") ?? null;

        return hotelName;
    }

    public string GetHotelAddress(HtmlDocument htmlDoc)
    {
        var hotelAddressNode = htmlDoc.DocumentNode.SelectSingleNode("//span[@id='hp_address_subtitle']");
        var hotelAddress = hotelAddressNode?.InnerText ?? null;
        hotelAddress = hotelAddress?.Trim()?.Replace("\n", "") ?? null;

        return hotelAddress;
    }

    public int GetHotelClassification(HtmlDocument htmlDoc)
    {
        int rating = 0;

        var hotelClassificationNode = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("hp__hotel_ratings__stars__clarification_track")).FirstOrDefault();
        var starsNode = hotelClassificationNode?.Descendants().Where(n => n.HasClass("star_track")).FirstOrDefault() ?? null;
        var classString = starsNode?.Attributes["class"]?.Value ?? null;

        var classes = classString?.Split(' ') ?? null;
        var ratingString = classes?.Where(s => s.StartsWith("ratings_stars")).FirstOrDefault() ?? null;

        if (!string.IsNullOrEmpty(ratingString))
        {
            _ = int.TryParse(ratingString[^1].ToString().AsSpan(), out rating);
        }

        return rating;
    }

    public double GetHotelReview(HtmlDocument htmlDoc)
    {
        double reviewValue = 0f;
        var reviewNode = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("js--hp-scorecard-scoreval")).FirstOrDefault();
        var reviewText = reviewNode?.InnerText ?? null;

        if (!string.IsNullOrEmpty(reviewText))
        {
            _ = double.TryParse(reviewText, out reviewValue);
        }

        return reviewValue;
    }

    public int GetReviewsCount(HtmlDocument htmlDoc)
    {
        int reviewCount = 0;
        var reviewCountParentNode = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("score_from_number_of_reviews")).FirstOrDefault();
        var reviewCountText = reviewCountParentNode?.Descendants()?.Where(n => n.HasClass("count"))?.FirstOrDefault()?.InnerText ?? null;

        if (!string.IsNullOrEmpty(reviewCountText))
        {
            _ = int.TryParse(reviewCountText, out reviewCount);
        }

        return reviewCount;
    }

    public string GetDescription(HtmlDocument htmlDoc)
    {
        string description = null;
        var summaryNode = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("hotel_description_wrapper_exp")).FirstOrDefault();
        var summaryTexts = summaryNode?.Descendants().Where(n => n.Name.Equals("p", StringComparison.InvariantCultureIgnoreCase)) ?? null;

        StringBuilder sb = new(1024);

        if (summaryTexts != null && summaryTexts.Any())
        {
            summaryTexts.ToList().ForEach(s => sb.Append(s.InnerText));
        }

        description = sb.ToString();
        description = description.Replace("\n", "");

        return description;
    }

    public IReadOnlyList<string> GetRoomCategories(HtmlDocument htmlDoc)
    {
        List<string> roomCategories = new();
        var roomCategoryNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("ftd")).ToList();

        if (roomCategoryNodes != null && roomCategoryNodes.Any())
        {
            roomCategoryNodes.ForEach(n => roomCategories.Add(n.InnerText.Trim().Replace("\n", "")));
        }

        return roomCategories;
    }

    public IReadOnlyList<string> GetAlternativeHotels(HtmlDocument htmlDoc)
    {
        StringBuilder sb = new(1024);
        List<string> alternativeHotels = new();

        var alternativeHotelNodes = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("althotels-name")).ToList();

        foreach (var node in alternativeHotelNodes)
        {
            var anchorElement = node.Descendants().Where(n => n.Name.Equals("a", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if(anchorElement != null)
            {
                alternativeHotels.Add(anchorElement.InnerText.Trim().Replace("\n", ""));
            }
        }

        return alternativeHotels;
    }
}
