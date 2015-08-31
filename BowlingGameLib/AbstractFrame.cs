using System;
using System.Collections.Generic;

namespace BowlingGameLib
{
    public abstract class AbstractFrame :IFrame
    {
        public Bowl FirstBowl;
        public Bowl SecondBowl;
        private const short LASTFRAME = 10;
        protected const short MAXPOINTS = 10;
        
        public short Index { get; set; }
        public int? Score { get; set; }


        protected AbstractFrame(short frameIndex)
        {
            this.Index = frameIndex;
        }

        protected AbstractFrame(Bowl bowl1, Bowl bowl2, short frameIndex)
            : this(frameIndex)
        {
            FirstBowl = bowl1;
            SecondBowl = bowl2;
        }

        public virtual bool IsStrike
        {
            get
            {
                return FirstBowl != null && FirstBowl.PinsDrop >= 10;
            }
        }

        public virtual short Value()
        {
            short value = 0;
            if (FirstBowl != null)
                value += FirstBowl.PinsDrop;

            if (SecondBowl != null)
                value += SecondBowl.PinsDrop;

            return value;
        }

        public abstract int? GetScoreForStrikeFrame(Dictionary<int, IFrame> gamesFrame);
        public abstract int? GetScoreForSpareFrame(Dictionary<int, IFrame> gamesFrame);
        
        public virtual bool IsValid
        {
            get { throw new NotImplementedException(); }
        }

        public virtual bool IsSpare
        {
            get { throw new NotImplementedException(); }
        }
        
        public void UpdateScore(short frameIndexCounter, Dictionary<int, IFrame> gamesFrame)
        {
            if (Index != LASTFRAME && Index == frameIndexCounter - 1 && (IsStrike || IsSpare))
            {
                Score = null;
                
            }else if (IsStrike || IsSpare)
            {
                Score = GetScoreForStrikeOrSpare(gamesFrame);
            }
            else
            {
                Score = Value();
            }
           
        }

        private int? GetScoreForStrikeOrSpare(Dictionary<int, IFrame> gamesFrame)
        {
            return (IsStrike) ? GetScoreForStrikeFrame(gamesFrame) : GetScoreForSpareFrame(gamesFrame);
        }

       
    }
}
