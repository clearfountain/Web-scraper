using Microsoft.AspNetCore.Mvc;
using Scraping.Application.Contracts.Infrastructure;
using Scraping.Infrastructure.Scraper;
using System.Net;

namespace Scraping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        #region fields, ctor

        private readonly IReportingService _reportingService;

        public ReportsController(IReportingService reportingService)
        {
            _reportingService = reportingService ?? throw new ArgumentNullException(nameof(_reportingService));
        }

        #endregion

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Index()
        {
            await _reportingService.GenerateExcelFromData();
            return Ok();
        }

        [HttpGet("get-hotel-rate")]
        public IActionResult HotelRates([FromQuery] int hotelId, [FromQuery] string arrivalDate)
        {
            var data = _reportingService.GetHotelRates(hotelId, arrivalDate);
            return data != null ? Ok(data) : NotFound("Not found");
        }
    }
}
