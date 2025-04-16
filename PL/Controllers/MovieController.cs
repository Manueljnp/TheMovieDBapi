using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;

namespace PL.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        [HttpGet]
        public async Task<ActionResult> PeliculasPopulares()
        {
            List<ML.Peliculas> peliculas = new List<ML.Peliculas>();
            
            using (HttpClient client = new HttpClient())
            {
                string url = "https://api.themoviedb.org/3/movie/popular?api_key=1817a557e2ee01acd12c79b392e47307&language=es-MX&page=1";

                var respuesta = await client.GetStringAsync(url);

                JObject json = JObject.Parse(respuesta);

                var resultados = json["results"];

                foreach(var item in resultados)
                {
                    ML.Peliculas peli = new ML.Peliculas
                    {
                        title = item["title"].ToString(),
                        overview = item["overview"].ToString(),
                        poster_path = item["poster_path"].ToString(),
                        release_date = item["release_date"].ToString(),
                        vote_average = item["vote_average"].ToObject<double>()
                    };
                    peliculas.Add(peli);
                }
            }
            return View(peliculas);
        }

        //Consumir WebApi en MVC con HttpClient
        //API REST

        //Primero Instalar en PL (web):
        //Json.NET => https://www.nuget.org/packages/Newtonsoft.Json/
        //NuGet\Install-Package Newtonsoft.Json -Version 13.0.3
        //Microsoft ASP.Net Web API 2.2 client Libraries => https://nugetmusthaves.com/Package/Microsoft.AspNet.WebApi.Client
        //Install-Package Microsoft.AspNet.WebApi.Client 
        //System.Net.Http => https://www.nuget.org/packages/System.Net.Http/
        //NuGet\Install-Package System.Net.Http -Version 4.3.4
    }
}