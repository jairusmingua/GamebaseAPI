using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTest.Controllers
{
    public class NewsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<News> Get()
        {
            List<News> news;
            using (var context = new gamebase1Entities())
            {

                news = context.News.Take(10).ToList(); 

            }

            return news;
        }

      
    }
}