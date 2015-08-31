using System.Collections.Generic;

namespace BowlingGameLib
{
    public interface IFrame
    {
        int? Score { get; set; }
        bool IsStrike { get; }
        bool IsSpare { get; }
        bool IsValid { get; }
        void UpdateScore(short frameIndex, Dictionary<int, IFrame> frames);
    }
}
