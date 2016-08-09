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

    [Fact]
    public void Test_Delete_DeletesMoviesFromDatabase()
    {
      //Arrange
      string title1 = "Das Shoe";
      string rating1 = "R";
      Movie testMovie1 = new Movie(title1, rating1);
      testMovie1.Save();

      string title2 = "La Vie En Grosse";
      string rating2 = "PG-13";
      Movie testMovie2 = new Movie(title2, rating2);
      testMovie2.Save();
      List<Movie> testMovie = new List<Movie>{};

      //Act
      testMovie1.Delete();
      testMovie2.Delete();
      List<Movie> resultMovie = Movie.GetAll();

      //Assert
      Assert.Equal(testMovie, resultMovie);
    }

    [Fact]
    public void Test_Update_UpdatesMovieInDatabase()
    {
      //Arrange
      string title = "The Godmother";
      string rating = "R";
      Movie testMovie = new Movie(title, rating);
      testMovie.Save();
      string newTitle = "The Grandson";
      string newRating = "PG";

      //Act
      testMovie.Update(newTitle, newRating);
      string result1 = testMovie.GetTitle();
      string result2 = testMovie.GetRating();

      //Assert
      Assert.Equal(newTitle, result1);
      Assert.Equal(newRating, result2);
    }

    [Fact]
    public void Test_AddTheater_AddsTheaterToMovie()
    {
      //Arrange
      DateTime newDate = new DateTime(2016, 08, 02);
      Movie testMovie = new Movie("Miserable", "R");
      testMovie.Save();
      Theater theaterLocation1 = new Theater("Cinemaplex", newDate);
      theaterLocation1.Save();
      Theater theaterLocation2 = new Theater("Cinemart", newDate);
      theaterLocation2.Save();
      //Act
      List<Theater> resultList = new List<Theater>{};
      resultList.Add(theaterLocation1);
      List<Theater> testList = new List<Theater>{theaterLocation1};
      //Assert
      Assert.Equal(testList, resultList);
    }

    public void Dispose()
    {
      Movie.DeleteAll();
      Theater.DeleteAll();
    }

  }
}
