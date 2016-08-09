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

    [Fact]
    public void Test_GetMovies_GetAllTheatersInThisMovie()
    {
      //Arrange
      DateTime newDate = new DateTime(2016, 08, 02);
      Movie newMovie = new Movie("The Dimming", "PG-13");
      newMovie.Save();
      Theater theaterLocation1 = new Theater("Le Bijou", newDate);
      theaterLocation1.Save();
      Theater theaterLocation2 = new Theater("Les Bijoux", newDate);
      theaterLocation2.Save();

      newMovie.AddTheater(theaterLocation1);
      newMovie.AddTheater(theaterLocation2);
      List<Theater> testMovieTheaters = new List<Theater> {theaterLocation1, theaterLocation2};
      //Act
      List<Theater> resultMovieTheaters = newMovie.GetTheaters();
      //Assert
      Assert.Equal(testMovieTheaters, resultMovieTheaters);
    }

    [Fact]
    public void Test_SearchByTheater_SearchForMovieByTheater()
    {
        DateTime newDate = new DateTime(2016, 08, 01);
        Theater testTheater1 = new Theater("Watch Movies Here", newDate);
        testTheater1.Save();
        Theater testTheater2 = new Theater("Cinema Mania", newDate);
        testTheater2.Save();
        Theater testTheater3 = new Theater("Cinematic Cinema", newDate);
        testTheater3.Save();

        Movie testMovie1 = new Movie("How to Get Away with Loiter", "PG");
        testMovie1.Save();
        Movie testMovie2 = new Movie("Red Swan", "R");
        testMovie2.Save();
        Movie testMovie3 = new Movie("The Occurence 2", "R");
        testMovie3.Save();

        testMovie1.AddTheater(testTheater1);
        testMovie2.AddTheater(testTheater2);
        testMovie3.AddTheater(testTheater3);

        List<Movie> result = Movie.SearchMovieByTheater("Cinema");
        List<Movie> testMovies = new List<Movie> {testMovie2, testMovie3};

        Assert.Equal(testMovies, result);
    }

    public void Dispose()
    {
      Movie.DeleteAll();
      Theater.DeleteAll();
    }

  }
}
