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

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM movies WHERE id = @MovieId;", conn);

      SqlParameter movieIdParameter = new SqlParameter();
      movieIdParameter.ParameterName = "@MovieId";
      movieIdParameter.Value = this.GetId();

      cmd.Parameters.Add(movieIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Movie Find(int newId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM movies WHERE id = (@MovieId);", conn);

      SqlParameter movieParameter = new SqlParameter();
      movieParameter.ParameterName = "@MovieId";
      movieParameter.Value = newId;
      cmd.Parameters.Add(movieParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      int id = 0;
      string title = null;
      string rating = null;

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        title = rdr.GetString(1);
        rating = rdr.GetString(2);
      }
      Movie foundMovie = new Movie(title, rating, id);
      return foundMovie;
    }

    public void Update(string newTitle, string newRating)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("Update movies SET title = @NewTitle, rating = @NewRating OUTPUT INSERTED.title, INSERTED.rating WHERE id = @MovieId;", conn);

      SqlParameter newTitleParameter = new SqlParameter();
      newTitleParameter.ParameterName = "@NewTitle";
      newTitleParameter.Value = newTitle;
      cmd.Parameters.Add(newTitleParameter);

      SqlParameter newRatingParameter = new SqlParameter();
      newRatingParameter.ParameterName = "@NewRating";
      newRatingParameter.Value = newRating;
      cmd.Parameters.Add(newRatingParameter);

      SqlParameter movieIdParameter = new SqlParameter();
      movieIdParameter.ParameterName = "@MovieId";
      movieIdParameter.Value = this.GetId();
      cmd.Parameters.Add(movieIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._title = rdr.GetString(0);
        this._rating = rdr.GetString(1);
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

    public void AddTheater(Theater newTheater)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO movies_theaters (movie_id, theater_id) VALUES (@MovieId, @TheaterId);", conn);

      SqlParameter movieIdParameter = new SqlParameter();
      movieIdParameter.ParameterName = "@MovieId";
      movieIdParameter.Value = this.GetId();
      cmd.Parameters.Add(movieIdParameter);

      SqlParameter theaterIdParameter = new SqlParameter();
      theaterIdParameter.ParameterName = "@TheaterId";
      theaterIdParameter.Value = newTheater.GetId();
      cmd.Parameters.Add(theaterIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Theater> GetTheaters()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT theaters.* FROM movies JOIN movies_theaters ON (movies.id = movies_theaters.movie_id) JOIN theaters ON (movies_theaters.theater_id = theaters.id) WHERE movies.id = @MovieId;", conn);
      SqlParameter theaterIdParameter = new SqlParameter();
      theaterIdParameter.ParameterName = "@MovieId";
      theaterIdParameter.Value = this.GetId().ToString();

      cmd.Parameters.Add(theaterIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      List<Theater> movies = new List<Theater> {};
      while(rdr.Read())
      {
        int thisTheaterId = rdr.GetInt32(0);
        string theaterLocation = rdr.GetString(1);
        DateTime theaterDate = rdr.GetDateTime(2);
        Theater foundTheater= new Theater(theaterLocation, theaterDate, thisTheaterId);
        movies.Add(foundTheater);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return movies;
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
