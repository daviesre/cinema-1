using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Cinema
{
  public class Movie
  {
    //properties
    private int _id;
    private string _title;
    private string _rating;

    //constructor
    public Movie(string title, string rating, int Id = 0)
    {
      _title = title;
      _rating = rating;
      _id = Id;
    }

    //getters and setters
    public string GetTitle()
    {
      return _title;
    }
    public void SetTitle(string newTitle)
    {
      _title = newTitle;
    }

    public string GetRating()
    {
      return _rating;
    }

    public void SetRating(string newRating)
    {
      _rating = newRating;
    }

    //other methods

  }
}
