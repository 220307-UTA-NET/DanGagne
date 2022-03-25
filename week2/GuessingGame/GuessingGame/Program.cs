
namespace GuessingGame //filing cabinet where to store more information for organization
{
	class Program
	{
		static void Main(string[] args)
		{
			//var rand = new Random();
			Console.WriteLine("Welcome to the Guessing Game!");
			
			while (true)
			{
				Console.WriteLine("Enter an option from the menu.");
				Console.WriteLine("[1] - Guessing Game");
				Console.WriteLine("[2} - Math Challenge");
				Console.WriteLine("[0] - Exit");

				int? menu = int.Parse(Console.ReadLine());
					
				switch(menu)
				{
					case 0:
						Console.Clear();
						Console.WriteLine("Goodbye!");
						return;
					
					case 1:
						GuessingGame();
						break;
					case 2:
						AdditionChallenge();
						break;
				}
			}
		}

		static void GuessingGame() //GuessingGame function
		{	
			var rand = new Random();
			Console.Clear();
			Console.WriteLine("Lets Play!");
			int secret = rand.Next(21); //limit random number and save to int secret

						
			while (true)
			{
				Console.WriteLine("Guess a number between 0 and 20: "); // Starting question
				int? guess;

				try
				{
					guess = int.Parse(Console.ReadLine());
				}
				catch (System.Exception)
				{
					
					Console.WriteLine("Bad input detected. Returning to menu");
					break;
				}

				//int guess = int.Parse(Console.ReadLine()); //Take input
				
				if (guess < secret)
				{
					Console.Clear();
					Console.WriteLine("Try again.");
					Console.WriteLine("The number is larger than: " +guess);
					//Console.WriteLine("Guess again.");
					//guess = int.Parse(Console.ReadLine());
				}

				else if (guess > secret)
				{
					Console.Clear();
					Console.WriteLine("Try again.");
					Console.WriteLine("The number is smaller than: "+guess);
					//Console.WriteLine("Guess again.");
					//guess = int.Parse(Console.ReadLine());
				}

				else 
				{
					Console.Clear();
					Console.WriteLine("You got it!");
					Console.WriteLine("The number was: "+guess);
					Console.WriteLine("Press any key to continue");
					Console.ReadLine();
					Console.Clear();
					break;
				}
			}
		}

		static void AdditionChallenge() //AdditionChallenge function
		{	
			var rand = new Random();
			//var rand1 = new Random(); // Generating random values for problem
			int num1 = rand.Next(101);
			int num2 = rand.Next(101);

			int solution = num1+num2; //finding correct answer
			Console.Clear();
			Console.WriteLine("Welcome to the addition challenge!");

			while (true)
			{
				Console.WriteLine("Your question is: ");
				Console.WriteLine(num1+" + "+num2+ " = ???");
				Console.WriteLine("Please enter the solution.");
				string? userGuess = Console.ReadLine();

				//checking if input can be parsed to an input
				int intUserGuess;
				bool success = int.TryParse(userGuess, out intUserGuess);
				if (success)
				{
					intUserGuess = int.Parse(userGuess);
				}
				else
				{
					Console.WriteLine("Invalid input. Exiting Addition Challenge");
					break;
				}

			

			
				if (intUserGuess == solution)
				{
					Console.WriteLine("You got it!");

					Console.WriteLine("Press any key to continue");
					Console.ReadLine();
					Console.Clear();
					break;
				}
				else 
				{
					Console.WriteLine("Try again. You were off by: " + Math.Abs( solution - intUserGuess));
					Console.WriteLine("Press any key to continue");
					Console.ReadLine();
					Console.Clear();
				}
			}
		}
	}
}		