using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Infrastructure.OpenTravelData.Readers;

public static class CsvReaderFactory
{
    private const string Delimiter = "^";

    public static List<TRecord> Load<TRecord>(string filePath, ClassMap<TRecord> map)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = Delimiter,
            HasHeaderRecord = true,
            BadDataFound = null,
        });
        csv.Context.RegisterClassMap(map);
        return csv.GetRecords<TRecord>().ToList();
    }
    
    public static List<TRecord> Load<TRecord, TMain, TSub>(
        string mainPath, ClassMap<TMain> mainMap,
        string subPath, ClassMap<TSub> subMap,
        Func<TMain, string?> mainKeySelector,
        Func<TSub, string?> subKeySelector,
        Func<TMain, TSub?, TRecord> mapper)
    {
        var mainList = Load(mainPath, mainMap);
        var subList = Load(subPath, subMap);

        var joined =
            from main in mainList
            join sub in subList
                on mainKeySelector(main)?.Trim()
                equals subKeySelector(sub)?.Trim()
                into subGroup
            from sub in subGroup.DefaultIfEmpty()
            select mapper(main, sub);

        return joined.ToList();
    }
}