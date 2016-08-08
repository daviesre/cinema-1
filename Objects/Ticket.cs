using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cinema
{
  public class Ticket
  {
    private int _id;
    private int _movie_id;
    private int _quantity;

    public Ticket (int MovieId, int Quantity, int Id = 0)
    {
      _id = Id;
      _movie_id = MovieId;
      _quantity = Quantity;
    }

    public override bool Equals(System.Object otherTicket)
    {
      if (!(otherTicket is Ticket))
      {
        return false;
      }
      else
      {
        Ticket newTicket = (Ticket) otherTicket;
        bool idEquality = this.GetId() == newTicket.GetId();
        bool MovieIdEquality = this.GetMovieId() == newTicket.GetMovieId();
        bool QuantityEquality = this.GetQuantity() == newTicket.GetQuantity();
        return (idEquality && MovieIdEquality && QuantityEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public int GetMovieId()
    {
      return _movie_id;
    }
    public void SetMovieId(int newMovieId)
    {
      _movie_id = newMovieId;
    }

    public int GetQuantity()
    {
      return _quantity;
    }
    public void SetQuantity(int newQuantity)
    {
      _quantity = newQuantity;
    }

    public static List<Ticket> GetAll()
    {
      List<Ticket> alltickets = new List<Ticket>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tickets;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int ticketId = rdr.GetInt32(0);
        int ticketBookId = rdr.GetInt32(1);
        int ticketQuantity = rdr.GetInt32(1);

        Ticket newTicket = new Ticket(ticketBookId, ticketQuantity, ticketId);
        alltickets.Add(newTicket);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return alltickets;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO tickets (movie_id, quantity) OUTPUT INSERTED.id VALUES (@TicketMovieId, @TicketQuantity);", conn);

      SqlParameter movieIdParameter = new SqlParameter();
      movieIdParameter.ParameterName = "@TicketMovieId";
      movieIdParameter.Value = this.GetMovieId();

      SqlParameter quantityParameter = new SqlParameter();
      quantityParameter.ParameterName = "@TicketQuantity";
      quantityParameter.Value = this.GetQuantity();

      cmd.Parameters.Add(movieIdParameter);
      cmd.Parameters.Add(quantityParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM tickets;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
