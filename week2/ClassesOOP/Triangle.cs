namespace ClassesOOP
{
    class Triangle
    {
        //Fields
        int numberOfSides;
        int sideA;
        int sideB;
        int sideC;
        int numberOfPoints;
        double angleX;
        double angleY;
        double angleZ;

        // any side must not be greater than or equal to the sum of the other two sides
        // c =+> a+b
        

        //Constructor
        public Triangle()
        {
            this.numberOfSides=3;
            this.numberOfPoints=3;
        }

      
        public Triangle(int A)
        {
            //Defined Equilateral Triangle
            this.numberOfSides=3;
            this.numberOfPoints=3;
            this.sideA=A;
            this.sideB=A;
            this.sideC=A;
        }

        public Triangle(int A, int B)
        {
            //Defined Isosceles Triangle
            this.numberOfSides=3;
            this.numberOfPoints=3;
            this.sideA=A;
            this.sideB=B;
            this.sideC=A;
        }
        
        public Triangle(int A, int B, int C)
        {
            this.numberOfSides=3;
            this.numberOfPoints=3;
            this.sideA=A;
            this.sideB=B;
            this.sideC=C;

            //another validation Trygg's method
            int[] sides = {this.sideA, this.sideB, this.sideC};
            Array.Sort(sides);
            if(sides[2] !> (sides[0]+sides[1]))
            {
                throw new Exception("Trygg's way");
            }


        }

        public Triangle(double A, double B, double C)
        {
            this.numberOfSides=3;
            this.numberOfPoints=3;
            this.sideA=(int)A; //explicit casting, taking the floor (rounding down)
            this.sideB=Convert.ToInt32(B); //explicit conversion, taking the ceiling (rounding up)
            this.sideC=(int)C;


            //check if triangle
            int hypotenuse;
            if ((this.sideA >= this.sideB) && (this.sideA >= this.sideC))
            {
                if (this.sideA > (this.sideB + this.sideC))
                {
                    throw new Exception("Values will not make triangle");
                }
            }
            else if ((this.sideB >= this.sideA) && (this.sideB >= this.sideC))
            {
                if (this.sideB > (this.sideA + this.sideC))
                {
                    throw new Exception("Values will not make triangle");
                }
            }
            if ((this.sideC >= this.sideB) && (this.sideC >= this.sideA))
            {
                if (this.sideC > (this.sideB + this.sideA))
                {
                    throw new Exception("Values will not make triangle");
                }
            }
            else
            {
                throw new Exception("Values will not make triangle");
                //not a triangle
            }
        }

        //Overloading -applied to a method or constructor
        //Creates a new definition for a method. that is differentiated based on the parameters

        //Methods
        public int Perimeter()
        {
            int perimeter = sideA+sideB+sideC;
            return perimeter;
        }
    }
}