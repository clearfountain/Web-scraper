using Scraping.Domain.Entities;

namespace Scraping.Application.Contracts.Infrastructure;

public interface IReportingService
{
    Task GenerateExcelFromData();
    HotelForReporting GetHotelRates(int hotelId, string arrivalDate);
}
