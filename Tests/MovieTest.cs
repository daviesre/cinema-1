using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Cinema
{
  public class MovieTest : IDisposable
  {
    public MovieTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cinema_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      //Arrange, Act
      int result = Movie.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameTitleAndRating()
    {
      //Arrange, Act
      Movie firstMovie = new Movie("Cardboard Moon", "PG-13");
      Movie secondMovie = new Movie("Cardboard Moon", "PG-13");
      //Assert
      Assert.Equal(firstMovie, secondMovie);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Movie testMovie = new Movie("Das Help", "PG");
      testMovie.Save();

      //Act
      List<Movie> result = Movie.GetAll();
      List<Movie> testList = new List<Movie>{testMovie};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetAll_GetsAllMoviesFromDatabase()
    {
      //Arrange
      string title1 = "Star Wars 2";
      string rating1 = "PG-13";
      Movie testMovie1 = new Movie(title1, rating1);
      testMovie1.Save();

      string title2 = "Star Wars: The Warsening";
      string rating2 = "R";
      Movie testMovie2 = new Movie(title2, rating2);
      testMovie2.Save();
      List<Movie> testMovies = new List<Movie> {testMovie1, testMovie2};

      //Act
      List<Movie> resultMovie = Movie.GetAll();

      //Assert
      Assert.Equal(testMovies.Count, resultMovie.Count);
    }

    [Fact]
    public void Test_Find_FindsMovieInDatabase()
    {
      //Arrange
      Movie testMovie = new Movie("Red Swan", "R");
      testMovie.Save();

      //Act
      Movie foundMovie = Movie.Find(testMovie.GetId());

      //Assert
      Assert.Equal(testMovie, foundMovie);
    }

    public void Dispose()
    {
      Movie.DeleteAll();
    }

  }
}
