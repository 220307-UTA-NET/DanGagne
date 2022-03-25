class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter n");
        int n = int.Parse(Console.ReadLine());

        //Oneliner without loop
        Console.WriteLine((Math.Pow(2,n)-1)/(2-1));

        //with loop
        double totalSum=0;
        for (int i=0; i<n; i++)
        {
            totalSum+=Math.Pow(2,i);
        }
        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(totalSum);
    }
}
