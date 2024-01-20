using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Class
{
    public class Minefield
    {
        public bool gameOver = false; //flag to track whether game is over
        public bool gameWon = false; //flag to track whether game is won

        private char[,] grid; //2D array to store grid data of minefields
        private bool[,] mines; //2D array to store location of mines

        private int gridSize; //Grid size from user input, gridSize * gridSize = total number of squares
        private int revealCount = 0; //Variable to track number of squares revealed, to check whether user has won.
        private int minesCount = 0; //Mines count from user input.

        public char[,] getGrid()
        {
            return grid;
        }

        public bool[,] getMines()
        {
            return mines;
        }

        public Minefield(int gridSize, int minesCount)
        {
            this.gridSize = gridSize;
            this.minesCount = minesCount;
            grid = new char[gridSize, gridSize]; //Set size for 2D array according to gridSize
            mines = new bool[gridSize, gridSize]; //Set size for 2D array according to gridSize

            initBoard(); //Function to initialize minefield
            plantMines(); //Function to plant mines in minefield

            Console.Write("\nHere is your minefield:\n");
            printBoard();
        }

        private void initBoard() //Function to initialize minefield
        {
            for (int i = 0; i < gridSize; i++) //Loop through every row
            {
                for (int j = 0; j < gridSize; j++) //Loop through every columns
                {
                    grid[i, j] = '-'; //Set value of '-' to all squares by default
                }
            }
        }

        private void plantMines()
        {
            Random random = new Random();
            int minePlaced = 0; //To ensure the number of mines placed match with the number of mines from user input.

            while (minePlaced < minesCount)
            {
                int row = random.Next(0, gridSize); //random function to plant mines randomly.
                int col = random.Next(0, gridSize); //random function to plant mines randomly.

                if (!mines[row, col]) //To avoid mines being planted on same column twice.
                {
                    mines[row, col] = true;
                    minePlaced++;
                }
            }
        }

        private void printBoard() //Function to print the minefields
        {
            Console.Write("  ");
            for (int i = 1; i <= gridSize; i++) //Print header for minefields
                Console.Write(i + " ");

            Console.Write("\n");

            for (int i = 0; i < gridSize; i++)
            {
                Console.Write((char)('A' + i) + " "); //For every row, print the A, B, C... in front
                for (int j = 0; j < gridSize; j++)
                {
                    Console.Write(grid[i, j] + " "); //Print value for each grid
                }
                Console.Write("\n");
            }
        }

        public void startGame() //Function to start game, after initalizing minefields and planting mines.
        {
            try
            {
                while (true) //While loop to keep the game going if game is not yet ended.
                {
                    Console.Write("Select a square to reveal (e.g. A1): ");
                    string userInput = Console.ReadLine(); //Get users selected square

                    if (userInput == null || userInput.Length != 2 || userInput[0] < 'A' || userInput[0] >= 'A' + gridSize || userInput[1] < '1' || userInput[1] >= '1' + gridSize) //Validate user input
                        Console.WriteLine("Incorrect input.\n");
                    else
                    {
                        int row = userInput[0] - 'A'; //Get row number by index, minus 'A' because it is a char variable.
                        int col = userInput[1] - '1'; //Get row number by index, minus '1' because it is a char variable.

                        if (grid[row, col] != '-') //Check whether the selected square has already been revealed.
                        {
                            Console.Write("The square has already been revealed previously, please select another square.\n");
                        }
                        else
                        {
                            revealSquare(row, col); //Function to reveal user selected square
                            Console.WriteLine("This square contains " + grid[row, col] + " adjacent mines.\n"); //To display the number of adjacent mines of the square that user selected

                            if (gameWon == true || gameOver == true) //Check whether game is over, break loop if game over.
                            {
                                break;
                            }
                            else
                            {
                                Console.Write("\nHere is your updated minefield:\n"); //If game is not yet ended, display updated minefields
                                printBoard();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void revealSquare(int row, int col)
        {
            if (row < 0 || row >= gridSize || col < 0 || col >= gridSize || grid[row, col] != '-') //To check whether row and col are valid, also a base case to stop the recursion
                return;

            int nearbyMines = countNearbyMines(row, col); //Call function to calculate number of nearby mines

            grid[row, col] = nearbyMines > 0 ? (char)(nearbyMines + '0') : '0'; //If nearby mines greater than 0 set grid value to nearby mines value, using (char)(nearbyMines + '0') because converting int to char.
            revealCount++; //For each time this function is called, increment the square revealed count by 1

            if (nearbyMines == 0) 
            {
                //If nearby mines of the selected square is 0, recursively reveal all nearby squares until squares with value is found.
                revealSquare(row - 1, col - 1);
                revealSquare(row - 1, col);
                revealSquare(row - 1, col + 1);

                revealSquare(row, col - 1);
                revealSquare(row, col + 1);

                revealSquare(row + 1, col - 1);
                revealSquare(row + 1, col);
                revealSquare(row + 1, col + 1);
            }

            if ((revealCount + minesCount) == (gridSize * gridSize)) //If total number of squares revealed + number of mines in the game equals to total number of squares in the game, it means user has won the game
            {
                gameWon = true; //Set game won flag to true
            }
            else if (mines[row, col])
            {
                gameOver = true; //Set game over flag to true
            }
        }

        private int countNearbyMines(int row, int col) //Function to calculate nearby mines
        {
            int count = 0;

            //Top left    Top    Top Right
            //      \       |       /
            //       \      |      /
            //Left---Selected Square---Right
            //       /      |      \
            //      /       |       \
            //Bottom left  Bottom    Bottom Right

            for (int i = row - 1; i <= row + 1; i++) //Loop through top left, top, top right, left, right, bottom left, bottom and bottom right of the selected square
                for (int j = col - 1; j <= col + 1; j++)
                    if (i >= 0 && i < gridSize && j >= 0 && j < gridSize && mines[i, j])
                        count++; //Increment mine count by 1 if it is a square with mine.

            return count;
        }
    }
}
