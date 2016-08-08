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

    //Equals method
    public override bool Equals(System.Object otherMovie)
    {
      if (!(otherMovie is Movie))
      {
        return false;
      }
      else
      {
        Movie newMovie = (Movie) otherMovie;
        bool idEquality = this.GetId() == newMovie.GetId();
        bool titleEquality = this.GetTitle() == newMovie.GetTitle();
        bool ratingEquality = this.GetRating() == newMovie.GetRating();
        return (idEquality && titleEquality && ratingEquality);
      }
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

    public int GetId()
    {
      return _id;
    }

    //other methods
    public static List<Movie> GetAll()
    {
      List<Movie> allMovies = new List<Movie>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM movies;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int movieId = rdr.GetInt32(0);
        string movieTitle = rdr.GetString(1);
        string movieRating = rdr.GetString(2);
        Movie newMovie = new Movie(movieTitle, movieRating, movieId);
        allMovies.Add(newMovie);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allMovies;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO movies (title, rating) OUTPUT INSERTED.id VALUES (@Title, @Rating);", conn);

      SqlParameter titleParameter = new SqlParameter();
      titleParameter.ParameterName = "@Title";
      titleParameter.Value = this.GetTitle();
      cmd.Parameters.Add(titleParameter);

      SqlParameter ratingParameter = new SqlParameter();
      ratingParameter.ParameterName = "@Rating";
      ratingParameter.Value = this.GetRating();
      cmd.Parameters.Add(ratingParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr!=null)
      {
        rdr.Close();
      }
      
      if(conn!=null)
      {
        conn.Close();
      }

    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE from movies;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
