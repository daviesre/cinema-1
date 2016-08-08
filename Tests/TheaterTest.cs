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


  }
}
