namespace FileInteraction
{     
     
     class Person
    {
        //fields - variables that are part of each instance of a class object
        public string name;
        public double height;
        public int age;


        //Constructor - a method used to initialize instances of the class
        //default values can be assigned in class
        public Person(string name="Fred", double height=60.2, int age=25)
        {
            this.name = name;
            this.height = height;
            this.age = age;

        }

        public int GrowUp()
        {
            this.age++;
            return this.age;
        }

    }
}