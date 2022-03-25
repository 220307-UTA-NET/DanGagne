using System.IO;

namespace FileInteraction
{
    class Program

    {
        public static void Main(string[] args)
        {
            //create array of strings and filepath
            string[] text = {"Hi", "Hello there!", "How's it going?"};
            string path = @".\TestFiles.txt";


            //testing if file exists yet
            if (! File.Exists(path))
            {
                File.WriteAllLines(path,text);
            }
            else
            { 
                File.AppendAllLines(path, text);
            }

            string[] readText = File.ReadAllLines(path);
            foreach(string s in readText)
            {
                Console.WriteLine(s);
            }

            //In Bash//
            //echo "text" >> ./filname.txt
            //take this string "text", convert it to a stream of data
            // > or >> is a stream redirect, and we redirect to a target file

            Person Kevin = new Person("Kev", 65.4, 21);
            Console.WriteLine(Kevin.name+ " is " +Kevin.age);

            Console.WriteLine(Kevin.name+ " is now " +Kevin.GrowUp());
        }
    }
}
