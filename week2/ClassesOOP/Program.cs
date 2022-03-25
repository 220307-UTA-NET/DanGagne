using System;
using System.Collections.Generic;
using System.IO;

namespace ClassesOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Square squareOne = new Square(5);
            squareOne.GiveValues();
           // Console.WriteLine(squareOne.get.area);

           try
           {
               Triangle triangleOne = new Triangle(8, 5, 6);
               Console.WriteLine("Perimeter of the triangle = " +triangleOne.Perimeter());
           }
           catch( Exception e)
           {
               Console.WriteLine("Not a valid triangle");
           }
           Console.WriteLine("Program continues after try catch");
           
        }
    }
}