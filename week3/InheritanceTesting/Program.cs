namespace InheritanceTesting
{
    public class Program
    {
        static void Main()
        {
            Bith bith1 = new Bith("Figrin");
            Wookiee wookiee1 = new Wookiee("Chewbacca");
            Trandoshan trandoshan1 = new Trandoshan("Bossk");
            wookiee1.Introduction();
            wookiee1.Action();
            trandoshan1.Introduction();
            trandoshan1.Action();
            bith1.Introduction();
            bith1.Action();
            Console.WriteLine(bith1._lifespan);
            Console.WriteLine(wookiee1._classification);
        }
    }
}