using System;
using School.Database;
using School.Logic;

namespace School.App
{
    class Program
    {
        static void Main()
        {
            //Console.WriteLine("Hello Again");
            //Student temp = new Student(123, "Jonathon");
            //Console.WriteLine(temp.Introduce());

            //Teacher temp2 = new Teacher(098, "Brian");
            //Console.WriteLine(temp2.Introduce());

            string connectionString = File.ReadAllText(@"\Revature\DanGagne\ConnectionString\SchoolStringDB.txt");
            IRepository repo = new SQLRepository(connectionString);

           
            //IEnumerable<Teacher> teachers = repo.GetAllTeachers();
            //foreach (Teacher teacher in teachers)
            //{
            //    Console.WriteLine(teacher.Introduce()); 
            //}

            //Console.WriteLine(repo.GetStudentName(2));
            //Teacher NewTeacher = repo.CreateNewTeacher("Jerome");
            //Console.WriteLine(NewTeacher.Introduce());
           
        }

       
    }
}
