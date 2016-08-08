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

    public void Dispose()
    {
      Movie.DeleteAll();
    }

  }
}
