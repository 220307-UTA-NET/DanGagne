using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Logic;

namespace School.Database
{
        //repository design pattern:implement basic CRUD operations
        //                          Create, Read, Update, Delete
        // operations that the rest of the app may need, but we can abstract the implementation details
        //advantage: makes the rest of our code more unit testable

        //often repos don't have immediately instead using a special save method
        //to wrap all the changes into one transaction

        //sometimes one repo per entity per type of thing we want to ttrack in the DB
        //transactions across multiple repositories requires the "unit of work" deisgn pattern
        //which manages multiple repositories and save the chnges of all of the at once.
    public interface IRepository
    {

        //an interface is a contract, that defines a set of conditions
        //it can contain methods, properrties, or events
        //but it does not fully define them, only provides the signature
        //                                  Access-Modifier, Return-Type, Method-Name (Parameter types/names)

        //In C# we can use multiple interfaces to simulate multiple inheritance

        IEnumerable<Teacher> GetAllTeachers();
        Teacher CreateNewTeacher(string Name);

        string GetStudentName(int ID);
    }
}
