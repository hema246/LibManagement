using LibraryManagement;
using Moq;
using FluentAssertions;
namespace LibraryManagement.Tests
{
    public class StudentDetailTests
    {
        [Fact]
        public void AddStudents_WhenCalled_ReturnValues()
        {   
            //arrange
            var add = new Mock<IStudentDetails>();
            add.Setup(x => x.AddStudentDetails()).Returns(1);
            //act
            var res=add.Object.AddStudentDetails();
            //flent assert
            res.Should().Be(1);
        }
       
        [Fact]
        public void DeleteStudents_WhenCalled_ReturnValus()
        {   
            //arrange
            var delete = new Mock<IStudentDetails>();
            delete.Setup(x => x.DeleteStudentDetails()).Returns(1);
            //act
            var result=delete.Object.DeleteStudentDetails();
            //fluent assert
            result.Should().Be(1);
        }
    }
}