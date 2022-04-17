using Core;

using Newtonsoft.Json;

namespace DataConnectors;

public class JsonFilesDataConnector : IFullAccessDataConnector
{
    public int BulkInsert(Equities equity, Timeframe timeframe, EquityPoint[] equityPoints, string password = null!, int timeout = 0)
    {
        var jsonFileName = Utility.GetJsonFileName(equity, timeframe);
        string convertedJson;

        if (File.Exists(jsonFileName))
        {
            var json = File.ReadAllText(jsonFileName);
            var jsonData = JsonConvert.DeserializeObject<EquityPoint[]>(json);
            jsonData = jsonData?.Union(equityPoints).OrderBy(data => data.PointDateTime).ToArray();
            convertedJson = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
        }
        else
        {
            convertedJson = JsonConvert.SerializeObject(equityPoints, Formatting.Indented);
        }

        File.WriteAllText(jsonFileName, convertedJson);

        return equityPoints.Length;
    }

    public bool Insert(Equities equity, Timeframe timeframe, EquityPoint equityPoint, string password = null!, int timeout = 0)
    {
        var jsonFileName = Utility.GetJsonFileName(equity, timeframe);
        string convertedJson;

        if (File.Exists(jsonFileName))
        {
            var json = File.ReadAllText(jsonFileName);
            var jsonData = JsonConvert.DeserializeObject<EquityPoint[]>(json);
            jsonData = jsonData?.Append(equityPoint).OrderBy(p => p.PointDateTime).ToArray();
            convertedJson = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
        }
        else
        {
            convertedJson = JsonConvert.SerializeObject(equityPoint, Formatting.Indented);
        }

        File.WriteAllText(jsonFileName, convertedJson);

        return true;
    }

    public EquityPoint[]? BulkRead(Equities equity, Timeframe timeframe, DateTime startDateTime, DateTime endDateTime,
        string password = null!, int timeout = 0)
    {
        var jsonFileName = Utility.GetJsonFileName(equity, timeframe);

        if (!File.Exists(jsonFileName))
        {
            return null;
        }

        var json = File.ReadAllText(jsonFileName);
        var jsonData = JsonConvert.DeserializeObject<EquityPoint[]>(json);

        bool isMatchesDateTimeRange(DateTime dt) => (dt >= startDateTime) && (dt <= endDateTime);
        return jsonData?.Where(d => isMatchesDateTimeRange(d.PointDateTime)).ToArray();
    }

    public EquityPoint? Read(Equities equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0)
    {
        var jsonFileName = Utility.GetJsonFileName(equity, timeframe);

        if (!File.Exists(jsonFileName))
        {
            return null;
        }

        var json = File.ReadAllText(jsonFileName);

        try // Try to read an array of data
        {
            var jsonData = JsonConvert.DeserializeObject<EquityPoint[]>(json);

            if (jsonData is not null)
                foreach (var response in jsonData)
                {
                    if (response.PointDateTime == dateTime)
                        return response;
                }
        }

        catch (JsonSerializationException)
        {
            try // Try to read a single value
            {
                var response = JsonConvert.DeserializeObject<EquityPoint>(json);
                if (response!.PointDateTime == dateTime)
                    return response;
            }
            catch (Exception)
            {
            }
        }

        catch (Exception)
        {
        }

        return null;
    }

    public bool Delete(Equities equity, Timeframe timeframe, DateTime dateTime, string password = null!, int timeout = 0)
    {
        var jsonFileName = Utility.GetJsonFileName(equity, timeframe);

        if (!File.Exists(jsonFileName))
        {
            return false;
        }

        var json = File.ReadAllText(jsonFileName);
        var jsonData = JsonConvert.DeserializeObject<EquityPoint[]>(json);

        if (jsonData is not null)
        {
            foreach (var response in jsonData)
            {
                if (response.PointDateTime == dateTime)
                {
                    jsonData = jsonData.Where(d => d.PointDateTime != dateTime).ToArray();

                    var cleanedJson = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
                    File.WriteAllText(jsonFileName, cleanedJson);

                    return true;
                }
            }
        }

        return false;
    }
}
