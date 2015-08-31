using System.Collections.Generic;
using System.Text;
using BowlingGameLib.Exceptions;

namespace BowlingGameLib
{
    public class Game
    {
        private short _frameIndexCounter = 1;
        private readonly FrameFactory _frameFactory;

        public Game()
        {
            _frameFactory = new FrameFactory();
        }

        public Dictionary<int, IFrame> Frames = new Dictionary<int, IFrame>();

        public void RollTheBowl(short bowl1, short bowl2)
        {
            IFrame frame = _frameFactory.GetNextFrame(bowl1, bowl2, _frameIndexCounter);
            RollTheBowl(frame);
        }

        public void RollTheLastBowl(short bowl1, short bowl2, short bowl3)
        {
            IFrame frame = _frameFactory.GetNextFrame(bowl1, bowl2, bowl3, _frameIndexCounter);
            RollTheBowl(frame);
        }

        public void RollTheBowl(IFrame frame)
        {
            AddNextFrame(frame);
        }

        private void AddNextFrame(IFrame frame)
        {
            if (frame.IsValid)
            {
                if (!Frames.ContainsKey(_frameIndexCounter) && _frameIndexCounter <= 10)
                {
                    Frames.Add(_frameIndexCounter++, frame);
                }
                else
                {
                    throw new GameExceptions("Player allready played for this frame");
                }
            }
            else
                throw new GameExceptions("Wrong number of pin hit in frame please try again");
        }

        public void Reset()
        {
            _frameIndexCounter = 1;
            Frames = new Dictionary<int, IFrame>();
        }

        public int FinalScore()
        {
            int finalScore = UpdateScore();
            return finalScore;
        }

        private int UpdateScore()
        {
            int finalScore = 0;
            foreach (var frameItem in Frames)
            {
                var frame = frameItem.Value;
                frame.UpdateScore(_frameIndexCounter,Frames);
                frame.Score += finalScore;
                finalScore = frame.Score ?? 0;
            }
            return finalScore;
        }
        
        public override string ToString()
        {
            StringBuilder gameSummary = new StringBuilder();
            foreach (var frame in Frames)
            {
                gameSummary.AppendLine(frame.Value.ToString());
            }
            return gameSummary.ToString();
        }

    }
}
