// using Xunit;
// using System.Collections.Generic;
// using System;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace Cinema
// {
//   public class TicketTest : IDisposable
//   {
//     public TicketTest()
//     {
//       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cinema_test;Integrated Security=SSPI;";
//     }
//
//     public void Dispose()
//     {
//       Ticket.DeleteAll();
//       User.DeleteAll();
//     }
//
//     [Fact]
//     public void Test1_DatabaseEmptyAtFirst()
//     {
//       //Arrange, Act
//       int result = Ticket.GetAll().Count;
//
//       //Assert
//       Assert.Equal(0, result);
//     }
//
//     [Fact]
//     public void Test2_Equal_ReturnsTrueIfNamesAreTheSame()
//     {
//       //Arrange, Act
//       Ticket firstTicket = new Ticket(1,1);
//       Ticket secondTicket = new Ticket(1,1);
//
//       //Assert
//       Assert.Equal(firstTicket, secondTicket);
//     }
//
//     [Fact]
//     public void Test3_Save_SavesToDatabase()
//     {
//       //Arrange
//       Ticket testTicket = new Ticket(1,1);
//
//       //Act
//       testTicket.Save();
//       List<Ticket> result = Ticket.GetAll();
//       List<Ticket> testList = new List<Ticket>{testTicket};
//
//       //Assert
//       Assert.Equal(testList, result);
//     }
//
//     [Fact]
//     public void Test4_Save_AssignsIdToTicketObject()
//     {
//       //Arrange
//       Ticket testTicket = new Ticket(1,1);
//       testTicket.Save();
//
//       //Act
//       Ticket savedTicket = Ticket.GetAll()[0];
//
//       int result = savedTicket.GetId();
//       int testId = testTicket.GetId();
//
//       //Assert
//       Assert.Equal(testId, result);
//     }
//
//     [Fact]
//     public void Test5_Find_FindsTicketInDatabase()
//     {
//       //Arrange
//       Ticket testTicket = new Ticket(1,2);
//       testTicket.Save();
//
//       //Act
//       Ticket foundTicket = Ticket.Find(testTicket.GetId());
//
//       //Assert
//       Assert.Equal(testTicket, foundTicket);
//     }
//
//     // [Fact]
//     // public void Test6_Delete_DeletesTicketFromDatabase()
//     // {
//     //   //Arrange
//     //   Ticket testTicket1 = new Ticket(1,2);
//     //   testTicket1.Save();
//     //
//     //   Ticket testTicket2 = new Ticket(1,3);
//     //   testTicket2.Save();
//     //
//     //   //Act
//     //   testTicket1.Delete();
//     //   List<Ticket> resultTickets = Ticket.GetAll();
//     //   List<Ticket> testTicketList = new List<Ticket> {testTicket2};
//     //
//     //   //Assert
//     //   Assert.Equal(testTicketList, resultTickets);
//     // }
//
//   }
// }
