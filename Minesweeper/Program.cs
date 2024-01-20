using Minesweeper.Class;
using System.Numerics;

namespace Minesweeper
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Minesweeper!\n");
            int gridSize = getGridSize(); //Get grid size from user
            int minesCount = getMinesCount(gridSize); //Get mines count from user

            Minefield game = new Minefield(gridSize, minesCount); //Initialize minefield class
            game.startGame();

            if (game.gameOver)
                Console.WriteLine("Oh no, you detonated a mine! Game over.\n");
            else if (game.gameWon)
                Console.WriteLine("Congratulations, you have won the game!\n");

            Console.Write("Press any key to play again...");
            Console.ReadLine();
            Console.Clear();
            Main(args);
        }

        public static int getGridSize() //Function to get grid size
        {
            try
            {
                int gridSize = 0;

                while (true) //Keep looping until a correct input is entered.
                {
                    Console.Write("Enter the size of the grid (e.g. 4 for a 4x4 grid): ");
                    if (int.TryParse(Console.ReadLine(), out gridSize)) //Try parse user input, prompt error if failed
                    {
                        if (gridSize < 2) //Check whether user input value is within 2 to 10
                            Console.WriteLine("Minimum size of grid is 2.\n");
                        else if (gridSize > 10)
                            Console.WriteLine("Maximum size of grid is 10.\n");
                        else
                            return gridSize;
                    }
                    else
                        Console.WriteLine("Incorrect input.\n");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public static int getMinesCount(int gridSize) //Function to get mines count
        {
            try
            {
                int minesCount = 0;

                while (true)  //Keep looping until a correct input is entered.
                {
                    Console.Write("Enter the number of mines to place on the grid (maximum is 35% of the total squares):");
                    if (int.TryParse(Console.ReadLine(), out minesCount)) //Try parse user input, prompt error if failed
                    {
                        int maxMines = (int)((gridSize * gridSize) * 0.35); //Get the number for 35% of total squares
                        if (minesCount > maxMines) //If input more than 35% of squares, prompt error.
                            Console.WriteLine("Maximum number is 35% of total sqaures.\n");
                        else if (minesCount < 1) //If input less than 1, prompt error.
                            Console.WriteLine("There must be at least 1 mine.\n");
                        else
                            return minesCount;
                    }
                    else
                        Console.WriteLine("Incorrect input.\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
    }
}