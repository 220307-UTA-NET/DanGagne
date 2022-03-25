string [] arrayName = { "Kevin", "Dan", "Anu", "Kelly", "Trygg"};

string Nametwo = arrayName [2];

arrayName[1] = "Cam";

//Sorts array alphabetically on strings
Array.Sort(arrayName);
//Reverses array
Array.Reverse(arrayName);
//Enumerables, take each element in loop and set equal to string name
foreach(string name in arrayName)
{
    Console.WriteLine(name);
}

int[] myNumbers = {3,9,12,5,10,23,7,4};

//Sorts numerically on ints
Array.Sort(myNumbers);

foreach(int i in myNumbers)
{
    Console.WriteLine(i);
}


//Concat two arrays
static void ConcatArray()
{
    int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 }; 
    int[] numbersB = { 1, 3, 5, 7, 8 }; 

    var allNumbers = numbersA.Concat(numbersB); 

    Console.WriteLine("All numbers from both arrays:"); 
    foreach (var n in allNumbers) 
    { 
        Console.WriteLine(n); 
    } 
}
