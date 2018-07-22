using System;
using System.Linq;
using RTH.LiturgySchedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using Newtonsoft.Json;


namespace LitSched
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize SqlLite library
            SQLitePCL.Batteries_V2.Init();
            
            var sb = new SqliteConnectionStringBuilder();
            sb.DataSource = @"C:/temp/sl.db";

            var options = new DbContextOptionsBuilder<DataModel>()
                .UseSqlite<DataModel>(sb.ConnectionString)
                .Options;

            
            using (var factory = new DbContextFactory(options, true)) {
            
                using (var db = factory.CreateContext())
                {
                    DataModel.Initialize(db);
                }
                
                using (var db = factory.CreateContext())
                {
                    Console.WriteLine(JsonConvert.SerializeObject( db.Masses.OrderByDescending(x => x.DateTime)));
                    Console.WriteLine(JsonConvert.SerializeObject( db.People.OrderBy(x => x.Name) ));
                    Console.WriteLine(JsonConvert.SerializeObject( db.Roles ));
                }
            }
        }
    }
    
}
