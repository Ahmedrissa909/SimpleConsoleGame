using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleConsoleGame
{
    class SimpleGame
    {
        static void Main()
        {
            //Initialize height buffer to remove scroll bar
            Console.BufferHeight = Console.WindowHeight;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            //Define a random number generator
            Random rnd = new Random();

            BeginTheGame:

            // The main title of this game
            drawTitle();

            int numberGuess = rnd.Next(0, 100);
            double userNumber;
            int countGuess = 0;
            string userValue;
            
            while (true)
            {
                Console.CursorVisible = false;
                drawTitle();
                Console.CursorTop = CursorTop;
                Console.CursorVisible = true;
                Console.Write("What is my number? ");
                userValue = Console.ReadLine().ToLower();
                if (userValue == null || userValue.Length <= 0)
                    continue;
                else if (userValue is "q" || userValue is "exit")   //check if user want to exit the game
                    break;
                else if(!isNumber(userValue))                       //check user value to contert it to number
                {
                    drawError("Please type the numbers which I guessed");
                    continue;
                }

                userNumber = double.Parse(userValue);
                if (userNumber > numberGuess)
                    Console.WriteLine("You just entered a bigger number");
                else if (userNumber < numberGuess)
                    Console.WriteLine("You just entered a smaller number");
                else
                {
                    Console.WriteLine($"Excelent!, you found it efter {countGuess} times, it was {numberGuess}.");
                    Console.Write("Do you want to play again? (y/n) : ");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.N)
                        break;
                    CursorTop = 0;
                    Console.CursorTop = CursorTop;
                    Console.Clear();
                    goto BeginTheGame;
                }
                countGuess++;
            }

            Console.WriteLine("\nHave a nice day but come back soon!");
            Thread.Sleep(1000); // waiting 1 second
        }

        #region Some custom methods

        static void drawTitle()
        {
            CursorTop = Console.CursorTop;
            Console.CursorTop = 0;
            drawLoopChar("*", Console.WindowWidth);
            drawTitle($"Hello {Environment.UserName}, and welcome to \"The Guessing Number\"");
            drawTitle("Press \"q\" if you want to exit the game");
            drawLoopChar("*", Console.WindowWidth);
            Console.WriteLine(">>> I have a number between 0 and 100, can you guess it? <<<");
            Console.WriteLine();
        }

        static void drawError(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Error.Write("Please type the numbers which I guessed");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Error.WriteLine();
        }

        /// <summary>
        /// This text is going to show with some styles
        /// </summary>
        /// <param name="value">This is a text for this title</param>
        static void drawTitle(string value)
        {
            // try to get the center of console window
            int center = (Console.WindowWidth - value.Length) / 2 - 5;
            drawLoopChar("*", center);//stars of the left side
            drawLoopChar(" ", 5);//some space between stars and title
            Console.Write(value);//the title
            drawLoopChar(" ", 5);//...
            drawLoopChar("*", center % 2 == 0 ? center : center + 1);//stars of the right side
        }

        /// <summary>
        /// Write some characters in a for loop
        /// </summary>
        /// <param name="value">The main content which is going to show</param>
        /// <param name="count">Enter an integer value</param>
        static void drawLoopChar(string value, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(value);
            }
        }

        /// <summary>
        /// This method checks numbers
        /// </summary>
        /// <param name="value">Write some values to check</param>
        /// <returns>It returns false if the value have non numeric characters otherwise it will be true</returns>
        static bool isNumber(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] < 48 || value[i] > 57) return false;
            }
            return true;
        }

        static int CursorTop;
        #endregion
    }
}
