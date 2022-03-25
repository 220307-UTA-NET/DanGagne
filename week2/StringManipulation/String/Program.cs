string newString;
int newInt;

Console.WriteLine("Please enter your message and press enter.");
newString=Console.ReadLine();

Console.WriteLine("Please enter a number LESS THAN the length of your string and press enter ");
newInt = int.Parse(Console.ReadLine());

char newChar;
Console.WriteLine("For which character should I search in your original message?");
newChar = char.Parse(Console.ReadLine());

Console.WriteLine("What is your first name?");
string firstName = Console.ReadLine();

Console.WriteLine("What is your last name?");
string lastName = Console.ReadLine();

Console.WriteLine(firstName+ " " +lastName);

Console.WriteLine(firstName[0].ToString()+lastName[0].ToString());

Console.WriteLine(firstName+" "+lastName.Substring(0,2));


//Console.WriteLine(firstName+ " "+ lastName[0]+lastName[1]);

Console.WriteLine(lastName.Length);

//Console.WriteLine(newString.IndexOf(newChar));
