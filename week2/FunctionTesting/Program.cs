namespace FunctionTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program starting: ");

            NewFunction();

            Console.WriteLine("Currently running in Main()");

            PrintValue(6);

            string returned;
            returned = GetString();
            Console.WriteLine(returned);

            FindPrime(); //Nest loops   
        }


        


        static string GetString() //uses string to send a string back to main function, does not accept anything
        {
            string x = "hello there!";
            return x;
        }
        static void NewFunction() //void is not returning anything to main
        {
            Console.WriteLine("Currently running NewFunction()");
        }

        static void PrintValue(int num) // int num is what is being accepted by PrintValue function, does not return anything
        {
            Console.WriteLine("PrintValue was passed: "+num);
        }

        static void FindPrime()
        {
            int i, j;
   
            for(i = 2; i<100; i++) 
            {
                for(j = 2; j <= (i/j); j++)
                {
                    if( (i%j) ==0)//( !(i%j) ) 
                    {
                        break; // if factor found, not prime
                    }
                }
                if (j > (i/j))
                {
                    Console.WriteLine(i+" Is prime");
                }
                    
            }
        }
    }
}
