namespace Core
{
    public interface IFullAccessDataProviderConnector
    {
        CreateDataConnectorAnswer Create(CreateDataConnectorRequest request);
        ReadDataConnectorAnswer Read(ReadDataConnectorRequest request);
        UpdateDataConnectorAnswer Update(UpdateDataConnectorRequest request);
        DeleteDataConnectorAnswer Delete(DeleteDataConnectorRequest request);
    }
}