using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Class;
using System;
using System.Diagnostics;
using System.IO;

namespace MinesweeperTests
{
    [TestClass]
    public class MinesweeperTests
    {

        [TestMethod]
        public void TestGetGridSize_ValidInput() //To test whether function can work properly with valid input.
        {
            // Arrange
            string input = "5\n";
            int expectedOutput = 5;

            // Act
            StringReader sr = new StringReader(input);
            Console.SetIn(sr); //Set Console.ReadLine() to read from StringReader
            var actualOutput = Minesweeper.Program.getGridSize();

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestGetGridSize_InvalidInput() //To test whether function prompt error message correctly with invalid innput.
        {
            // Arrange
            string input = "abc\n5\n"; //Enter "abc" (incorrect input), then enter "5" (correct input) to break to loop. Otherwise the testcase will stuck in infinite loop
            string expectedOutput = "Incorrect input.\n";
            int result = 0;

            // Act
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            StringReader sr = new StringReader(input);
            Console.SetIn(sr); // Set Console.ReadLine() to read from StringReader
            Minesweeper.Program.getGridSize();
            result = sw.ToString().Contains(expectedOutput) ? 1 : 0; //Set result to 1 if expected error message is detected.

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestGetGridSize_InvalidMinInput() //To test whether function prompt error message correctly with invalid innput.
        {
            // Arrange
            string input = "-5\n5\n";  //Enter "-5" (incorrect input), then enter "5" (correct input) to break to loop. Otherwise the testcase will stuck in infinite loop
            string expectedOutput = "Minimum size of grid is 2.\n";
            int result = 0;

            // Act
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            StringReader sr = new StringReader(input);
            Console.SetIn(sr); // Set Console.ReadLine() to read from StringReader
            Minesweeper.Program.getGridSize();
            result = sw.ToString().Contains(expectedOutput) ? 1 : 0; //Set result to 1 if expected error message is detected.

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestGetGridSize_InvalidMaxInput() //To test whether function prompt error message correctly with invalid innput.
        {
            // Arrange
            string input = "100\n5\n";  //Enter "100" (incorrect input), then enter "5" (correct input) to break to loop. Otherwise the testcase will stuck in infinite loop
            string expectedOutput = "Maximum size of grid is 10.\n";
            int result = 0;

            // Act
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            StringReader sr = new StringReader(input);
            Console.SetIn(sr); // Set Console.ReadLine() to read from StringReader
            Minesweeper.Program.getGridSize();
            result = sw.ToString().Contains(expectedOutput) ? 1 : 0; //Set result to 1 if expected error message is detected.

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestGetMinesCount_ValidInput() //To test whether function can work properly with valid input.
        {
            // Arrange
            int gridSize = 5;
            string input = "5\n";
            int expectedOutput = 5;

            // Act
            StringReader sr = new StringReader(input);
            Console.SetIn(sr); // Set Console.ReadLine() to read from StringReader
            int actualOutput = Minesweeper.Program.getMinesCount(gridSize);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestGetMinesCount_InvalidInput()//To test whether function prompt error message correctly with invalid innput.
        {
            // Arrange
            int gridSize = 5;
            string input = "abc\n5\n"; //Enter "abc" (incorrect input), then enter "5" (correct input) to break to loop. Otherwise the testcase will stuck in infinite loop
            string expectedOutput = "Incorrect input.\n";
            int result = 0;

            // Act
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            StringReader sr = new StringReader(input);
            Console.SetIn(sr); // Set Console.ReadLine() to read from StringReader
            Minesweeper.Program.getMinesCount(gridSize);
            result = sw.ToString().Contains(expectedOutput) ? 1 : 0; //Set result to 1 if expected error message is detected.

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestGetMinesCount_InvalidMinInput() //To test whether function prompt error message correctly with invalid innput.
        {
            // Arrange
            int gridSize = 5;
            string input = "-5\n5\n";//Enter "-5" (incorrect input), then enter "5" (correct input) to break to loop. Otherwise the testcase will stuck in infinite loop
            string expectedOutput = "There must be at least 1 mine.\n";
            int result = 0;

            // Act
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            StringReader sr = new StringReader(input);
            Console.SetIn(sr); // Set Console.ReadLine() to read from StringReader
            Minesweeper.Program.getMinesCount(gridSize);
            result = sw.ToString().Contains(expectedOutput) ? 1 : 0;//Set result to 1 if expected error message is detected.

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestGetMinesCount_InvalidMaxInput() //To test whether function prompt error message correctly with invalid innput.
        {
            // Arrange
            int gridSize = 8;
            string input = "23\n5\n"; //Enter "23" (incorrect input), then enter "5" (correct input) to break to loop. Otherwise the testcase will stuck in infinite loop
            string expectedOutput = "Maximum number is 35% of total sqaures.\n";
            int result = 0;

            // Act
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            StringReader sr = new StringReader(input);
            Console.SetIn(sr); // Set Console.ReadLine() to read from StringReader
            Minesweeper.Program.getMinesCount(gridSize);
            result = sw.ToString().Contains(expectedOutput) ? 1 : 0; //Set result to 1 if expected error message is detected.

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestGameWon() //To test whether game won when all conditions are met.
        {
            // Arrange
            int gridSize = 8;
            int minesCount = 20;
            Minefield minefield = new Minefield(gridSize, minesCount);
            bool[,] mines = minefield.getMines();

            //Loop through all squares
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (!mines[i, j]) 
                        minefield.revealSquare(i, j); //Reveal all squares if it is not mine. Simulating win condition.
                }
            }

            // Act
            bool gameWon = minefield.gameWon; //Game won flag should be true if all squares without mine is revealed.

            // Assert
            Assert.IsTrue(gameWon, "The game should be won when all non-mine squares are revealed.");
        }

        [TestMethod]
        public void TestGameOver() //To test whether game over when all conditions are met.
        {
            // Arrange
            int gridSize = 8;
            int minesCount = 20;
            Minefield minefield = new Minefield(gridSize, minesCount);
            bool[,] mines = minefield.getMines();

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (mines[i, j])
                        minefield.revealSquare(i, j); //Reveal square with mine. Simulating lose condition.
                }
            }

            // Act
            bool gameOver = minefield.gameOver; //Game over flag should be true if all square with mine is revealed.

            // Assert
            Assert.IsTrue(gameOver, "The game should be over when a mine is revealed.");
        }
    }
}