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

    [Fact]
    public void Test3_Save_SavesToDatabase()
    {
      //Arrange
      Order testOrder = new Order(1,1,2);

      //Act
      testOrder.Save();
      List<Order> result = Order.GetAll();
      List<Order> testList = new List<Order>{testOrder};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test4_Save_AssignsIdToOrderObject()
    {
      //Arrange
      Order testOrder = new Order(1,1,2);
      testOrder.Save();

      //Act
      Order savedOrder = Order.GetAll()[0];

      int result = savedOrder.GetId();
      int testId = testOrder.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test5_Find_FindsOrderInDatabase()
    {
      //Arrange
      Order testOrder = new Order(1,2,1);
      testOrder.Save();

      //Act
      Order foundOrder = Order.Find(testOrder.GetId());

      //Assert
      Assert.Equal(testOrder, foundOrder);
    }

    [Fact]
    public void Test6_Delete_DeletesOrderFromDatabase()
    {
       //Arrange
      Order testOrder1 = new Order(1,2,1);
      testOrder1.Save();

      Order testOrder2 = new Order(1,1,5);
      testOrder2.Save();

       //Act
      testOrder1.Delete();
      List<Order> resultOrders = Order.GetAll();
      List<Order> testOrderList = new List<Order> {testOrder2};

       //Assert
      Assert.Equal(testOrderList, resultOrders);
    }


  }
}
