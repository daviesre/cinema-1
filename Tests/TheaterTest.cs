using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cinema
{
  public class TheaterTest : IDisposable
  {
    public TheaterTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cinema_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Theater.DeleteAll();
      Movie.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = Theater.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfTheaterIsSame()
    {
      DateTime fakeTime=new DateTime(2016,08,02);
      Theater firstTheater = new Theater("Regal", fakeTime);
      Theater secondTheater = new Theater("Regal",fakeTime);

      Assert.Equal(firstTheater, secondTheater);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      DateTime fakeTime=new DateTime(2016,08,02);
      Theater testTheater = new Theater("Regal", fakeTime);
      testTheater.Save();

      List<Theater> result = Theater.GetAll();
      List<Theater> testList = new List<Theater>{testTheater};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToTheater()
    {
      DateTime fakeTime=new DateTime(2016,08,02);
      Theater testTheater = new Theater("Regal", fakeTime);
      testTheater.Save();

      Theater savedTheater = Theater.GetAll()[0];
      int result = savedTheater.GetId();
      int testId = testTheater.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsTheaterInDatabase()
    {
      DateTime fakeTime=new DateTime(2016,08,02);
      Theater testTheater = new Theater("Regal", fakeTime);
      testTheater.Save();

      Theater foundTheater = Theater.Find(testTheater.GetId());

      Assert.Equal(testTheater, foundTheater);
    }

    [Fact]
    public void T6_Update_UpdatesTheaterInDatabase()
    {
      //Arrange
      string location = "Regal";
      DateTime fakeTime = new DateTime(2016,08,02);
      Theater testTheater = new Theater(location, fakeTime);
      testTheater.Save();
      string newLocation = "AMC";
      DateTime fakeTime2 = new DateTime(2016,09,02);


      //Act
      testTheater.Update(newLocation, fakeTime2);

      string result1 = testTheater.GetLocation();
      DateTime result2 = testTheater.GetDateTime();

      //Assert
      Assert.Equal(newLocation, result1);
      Assert.Equal(fakeTime2, result2);
    }

    [Fact]
    public void T7_Delete_DeletesTheatersFromDatabase()
    {
      //Arrange
      string location1 = "Regal";
      DateTime fakeTime = new DateTime(2016,08,02);
      Theater testTheater1 = new Theater(location1, fakeTime);
      testTheater1.Save();

      string location2 = "AMC";
      DateTime fakeTime2 = new DateTime(2016,09,02);
      Theater testTheater2 = new Theater(location2, fakeTime2);
      testTheater2.Save();
      List<Theater> testTheater = new List<Theater>{};

      //Act
      testTheater1.Delete();
      testTheater2.Delete();
      List<Theater> resultTheater = Theater.GetAll();

      //Assert
      Assert.Equal(testTheater, resultTheater);
    }

    [Fact]
    public void T8_AddMovie_AddsMovieToTheater()
    {
      Movie testMovie = new Movie("ET", "PG");
      testMovie.Save();
      DateTime fakeTime = new DateTime(2016,08,02);
      Theater testTheater = new Theater("Regal", fakeTime);
      testTheater.Save();

      testTheater.AddMovies(testMovie);
      List<Movie> result = testTheater.GetMovies();
      List<Movie> testList = new List<Movie> {testMovie};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T9_GetMovies_ReturnsAllTheaterMovie()
    {
      DateTime fakeTime = new DateTime(2016,08,02);
      Theater testTheater = new Theater("AMC", fakeTime);
      testTheater.Save();

      Movie testMovie1 = new Movie("ET", "PG");
      testMovie1.Save();

      Movie testMovie2 = new Movie("XXX", "R");
      testMovie2.Save();

      testTheater.AddMovies(testMovie1);
      List<Movie> result = testTheater.GetMovies();
      List<Movie> testList= new List<Movie>{testMovie1};

      Assert.Equal(testList,result);
    }

  }
}
