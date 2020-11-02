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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}