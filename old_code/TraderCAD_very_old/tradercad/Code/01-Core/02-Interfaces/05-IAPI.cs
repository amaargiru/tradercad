namespace Core
{
    public interface IApi
    {
        ApiAnswer Read(ApiRequest request);
    }
}