namespace Battleship.Classes
{
    public class Ship
    {
        //fields
        internal int length { get; set; }
        internal string? name { get; set; }
        internal List<string> hitMiss { get; set; }
        internal List<int[]> locationIndicies { get; set; }

        //Constructor
        public Ship() { }
        public Ship(string name, int length)
        {
            this.name = name;
            this.length = length;
            locationIndicies = new List<int[]>();
        }

        //Methods
        public int GetLength { get { return length; } }
        public string GetName { get { return name; } }
        public int GetOneIndex(int[] index)
        {
            foreach(var i in locationIndicies)
            {
                if (i[0] == index[0] && i[1] == index[1])
                { 
                    return locationIndicies.IndexOf(i); 
                }
            }
            return -1;
        
        }
        public void SetLocationIndicies(int a, int b)
        {
            int[] index = new[]{ a, b };
            locationIndicies.Add(index);
        }
        public void ChangeLocationIndicies(int index)
        {
            try
            {
                locationIndicies.RemoveAt(index);
            }
            catch { }                
        }
        public void ClearIndicies()
        {
            locationIndicies.Clear();
        }
        public void PrintIndicies()
        {
            string sunk = "Floating";
            if (locationIndicies.Count == 0)
            { sunk ="Sunk!"; }
            Console.WriteLine($"{this.GetName}: {sunk}\t");

            //foreach (var ind in locationIndicies)
            //{
            //    char letter = (char)(ind[1] + 65);
            //    Console.WriteLine($"{letter}, {ind[0] + 1}");
            //}

        }
        public int GetListLength { get { return locationIndicies.Count; } }


    }
}