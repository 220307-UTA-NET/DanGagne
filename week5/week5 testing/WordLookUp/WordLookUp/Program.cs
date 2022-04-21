using System;
using System.Text.Json;
using WordLookUp.DTOs;

namespace WordLookUp
{
    public class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();

            Console.WriteLine("Enter a word to look up its definitions.");
            string input = Console.ReadLine();
            string answer = await client.GetStringAsync($"https://api.dictionaryapi.dev/api/v2/entries/en/{input}");
            List<ToDo> todoList = JsonSerializer.Deserialize<List<ToDo>>(answer);

            for (int i = 0; i < todoList[0].meanings.Count(); i++)
            {
                foreach (ToDo toDo in todoList)
                {
                    Console.WriteLine(toDo.meanings[i].definitions[0].definition);
                }
            }
        }
    }
}