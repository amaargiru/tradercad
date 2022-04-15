namespace Core
{
    public interface IReadOnlyDataProviderConnector
    {
        ReadDataConnectorAnswer Read(ReadDataConnectorRequest request);
    }
}