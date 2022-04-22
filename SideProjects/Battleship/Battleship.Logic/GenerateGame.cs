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

        //methods
        public GenerateGame() { }  
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
                    foreach(Ship ship in fleet)
                    { ship.PrintIndicies();}
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
        public string [,] FillGameBoard()
        {
            string[,] board = new string[10, 10];
            int hitTest = 0;
            while (hitTest < 17)
            {
                board = GetBlankBoard(board, "MISS");
                ship5.ClearIndicies();
                board = AddShip(board, ship5);
                ship4.ClearIndicies();
                board = AddShip(board, ship4);
                ship3.ClearIndicies();
                board = AddShip(board, ship3);
                ship2.ClearIndicies();
                board = AddShip(board, ship2);
                ship1.ClearIndicies();
                board = AddShip(board, ship1);
                hitTest = CheckFor17HITS(board);
            }
            return board;
        }
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
        public int[] GetStart()
        {
            Random rand = new Random();
            int col = (int)rand.NextInt64(0, 9);
            int row = (int)rand.NextInt64(0, 9);
            int[] start = new int[] {col,row};
            return start;
        }
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
        public void FillFleet()
        {
            fleet.Add(ship5);
            fleet.Add(ship4);
            fleet.Add(ship3);
            fleet.Add(ship2);
            fleet.Add(ship1);
        }
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
        public void ShowHiddenBoard(string [,] gameboard)
        {
            Console.Write("\t***** BATTLESHIP *****\n\n\t|  A  |  B  |  C  |  D  |  E  |  F  |  G  |  H  |  I  |  J  |");
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
        public int[] ConvertInput()
        {
            Console.WriteLine("\n\nInput your guess!");
            int[] guessIndex = new int[2];
            while (true)
            {
                string guess = Console.ReadLine().ToUpper();
                if(guess.Length != 2 && guess.Length != 3)
                {
                    guess = "999";
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
                int number = Int32.Parse(guess1) - 1;
                int letter = (int)guess2 - 65;
                guessIndex = new int[] { number, letter };
                if (letter < 10 && letter > -1 && number < 10 && number > -1)
                {                    
                    break; 
                }
                Console.WriteLine("That's not a valid input!\nTry again.");
            }
            return guessIndex;

        }
        public string[,] UpdateGameBoard(string[,] gameboard, string[,] board, int[]guessIndex)
        {

            string update = board[guessIndex[0], guessIndex[1]];
            gameboard[guessIndex[0], guessIndex[1]] = update;
            return gameboard;
        }
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
        public void UpdateShips(int[] guess, Ship ship)
        {
            int hitIndex =ship.GetOneIndex(guess);  
            ship.ChangeLocationIndicies(hitIndex);
            //IsShipSunk(ship);
            
        }

    
    }
}