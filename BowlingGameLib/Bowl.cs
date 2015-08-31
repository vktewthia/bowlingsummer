using BowlingGameLib.Exceptions;

namespace BowlingGameLib
{
    public class Bowl
    {
        private const short MAXPOINT = 10;

        public Bowl(short pinsHit)
        {
            if (pinsHit >= 0 && pinsHit <= MAXPOINT)
            {
                PinsDrop = pinsHit;
            }
            else
            {
                throw new GameExceptions("Pin hit in a bowl cannot be more than 10 or less than 0");
            }
        }

        public short PinsDrop { get; protected set; }

        public override string ToString()
        {
            return string.Format("{0 }",PinsDrop);
        }
    }
}
