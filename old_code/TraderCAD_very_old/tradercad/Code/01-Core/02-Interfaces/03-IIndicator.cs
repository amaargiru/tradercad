namespace Core
{
    public interface IIndicator
    {
        IndicatorAnswer Read(IndicatorRequest request);
    }
}