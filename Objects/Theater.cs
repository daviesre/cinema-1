using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cinema
{
  public class Theater
  {
    private int _id;
    private string _name;
    private DateTime _dateTime;


    public Theater (string Name, DateTime DateTime, int Id = 0)
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

    public DateTime GetDateTime()
    {
      return _dateTime;
    }

    public void SetDateTime(DateTime newDateTime)
    {
      _dateTime = newDateTime;
    }

    public override bool Equals(System.Object otherTheater)
   {
     if (!(otherTheater is Theater))
     {
       return false;
     }
     else
     {
       Theater newTheater = (Theater) otherTheater;
       bool idEquality = this.GetId() == newTheater.GetId();
       bool nameEquality = this.GetName() == newTheater.GetName();
       bool dateTimeEquality = this.GetDateTime() == newTheater.GetDateTime();

       return (idEquality && nameEquality && dateTimeEquality);
     }
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
        DateTime theaterDateTime = rdr.GetDateTime(2);

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
