namespace Battleship.Logic
{
    public class GenerateGame
    {
        //fields

        //constructor
        public GenerateGame() { }
        //methods
        public void StartGame()
        {
            string[,] hiddenboard = FillGameBoard();
            string[,] gameboard = new string[10, 10];
            gameboard = GetBlankBoard(gameboard, "----");
            int hit = 0;
            while (hit != 17)
            {
                Console.Clear();
                ShowHiddenBoard(gameboard);
                //ShowHiddenBoard(board);
                Console.WriteLine("\n\nInput your guess!");
                string guess = Console.ReadLine().ToUpper();
                int[] guessIndex = ConvertInput(guess);
                gameboard=UpdateGameBoard(gameboard, hiddenboard, guessIndex);
                hit = CheckFor17HITS(gameboard);
            }
            Console.WriteLine("You Won!\nGame Exiting");
          
        }  
        public string [,] FillGameBoard()
        {
            string[,] board = new string[10, 10];
            int hitTest = 0;
            while (hitTest < 17)
            {
                board = GetBlankBoard(board, "MISS");
                board = AddShip(board, 5);
                board = AddShip(board, 4);
                board = AddShip(board, 3);
                board = AddShip(board, 3);
                board = AddShip(board, 2);
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
        public string[,] AddShip(string[,] board, int length)
        {
            while (true)
            {
                int[] start = GetStart();
                if (start[0] + (length - 1) < 10)
                {
                    for (int i = 0; i < length; i++)
                    {
                        board.SetValue("HIT!", start[0]+i, start[1]);
                    }
                    break;
                }
                else if (start[1] + (length - 1) < 10)
                {
                    for (int i = 0; i < length; i++)
                    {
                        board.SetValue("HIT!", start[0], start[1]+i);
                    }
                    break;
                }
                else if (start[0] - (length - 1) < 10)
                {
                    for (int i = 0; i < length; i++)
                    {
                        board.SetValue("HIT!", start[0]-i, start[1]);
                    }
                    break;
                }            
                else if (start[1] - (length - 1) < 10)
                {
                    for (int i = 0; i < length; i++)
                    {
                        board.SetValue("HIT!", start[0], start[1]-i);
                    }
                    break;
                }                               
            }   
            return board;
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
            Console.Write("\t|  A  |  B  |  C  |  D  |  E  |  F  |  G  |  H  |  I  |  J  |");
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
                Console.Write($" {gameboard[i / row, i % col]} ");
                c++;

            }
        }
        public int[] ConvertInput(string guess)
        {
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
            int number = Int32.Parse(guess1)-1;
            int letter = (int)guess2-65;
            int[] guessIndex = new int[] {number, letter};
            return guessIndex;

        }
        public string[,] UpdateGameBoard(string[,] gameboard, string[,] board, int[]guessIndex)
        {
            string update = board[guessIndex[0], guessIndex[1]];
            gameboard[guessIndex[0], guessIndex[1]] = update;
            return gameboard;
        }
    
    }
}