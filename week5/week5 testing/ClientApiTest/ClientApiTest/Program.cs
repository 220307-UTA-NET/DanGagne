using System;
using System.Text.Json;
using ClientApiTest.DTOs;
using System.Net.Http.Headers;
using System.Text;

namespace ClientApiTest
{
    public class Program
    {
        static async Task Main()
        {
            
            while (true)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7015/");
                var products = "";
                Console.WriteLine("Select 1 to delete a customer by ID.\nSelect 2 to view all customers.\nSelect 3 to add a new customer.");
                switch (Console.ReadLine())
                {
                    case "1":
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        Console.WriteLine("Enter a customer id");
                        int input = Int32.Parse(Console.ReadLine());
                        
                        HttpResponseMessage response = client.DeleteAsync($"customer/deleteOne/id?ID={input}").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            products = response.Content.ReadAsStringAsync().Result;
                            Console.WriteLine(products);
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        }
                        break;

                    case "2":
                        HttpResponseMessage response2 = client.GetAsync($"customer/all").Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            products = response2.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1})", (int)response2.StatusCode, response2.ReasonPhrase);
                        }


                        List<ToDo> todo = JsonSerializer.Deserialize<List<ToDo>>(products);

                        foreach (ToDo toDo in todo)
                        {
                            Console.WriteLine(toDo.Introduce());
                        }
                        break;
                    case "3":
                        Console.WriteLine("Enter your customer info please");
                        string fname = Console.ReadLine();
                        string lname = Console.ReadLine();
                        string address = Console.ReadLine();
                        string city = Console.ReadLine();
                        string state = Console.ReadLine();
                        string country = Console.ReadLine();
                        int storeID = Int32.Parse(Console.ReadLine());
                        ToDo toDo1 = new ToDo(fname, lname, address, city, state, country, storeID);

                        var content1 = JsonSerializer.Serialize(toDo1);
                        var content2 = new StringContent(content1, UnicodeEncoding.UTF8, "application/json");
                        
                        HttpResponseMessage response3 = client.PostAsync($"customer/add", content2).Result;
                        if (response3.IsSuccessStatusCode)
                        {
                            products = response3.Content.ReadAsStringAsync().Result;
                            Console.WriteLine(products);
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1})", (int)response3.StatusCode, response3.ReasonPhrase);
                        }

                        break;
                    case "0":
                        return;
                }
            
            }
        }
    }
}
