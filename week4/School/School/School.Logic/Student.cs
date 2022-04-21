﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace School.Logic
{
    public class Student
    {
        //FIELDS
        int ID;
        string Name;
        //List<courses>

        //CONSTRUCTOR
        public Student() { }
        public Student(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        //Methods

        public int GetID()
            { return this.ID; }
        public string GetName()
            { return this.Name; }
        public void SetName(string Name)
            { this.Name = Name; }

        public string Introduce()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Hello, my name is {this.Name}, and I am Student {this.ID}");
            return sb.ToString();
        }
        

    
    }
}