using System;
public class Program
{
    static void Main()
    {
        int[,] matrix = new int[8, 8];
        int row = matrix.GetLength(0);
        int col = matrix.GetLength(0);

        //string s = Console.ReadLine();
        //string name = "";
        //foreach (char c in s)
        //{
        //    if (c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9' || c == '0')
        //    { }
        //    else
        //    {
        //        name = name + c;
        //    }

        //}
        //Console.WriteLine(name);

        while (true)
        {
            for (int i = 0; i < row * col; i++)
            {
                matrix[i / col, i % col] = i;
            }

            int start = Int32.Parse(Console.ReadLine());
            int end = Int32.Parse(Console.ReadLine());
            int[] startIndex = GetIndicies(start, matrix);
            int[] endIndex = GetIndicies(end, matrix);

            Console.WriteLine($"{startIndex[0]}, {startIndex[1]}");
            Console.WriteLine($"{endIndex[0]}, {endIndex[1]}");

            //find delta of indicies
            int delta1 = Math.Abs(startIndex[0] - endIndex[0]);
            int delta2 = Math.Abs(startIndex[1] - endIndex[1]);
            Console.WriteLine($"{delta1}, {delta2}");
            int deltaSum = delta1 + delta2;
            if (deltaSum == 2)
            {
                if ((startIndex[0] == 0 || startIndex[0] == 7) && (startIndex[1] == 0 || startIndex[1] == 7))
                { Console.WriteLine("4 Moves"); }
                else { Console.WriteLine("2 Moves"); }
            }
            else if (deltaSum == 3)
            {
                if (delta1 > 2 || delta2 > 2)
                { Console.WriteLine("3 Moves"); }
                else { Console.WriteLine("1 Moves"); }
            }
            else if (deltaSum == 4)
            {
                if (delta1 == delta2)
                { Console.WriteLine("4 Moves"); }
                else { Console.WriteLine("2 Moves"); }
            }
            else if (deltaSum == 6)
            {
                if (delta1 > 4 || delta2 > 4)
                { Console.WriteLine("4 Moves"); }
                else { Console.WriteLine("2 Moves"); }
            }
            else if (deltaSum == 9||deltaSum==7)
            {
                if (delta1 > 6 || delta2 > 6)
                { Console.WriteLine("5 Moves"); }
                else { Console.WriteLine("3 Moves"); }
            }
            else if (deltaSum==1 || deltaSum==5)
            { Console.WriteLine("3 moves!"); }         
            else if(deltaSum ==8 || deltaSum==10 || deltaSum==12)
            { Console.WriteLine("4 moves!"); }
            else if (deltaSum==11||deltaSum==13)
            { Console.WriteLine("5 moves"); }
            else if (deltaSum==14)
            { Console.WriteLine("6 moves"); }
         }
    }

    public void FindPrimeString()
    {
        int input = Int32.Parse(Console.ReadLine());
        string output = "2";
        for (int i = 3; output.Length < input+5; i++)
        {
            int j = i - 1;
            while (j != 1)
            {
                if (i % j != 0)
                {
                    j--;
                    if (j == 1)
                    { 
                        output = output + i;                           
                    }
                }
                else if (i % j == 0)
                { break; }
            }
        }
        
        Console.WriteLine(output);
        Console.WriteLine(output.Substring(input, 5));
    }

    public static int[] GetIndicies(int a, int[,] matrix)
    {
        int start_i = -1;
        int start_j = -1;
        for (int i = 0; i < 8 && start_i < 0; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (matrix[i, j] == a)
                {
                    start_i = i;
                    start_j = j;
                    break;
                }
            }
        }
        return new int[] { start_i, start_j };
    }
}

