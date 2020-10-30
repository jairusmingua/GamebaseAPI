using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTest.Controllers
{
    public class SearchController : ApiController
    {
        [Route("api/search/{q=q}/{c?}")]
        [HttpGet]
        public IEnumerable<Game> Get(string q,string c="5")
        {
            List<Game> results;
            using (var context = new gamebase1Entities())
            {
                IQueryable<Game> searchresults = context.Games.Where(g => g.GameTitle.ToLower().Contains(q)).Take(int.Parse(c));
                results = searchresults.ToList();
                return results;
            }
        }
    }
}
