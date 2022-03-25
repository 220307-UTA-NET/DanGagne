namespace InheritanceTesting
{
    public class Aliens
    {
        //fields
        protected string homeWorld;
        protected string spaceLanguage;
        protected string name;
        protected int lifespan;
        public int _lifespan { get => lifespan;}
        protected string classification;
        public string _classification {get => classification;}



        //constructors
        public Aliens()
        {
            this.homeWorld="Planet the alien is from.";
            this.spaceLanguage="Native language of the alien.";
            this.classification="Alien's classification";
            this.lifespan=75;
            this.name="An alien's name";
            
        }

        //methods
        public virtual void Action()
        {
            Console.WriteLine("Star Wars aliens all have unique abilities");
        }
        public virtual void Introduction()
        {
            Console.WriteLine(this.name+" is from "+this.homeWorld+" and speaks "+this.spaceLanguage);
        }
    }
}