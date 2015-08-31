using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameLib.Exceptions
{
    public class GameExceptions : ArgumentException
    {
        public GameExceptions(string message):base(message)
        {
            
        }
        //Can do logging.
       
    }
}
