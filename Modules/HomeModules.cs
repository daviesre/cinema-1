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
        // Theater newTheater = new Theater(Request.Form["theater-location"], Request.Form["theater-date"] );
        // newTheater.Save();
        Movie newMovie = new Movie(Request.Form["movie-title"], Request.Form["movie-rating"] );
        newMovie.Save();
        return View["success.cshtml"];
      };

    }

  }
}
