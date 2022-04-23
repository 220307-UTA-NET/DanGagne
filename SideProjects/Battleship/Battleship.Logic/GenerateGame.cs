using Battleship.Classes;

namespace Battleship.Logic
{
    public class GenerateGame
    {
        //fields
        Ship ship5 = new Ship("Carrier", 5);
        Ship ship4 = new Ship("Battleship", 4);
        Ship ship3 = new Ship("Destroyer", 3);
        Ship ship2 = new Ship("Submarine", 3);
        Ship ship1 = new Ship("Patrol Boat", 2);
        List<Ship> fleet = new List<Ship>();

        //constructor
        public GenerateGame() { }

        //methods
        /// <summary>
        /// Generates filled gameboard and blank one to show in console
        /// Loops until all ships have been sunk(17 HIT!s)
        /// Updates game to show choices as HIT! or MISS
        /// </summary>
        public void StartGame()
        {
            bool game = true;
            while (game)
            { 

                string[,] hiddenboard = FillGameBoard();
                string[,] gameboard = new string[10, 10];
                FillFleet();
                gameboard = GetBlankBoard(gameboard, "----");
                int hit = 0;
                while (hit != 17)
                {                       
                    Console.Clear();                 
                    ShowHiddenBoard(gameboard);
                    hit = CheckFor17HITS(gameboard);
                    if(hit == 17)
                    { break;}
                    int[] guessIndex = ConvertInput();
                    foreach(Ship ship in fleet)
                    { UpdateShips(guessIndex, ship); }
                    gameboard = UpdateGameBoard(gameboard, hiddenboard, guessIndex);                   
                }
                Console.WriteLine("\nYou Won!\nPlay Again? [Y/N]");
                game=PlayAgain();
            }
          
        }  
        /// <summary>
        /// Takes the gameboard filled with MISS
        /// Adds in HIT! for length of each ship
        /// Checks that no ships intersect (need 17 HIT!s)
        /// </summary>
        /// <returns>the filled gameboard with HIT! or MISS</returns>
        public string [,] FillGameBoard()
        {
            string[,] board = new string[10, 10];
            int hitTest = 0;
            while (hitTest < 17)
            {
                board = GetBlankBoard(board, "MISS");
                foreach (Ship ship in fleet)
                { ship.ClearIndicies(); }
                board = AddShip(board, ship5);
                board = AddShip(board, ship4);
                board = AddShip(board, ship3);
                board = AddShip(board, ship2);
                board = AddShip(board, ship1);
                hitTest = CheckFor17HITS(board);
            }
            return board;
        }
        /// <summary>
        ///Fills the 2d arrays that represent the two boards
        /// </summary>
        /// <param name="board">2d array that represents the board</param>
        /// <param name="value">the string being put in each index</param>
        /// <returns></returns>
        public string[,] GetBlankBoard(string[,] board, string value)
        {                     
            int row = board.GetLength(0);
            int col = board.GetLength(1);

            for (int i = 0; i < row * col; i++)
            {
                board[i / row, i % col] = value;
            }
            return board;
        }
        /// <summary>
        /// Gets a random index within the board to start building a ship from
        /// </summary>
        /// <returns>an index that exists on the board</returns>
        public int[] GetStart()
        {
            Random rand = new Random();
            int col = (int)rand.NextInt64(0, 9);
            int row = (int)rand.NextInt64(0, 9);
            int[] start = new int[] {col,row};
            return start;
        }
        /// <summary>
        /// Randomly chooses to place a ship vertical or horizontally
        /// Ensures ships are placed entirely on the board
        /// Adds the indexes to a list on the ship class to know which ships are hit
        /// </summary>
        /// <param name="board">board being updated with hidden HIT!s</param>
        /// <param name="ship">the ship being added to the board</param>
        /// <returns>the board filled with a new ship</returns>
        public string[,] AddShip(string[,] board, Ship ship)
        {
            Random random = new Random();
            bool horizontalOrVertical = random.Next(2) == 0;
            int[] start = GetStart();
            switch (horizontalOrVertical)
            {
                case true:
                    {
                        int leftOrRight = -1;
                        while (true)
                        {
                            if ((start[0] + leftOrRight*(ship.GetLength-1) < 10) && (start[0] + leftOrRight * (ship.GetLength - 1) > -1))
                            {
                                //Console.WriteLine($"{start[0]} - {start[0] + leftOrRight * (length - 1)}, {start[1]}");
                                for (int i = 0; i < ship.GetLength; i++)
                                {
                                    board.SetValue("HIT!", start[0] + i*leftOrRight, start[1]);
                                    ship.SetLocationIndicies(start[0] + i * leftOrRight, start[1]);
                                }
                                break;
                            }
                            else { leftOrRight = 1; }
                        }
                        break;
                    }

                case false:
                    {
                        int upOrDown = -1;
                        while(true)
                        {
                            if ((start[1] + upOrDown * (ship.GetLength - 1) < 10) && (start[1]+upOrDown*(ship.GetLength-1) > -1))                              
                            {
                                //Console.WriteLine($"{start[0]},  {start[1]} - {start[1] + upOrDown * (length - 1)}");
                                for (int i = 0; i < ship.GetLength; i++)
                                {
                                    board.SetValue("HIT!", start[0], start[1] + i*upOrDown);
                                    ship.SetLocationIndicies(start[0], start[1] + i * upOrDown);
                                }
                                break;
                            }
                            else { upOrDown = 1; }
                        }
                        break;
                    }
            }  
            return board;
        }
        /// <summary>
        /// Adds the ships to a List so they can easily be iterated through
        /// </summary>
        public void FillFleet()
        {
            fleet.Add(ship5);
            fleet.Add(ship4);
            fleet.Add(ship3);
            fleet.Add(ship2);
            fleet.Add(ship1);
        }
        /// <summary>
        /// Checks that a board has 17 HIT!s (total length of all ships
        /// This ensures no ships overlap and also decides the win condition
        /// </summary>
        /// <param name="board">the board being checked for 17 HIT!s</param>
        /// <returns>the number of HIT!s</returns>
        public int CheckFor17HITS(string[,] board)
        {
            int row = 10;
            int col = 10;
            int c = 0;
            for (int i = 0; i < 100; i++)
            {
                if( board[i / row, i % col] == "HIT!")
                {
                    c++;
                }

            }
            return c;
        }
        /// <summary>
        /// Prints out the game board for the console
        /// Adds the numbers and letters to reference in call
        /// </summary>
        /// <param name="gameboard">the board being printed to the console</param>
        public void ShowHiddenBoard(string [,] gameboard)
        {
            Console.WriteLine("\t\t\t***** BATTLESHIP *****");
            foreach (Ship ship in fleet)
            { ship.PrintIndicies(); }
            Console.Write("\n\t|  A  |  B  |  C  |  D  |  E  |  F  |  G  |  H  |  I  |  J  |");
            int row = 10;
            int col = 10;
            for (int i = 0, b = 1, c = 0; i < 100; i++)
            {
                if (c == 10 || i == 0)
                {
                    Console.WriteLine("\n");
                    Console.Write($"     {b}\t|");
                    b++;
                    c = 0;
                }
                string print = gameboard[i / row, i % col];
                if (print == "HIT!")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" {print} ");
                    Console.ForegroundColor= ConsoleColor.White;
                }
                else if(print == "MISS")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($" {print} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                { Console.Write($" {print} "); }
                c++;

            }
        }
        /// <summary>
        /// checks the users input and converts from a letter and a number to the index on the game board
        /// </summary>
        /// <returns>a valid index of the gameboard</returns>
        public int[] ConvertInput()
        {
            Console.Write("\n\nInput your guess: ");
            int[] guessIndex = new int[2];
            while (true)
            {
                string guess = Console.ReadLine().ToUpper();
                try 
                { 
                    if (guess.Length != 2 && guess.Length != 3)
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    InputRejection();
                    goto Error;
                }
                string guess1 = "";
                char guess2 = '\0';

                foreach (char c in guess)
                {
                    if (char.IsDigit(c))
                    {
                        guess1 += c;
                    }
                    else if (char.IsLetter(c))
                    {
                        guess2 = c;
                    }
                }
                try
                {
                    int number = Int32.Parse(guess1) - 1;
                    int letter = (int)guess2 - 65;
                    guessIndex = new int[] { number, letter };
                    if (letter < 10 && letter > -1 && number < 10 && number > -1 && letter != -65)
                    {
                        break;
                    }
                    else { throw new FormatException(); }
                }              
                catch (FormatException)
                {
                    InputRejection();
                }
            Error:
                Thread.Sleep(100);
            }
           
            return guessIndex;

        }
        /// <summary>
        /// Updates the game board after every guess
        /// Changes the selected index to show the HIT! or MISS from the hidden board
        /// </summary>
        /// <param name="gameboard">the board printed to the console</param>
        /// <param name="board">the board with the hidden values</param>
        /// <param name="guessIndex">index that the player guessed</param>
        /// <returns>the updated gameboard</returns>
        public string[,] UpdateGameBoard(string[,] gameboard, string[,] board, int[]guessIndex)
        {

            string update = board[guessIndex[0], guessIndex[1]];
            gameboard[guessIndex[0], guessIndex[1]] = update;
            return gameboard;
        }
        /// <summary>
        /// after win conditions are met this checks for a reset or to exit the application
        /// </summary>
        /// <returns>true on a replay, false on a quit</returns>
        public bool PlayAgain()
        {
            string playAgain = Console.ReadLine().ToUpper();
            if (playAgain=="Y" || playAgain=="YES")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Removes indicies from the ships if they are hit
        /// when the list is empty the ship will display as sunk
        /// </summary>
        /// <param name="guess">index guessed</param>
        /// <param name="ship">ship that it being checked for a hit</param>
        public void UpdateShips(int[] guess, Ship ship)
        {
            int hitIndex = ship.GetOneIndex(guess);  
            ship.ChangeLocationIndicies(hitIndex);          
        }
        /// <summary>
        /// formatting for error handling on bad input from player
        /// </summary>
        public static void InputRejection()
        {
            Console.WriteLine("\nThat's not a valid input! \nTry again.");
            Console.SetCursorPosition(0, 29);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 29);
            Console.Write("Input your guess: ", Console.WindowWidth);
        }
  
    }
}