using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameLib
{
    public class FrameFactory
    {
        public IFrame GetNextFrame(short bowl1, short bowl2, short frameCounter)
        {
            return new Frame(bowl1, bowl2, frameCounter);
        }

        public IFrame GetNextFrame(short bowl1, short bowl2, short bowl3, short frameCounter)
        {
            return new LastFrame(bowl1, bowl2, bowl3, frameCounter);
        }

    }
}
