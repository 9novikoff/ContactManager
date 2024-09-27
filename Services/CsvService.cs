using System.Globalization;
using CsvHelper;

namespace ContactManager.Services;

public class CsvService
{
    public List<T> ReadCsvFile<T>(Stream fileStream)
    {
        try
        {
            using var reader = new StreamReader(fileStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();

        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error reading CSV file", ex);
        }
    }
}