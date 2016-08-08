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

    public void Dispose()
    {
      Ticket.DeleteAll();
    }

    [Fact]
    public void Test1_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Ticket.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test2_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      Ticket firstTicket = new Ticket(1,1);
      Ticket secondTicket = new Ticket(1,1);

      //Assert
      Assert.Equal(firstTicket, secondTicket);
    }

    [Fact]
    public void Test3_Save_SavesToDatabase()
    {
      //Arrange
      Ticket testTicket = new Ticket(1,1);

      //Act
      testTicket.Save();
      List<Ticket> result = Ticket.GetAll();
      List<Ticket> testList = new List<Ticket>{testTicket};

      //Assert
      Assert.Equal(testList, result);
    }

  }
}
