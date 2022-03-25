using System;

namespace Inheritance
{
    class Program
    {
        static void Main()
        {
            Base myBase = new Base();
            Console.WriteLine(myBase.baseString);
            myBase.Speak();
            Derived myDerived = new Derived();
            Console.WriteLine(myDerived.baseString);
            Console.WriteLine(myDerived.derivedString);
            myDerived.Speak();
            myDerived.Speak("new string");
        }
    }
}