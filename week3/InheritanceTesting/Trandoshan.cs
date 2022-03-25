namespace InheritanceTesting
{
    public class Trandoshan : Aliens
    {
        //fields

        //constructors
        public Trandoshan(string name)
        {
            this.homeWorld="Trandosha";
            this.spaceLanguage="Dosh";
            this.classification="Reptile";
            this.lifespan=60;
            this.name=name;
        }

        //methods
        public override void Action()
        {
            Console.WriteLine(this.name+" *Regrows a limb after losing a fight with a Wookiee*");
        }
    }
}