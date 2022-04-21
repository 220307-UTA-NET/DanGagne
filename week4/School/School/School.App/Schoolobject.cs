using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using School.Database;
using School.Logic;
[assembly: InternalsVisibleTo("School.Test")]


namespace School.App
{
    internal class Schoolobject
    {
        //Fields
        IRepository repo;

        //Constructor
        public Schoolobject(IRepository repo)
        {
            this.repo = repo;
        }

        //Methods
        public Student GetStudent(int ID)
        {
            return new Student(ID, this.repo.GetStudentName(ID));
        }
    }
}
