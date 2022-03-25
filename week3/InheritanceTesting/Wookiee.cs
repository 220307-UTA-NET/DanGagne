namespace InheritanceTesting
{
    public class Wookiee : Aliens
    {
        //fields

        //constructors
        public Wookiee(string name)
        {
            this.homeWorld="Kashyyyk";
            this.spaceLanguage="Shyriiwook";
            this.lifespan=400;
            this.classification="Mammal";
            this.name = name;
        }

        //methods
        public override void Action()
        {
            Console.WriteLine(this.name+" *Rips off a Trandoshan's arm*");
        }
    }
}