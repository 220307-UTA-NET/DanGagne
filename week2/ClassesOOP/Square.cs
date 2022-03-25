using System;
using System.Collections.Generic;
using System.IO;


namespace ClassesOOP
{
    class Square
    {
        int numberOfSides = 4;
        double perimeter;
        double sideLength;
        double area;
        

       
        

        public Square(double sideLength)
        {
            this.numberOfSides=numberOfSides;
            this.sideLength=sideLength;
            setCalcArea(this.sideLength);
            setCalcPerimeter(this.numberOfSides, this.sideLength);
            
        }

        void setCalcPerimeter (int Side, double Length)
        {
            this.perimeter = (numberOfSides *Length);
        }

        void setCalcArea(double length)
        {
            this.area = length*length;
        }

        //Getter -returns values of private field
        public void GiveValues()
        {
            Console.WriteLine("Side: "+this.sideLength+ "\nArea: "+this.area+ "\nPerimeter: "+this.perimeter);
        }

    }
}