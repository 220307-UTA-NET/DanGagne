using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Logic
{
    internal class Course
    {
        //FIELDS
        string CourseID;
        string CourseName;
        int TeacherID;
        DateTime StartDate;
        DateTime EndDate;
        //List<students>??

        //Constructor
        public Course() { }
        public Course(string CourseID, string CourseName, int TeacherID, DateTime StartDate, DateTime EndDate)
        {
            this.CourseID = CourseID;   
            this.CourseName = CourseName;
            this.TeacherID = TeacherID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }

        //Methods
        public string GetCourse()
            { return this.CourseID; }
        public string GetCourseName()
            { return this.CourseName; }
        public int GetTeacherID()
            { return this.TeacherID; }
        public DateTime GetStartDate()
            { return this.StartDate; }
        public DateTime GetEndDate()
            { return this.EndDate; }

        public void SetTeacherID(int TeacherID)
        { this.TeacherID = TeacherID; }

        public void SetStartDate(DateTime StartDate)
            { this.StartDate = StartDate; }

        public void SetEndDate(DateTime EndDate)
            { this.EndDate = EndDate; }
    }
}
