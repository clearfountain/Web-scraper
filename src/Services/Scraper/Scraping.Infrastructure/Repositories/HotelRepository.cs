using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Scraping.Application.Contracts.Persisitence;
using Scraping.Domain.Entities;

namespace Scraping.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private Dictionary<int, HotelForReporting> Store;
    private HotelForReporting Hotel;

    public HotelRepository()
    {
        Store = new Dictionary<int, HotelForReporting>();
        LoadData();
    }

    public HotelForReporting Get(int hotelId, string arrivalDate)
    {
        var date = DateTime.Parse(arrivalDate).Date;
        var result = new HotelForReporting();
        try
        {
            var hotel = Store[hotelId];
            result.Hotel = hotel.Hotel;
            result.HotelRates = new List<HotelRate>();
            foreach (var r in hotel.HotelRates)
            {
                if (r.TargetDay.Date == date)
                {
                    result.HotelRates.Add(r);
                }
            }
        }
        catch (KeyNotFoundException e)
        {
            return null;
        }
        return result;
    }

    public HotelForReporting GetDataForExport()
    {
        if (Hotel != null) return Hotel;

        try
        {
            var directory = Environment.CurrentDirectory;
            var path = Path.Combine(directory, @"files\task 2 - hotelrates.json");
            StreamReader reader = new(path);
            var data = reader.ReadToEnd();

            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new DefaultNamingStrategy()
                }
            };

            Hotel = JsonConvert.DeserializeObject<HotelForReporting>(data, serializerSettings);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return Hotel;
    }

    private void LoadData()
    {
        var store = new List<HotelForReporting>();
        try
        {
            var directory = Environment.CurrentDirectory;
            var path = Path.Combine(directory, @"files\task 3 - hotelsrates.json");
            StreamReader reader = new StreamReader(path);
            var data = reader.ReadToEnd();
            var res = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new DefaultNamingStrategy()
                }
            };
            store = JsonConvert.DeserializeObject<List<HotelForReporting>>(data, res);
            foreach (var s in store)
            {
                Store.Add(s.Hotel.HotelID, s);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
