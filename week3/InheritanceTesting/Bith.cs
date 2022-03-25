namespace InheritanceTesting
{
    public class Bith : Aliens
    {
        //fields

        //constructors
        public Bith(string name)
        {
            this.homeWorld="Bith";
            this.spaceLanguage="Bith"; 
            this.classification="Craniopod";
            this.lifespan=85;
            this.name=name;
        }

        //methods
        public override void Action()
        {
            Console.WriteLine(this.name+" *Plays the space clarinet*");
        }
    }
}