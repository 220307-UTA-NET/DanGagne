namespace Inheritance
{
    public class Base
    {
        //Fields
        public string baseString;

        //Constructor
        public Base()
        {
            this.baseString="Base";
        }

        //Methods
        public virtual void Speak() //allows method to be overriden by child class
        {
            Console.WriteLine("Hello, I am a Base type object.");
        }
    }
}