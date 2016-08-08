using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cinema
{
  public class TicketTest : IDisposable
  {
    public TicketTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cinema_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Ticket.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      User.DeleteAll();
    }

  }
}
