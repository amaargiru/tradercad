namespace Core
{
    public interface IVisualizer
    {
        VisualizerAnswer Read(VisualizerRequest request);
    }
}