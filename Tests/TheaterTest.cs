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

  }
}
