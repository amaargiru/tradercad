// Read more at TraderCAD.com

using System;

namespace Core
{
    public record ApiRequest
    {
        public string ApiKey { get; init; } = null!;
        public string Equity { get; init; } = null!;
        public string Indicator { get; init; } = null!;
        public decimal[]? Coeffs { get; init; }
        public string Visualizer { get; init; } = null!;
        public bool PictureRequest { get; init; }
        public bool JsonRequest { get; init; }
        public DateTime StartDateTime { get; init; }
        public DateTime EndDateTime { get; init; }
        public TimeSpan Timeframe { get; init; }
        public PictureSettings? PictureSettings { get; init; }
        public TimeSpan? TimeoutMs { get; init; }
    };
}