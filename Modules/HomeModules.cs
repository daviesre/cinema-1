using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Cinema
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/movies"] = _ => {
        List<Movie> AllMovies = Movie.GetAll();
        return View["movies.cshtml", AllMovies];
      };

      Get["/theaters"] = _ => {
        List<Theater> AllTheaters = Theater.GetAll();
        return View["theaters.cshtml", AllTheaters];
      };

      Get["/theaters/new"] = _ => {
        return View["theater_form.cshtml"];
      };

      Post["/theaters/new"] = _ => {
        Theater newTheater = new Theater(Request.Form["theater-location"], Request.Form["theater-date"]);
        newTheater.Save();
        return View["success.cshtml"];
      };

      Get["/movies/new"] = _ => {
        return View["movies_form.cshtml"];
      };

      Post["/movie/new"] = _ => {
        Movie newMovie = new Movie(Request.Form["movie-title"], Request.Form["movie-rating"]);
        newMovie.Save();
        return View["success.cshtml"];
      };

      Get["movies/{id}"] = parameters => {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Movie SelectedMovie = Movie.Find(parameters.id);
          List<Theater> MovieTheaters = SelectedMovie.GetTheaters();
          List<Theater> AllTheaters = Theater.GetAll();
          model.Add("movie", SelectedMovie);
          model.Add("movieTheaters", MovieTheaters);
          model.Add("allTheaters", AllTheaters);
          return View["movie.cshtml", model];
        };

        Get["theaters/{id}"] = parameters => {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Theater SelectedTheater = Theater.Find(parameters.id);
          List<Movie> TheaterMovies = SelectedTheater.GetMovies();
          List<Movie> AllMovies = Movie.GetAll();
          model.Add("theater", SelectedTheater);
          model.Add("theaterMovies", TheaterMovies);
          model.Add("allMovies", AllMovies);
          return View["theater.cshtml", model];
        };

        Post["movie/add_theater"] = _ => {
          Theater theater = Theater.Find(Request.Form["theater-id"]);
          Movie movie = Movie.Find(Request.Form["movie-id"]);
          movie.AddTheater(theater);
          return View["success.cshtml"];
        };

        Post["theater/add_movie"] = _ => {
          Theater theater = Theater.Find(Request.Form["theater-id"]);
          Movie movie = Movie.Find(Request.Form["movie-id"]);
          theater.AddMovies(movie);
          return View["success.cshtml"];
        };

        Post["/movies/delete"] = _ => {
          Movie.DeleteAll();
          return View["cleared.cshtml"];
        };

        Get["theater/edit/{id}"] = parameters => {
          Theater SelectedTheater = Theater.Find(parameters.id);
          return View["theater_edit.cshtml", SelectedTheater];
        };

        Patch["theater/edit/{id}"] = parameters => {
          Theater SelectedTheater = Theater.Find(parameters.id);
          SelectedTheater.Update(Request.Form["theater-name"], Request.Form["theater-date"]);
          return View["success.cshtml"];
        };

        Get["theater/delete/{id}"] = parameters => {
          Theater SelectedTheater = Theater.Find(parameters.id);
          return View["theater_delete.cshtml", SelectedTheater];
        };
        Delete["theater/delete/{id}"] = parameters => {
          Theater SelectedTheater = Theater.Find(parameters.id);
          SelectedTheater.Delete();
          return View["success.cshtml"];
        };

        Post["/theaters/delete"] = _ => {
          Theater.DeleteAll();
          return View["cleared.cshtml"];
        };

        Get["movie/edit/{id}"] = parameters => {
          Movie SelectedMovie = Movie.Find(parameters.id);
          return View["movie_edit.cshtml", SelectedMovie];
        };

        Patch["movie/edit/{id}"] = parameters => {
          Movie SelectedMovie = Movie.Find(parameters.id);
          SelectedMovie.Update(Request.Form["movie-title"], Request.Form["movie-rating"]);
          return View["success.cshtml"];
        };

        Get["movie/delete/{id}"] = parameters => {
          Movie SelectedMovie = Movie.Find(parameters.id);
          return View["movie_delete.cshtml", SelectedMovie];
        };
        Delete["movie/delete/{id}"] = parameters => {
          Movie SelectedMovie = Movie.Find(parameters.id);
          SelectedMovie.Delete();
          return View["success.cshtml"];
        };
    }
  }
}
