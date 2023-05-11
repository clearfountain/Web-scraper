using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Scraping.Application.Contracts.Infrastructure;
using System.Net;

namespace Scraping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapeController : ControllerBase
    {

        #region fields, ctor
        private readonly IScraperService _scraperService;

        public ScrapeController(IScraperService scraperService)
        {
            _scraperService = scraperService ?? throw new ArgumentNullException(nameof(_scraperService));
        }

        #endregion

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Index()
        {
            var filePathInFolder = @"Scraper\Scraping.API\files\Kempinski Hotel Bristol Berlin, Germany - Booking.com.html";
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var filePath = Path.Combine(path, filePathInFolder);

            var hotel = _scraperService.ExtractHotelFromFile(filePath);

            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new DefaultNamingStrategy()
                }
            };

            var response = JsonConvert.SerializeObject(hotel, serializerSettings);

            return Ok(response);
           
        }
    }
}
