// Read more at TraderCAD.com

using System;

namespace Core
{
    public record VisualizerRequest
    {
        public string Visualizer { get; init; } = null!;
        public string Headline { get; init; } = null!;
        public FinancePoint[] Data { get; init; } = null!;
        public bool IndicatorDrawsInSeparateChart;
        public FinancePoint?[][] ExtraData { get; init; }
        public IndicatorPoint?[] IndicatorData { get; init; }
        public PictureSettings PictureSettings { get; init; } = null!;
        public TimeSpan? TimeoutMs { get; init; }
    };
}