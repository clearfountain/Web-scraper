using Scraping.Application.Contracts.Persisitence;
using Scraping.Application.Models.Reporting;
using System.ComponentModel;
using System.Data;
using OfficeOpenXml;
using FastMember;
using Scraping.Infrastructure.Helpers;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Scraping.Application.Contracts.Infrastructure;
using Scraping.Domain.Entities;

namespace Scraping.Infrastructure.Reporting
{
    public class ReportingService : IReportingService
    {
        private readonly IHotelRepository _hotelRepository;

        public ReportingService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task GenerateExcelFromData()
        {
            var table = new DataTable();
            var data = _hotelRepository.GetDataForExport();
            var excelData = new List<ExcelExportData>();
            foreach (var s in data.HotelRates)
            {
                var toAdd = new ExcelExportData()
                {
                    ARRIVAL_DATE = s.TargetDay.Date.Format(),
                    DEPARTURE_DATE = HotelHelper.GetDepartureDate(s.Los, s.TargetDay).Format(),
                    PRICE = s.Price.NumericFloat,
                    CURRENCY = s.Price.Currency,
                    RATENAME = s.RateName,
                    ADULTS = s.Adults
                };

                var breakFastTag = s.RateTags.FirstOrDefault(x => x.Name == "breakfast");
                if (breakFastTag != null)
                {
                    toAdd.BREAKFAST_INCLUDED = breakFastTag.Shape ? 1 : 0;
                } else
                {
                    toAdd.BREAKFAST_INCLUDED = 0;
                }
                excelData.Add(toAdd);
            }

            using (var reader = ObjectReader.Create(excelData))
            {
                table.Load(reader);
            }

            var fileInfo = new FileInfo("files\\task2.xlsx");
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                fileInfo = new FileInfo("files\\task2.xlsx");
            }
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var pkg = new ExcelPackage(fileInfo))
            {
                var workSheet = pkg.Workbook.Worksheets.Add("Hostel report");
                workSheet.Cells["A1"].LoadFromDataTable(table, PrintHeaders: true);
                await pkg.SaveAsync();
            }
        }
    
        public HotelForReporting GetHotelRates(int hotelId, string arrivalDate)
        {
            return _hotelRepository.Get(hotelId, arrivalDate);
        }
    }
}
