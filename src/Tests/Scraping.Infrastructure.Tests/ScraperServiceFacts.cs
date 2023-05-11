using HtmlAgilityPack;
using Scraping.Application.Contracts.Infrastructure;
using Scraping.Infrastructure.Scraper;
using System.Text;

namespace Scraping.Infrastructure.Tests;

public class ScraperServiceFacts
{
    private IScraperService _scraperService;

    [OneTimeSetUp]
    public void Setup()
    {
        _scraperService = new ScraperService();
    }

    [Test]
    public void GetHotelName_Should_Return_Hotel_Name_When_Hotel_Name_Exists()
    {
        //Arrange
        var expected = "Kempinski Hotel Bristol Berlin";
        HtmlDocument htmlDoc = new();

        string html = @"<h1 class=""item"">
                                <span class=""fn"" id=""hp_hotel_name"">
                                    Kempinski Hotel Bristol Berlin
                                </span>
                        </h1>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetHotelName(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetHotelName_Should_Return_Null_When_Hotel_Name_Does_Not_Exist()
    {
        //Arrange
        string expected = null;
        HtmlDocument htmlDoc = new();

        string html = @"<h1 class=""item"">
                                <span class=""fn"">
                                    Kempinski Hotel Bristol Berlin
                                </span>
                        </h1>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetHotelName(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }


    [Test]
    public void GetHotelAddress_Should_Return_Hotel_Address_When_Hotel_Address_Exists()
    {
        //Arrange
        var expected = "12 ABC Road, Berlin";
        HtmlDocument htmlDoc = new();

        string html = @"<h1 class=""item"">
                                <span class=""fn"" id=""hp_address_subtitle"">
                                    12 ABC Road, Berlin
                                </span>
                        </h1>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetHotelAddress(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetHotelAddress_Should_Return_Null_When_Hotel_Address_Does_Not_Exist()
    {
        //Arrange
        string expected = null;
        HtmlDocument htmlDoc = new();

        string html = @"<h1 class=""item"">
                                <span class=""fn"">
                                    12 ABC Road, Berlin
                                </span>
                        </h1>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetHotelAddress(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }


    [Test]
    public void GetHotelClassification_Should_Return_Classification_When_Classification_Exists()
    {
        //Arrange
        int expected = 4;
        HtmlDocument htmlDoc = new();

        string html = @" <span class=""nowrap hp__hotel_ratings"">
                                    <span
                                        class=""hp__hotel_ratings__stars hp__hotel_ratings__stars__clarification_track"">
                                        <i class=""b-sprite stars ratings_stars_4 star_track""
                                            title=""This is the official star rating given to the property by an independent third party - the Hotelstars Union. The property is compared to the industry standard and scored based on price, facilities and services offered. Use the star rating to help choose your stay!""
                                            data-component=""track"" data-track=""view"" data-hash=""YdVPYKDcdSBGRRaGaAUC""
                                            data-stage=""1""><span class=""invisible_spoken"">This is the official star
                                                rating
                                    </span>
                            </span>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetHotelClassification(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetHotelClassification_Should_Return_Zero_When_Classification_Does_Not_Exist()
    {
        //Arrange
        int expected = 0;
        HtmlDocument htmlDoc = new();

        string html = @" <span class=""nowrap hp__hotel_ratings"">
                                    <span
                                        class=""hp__hotel_ratings__stars hp__hotel_ratings__stars__clarification_track"">
                                        <i class=""b-sprite stars star_track""
                                            title=""This is the official star rating given to the property by an independent third party - the Hotelstars Union. The property is compared to the industry standard and scored based on price, facilities and services offered. Use the star rating to help choose your stay!""
                                            data-component=""track"" data-track=""view"" data-hash=""YdVPYKDcdSBGRRaGaAUC""
                                            data-stage=""1""><span class=""invisible_spoken"">This is the official star
                                                rating
                                    </span>
                            </span>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetHotelClassification(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetHotelReview_Should_Return_Review_When_Review_Exists()
    {
        //Arrange
        double expected = 9.5;
        HtmlDocument htmlDoc = new();

        string html = @"<span class=""rating notranslate"">
                            <span
                                class=""average js--hp-scorecard-scoreval"">9.5</span><span
                                class=""out_of"">/<span class=""best"">10</span></span>
                        </span>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetHotelReview(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetHotelReview_Should_Return_Zero_When_Review_Does_Not_Exist()
    {
        //Arrange
        double expected = 0.0;
        HtmlDocument htmlDoc = new();

        string html = @"<span class=""rating notranslate"">
                            <span
                                class="""">8.3</span><span
                                class=""out_of"">/<span class=""best"">10</span></span>
                        </span>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetHotelReview(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetReviewsCount_Should_Return_ReviewsCount_When_ReviewsCount_Exists()
    {
        //Arrange
        int expected = 1000;
        HtmlDocument htmlDoc = new();

        string html = @"<span class=""trackit score_from_number_of_reviews"">
                            Score from <strong class=""count"">1000</strong> reviews
                        </span>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetReviewsCount(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetReviewsCount_Should_Return_Zero_When_ReviewsCount_Does_Not_Exist()
    {
        //Arrange
        int expected = 0;
        HtmlDocument htmlDoc = new();

        string html = @"<span class=""trackit"">
                            Score from <strong class=""count"">1933</strong> reviews
                        </span>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetReviewsCount(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetDescription_Should_Return_Description_When_Description_Exists()
    {
        //Arrange
        string expected = "Paragraph 1.Paragraph 2.";
        HtmlDocument htmlDoc = new();

        string html = @"<div class=""hp_hotel_description_hightlights_wrapper "">
                                    <div class=""hotel_description_wrapper_exp "">
                                        <div id=""summary"" class="""">
                                        </div>
                                         <p>Paragraph 1.</p>
                                        <p>Paragraph 2.</p>
                                    </div>
                         </div>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetDescription(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetDescription_Should_Return_Empty_When_Description_Does_Not_Exist()
    {
        //Arrange
        string expected = string.Empty;
        HtmlDocument htmlDoc = new();

        string html = @"<div class=""hp_hotel_description_hightlights_wrapper "">
                                    <div class=""hotel_description_wrapper_exp "">
                                        <div id=""summary"" class="""">
                                        </div>
                                         <i>Paragraph 1.</i>
                                        <i>Paragraph 2.</i>
                                    </div>
                         </div>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetDescription(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetRoomCategories_Should_Return_Room_Categories_When_Room_Categories_Exist()
    {
        //Arrange
        List<string> expected = new() { "Penthouse with pool", "Single room suite", "Double room with balcony" };
        HtmlDocument htmlDoc = new();

        string html = @"<table>
                            <tbody>
                                <tr>
                                    <td class=""ftd"">
                                        Penthouse with pool
                                    </td>
                                    <td class=""ftd"">
                                        Single room suite
                                    </td>
                                     <td class=""ftd"">
                                        Double room with balcony
                                    </td>
                                </tr>
                            </tbody>
                         </table>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetRoomCategories(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetRoomCategories_Should_Return_Empty_When_Room_Categories_Do_Not_Exist()
    {
        //Arrange
        List<string> expected = new();
        HtmlDocument htmlDoc = new();

        string html = @"<table>
                            <tbody>
                                <tr>
                                </tr>
                            </tbody>
                         </table>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetRoomCategories(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetAlternativeHotels_Should_Return_Alternative_Hotels_When_Alternative_Hotels_Exist()
    {
        //Arrange
        List<string> expected = new() { "Hotel A", "Hotel B", "Hotel C" };
        HtmlDocument htmlDoc = new();

        string html = @"<p class=""althotels-name"">
                            <a class=""althotel_link""
                                href=""http://www.example.com"">
                                Hotel A
                            </a>
                            <i><span>5-star hotel</span></i>
                        </p>
                        <p class=""althotels-name"">
                            <a class=""althotel_link""
                                href=""http://www.example.com"">
                                Hotel B
                            </a>
                            <i><span>5-star hotel</span></i>
                        </p>
                        <p class=""althotels-name"">
                            <a class=""althotel_link""
                                href=""http://www.example.com"">
                                Hotel C
                            </a>
                            <i><span>5-star hotel</span></i>
                        </p>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetAlternativeHotels(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetAlternativeHotels_Should_Return_Empty_When_Alternative_Hotels_Do_Not_Exist()
    {
        //Arrange
        List<string> expected = new();
        HtmlDocument htmlDoc = new();

        string html = @"<p class="""">
                            <a class=""althotel_link""
                                href=""http://www.example.com"">
                                Hotel A
                            </a>
                            <i><span>5-star hotel</span></i>
                        </p>
                        <p class="""">
                            <a class=""althotel_link""
                                href=""http://www.example.com"">
                                Hotel B
                            </a>
                            <i><span>5-star hotel</span></i>
                        </p>
                        <p class="""">
                            <a class=""althotel_link""
                                href=""http://www.example.com"">
                                Hotel C
                            </a>
                            <i><span>5-star hotel</span></i>
                        </p>";

        byte[] byteArray = Encoding.ASCII.GetBytes(html);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader reader = new StreamReader(stream);

        htmlDoc.Load(reader);

        //Act
        var actual = _scraperService.GetAlternativeHotels(htmlDoc);

        //Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

}
