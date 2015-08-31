using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowlingGameLib.Exceptions;

namespace BowlingGameLib
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string option = String.Empty;
            do
            {
                Console.Clear();
                Game game = new Game();
                Console.WriteLine("**** New Bowling Game ****");
                bool isLiveScore = AskUserForLiveScore();
                Console.WriteLine("");
                Console.WriteLine(@"Type score as 0-9, X for strike, / for spare");
                Console.WriteLine("");

                for (short i = 1; i <= 10; i++)
                {
                    try
                    {
                        game.RollTheBowl(GetInput(i));

                        if (isLiveScore)
                        {
                            Console.WriteLine("-------------------------------");
                            game.FinalScore();
                            Console.WriteLine("{0}", game.ToString());
                            Console.WriteLine("-------------------------------");
                        }

                    }
                    catch (GameExceptions ex)
                    {
                        Console.WriteLine("*** Warning *** {0}", ex.Message);
                        i--;
                    }
                }

                var score = game.FinalScore();

                Console.WriteLine("---------------------------Output for the Game------------------------------------");
                Console.WriteLine(game.ToString());
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("Score - {0}", score);
                Console.WriteLine("----------------------------------------------------------------------------------");
                game.Reset();
                Console.Write("Do you want to play another game hit 'Y' for yes or any key for no :   ");
                option = Console.ReadLine();

            } while (option.ToLower().Equals("y"));
        }

        private static bool AskUserForLiveScore()
        {
            Console.Write("Wana see score after each turn, hit 'Y' for yes or any key for no :   ");
            var userOption = Console.ReadLine();
            return userOption.ToLower().Equals("y");
        }

        private static AbstractFrame GetInput(short frameIndex)
        {
            AbstractFrame frame = null;

            Console.WriteLine("Input for Frame-{0} ", frameIndex);
            Bowl bowl1 = RollFirstBowl();
            Bowl bowl2 = RollSecondBowl(bowl1, frameIndex);

            frame = new Frame(bowl1, bowl2, frameIndex);

            if (frameIndex == 10 && (frame.IsStrike || frame.IsSpare))
            {
                Bowl bowl3 = RollThirdBowl(bowl2);
                frame = new LastFrame(bowl1, bowl2, bowl3, frameIndex);
            }

            return frame;
        }

        private static Bowl RollFirstBowl()
        {
            string pinHit = string.Empty;
            short inputValue = 0;
            do
            {
                Console.Write(" Bowl 1   ");
                pinHit = Console.ReadLine();

            } while (!ValidateInputFirstBowl(pinHit, out inputValue));

            return new Bowl(inputValue);
        }

        private static Bowl RollSecondBowl(Bowl bowl1, short frameIndex)
        {
            short inputValue = 0;
            bool isInValidInput = true;

            if (bowl1.PinsDrop < 10 || frameIndex == 10)
            {
                while (isInValidInput)
                {
                    Console.Write(" Bowl 2   ");
                    string pinHit = Console.ReadLine();
                    isInValidInput = !(ValidateInputOtherBowl(pinHit, bowl1, out inputValue));
                }
            }
            return new Bowl(inputValue);
        }

        private static Bowl RollThirdBowl(Bowl bowl1)
        {
            short inputValue = 0;
            bool isInValidInput = true;
            while (isInValidInput)
            {
                Console.Write(" Extra Bowl    ");
                string pinHit = Console.ReadLine();
                isInValidInput = !(ValidateInputOtherBowl(pinHit, bowl1, out inputValue));
            }

            return new Bowl(inputValue);
        }

        private static bool ValidateInputFirstBowl(string input, out short value)
        {
            if (input.Length == 1 && input.ToLower().Contains('x'))
            {
                value = 10;
                return true;
            }
            if (input.Length == 1 && input.ToLower().Contains('-'))
            {
                value = 0;
                return true;
            }
            return Int16.TryParse(input, out value);
        }

        private static bool ValidateInputOtherBowl(string input, Bowl first, out short value)
        {
            if (input.Length == 1 && input.ToLower().Contains('/'))
            {
                value = Convert.ToInt16(10 - first.PinsDrop);
                return true;
            }
            if (input.Length == 1 && input.ToLower().Contains('x'))
            {
                value = 10;
                return true;
            }
            if (input.Length == 1 && input.ToLower().Contains('-'))
            {
                value = 0;
                return true;
            }
            return Int16.TryParse(input, out value);
        }
    }
}
