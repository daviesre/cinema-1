using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cinema
{
  public class UserTest : IDisposable
  {
    public UserTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cinema_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = User.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      User.DeleteAll();
    }

    [Fact]
    public void Test2_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      User firstUser = new User("Sara");
      User secondUser = new User("Sara");

      //Assert
      Assert.Equal(firstUser, secondUser);
    }

    [Fact]
    public void Test3_Save_SavesToDatabase()
    {
      //Arrange
      User testUser = new User("Sara");

      //Act
      testUser.Save();
      List<User> result = User.GetAll();
      List<User> testList = new List<User>{testUser};

      //Assert
      Assert.Equal(testList, result);
    }


  }
}
