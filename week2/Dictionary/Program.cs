// Dictionary Testing

//Key value pairs

Dictionary<string, int> newDictionary = new Dictionary<string, int>(){{"one",1},{"seven",7}};
//newDictionary.Add("ONE",1);
//newDictionary.Add("one",1);
newDictionary.Add("two",2);
newDictionary.Add("three",3);
newDictionary.Add("four",4);
newDictionary.Add("five",5);
newDictionary.Add("six",6);

//Get Keys and Values
foreach( KeyValuePair<string, int> kvp in newDictionary )
{
    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
}

//Get Keys
Dictionary<string, int>.KeyCollection keyColl = newDictionary.Keys;
foreach( string s in keyColl )
{
    Console.WriteLine("Key = {0}", s);
}

Console.WriteLine(newDictionary["two"]);  //prints "0"
