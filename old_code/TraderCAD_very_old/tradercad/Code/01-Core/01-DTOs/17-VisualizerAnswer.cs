// Read more at TraderCAD.com

using System.Drawing;

namespace Core
{
    public enum VisualizerResult
    {
        Ok,
        WrongData,
        NotEnoughData,
        TimeoutExpired
    }
    public record VisualizerAnswer
    {
        public string Visualizer { get; init; } = null!;
        public VisualizerResult Result { get; init; }
        public Bitmap? Bitmap { get; init; }
        public string? Json { get; init; }
    };
}