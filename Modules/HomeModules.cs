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
        List<Movie> AllMovies = Movie.GetAll();
        return View["index.cshtml", AllMovies];
      };

      Get["/admin/new"] = _ => {
        return View["admin.cshtml"];
      };

      Post["/admin/new"] = _ => {
        Theater newTheater = new Theater(Request.Form["theater-location"], Request.Form["theater-date"] );
        newTheater.Save();
        Movie newMovie = new Movie(Request.Form["movie-title"], Request.Form["movie-rating"] );
        newMovie.Save();

        return View["success.cshtml"];
      };

      Get["movies/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Movie selectedMovie = Movie.Find(parameters.id);
        List<Theater> movieTheaters = selectedMovie.GetTheaters();
        List<Theater> allTheaters = Theater.GetAll();
        model.Add("movie", selectedMovie);
        model.Add("movieTheaters", movieTheaters);
        model.Add("allTheaters", allTheaters);
        return View["movie.cshtml", model];
      };

      Post["movie/add_theater"] = _ => {
        Theater theater = Theater.Find(Request.Form["theater-id"]);
        Movie movie = Movie.Find(Request.Form["movie-id"]);
        movie.AddTheater(theater);
        return View["success.cshtml"];
      };

      // Post["theater/add_movie"] = _ => {
      //   Theater theater = Theater.Find(Request.Form["theater-id"]);
      //   Movie movie = Movie.Find(Request.Form["movie-id"]);
      //   theater.AddMovie(movie);
      //   return View["success.cshtml"];
      // };

    }
  }
}
