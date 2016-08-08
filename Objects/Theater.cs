using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cinema
{
  public class Theater
  {
    private int _id;
    private string _name;
    private DateTime _date_time;


    public Theater (string Name, DateTime DateTime, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _dateTime = DateTime;
    }

    public int GetId()
    {
      _id = id;
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
      return _dateTime.ToString();
    }
    public void SetDateTime(DateTime newDateTime)
    {
      _dateTime = newDateTime;
    }
    public void Delete()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("DELETE FROM theater WHERE id = @TheaterId;", conn);

          SqlParameter authorIdParameter = new SqlParameter();
          theaterIdParameter.ParameterName = "@TheaterId";
          theaterIdParameter.Value = this.GetId();

          cmd.Parameters.Add(theaterIdParameter);

          cmd.ExecuteNonQuery();

          if (conn != null)
          {
            conn.Close();
          }
        }
  }
}
