using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cinema
{
  public class ShowingTest : IDisposable
  {
    public ShowingTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cinema_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Showing.DeleteAll();
      Movie.DeleteAll();
      Theater.DeleteAll();
    }

    [Fact]
    public void Test5_Find_FindsShowingInDatabase()
    {
      //Arrange
      Movie testMovie = new Movie("Red Swan", "R");
      testMovie.Save();
      Console.WriteLine(testMovie.GetId());
      DateTime fakeTime=new DateTime(2016,08,02);
      Theater testTheater = new Theater("Regal", fakeTime);
      testTheater.Save();
      testMovie.AddTheater(testTheater);
      Showing testShowing = new Showing(testMovie.GetId(), testTheater.GetId());
      // testShowing.Save();
      //Act
      Showing foundShowing = Showing.Find(testShowing.GetId());

      //Assert
      Assert.Equal(testShowing, foundShowing);

    }
  }
}
