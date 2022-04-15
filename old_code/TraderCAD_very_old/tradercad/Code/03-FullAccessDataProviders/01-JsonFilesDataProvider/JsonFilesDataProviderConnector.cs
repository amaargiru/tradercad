using Core;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace FullAccessDataProviders
{
    public class JsonFilesDataProviderConnector : IFullAccessDataProviderConnector
    {
        public CreateDataConnectorAnswer Create(CreateDataConnectorRequest request)
        {
            Utility.CheckRequestEquityForInvalidPathChars(request.Equity);

            string jsonFileName = GetJsonFileName(request);
            string convertedJson = JsonConvert.SerializeObject(request.Data, Formatting.Indented);
            File.WriteAllText(jsonFileName, convertedJson);

            return new CreateDataConnectorAnswer
            {
                Equity = request.Equity,
                Result = CreateDataConnectorResult.Ok
            };
        }

        public ReadDataConnectorAnswer Read(ReadDataConnectorRequest request)
        {
            Utility.CheckRequestEquityForInvalidPathChars(request.Equity);

            string jsonFileName = GetJsonFileName(request);
            string json = File.ReadAllText(jsonFileName);
            FinancePoint[] jsonData = JsonConvert.DeserializeObject<FinancePoint[]>(json);

            FinancePoint[] matchedJsonData = Array.FindAll(jsonData, p =>
                p.PointDateTime >= request.StartDateTime &&
                p.PointDateTime <= request.EndDateTime);

            return new ReadDataConnectorAnswer
            {
                Equity = request.Equity,
                Result = ReadDataConnectorResult.Ok,
                Data = matchedJsonData
            };
        }

        public UpdateDataConnectorAnswer Update(UpdateDataConnectorRequest request)
        {
            Utility.CheckRequestEquityForInvalidPathChars(request.Equity);

            string jsonFileName = GetJsonFileName(request);
            string json = File.ReadAllText(jsonFileName);
            FinancePoint[] jsonData = JsonConvert.DeserializeObject<FinancePoint[]>(json);

            jsonData = jsonData.Union(request.Data).ToArray();
            Array.Sort(jsonData);

            string convertedJson = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            System.IO.File.WriteAllText(jsonFileName, convertedJson);

            return new UpdateDataConnectorAnswer
            {
                Equity = request.Equity,
                Result = UpdateDataConnectorResult.Ok
            };
        }

        public DeleteDataConnectorAnswer Delete(DeleteDataConnectorRequest request)
        {
            Utility.CheckRequestEquityForInvalidPathChars(request.Equity);

            string jsonFileName = GetJsonFileName(request);
            string json = File.ReadAllText(jsonFileName);
            FinancePoint[] jsonData = JsonConvert.DeserializeObject<FinancePoint[]>(json);

            FinancePoint[] matchedJsonData = Array.FindAll(jsonData, p =>
            p.PointDateTime < request.StartDateTime ||
            p.PointDateTime > request.EndDateTime);
            Array.Sort(matchedJsonData);

            string convertedJson = JsonConvert.SerializeObject(matchedJsonData, Formatting.Indented);
            System.IO.File.WriteAllText(jsonFileName, convertedJson);

            return new DeleteDataConnectorAnswer
            {
                Equity = request.Equity,
                Result = DeleteDataConnectorResult.Ok
            };
        }

        public static string GetJsonFileName(ICrudConnectorRequest request)
        {
            return
                request.Equity + "_" +
                request.Timeframe.ToString().Replace(':', '.') + ".json";
        }
    }
}