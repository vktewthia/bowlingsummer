using System.Collections.Generic;

namespace BowlingGameLib
{
    public class LastFrame : AbstractFrame
    {
        public Bowl ExtraBowl; 

        public LastFrame(Bowl bowl1, Bowl bowl2,Bowl bowl3,short frameIndex)
            : base(bowl1,bowl2,frameIndex)
        {
            ExtraBowl = bowl3;
        }

        public LastFrame(short b1, short b2, short b3, short index)
            : this(new Bowl(b1), new Bowl(b2),new Bowl(b3),index)
        {
        }
        
        public override bool IsValid
        {
            get
            {
                return (FirstBowl.PinsDrop + SecondBowl.PinsDrop <= 20)
                    && (FirstBowl.PinsDrop + SecondBowl.PinsDrop + ExtraBowl.PinsDrop <= 30); 
            }
        }

        public override short Value()
        {
            int value = 0;

            if (this.IsStrike || this.IsSpare)
            {
                value += FirstBowl.PinsDrop + SecondBowl.PinsDrop + ExtraBowl.PinsDrop;
            }
            else
            {
                value += FirstBowl.PinsDrop + SecondBowl.PinsDrop;
            }
            

            return (short)value;
        }

        public override bool IsSpare
        {
            get { return base.Value() >= 10; }
        }

        public override int? GetScoreForStrikeFrame(Dictionary<int, IFrame> gamesFrame)
        {
            return Value();
        }

        public override int? GetScoreForSpareFrame(Dictionary<int, IFrame> gamesFrame)
        {
            return Value();
        }

        public override string ToString()
        {
            return string.Format("Frame-{0} |{1} ,{2} ,{3} | Score - {4}", Index, this.FirstBowl.ToString(), this.SecondBowl.ToString(), this.ExtraBowl.ToString(),this.Score);
        }
    }
}
