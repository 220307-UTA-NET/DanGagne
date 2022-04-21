using School.Logic;
using Xunit;
using Moq;
using School.Database;
using School.App;

namespace School.Test
{
    public class StudentTest
    {
        [Fact]
        public void Student_CreateStudentObject_ValidStudent()
        {
            //ARRANGE
            Student test = new Student(01, "Kevin");
            

            //ACT
            string actual = test.GetName();

            //ASSERT
            string expected = "Kevin";
            Assert.Equal(expected, actual);


        }

        [Fact]
        public void Main_GetStudent_ValidStudent()
        {
            //the test that I'm writing should NOT involve calling the SQLRepository class
            // A unit test should NOT involve the database and above all it should not involve the PRODUCTION database

            //arrange
            Mock<IRepository> mockRepo = new ();
            mockRepo.Setup(x => x.GetStudentName(5)).Returns("Lawrence");

            var school = new App.Schoolobject(mockRepo.Object);

            //act
            Student test = school.GetStudent(5);
            string actual = test.GetName();

            //assert
            string expected = "Lawrence";
            Assert.Equal(expected, actual);
        }
    }
}