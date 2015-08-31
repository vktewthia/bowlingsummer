using System.Collections.Generic;

namespace BowlingGameLib
{
    public class Frame : AbstractFrame
    {

        public Frame(short b1, short b2, short index)
            :base(new Bowl(b1), new Bowl(b2), index)
        {

        }

         public Frame(Bowl bowl1, Bowl bowl2, short frameIndex) : base(bowl1, bowl2, frameIndex)
         {
             
         }

        public override bool IsValid
        {
            get { return (FirstBowl.PinsDrop + SecondBowl.PinsDrop <= 10); }
        }
        
        public override bool IsSpare
        {
            get
            {
                return Value() >= 10;
            }
           
        }
        
        public override int? GetScoreForStrikeFrame(Dictionary<int, IFrame> gamesFrame)
        {
            int scoreValue = 0;
           
           if (!gamesFrame.ContainsKey(this.Index + 1))
           {
               return null;
           }

           var nextFirstFrame = (AbstractFrame) gamesFrame[this.Index + 1];

            var lastFrame = nextFirstFrame as LastFrame;
            if (lastFrame != null)
            {
                scoreValue = MAXPOINTS + lastFrame.FirstBowl.PinsDrop + lastFrame.SecondBowl.PinsDrop;
            }
            else
            {
                if (nextFirstFrame.IsStrike)
                {
                    if (gamesFrame.ContainsKey(nextFirstFrame.Index + 1))
                    {
                        IFrame nextSecondFrame = gamesFrame[nextFirstFrame.Index + 1];
                        scoreValue = MAXPOINTS + MAXPOINTS + ((AbstractFrame)nextSecondFrame).FirstBowl.PinsDrop;
                        
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    scoreValue = MAXPOINTS + nextFirstFrame.Value();
                }
            }
            
            return scoreValue;
        }

        public override int? GetScoreForSpareFrame(Dictionary<int, IFrame> gamesFrame)
        {
            int? score = null;
            if (gamesFrame.ContainsKey(this.Index + 1))
            {
                IFrame nextFirstFrame = gamesFrame[this.Index + 1];
                score =  MAXPOINTS + ((AbstractFrame)nextFirstFrame).FirstBowl.PinsDrop;
            }
            return score;
        
        }

        public override string ToString()
        {
            return string.Format("Frame-{0}  |{1} ,{2} |  Score - {3}", Index, this.FirstBowl.ToString(), this.SecondBowl.ToString(),this.Score);
        }
    }
}
