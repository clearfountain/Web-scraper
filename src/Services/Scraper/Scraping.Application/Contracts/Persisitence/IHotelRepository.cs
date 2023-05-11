using Scraping.Domain.Entities;

namespace Scraping.Application.Contracts.Persisitence;

public interface IHotelRepository
{
    HotelForReporting GetDataForExport();
    HotelForReporting Get(int hotelId, string arrivalDate);
}
