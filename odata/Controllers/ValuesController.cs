using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Data.Common;

using Microsoft.EntityFrameworkCore.Sqlite;
using RTH.LiturgySchedule;

namespace odata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //return new string[] { "value1", "value2" };
            var sb = new SqliteConnectionStringBuilder();
            sb.DataSource = @"C:/temp/sl.db";

            var options = new DbContextOptionsBuilder<DataModel>()
                .UseSqlite<DataModel>(sb.ConnectionString)
                .Options;

            
            using (var factory = new DbContextFactory(options)) {
            
                using (var db = factory.CreateContext()) {
                    return
                        db.Masses.OrderByDescending(x => x.DateTime).Select(m => m.DateTime.ToString())
                        .ToList();
                }
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
