using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cinema
{
  public class Theater
  {
    private int _id;
    private string _name;
    private string _dateTime;


    public Theater (string Name, string DateTime, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _dateTime = DateTime;
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

    public string GetDateTime()
    {
      return _dateTime;
    }
    public void SetDateTime(string newDateTime)
    {
      _dateTime = newDateTime;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM theaters;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }


    public void Delete()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("DELETE FROM theaters WHERE id = @TheaterId;", conn);

       SqlParameter theaterIdParameter = new SqlParameter();
       theaterIdParameter.ParameterName = "@TheaterId";
       theaterIdParameter.Value = this.GetId();

       cmd.Parameters.Add(theaterIdParameter);

       cmd.ExecuteNonQuery();

       if (conn != null)
       {
         conn.Close();
       }
     }

    public static List<Theater> GetAll()
    {
      List<Theater> allTheaters = new List<Theater>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM theaters;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int theaterId = rdr.GetInt32(0);
        string theaterName = rdr.GetString(1);
        string theaterDateTime = rdr.GetString(2);

        Theater newTheater = new Theater(theaterName, theaterDateTime, theaterId);
        allTheaters.Add(newTheater);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allTheaters;
    }
  }
}
