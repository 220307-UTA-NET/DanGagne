namespace Battleship.Classes
{
    public class Ship
    {
        //fields
        internal int length { get; set; }
        internal string? name { get; set; }
        internal List<int[]> locationIndicies { get; set; }

        //Constructor
        public Ship(string name, int length)
        {
            this.name = name;
            this.length = length;
            locationIndicies = new List<int[]>();
        }

        //Methods
        public int GetLength { get { return length; } }
        public string GetName { get { return name; } }
        /// <summary>
        /// checks if guess from player exists in the list saved with this ship
        /// </summary>
        /// <param name="index">the index that the user guessed</param>
        /// <returns>the index of the guess in the ship's list if it exists</returns>
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
        /// <summary>
        /// When building the game and a ship is successfully placed the index where it is placed is added to a list
        /// </summary>
        /// <param name="a">index[0]</param>
        /// <param name="b">index[1]</param>
        public void SetLocationIndicies(int a, int b)
        {
            int[] index = new[]{ a, b };
            locationIndicies.Add(index);
        }
        /// <summary>
        /// Tries to remove index from a ship's list of indexes.
        /// </summary>
        /// <param name="index">index that is trying to be removed from the ship's list</param>
        public void ChangeLocationIndicies(int index)
        {
            try
            {
                locationIndicies.RemoveAt(index);
            }
            catch { }                
        }
        /// <summary>
        /// clears the ship's saved indicies on a failure to generate a good game board or a player wanting to reset the board after a win
        /// </summary>
        public void ClearIndicies()
        {
            locationIndicies.Clear();
        }
        /// <summary>
        /// shows whether the ship is Floating or Sunk based on list of indicies
        /// will print out all indicies of placed ships for game testing too
        /// </summary>
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
        


    }
}