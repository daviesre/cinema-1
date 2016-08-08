using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cinema
{
  public class Theater
  {
    private int _id;
    private string _location;
    private DateTime _dateTime;


    public Theater (string Location, DateTime DateTime, int Id = 0)
    {
      _id = Id;
      _location = Location;
      _dateTime = DateTime;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetLocation()
    {
      return _location;
    }

    public void SetLocation(string newLocation)
    {
      _location = newLocation;
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
        bool locationEquality = this.GetLocation() == newTheater.GetLocation();
        bool dateTimeEquality = this.GetDateTime() == newTheater.GetDateTime();

        return (idEquality && locationEquality && dateTimeEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO theaters (location, date_time) OUTPUT INSERTED.id VALUES (@TheaterLocation, @DateTime);", conn);

      SqlParameter locationParameter = new SqlParameter();
      locationParameter.ParameterName = "@TheaterLocation";
      locationParameter.Value = this.GetLocation();

      SqlParameter dateTimeParameter = new SqlParameter();
      dateTimeParameter.ParameterName = "@DateTime";
      dateTimeParameter.Value = this.GetDateTime();

      cmd.Parameters.Add(locationParameter);
      cmd.Parameters.Add(dateTimeParameter);

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
        string theaterLocation = rdr.GetString(1);
        DateTime theaterDateTime = rdr.GetDateTime(2);

        Theater newTheater = new Theater(theaterLocation, theaterDateTime, theaterId);
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
