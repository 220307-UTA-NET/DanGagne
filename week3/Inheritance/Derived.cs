namespace Inheritance
{
    //Derived extends(inherits from) Base class
    public class Derived : Base
    {
        //Fields

        public string derivedString;

        //Constructor

        public Derived ()
        {
            this.derivedString="Derived";
        }
        //Methods

        //Method Overriding lets us invoke a method from a base/super class in the sub/derived class
        public override void Speak() //required to extend or modify the virtual method
        {
            Console.WriteLine("Hello, I am a Derived type object.");
        }
        
        public void Speak(string s)
        {
            Console.WriteLine("The Speak method was passed: " +s);
        }
    }
}