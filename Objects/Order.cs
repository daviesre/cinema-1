using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cinema
{
  public class Order
  {
    private int _id;
    private int _showing_id;
    private int _user_id;
    private int _quantity;

    public Order (int ShowingId, int UserId, int Quantity, int Id = 0)
    {
      _id = Id;
      _showing_id = ShowingId;
      _user_id = UserId;
      _quantity = Quantity;
    }

    public override bool Equals(System.Object otherOrder)
    {
      if (!(otherOrder is Order))
      {
        return false;
      }
      else
      {
        Order newOrder = (Order) otherOrder;
        bool idEquality = this.GetId() == newOrder.GetId();
        bool ShowingIdEquality = this.GetShowingId() == newOrder.GetShowingId();
        bool UserIdEquality = this.GetUserId() == newOrder.GetUserId();
        bool QuantityEquality = this.GetQuantity() == newOrder.GetQuantity();
        return (idEquality && ShowingIdEquality && UserIdEquality && QuantityEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public int GetShowingId()
    {
      return _showing_id;
    }
    public void SetShowingId(int newShowingId)
    {
      _showing_id = newShowingId;
    }

    public int GetUserId()
    {
      return _user_id;
    }
    public void SetUserId(int newUserId)
    {
      _user_id = newUserId;
    }

    public int GetQuantity()
    {
      return _quantity;
    }
    public void SetQuantity(int newQuantity)
    {
      _quantity = newQuantity;
    }

    public static List<Order> GetAll()
    {
      List<Order> allorders = new List<Order>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM orders;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int orderId = rdr.GetInt32(0);
        int orderShowingId = rdr.GetInt32(1);
        int orderUserId = rdr.GetInt32(2);
        int orderQuantity = rdr.GetInt32(3);

        Order newOrder = new Order(orderShowingId, orderUserId, orderQuantity, orderId);
        allorders.Add(newOrder);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allorders;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO orders (showing_id, user_id, quantity) OUTPUT INSERTED.id VALUES (@OrderShowingId, @OrderUserId, @OrderQuantity);", conn);

      SqlParameter showingIdParameter = new SqlParameter();
      showingIdParameter.ParameterName = "@OrderShowingId";
      showingIdParameter.Value = this.GetShowingId();

      SqlParameter userIdParameter = new SqlParameter();
      userIdParameter.ParameterName = "@OrderUserId";
      userIdParameter.Value = this.GetUserId();

      SqlParameter quantityParameter = new SqlParameter();
      quantityParameter.ParameterName = "@OrderQuantity";
      quantityParameter.Value = this.GetQuantity();

      cmd.Parameters.Add(showingIdParameter);
      cmd.Parameters.Add(userIdParameter);
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

    public static Order Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM orders WHERE id = @OrderId;", conn);
      SqlParameter orderIdParameter = new SqlParameter();
      orderIdParameter.ParameterName = "@OrderId";
      orderIdParameter.Value = id.ToString();

      cmd.Parameters.Add(orderIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundOrderId = 0;
      int foundOrderShowingId = 0;
      int foundOrderUserId = 0;
      int foundOrderQuantity = 0;

      while (rdr.Read())
      {
        foundOrderId = rdr.GetInt32(0);
        foundOrderShowingId  = rdr.GetInt32(1);
        foundOrderUserId  = rdr.GetInt32(2);
        foundOrderQuantity = rdr.GetInt32(3);

      }
      Order foundOrder = new Order(foundOrderShowingId, foundOrderUserId, foundOrderQuantity, foundOrderId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundOrder;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM orders WHERE id = @OrderId;", conn);
      SqlParameter orderIdParameter = new SqlParameter();
      orderIdParameter.ParameterName = "@OrderId";
      orderIdParameter.Value = this.GetId();

      cmd.Parameters.Add(orderIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM orders;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
