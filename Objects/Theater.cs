using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cinema
{
  public class Theatre
  {
    private int _id;
    private string _name;
    private DateTime _date_time;


    public Theatre (string Name, DateTime DateTime, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _DateTime = DateTime;
    }

  }
}
