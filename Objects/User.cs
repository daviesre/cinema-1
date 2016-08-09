using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cinema
{
  public class User
  {
    private int _id;
    private string _name;

    public User(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public static List<User> GetAll()
    {
      List<User> allUsers = new List<User>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM users;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int userId = rdr.GetInt32(0);
        string userName = rdr.GetString(1);
        User newUser = new User(userName, userId);
        allUsers.Add(newUser);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allUsers;
    }

    public override bool Equals(System.Object otherUser)
    {
        if (!(otherUser is User))
        {
          return false;
        }
        else
        {
          User newUser = (User) otherUser;
          bool idEquality = this.GetId() == newUser.GetId();
          bool nameEquality = this.GetName() == newUser.GetName();
          return (idEquality && nameEquality);
        }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO users (name) OUTPUT INSERTED.id VALUES (@UserName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@UserName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static User Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE id = @UserId;", conn);
      SqlParameter userIdParameter = new SqlParameter();
      userIdParameter.ParameterName = "@UserId";
      userIdParameter.Value = id.ToString();
      cmd.Parameters.Add(userIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundUserId = 0;
      string foundUserName = null;

      while(rdr.Read())
      {
        foundUserId = rdr.GetInt32(0);
        foundUserName = rdr.GetString(1);
      }
      User foundUser = new User(foundUserName, foundUserId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundUser;
    }

    public void AddTicket(Ticket newTicket)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO users_tickets (user_id, ticket_id) VALUES (@UserId, @TicketId);", conn);
      SqlParameter userIdParameter = new SqlParameter();
      userIdParameter.ParameterName = "@UserId";
      userIdParameter.Value = this.GetId();
      cmd.Parameters.Add(userIdParameter);

      SqlParameter ticketIdParameter = new SqlParameter();
      ticketIdParameter.ParameterName = "@TicketId";
      ticketIdParameter.Value = newTicket.GetId();
      cmd.Parameters.Add(ticketIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Ticket> GetTickets()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT tickets.* FROM users JOIN users_tickets ON (users.id = users_tickets.user_id) JOIN tickets ON (users_tickets.ticket_id = tickets.id) WHERE users.id = @UserId",conn);

      SqlParameter userIdParameter = new SqlParameter();
      userIdParameter.ParameterName= "@UserId";
      userIdParameter.Value=this.GetId();
      cmd.Parameters.Add(userIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      List<Ticket> tickets = new List<Ticket> {};

      while(rdr.Read())
      {
        int ticketId = rdr.GetInt32(0);
        int ticketMovieId = rdr.GetInt32(1);
        int ticketQuantity = rdr.GetInt32(2);
        Ticket newTicket = new Ticket(ticketMovieId, ticketQuantity, ticketId);
        tickets.Add(newTicket);
      }

      if(rdr !=null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
      return tickets;
    }




    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM users;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
