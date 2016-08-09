using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cinema
{
  public class OrderTest : IDisposable
  {
    public OrderTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cinema_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Order.DeleteAll();
    }

    [Fact]
    public void Test1_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Order.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test2_Equal_ReturnsTrueIfOrderAreTheSame()
    {
      //Arrange, Act
      Order firstOrder = new Order(1,1,2);
      Order secondOrder = new Order(1,1,2);

      //Assert
      Assert.Equal(firstOrder, secondOrder);
    }


  }
}
